export function isDigit(c: unknown): boolean {
  return typeof c === "string" && c.trim() !== "" && !isNaN(Number(c));
}

export function isLetter(c: unknown): boolean {
  return typeof c === "string" && c.toLowerCase() !== c.toUpperCase();
}

export function isLetterOrDigit(c: unknown): boolean {
  return typeof c === "string" && (isDigit(c) || isLetter(c));
}

export function shortify(s: string, length: number): string {
  return s.length > length ? s.substring(0, length - 1) + "…" : s;
}

export function slugify(s?: string): string {
  s = s ?? "";
  const words = [];
  let word = "";
  for (let i = 0; i < s.length; i++) {
    const c = s[i];
    if (isLetterOrDigit(c)) {
      word += c;
    } else if (word.length) {
      words.push(word);
      word = "";
    }
  }
  if (word.length) {
    words.push(word);
  }
  return unaccent(words.join("-").toLowerCase());
}

const accents = new Map<string, string>([
  ["à", "a"],
  ["â", "a"],
  ["ç", "c"],
  ["è", "e"],
  ["é", "e"],
  ["ê", "e"],
  ["ë", "e"],
  ["î", "i"],
  ["ï", "i"],
  ["ô", "o"],
  ["ù", "u"],
  ["û", "u"],
  ["ü", "u"],
]);
export function unaccent(s: string): string {
  return [...s].map((c) => (c.toUpperCase() === c ? (accents.get(c) ?? c).toUpperCase() : accents.get(c) ?? c)).join("");
}