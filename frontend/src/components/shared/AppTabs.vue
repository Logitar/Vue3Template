<script setup lang="ts">
import { provide, ref } from "vue";
import { useI18n } from "vue-i18n";
import type { TabOptions } from "@/types/TabOptions";
import { bindTabKey, unbindTabKey } from "@/inject/AppTabs";

const { t } = useI18n();

withDefaults(
  defineProps<{
    id?: string;
  }>(),
  {
    id: "tabs",
  }
);

const tabs = ref<TabOptions[]>([]);

function bindTab(tab: TabOptions): void {
  tabs.value.push(tab);
}
function unbindTab(tab: TabOptions): void {
  const index = tabs.value.findIndex(({ id }) => tab.id === id);
  if (index >= 0) {
    tabs.value.splice(index, 1);
  }
}
provide(bindTabKey, bindTab);
provide(unbindTabKey, unbindTab);
</script>

<template>
  <div>
    <ul class="nav nav-tabs" :id="`tab_${id}_headers`" role="tablist">
      <li v-for="tab in tabs" :key="tab.id" class="nav-item" role="presentation">
        <button
          :class="{ 'nav-link': true, active: tab.active }"
          :id="`tab_${tab.id}_head`"
          data-bs-toggle="tab"
          :data-bs-target="`#tab_${tab.id}_pane`"
          type="button"
          role="tab"
          :aria-controls="`${tab.id}_pane`"
          :aria-selected="tab.active ? 'true' : 'false'"
          :disabled="tab.disabled"
        >
          {{ t(tab.title) }}
        </button>
      </li>
    </ul>
    <div class="tab-content mt-3" :id="`${id}_contents`">
      <slot></slot>
    </div>
  </div>
</template>
