import type { User } from "@/types/users";

function getEmailKey(address: string): string {
  return `user:email:${address.trim().toLowerCase()}`;
}
function getIdKey(id: string): string {
  return `user:id:${id.trim()}`;
}
function getPasswordKey(user: User): string {
  return `user:id:${user.id.trim()}:password`;
}
function getUsernameKey(username: string): string {
  return `user:username:${username.trim().toLowerCase()}`;
}

class UserRepository {
  storage: Storage;

  constructor(storage: Storage) {
    this.storage = storage;
  }

  find(id: string): User | undefined {
    const serialized: string | null = this.storage.getItem(getIdKey(id));
    if (!serialized) {
      return;
    }
    return JSON.parse(serialized) as User;
  }

  findByEmail(address: string): User | undefined {
    const id: string | null = this.storage.getItem(getEmailKey(address));
    if (!id) {
      return;
    }
    return this.find(id);
  }

  findByUsername(username: string): User | undefined {
    const id: string | null = this.storage.getItem(getUsernameKey(username));
    if (!id) {
      return;
    }
    return this.find(id);
  }

  findPassword(user: User): string | undefined {
    return this.storage.getItem(getPasswordKey(user)) ?? undefined;
  }

  save(user: User, passwordHash?: string): void {
    this.storage.setItem(getIdKey(user.id), JSON.stringify(user));
    this.storage.setItem(getUsernameKey(user.username), user.id);
    if (user.email) {
      this.storage.setItem(getEmailKey(user.email?.address), user.id);
    }

    if (passwordHash) {
      this.storage.setItem(getPasswordKey(user), passwordHash);
    }
  }
}

export default UserRepository;
