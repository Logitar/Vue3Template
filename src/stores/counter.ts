import { defineStore } from "pinia";
import { ref, computed } from "vue";

export const useCounterStore = defineStore("counter", () => {
  const count = ref<number>(0);
  const doubleCount = computed<number>(() => count.value * 2);
  function increment(): void {
    count.value++;
  }

  return { count, doubleCount, increment };
});
