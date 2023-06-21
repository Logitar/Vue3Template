export type ApiError = {
  data?: unknown;
  status: number;
};

export type ApiResult<T> = {
  data: T;
  status: number;
};

export type ErrorDetail = {
  code?: string;
};

export type GraphQLRequest<T> = {
  query: string;
  variables?: T;
};

export type GraphQLResponse<T> = {
  data?: T;
  errors?: unknown[];
};

export type SearchParameters = {
  search: TextSearch;
  sort: SortParameters[];
  skip: number;
  limit: number;
};

export type SearchResults<T> = {
  results: T[];
  total: number;
};

export type SearchTerm = {
  value: string;
};

export type SortParameters = {
  field: string;
  isDescending: boolean;
};

export type TextSearch = {
  terms: SearchTerm[];
  operator: string;
};
