import sleep from "./sleep";
import type { CreateTodoPayload, ReplaceTodoPayload, SearchTodosPayload, Todo } from "@/types/todos";
import type { SearchResults } from "@/types/search";
import { useTodoStore } from "@/stores/todo";

export async function createTodo(payload: CreateTodoPayload): Promise<Todo> {
  await sleep(1000);
  const todos = useTodoStore();
  return todos.create(payload);
}

export async function deleteTodo(id: string): Promise<Todo> {
  await sleep(1000);
  const todos = useTodoStore();
  return todos._delete(id);
}

export async function readTodo(id: string): Promise<Todo> {
  await sleep(1000);
  const todos = useTodoStore();
  return todos.read(id);
}

export async function replaceTodo(id: string, payload: ReplaceTodoPayload, version?: number): Promise<Todo> {
  await sleep(1000);
  const todos = useTodoStore();
  return todos.replace(id, payload, version);
}

export async function searchTodos(payload: SearchTodosPayload): Promise<SearchResults<Todo>> {
  await sleep(1000);
  const todos = useTodoStore();
  return todos.search(payload);
}
