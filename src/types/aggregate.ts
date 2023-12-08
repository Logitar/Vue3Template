import type { Actor } from "./actor";

export type Aggregate = {
  createdBy: Actor;
  createdOn: string;
  updatedBy: Actor;
  updatedOn: string;
  version: number;
};
