export default function (value: string | undefined, [allowedCharacters]: [string]): boolean {
  return typeof value === "string" && (allowedCharacters ? [...value].every((c) => allowedCharacters.includes(c)) : true);
}
