export default function (value: unknown, [targetValue]: [unknown]): boolean {
  return typeof value === typeof targetValue && value === targetValue;
}
