export type CurrentUser = {
  displayName: string;
  emailAddress?: string;
  pictureUrl?: string;
};

export type SignInPayload = {
  username: string;
  password: string;
};
