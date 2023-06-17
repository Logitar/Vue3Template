export function assign<T, TKey extends keyof T, TValue extends T[TKey]>(obj: T, key: TKey, value: TValue) {
  obj[key] = value;
}

export function isEmpty(obj: object): boolean {
  return Object.keys(obj).length === 0;
}
