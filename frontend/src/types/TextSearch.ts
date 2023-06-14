import type { SearchTerm } from "./SearchTerm";

export type TextSearch = {
  terms: SearchTerm[];
  operator: string;
};
