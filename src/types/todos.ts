import type { Actor } from "./actor";
import type { Aggregate } from "./aggregate";
import type { SearchPayload, SortOption } from "./search";

export type CreateTodoPayload = {
  displayName: string;
  description?: string;
};

export type ReplaceTodoPayload = {
  isDone: boolean;
  displayName: string;
  description?: string;
};

export type Todo = Aggregate & {
  doneBy?: Actor;
  doneOn?: string;
  isDone: boolean;
  displayName: string;
  description?: string;
};

export type TodoSort = "DisplayName" | "UpdatedOn";

export type TodoSortOption = SortOption & {
  field: TodoSort;
};

export type SearchTodosPayload = SearchPayload & {
  isDone?: boolean;
  sort: TodoSortOption[];
};
