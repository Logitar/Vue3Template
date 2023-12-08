import { isDigit } from "@/helpers/stringUtils";

export default function (s?: string): boolean {
  return typeof s === "string" && [...s].some(isDigit);
}
