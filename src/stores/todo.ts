import { arrayUtils, stringUtils } from "logitar-js";
import { defineStore } from "pinia";
import { nanoid } from "nanoid";
import { ref } from "vue";

import type { Actor } from "@/types/actor";
import type { CreateTodoPayload, ReplaceTodoPayload, SearchTodosPayload, Todo, TodoSortOption } from "@/types/todos";
import type { SearchResults } from "@/types/search";
import { useUserStore } from "./user";

const { cleanTrim, trim } = stringUtils;
const { orderBy } = arrayUtils;

function getCurrentActor(): Actor {
  const user = useUserStore();
  if (!user.currentUser) {
    return { id: "SYSTEM", type: "System", isDeleted: false, displayName: "System" };
  }
  return {
    id: user.currentUser.id,
    type: "User",
    isDeleted: false,
    displayName: user.currentUser.fullName ?? user.currentUser.uniqueName,
    emailAddress: user.currentUser.email?.address,
    pictureUrl: user.currentUser.picture,
  };
}

export const useTodoStore = defineStore(
  "todo",
  () => {
    const todos = ref<Todo[]>([]);

    function create(payload: CreateTodoPayload): Todo {
      const actor: Actor = getCurrentActor();
      const now: string = new Date().toISOString();
      const todo: Todo = {
        id: nanoid(),
        version: 1,
        createdBy: actor,
        createdOn: now,
        updatedBy: actor,
        updatedOn: now,
        isDone: false,
        displayName: payload.displayName.trim(),
        description: cleanTrim(payload.description),
      };
      todos.value.push(todo);
      return todo;
    }

    function _delete(id: string): Todo {
      const index: number = todos.value.findIndex((todo) => todo.id === id);
      const todo: Todo | undefined = todos.value[index];
      if (!todo) {
        throw { status: 404 };
      }
      todos.value.splice(index, 1);
      return todo;
    }

    function read(id: string): Todo {
      const index: number = todos.value.findIndex((todo) => todo.id === id);
      const todo: Todo | undefined = todos.value[index];
      if (!todo) {
        throw { status: 404 };
      }
      return todo;
    }

    function replace(id: string, payload: ReplaceTodoPayload, version?: number) {
      console.info(`Replacing todo ID=${id} at version ${version}.`);
      const index: number = todos.value.findIndex((todo) => todo.id === id);
      const todo: Todo | undefined = todos.value[index];
      if (!todo) {
        throw { status: 404 };
      }
      const actor: Actor = getCurrentActor();
      const now: string = new Date().toISOString();
      if (payload.isDone) {
        todo.doneBy = actor;
        todo.doneOn = now;
        todo.isDone = true;
      } else {
        todo.doneBy = undefined;
        todo.doneOn = undefined;
        todo.isDone = false;
      }
      todo.displayName = payload.displayName.trim();
      todo.description = cleanTrim(payload.description);
      todo.updatedBy = actor;
      todo.updatedOn = now;
      todo.version++;
      todos.value.splice(index, 1, todo);
      return todo;
    }

    function search(payload: SearchTodosPayload): SearchResults<Todo> {
      let items: Todo[] = [];
      todos.value.forEach((todo) => {
        if (payload.ids.length && !payload.ids.includes(todo.id)) {
          return;
        }
        if (typeof payload.isDone === "boolean" && payload.isDone !== todo.isDone) {
          return;
        }
        if (payload.search.terms.length) {
          switch (payload.search.operator) {
            case "And":
              if (payload.search.terms.some((term) => !todo.displayName.includes(trim(term.value, "%")))) {
                return;
              }
              break;
            case "Or":
              if (payload.search.terms.every((term) => !todo.displayName.includes(trim(term.value, "%")))) {
                return;
              }
              break;
          }
        }
        items.push(todo);
      });
      const total: number = items.length;

      const sort: TodoSortOption | undefined = payload.sort[0];
      if (sort) {
        switch (sort.field) {
          case "DisplayName":
            items = orderBy(items, "displayName", sort.isDescending);
            break;
          case "UpdatedOn":
            items = orderBy(items, "updatedOn", sort.isDescending);
            break;
        }
      }

      const start: number = payload.skip > 0 ? payload.skip : 0;
      const end: number = payload.limit > 0 ? start + payload.limit : items.length;
      items = items.slice(start, end);

      return { items, total };
    }

    return { todos, create, _delete, read, replace, search };
  },
  {
    persist: true,
  },
);
