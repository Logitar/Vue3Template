export function toDateTimeLocal(date: Date): string {
  const d = [
    date.getFullYear().toString().padStart(4, "0"),
    (date.getMonth() + 1).toString().padStart(2, "0"),
    date.getDate().toString().padStart(2, "0"),
  ].join("-");
  const t = [date.getHours().toString().padStart(2, "0"), date.getMinutes().toString().padStart(2, "0")].join(":");
  return [d, t].join("T");
}

// TODO(fpion): external library
