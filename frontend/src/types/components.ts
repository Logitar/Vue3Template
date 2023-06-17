export type AlertVariant = "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark";

export type BadgeVariant = "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark";

export type ButtonType = "button" | "submit" | "reset" | undefined;

export type ButtonVariant = "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark" | "link";

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
