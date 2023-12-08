import { beforeEach, describe, it, expect } from "vitest";
import { setActivePinia, createPinia } from "pinia";

import { useCounterStore } from "../counter";

describe("counterStore", () => {
  beforeEach(() => {
    setActivePinia(createPinia());
  });

  it.concurrent("should increment the counter", () => {
    const counter = useCounterStore();
    expect(counter.count).toBe(0);
    counter.increment();
    expect(counter.count).toBe(1);
    expect(counter.doubleCount).toBe(2);
    counter.increment();
    expect(counter.count).toBe(2);
    expect(counter.doubleCount).toBe(4);
  });
});
