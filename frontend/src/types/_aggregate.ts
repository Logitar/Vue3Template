import type { Actor } from "./_actor";

export type Aggregate = {
  createdBy: Actor;
  createdOn: string;
  updatedBy: Actor;
  updatedOn: string;
  version: number;
};
