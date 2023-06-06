function toast(title: string, body: string, variant: string): void {
  // TODO(fpion): implement toast
  // $bvToast.toast(t(body), {
  //   solid: true,
  //   title: t(title),
  //   variant,
  // });
}

export function handleError(e: any): void {
  if (e) {
    console.error(e);
  }
  toast("errorToast.title", "errorToast.body", "danger");
}
