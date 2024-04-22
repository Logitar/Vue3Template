export type SearchOperator = "And" | "Or";

export type SearchPayload = {
  ids: string[];
  search: TextSearch;
  sort: SortOption[];
  skip: number;
  limit: number;
};

export type SearchResults<T> = {
  items: T[];
  total: number;
};

export type SearchTerm = {
  value: string;
};

export type SortOption = {
  field: string;
  isDescending: boolean;
};

export type TextSearch = {
  terms: SearchTerm[];
  operator: SearchOperator;
};
