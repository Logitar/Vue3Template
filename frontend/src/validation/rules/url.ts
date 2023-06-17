export default function (s?: string): boolean {
  if (!s) {
    return true;
  }

  let url;

  try {
    url = new URL(s);
  } catch (_) {
    return false;
  }

  return url.protocol === "http:" || url.protocol === "https:";
}
