import type { InjectionKey } from "vue";

export const handleErrorKey = Symbol() as InjectionKey<(e: unknown) => void>;
