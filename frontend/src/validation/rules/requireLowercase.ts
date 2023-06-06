import { isLetter } from "@/helpers/stringUtils";

export default function (s?: string): boolean {
  return typeof s === "string" && [...s].some((c) => isLetter(c) && c === c.toLowerCase());
}
