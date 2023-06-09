import type { InjectionKey } from "vue";
import type { TabOptions } from "@/types/TabOptions";

export const bindTabKey = Symbol() as InjectionKey<(tab: TabOptions) => void>;
export const unbindTabKey = Symbol() as InjectionKey<(tab: TabOptions) => void>;
