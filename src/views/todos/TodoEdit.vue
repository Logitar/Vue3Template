<script setup lang="ts">
import { TarCheckbox } from "logitar-vue3-ui";
import { computed, inject, onMounted, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";

import AppBackButton from "@/components/shared/AppBackButton.vue";
import AppDelete from "@/components/shared/AppDelete.vue";
import AppSaveButton from "@/components/shared/AppSaveButton.vue";
import DescriptionTextarea from "@/components/shared/DescriptionTextarea.vue";
import DisplayNameInput from "@/components/shared/DisplayNameInput.vue";
import StatusDetail from "@/components/shared/StatusDetail.vue";
import StatusInfo from "@/components/shared/StatusInfo.vue";
import type { ApiError } from "@/types/api";
import type { Todo } from "@/types/todos";
import { createTodo, deleteTodo, readTodo, replaceTodo } from "@/api/todos";
import { handleErrorKey } from "@/inject/App";
import { useToastStore } from "@/stores/toast";

const handleError = inject(handleErrorKey) as (e: unknown) => void;
const route = useRoute();
const router = useRouter();
const toasts = useToastStore();
const { t } = useI18n();

const description = ref<string>("");
const displayName = ref<string>("");
const hasLoaded = ref<boolean>(false);
const isDeleting = ref<boolean>(false);
const isDone = ref<boolean>(false);
const todo = ref<Todo>();

const hasChanges = computed<boolean>(
  () =>
    isDone.value !== (todo.value?.isDone ?? false) ||
    displayName.value !== (todo.value?.displayName ?? "") ||
    description.value !== (todo.value?.description ?? ""),
);

async function onDelete(hideModal: () => void): Promise<void> {
  if (todo.value && !isDeleting.value) {
    isDeleting.value = true;
    try {
      await deleteTodo(todo.value.id);
      hideModal();
      toasts.success("todos.delete.success");
      router.push({ name: "TodoList" });
    } catch (e: unknown) {
      handleError(e);
    } finally {
      isDeleting.value = false;
    }
  }
}

function setModel(model: Todo): void {
  todo.value = model;
  description.value = model.description ?? "";
  displayName.value = model.displayName;
  isDone.value = model.isDone;
}

const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    if (todo.value) {
      const updatedTodo = await replaceTodo(
        todo.value.id,
        {
          isDone: isDone.value,
          displayName: displayName.value,
          description: description.value,
        },
        todo.value.version,
      );
      setModel(updatedTodo);
      toasts.success("todos.updated");
    } else {
      const createdTodo = await createTodo({
        displayName: displayName.value,
        description: description.value,
      });
      setModel(createdTodo);
      toasts.success("todos.created");
      router.replace({ name: "TodoEdit", params: { id: createdTodo.id } });
    }
  } catch (e: unknown) {
    handleError(e);
  }
});

onMounted(async () => {
  try {
    const id = route.params.id?.toString();
    if (id) {
      const todo = await readTodo(id);
      setModel(todo);
    }
  } catch (e: unknown) {
    const { status } = e as ApiError;
    if (status === 404) {
      router.push({ path: "/not-found" });
    } else {
      handleError(e);
    }
  }
  hasLoaded.value = true;
});
</script>

<template>
  <main class="container">
    <template v-if="hasLoaded">
      <h1>{{ todo?.displayName ?? t("todos.title.new") }}</h1>
      <StatusDetail v-if="todo" :aggregate="todo" />
      <p>
        <StatusInfo v-if="todo && todo.doneBy && todo.doneOn" :actor="todo.doneBy" :date="todo.doneOn" format="todos.doneOn" />
      </p>
      <form @submit.prevent="onSubmit">
        <div class="mb-3">
          <AppSaveButton class="me-1" :disabled="isSubmitting || !hasChanges" :exists="Boolean(todo)" :loading="isSubmitting" />
          <AppBackButton class="mx-1" :has-changes="hasChanges" />
          <AppDelete
            v-if="todo"
            class="ms-1"
            confirm="todos.delete.confirm"
            :displayName="todo.displayName"
            :loading="isDeleting"
            title="todos.delete.title"
            @confirmed="onDelete"
          />
        </div>
        <div class="mb-3">
          <TarCheckbox v-if="todo" id="is-done" :label="t('todos.isDone')" switch v-model="isDone" />
        </div>
        <DisplayNameInput required v-model="displayName" />
        <DescriptionTextarea v-model="description" />
      </form>
    </template>
  </main>
</template>
