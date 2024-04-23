import { stringUtils } from "logitar-js";

const { isDigit } = stringUtils;

export default function (s?: string): boolean {
  return typeof s === "string" && [...s].some(isDigit);
}
