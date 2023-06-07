export function toast(title: string, body: string, variant: string): void {
  // TODO(fpion): implement toast
  // $bvToast.toast(t(body), {
  //   solid: true,
  //   title: t(title),
  //   variant,
  // });
  alert(JSON.stringify({ title, body, variant }));
  // TODO(fpion): variant type?
} // TODO(fpion): move to another file

export function handleError(e: any): void {
  if (e) {
    console.error(e);
  }
  toast("errorToast.title", "errorToast.body", "danger");
}
