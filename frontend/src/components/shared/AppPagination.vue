<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    count?: number;
    first?: boolean;
    last?: boolean;
    modelValue?: number;
    next?: boolean;
    pages?: number;
    previous?: boolean;
    total?: number;
  }>(),
  {
    count: 10,
    first: true,
    last: true,
    modelValue: 1,
    next: true,
    pages: 5,
    previous: true,
    total: 0,
  }
);

const pageCount = computed<number>(() => Math.ceil(props.total / props.count));
const range = computed<number[]>(() => {
  if (props.pages < 1) {
    throw new Error(`props.pages must be superior or equal to 1.`);
  }

  const range = [props.modelValue];
  for (let i = 1; range.length < props.pages; i++) {
    let failures = 0;

    const next = props.modelValue + i;
    if (next <= pageCount.value) {
      range.push(next);
    } else {
      failures++;
    }

    if (range.length === props.pages) {
      break;
    }

    const previous = props.modelValue - i;
    if (previous >= 1) {
      range.unshift(previous);
    } else {
      failures++;
    }

    if (failures === 2) {
      break;
    }
  }

  return range;
});

const emit = defineEmits<{
  (e: "update:modelValue", value: number): void;
}>();
function go(page: number): void {
  if (props.modelValue !== page) {
    emit("update:modelValue", page);
  }
}
</script>

<template>
  <nav :aria-label="t('pagination.label')">
    <ul class="justify-content-center pagination">
      <li v-if="first" :class="{ 'page-item': true, disabled: modelValue === 1 }">
        <a v-if="modelValue > 1" class="page-link" href="#" @click="go(1)" :aria-label="t('pagination.first')">
          <span aria-hidden="true">&laquo;</span>
        </a>
        <span v-else class="page-link" :aria-label="t('pagination.first')">
          <span aria-hidden="true">&laquo;</span>
        </span>
      </li>
      <li v-if="previous" :class="{ 'page-item': true, disabled: modelValue === 1 }">
        <a v-if="modelValue > 1" class="page-link" href="#" @click="go(modelValue - 1)" :aria-label="t('pagination.previous')">
          <span aria-hidden="true">&lsaquo;</span>
        </a>
        <span v-else class="page-link" :aria-label="t('pagination.previous')">
          <span aria-hidden="true">&lsaquo;</span>
        </span>
      </li>
      <li
        v-for="page in range"
        :key="page"
        :class="{ 'page-item': true, active: modelValue === page }"
        :aria-current="modelValue === page ? 'page' : undefined"
      >
        <a v-if="modelValue !== page" class="page-link" href="#" @click="go(page)">{{ page }}</a>
        <span v-else class="page-link">{{ page }}</span>
      </li>
      <li v-if="next" :class="{ 'page-item': true, disabled: modelValue === pageCount }">
        <a v-if="modelValue < pageCount" class="page-link" href="#" @click="go(modelValue + 1)" :aria-label="t('pagination.next')">
          <span aria-hidden="true">&rsaquo;</span>
        </a>
        <span v-else class="page-link" :aria-label="t('pagination.next')">
          <span aria-hidden="true">&rsaquo;</span>
        </span>
      </li>
      <li v-if="last" :class="{ 'page-item': true, disabled: modelValue === pageCount }">
        <a v-if="modelValue < pageCount" class="page-link" href="#" @click="go(pageCount)" :aria-label="t('pagination.last')">
          <span aria-hidden="true">&raquo;</span>
        </a>
        <span v-else class="page-link" :aria-label="t('pagination.last')">
          <span aria-hidden="true">&raquo;</span>
        </span>
      </li>
    </ul>
  </nav>
</template>
