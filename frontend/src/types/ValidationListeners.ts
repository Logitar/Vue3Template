export type ValidationListeners = {
  blur: (e: unknown, shouldValidate?: boolean) => void;
  change: (e: unknown, shouldValidate?: boolean) => void;
  input: (e: unknown, shouldValidate?: boolean) => void;
};
