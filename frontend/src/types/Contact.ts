import type { Actor } from "./Actor";

export type Contact = {
  verifiedBy?: Actor;
  verifiedOn?: string;
  isVerified: boolean;
};
