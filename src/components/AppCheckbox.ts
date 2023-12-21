type Booleanish = boolean | "true" | "false";

export type CheckboxOptions = {
  ariaLabel?: string;
  disabled?: Booleanish;
  id?: string;
  inline?: Booleanish;
  label?: string;
  modelValue?: Booleanish;
  name?: string;
  required?: Booleanish;
  reverse?: Booleanish;
  switch?: Booleanish;
  value?: string;
};
