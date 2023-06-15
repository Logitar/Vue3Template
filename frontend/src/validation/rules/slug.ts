import { isLetterOrDigit } from "@/helpers/stringUtils";

export default function (s?: string): boolean {
  return typeof s === "string" && s.split("-").every((word) => word.length && [...word].every(isLetterOrDigit));
}
