export type RegisterPayload = {
  username: string;
  password?: string;
  emailAddress?: string;
  firstName?: string;
  lastName?: string;
};

export type SignInPayload = {
  username: string;
  password?: string;
  remember: boolean;
};
