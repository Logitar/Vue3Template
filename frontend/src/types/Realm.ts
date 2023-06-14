import type { Aggregate } from "./Aggregate";

export type Realm = Aggregate & {
  id: string;
  uniqueName: string;
  displayName?: string;
}; // TODO(fpion): implement
