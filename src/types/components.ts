import type { RouteLocationRaw } from "vue-router";

export type AlertVariant = "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark";

export type BadgeVariant = "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark";

export type BreadcrumbOptions = {
  to: RouteLocationRaw;
  target?: string;
  text?: string;
};

export type ButtonType = "button" | "submit" | "reset" | undefined;

export type ButtonVariant = "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark" | "link";

export type Hyperlink = {
  text?: string;
  url: string;
};

export type HyperlinkTarget = "_blank" | "_self" | "_parent" | "_top" | string;

export type PictureOptions = {
  alt?: string;
  src: string;
};

export type SelectOption = {
  value: string;
  text?: string;
};

export type TabOptions = {
  active?: boolean;
  disabled?: boolean;
  id: string;
  title: string;
};

export type ToastOptions = {
  message: string;
  solid?: boolean;
  title: string;
  variant?: ToastVariant;
};

export type ToastUtils = {
  error: (options?: ToastOptions) => void;
  success: (message: string, options?: ToastOptions) => void;
  warning: (message: string, options?: ToastOptions) => void;
};

export type ToastVariant = "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark";
