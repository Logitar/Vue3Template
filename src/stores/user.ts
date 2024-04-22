import { defineStore } from "pinia";
import { nanoid } from "nanoid";
import { ref } from "vue";

import locales from "@/resources/locales.json";
import type { Address, AddressPayload, Email, Phone, PhonePayload } from "@/types/users";
import type { CurrentUser, SaveProfilePayload, SignInPayload, UserProfile } from "@/types/account";
import type { Locale } from "@/types/i18n";
import { cleanTrim, isDigit, isNullOrEmpty, isNullOrWhiteSpace } from "@/helpers/stringUtils";

type User = {
  id: string;
  version: number;
  createdOn: string;
  updatedOn: string;
  uniqueName: string;
  uniqueNameNormalized: string;
  password: string;
  passwordChangedOn: string;
  authenticatedOn?: string;
  address?: Address;
  email?: Email;
  phone?: Phone;
  firstName?: string;
  middleName?: string;
  lastName?: string;
  fullName?: string;
  nickname?: string;
  birthdate?: string;
  gender?: string;
  locale?: Locale;
  timeZone?: string;
  picture?: string;
  profile?: string;
  website?: string;
};

function buildFullName(names: (string | undefined)[]): string | undefined {
  const cleanNames: string[] = [];
  names.forEach((name) => {
    const values: string[] = name?.split(" ") ?? [];
    values.forEach((value) => {
      if (!isNullOrEmpty(value)) {
        cleanNames.push(value);
      }
    });
  });
  return cleanTrim(cleanNames.join(" "));
}

function findLocale(code?: string): Locale | undefined {
  if (!code) {
    return undefined;
  }
  return locales.find((locale) => locale.code === code);
}

function formatAddress(address: AddressPayload): string {
  const lines: string[] = [];
  const streetLines: string[] = address.street.replace(/\r+/g, "").split("\n");
  streetLines.forEach((line) => {
    if (!isNullOrWhiteSpace(line)) {
      lines.push(line.trim());
    }
  });
  let regional: string = address.locality.trim();
  if (!isNullOrWhiteSpace(address.region)) {
    regional += ` ${address.region?.trim()}`;
  }
  if (!isNullOrWhiteSpace(address.postalCode)) {
    regional += ` ${address.postalCode?.trim()}`;
  }
  lines.push(regional);
  lines.push(address.country.trim());
  return lines.join("\n");
}

function formatPhone(phone: PhonePayload): string {
  const digits = [...phone.number].filter(isDigit).join("");
  return digits.length === 10 ? `+1${digits}` : `+${digits}`;
}

function normalize(value: string): string {
  return value.trim().toUpperCase();
}

function toUserProfile(user: User): UserProfile {
  return {
    createdOn: user.createdOn,
    updatedOn: user.updatedOn,
    username: user.uniqueName,
    passwordChangedOn: user.passwordChangedOn,
    authenticatedOn: user.authenticatedOn,
    address: user.address,
    email: user.email,
    phone: user.phone,
    firstName: user.firstName,
    middleName: user.middleName,
    lastName: user.lastName,
    fullName: user.fullName,
    nickname: user.nickname,
    birthdate: user.birthdate,
    gender: user.gender,
    locale: user.locale,
    timeZone: user.timeZone,
    picture: user.picture,
    profile: user.profile,
    website: user.website,
  };
}

export const useUserStore = defineStore(
  "users",
  () => {
    const currentUser = ref<User>();
    const users = ref<User[]>([]);

    function getProfile(): UserProfile {
      const user: User | undefined = currentUser.value;
      if (!user) {
        throw { status: 401 };
      }
      return toUserProfile(user);
    }

    function saveProfile(payload: SaveProfilePayload): UserProfile {
      const user: User | undefined = currentUser.value;
      if (!user) {
        throw { status: 401 };
      }
      const now: string = new Date().toISOString();
      let updated: boolean = false;
      if (payload.authenticationInformation) {
        if (payload.authenticationInformation.password.current !== user.password) {
          throw {
            data: { code: "InvalidCredentials", message: "The specified credentials did not match.", data: [] },
            status: 400,
          };
        }
        user.password = payload.authenticationInformation.password.new;
        user.passwordChangedOn = now;
        updated = true;
      }
      if (payload.contactInformation) {
        user.address = payload.contactInformation.address
          ? {
              isVerified: payload.contactInformation.address.isVerified,
              street: payload.contactInformation.address.street.trim(),
              locality: payload.contactInformation.address.locality.trim(),
              postalCode: cleanTrim(payload.contactInformation.address.postalCode),
              region: cleanTrim(payload.contactInformation.address.region),
              country: payload.contactInformation.address.country,
              formatted: formatAddress(payload.contactInformation.address),
            }
          : undefined;
        user.email = payload.contactInformation.email
          ? {
              isVerified: payload.contactInformation.email.isVerified,
              address: payload.contactInformation.email.address.trim(),
            }
          : undefined;
        user.phone = payload.contactInformation.phone
          ? {
              isVerified: payload.contactInformation.phone.isVerified,
              countryCode: cleanTrim(payload.contactInformation.phone.countryCode),
              number: payload.contactInformation.phone.number.trim(),
              extension: cleanTrim(payload.contactInformation.phone.extension),
              e164Formatted: formatPhone(payload.contactInformation.phone),
            }
          : undefined;
        updated = true;
      }
      if (payload.personalInformation) {
        user.firstName = cleanTrim(payload.personalInformation.firstName);
        user.middleName = cleanTrim(payload.personalInformation.middleName);
        user.lastName = cleanTrim(payload.personalInformation.lastName);
        user.fullName = buildFullName([payload.personalInformation.firstName, payload.personalInformation.middleName, payload.personalInformation.lastName]);
        user.nickname = cleanTrim(payload.personalInformation.nickname);
        user.birthdate = payload.personalInformation.birthdate?.toISOString();
        user.gender = cleanTrim(payload.personalInformation.gender);
        user.locale = findLocale(cleanTrim(payload.personalInformation.locale));
        user.timeZone = cleanTrim(payload.personalInformation.timeZone);
        user.picture = cleanTrim(payload.personalInformation.picture);
        user.profile = cleanTrim(payload.personalInformation.profile);
        user.website = cleanTrim(payload.personalInformation.website);
        updated = true;
      }
      if (updated) {
        user.version++;
        user.updatedOn = now;
      }
      currentUser.value = user;
      const index: number = users.value.findIndex(({ id }) => id === user.id);
      if (index < 0) {
        users.value.push(user);
      } else {
        users.value.splice(index, 1, user);
      }
      return toUserProfile(user);
    }

    function signIn(payload: SignInPayload): CurrentUser {
      const usernameNormalized: string = normalize(payload.username);
      let user: User | undefined = users.value.find(({ uniqueNameNormalized }) => uniqueNameNormalized === usernameNormalized);
      const now: string = new Date().toISOString();
      if (!user) {
        user = {
          id: nanoid(),
          version: 1,
          createdOn: now,
          updatedOn: now,
          uniqueName: payload.username,
          uniqueNameNormalized: usernameNormalized,
          password: payload.password,
          passwordChangedOn: now,
        };
        users.value.push(user);
      } else if (user.password !== payload.password) {
        throw {
          data: { code: "InvalidCredentials", message: "The specified credentials did not match.", data: [] },
          status: 400,
        };
      }
      user.authenticatedOn = now;
      currentUser.value = user;
      return {
        displayName: user.fullName ?? user.uniqueName,
        emailAddress: user.email?.address,
        pictureUrl: user.picture,
      };
    }

    function signOut(): void {
      currentUser.value = undefined;
    }

    return { currentUser, users, getProfile, saveProfile, signIn, signOut };
  },
  {
    persist: true,
  },
);
