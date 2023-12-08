import { ref, computed } from "vue";
import { defineStore } from "pinia";

export const useCounterStore = defineStore("counter", () => {
  const count = ref<number>(0);
  const doubleCount = computed<number>(() => count.value * 2);
  function increment(): void {
    count.value++;
  }

  return { count, doubleCount, increment };
});
