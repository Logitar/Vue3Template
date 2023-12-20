export type RecoverPasswordPayload = {
  username: string;
};

export type RegisterPayload = {
  username: string;
  password?: string;
  emailAddress?: string;
  firstName?: string;
  lastName?: string;
};

export type ResetPasswordPayload = {
  token: string;
  password: string;
};

export type SignInPayload = {
  username: string;
  password?: string;
  remember: boolean;
};
