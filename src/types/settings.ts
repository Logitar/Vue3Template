export type CountrySettings = {
  code: string;
  postalCode?: string;
  regions: string[];
};

export type PasswordSettings = {
  minimumLength?: number;
  uniqueCharacters?: number;
  requireNonAlphanumeric?: boolean;
  requireLowercase?: boolean;
  requireUppercase?: boolean;
  requireDigit?: boolean;
};

export type UsernameSettings = {
  allowedCharacters?: string;
};
