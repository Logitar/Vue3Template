export function orderBy(items: any[], key?: string): any[] {
  return typeof key === "string" && key !== ""
    ? [...items].sort((a, b) => (a[key] < b[key] ? -1 : a[key] > b[key] ? 1 : 0))
    : [...items].sort((a, b) => (a < b ? -1 : a > b ? 1 : 0));
}
