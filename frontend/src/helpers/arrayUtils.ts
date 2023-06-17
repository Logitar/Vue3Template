export function orderBy<T>(items: T[], key?: keyof T): T[] {
  return key ? [...items].sort((a, b) => (a[key] < b[key] ? -1 : a[key] > b[key] ? 1 : 0)) : [...items].sort();
}
