import type { Actor } from "./Actor";

export type Aggregate = {
  createdBy: Actor;
  createdOn: string;
  updatedBy: Actor;
  updatedOn: string;
  version: number;
};
