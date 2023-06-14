import type { SortParameters } from "./SortParameters";
import type { TextSearch } from "./TextSearch";

export type SearchParameters = {
  search: TextSearch;
  sort: SortParameters[];
  skip: number;
  limit: number;
};
