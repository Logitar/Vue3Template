import type { Actor } from "./Actor";

export type Contact = {
  verifiedBy?: Actor;
  verifiedOn?: Date;
  isVerified: boolean;
};
