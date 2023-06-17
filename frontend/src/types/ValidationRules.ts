export type ValidationRules = {
  confirmed?: string[];
  email?: boolean;
  max_length?: number;
  max_value?: number;
  min_length?: number;
  min_value?: number;
  regex?: string;
  require_digit?: boolean;
  require_lowercase?: boolean;
  require_non_alphanumeric?: boolean;
  require_uppercase?: boolean;
  required?: boolean;
  unique_chars?: number;
  url?: boolean;
  username?: string;
};
