export default function (s: string | undefined, [count]: [number]): boolean {
  if (typeof s !== "string") {
    return false;
  }
  const index = new Map<string, number>();
  [...s].forEach((c) => index.set(c, (index.get(c) ?? 0) + 1));
  return index.size >= count;
}
