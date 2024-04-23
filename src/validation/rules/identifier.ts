import { stringUtils } from "logitar-js";

const { isDigit, isLetterOrDigit } = stringUtils;

export default function (s?: string): boolean {
  return typeof s === "string" && s.length > 0 && !isDigit(s[0]) && [...s].every((c) => isLetterOrDigit(c) || c === "_");
}
