<template>
  <VcBlade
    v-loading="loading"
    :title="bladeTitle"
    :toolbar-items="bladeToolbar"
    :closable="closable"
    :expanded="expanded"
    :modified="isModified"
    width="70%"
    @close="$emit('close:blade')"
    @expand="$emit('expand:blade')"
    @collapse="$emit('collapse:blade')"
  >
    <VcContainer class="tw-p-2">
      <VcRow class="tw-space-x-4">
        <VcCol :size="6">
          <!-- Main Form -->
          <div class="tw-space-y-4">
            <Field
              v-slot="{ field, errorMessage, handleChange, errors }"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.PROVIDER_NAME')"
              :model-value="item.providerName"
              name="providerName"
            >
              <VcInput
                v-bind="field"
                v-model="item.providerName"
                :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.PROVIDER_NAME')"
                :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.PROVIDER_NAME_PLACEHOLDER')"
                clearable
                :error="!!errors.length"
                :error-message="errorMessage"
                @update:model-value="handleChange"
              />
            </Field>

            <Field
              v-slot="{ field, errorMessage, handleChange, errors }"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.MODEL')"
              :model-value="item.model"
              name="model"
            >
              <VcInput
                v-bind="field"
                v-model="item.model"
                :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.MODEL')"
                :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.MODEL_PLACEHOLDER')"
                clearable
                :error="!!errors.length"
                :error-message="errorMessage"
                @update:model-value="handleChange"
              />
            </Field>

            <Field
              v-slot="{ field, errorMessage, handleChange, errors }"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_TYPE')"
              :model-value="item.requestType"
              name="requestType"
            >
              <VcInput
                v-bind="field"
                v-model="item.requestType"
                :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_TYPE')"
                :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_TYPE_PLACEHOLDER')"
                clearable
                :error="!!errors.length"
                :error-message="errorMessage"
                @update:model-value="handleChange"
              />
            </Field>

            <Field
              v-slot="{ field, errorMessage, handleChange, errors }"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.TASK_TYPE')"
              :model-value="item.taskType"
              name="taskType"
            >
              <VcInput
                v-bind="field"
                v-model="item.taskType"
                :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.TASK_TYPE')"
                :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.TASK_TYPE_PLACEHOLDER')"
                clearable
                :error="!!errors.length"
                :error-message="errorMessage"
                @update:model-value="handleChange"
              />
            </Field>

            <Field
              v-slot="{ errorMessage, handleChange, errors }"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.USER_ID')"
              :model-value="item.userId"
              name="userId"
            >
              <VcInput
                v-model="item.userId"
                :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.USER_ID')"
                :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.USER_ID_PLACEHOLDER')"
                clearable
                :error="!!errors.length"
                :error-message="errorMessage"
                @update:model-value="handleChange"
              />
              <!-- <VcSelect
                v-model="item.userId"
                :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.USER')"
                :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.USER_PLACEHOLDER')"
                :options="[
                  { value: 'id', label: 'id' },
                  { value: 'name', label: 'name' },
                ]"
                option-value="value"
                option-label="label"
                searchable
                :clearable="false"
                :error="!!errors.length"
                :error-message="errorMessage"
                @update:model-value="handleChange"
              /> -->
            </Field>

            <Field
              v-slot="{ field, errorMessage, handleChange, errors }"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.ENTITY_ID')"
              :model-value="item.entityId"
              name="entityId"
            >
              <VcInput
                v-bind="field"
                v-model="item.entityId"
                :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.ENTITY_ID')"
                :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.ENTITY_ID_PLACEHOLDER')"
                clearable
                :error="!!errors.length"
                :error-message="errorMessage"
                @update:model-value="handleChange"
              />
            </Field>

            <Field
              v-slot="{ field, errorMessage, handleChange, errors }"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_DURATION')"
              :model-value="item.requestDuration"
              name="requestDuration"
            >
              <VcInput
                v-bind="field"
                v-model="item.requestDuration"
                :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_DURATION')"
                :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_DURATION_PLACEHOLDER')"
                type="number"
                clearable
                :error="!!errors.length"
                :error-message="errorMessage"
                @update:model-value="handleChange"
              />
            </Field>

            <VcCheckbox
              :model-value="item.isSuccess ?? false"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.IS_SUCCESS')"
            />

            <VcTextarea
              v-model="item.requestContext"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_CONTEXT')"
              :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_CONTEXT_PLACEHOLDER')"
              clearable
            />

            <VcTextarea
              v-model="item.prompt"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.PROMPT')"
              :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.PROMPT_PLACEHOLDER')"
              clearable
            />

            <VcTextarea
              v-model="item.response"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.RESPONSE')"
              :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.RESPONSE_PLACEHOLDER')"
              clearable
            />

            <VcTextarea
              v-model="item.errorText"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.ERROR_TEXT')"
              :placeholder="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.ERROR_TEXT_PLACEHOLDER')"
              clearable
            />
          </div>
        </VcCol>
      </VcRow>
    </VcContainer>
  </VcBlade>
</template>

<script setup lang="ts">
import { computed, onMounted, watch } from "vue";
import { useI18n } from "vue-i18n";
import { IBladeToolbar, IParentCallArgs, useBladeNavigation, useBeforeUnload, usePopup } from "@vc-shell/framework";
import { useAiRequestLogDetails } from "../composables";
import { Field, useForm } from "vee-validate";
import moment from "moment";

export interface Props {
  expanded?: boolean;
  closable?: boolean;
  param?: string;
  options?: Record<string, unknown>;
}

export interface Emits {
  (event: "parent:call", args: IParentCallArgs): void;
  (event: "close:blade"): void;
  (event: "expand:blade"): void;
  (event: "collapse:blade"): void;
}

const props = withDefaults(defineProps<Props>(), {
  expanded: true,
  closable: true,
});

const emit = defineEmits<Emits>();

defineOptions({
  name: "AiRequestLogDetails",
  url: "/ai-request-log",
});

const { t } = useI18n({ useScope: "global" });
const { onBeforeClose } = useBladeNavigation();
const { showConfirmation, showInfo } = usePopup();
const { meta } = useForm({ validateOnMount: false });

const { item, loading, loadAiRequestLog, saveAiRequestLog, deleteAiRequestLog, isModified, resetModificationState } =
  useAiRequestLogDetails();

const bladeTitle = computed(() => {
  return t("AI_REQUEST_LOG.PAGES.DETAILS.TITLE");
});

const createdDate = computed(() => {
  const date = new Date(item.value?.createdDate ?? "");
  return moment(date).format("L LT");
});

const bladeToolbar = computed((): IBladeToolbar[] => [
  // {
  //   id: "save",
  //   title: t("AI_REQUEST_LOG.PAGES.DETAILS.TOOLBAR.SAVE"),
  //   icon: "material-save",
  //   async clickHandler() {
  //     if (meta.value.valid) {
  //       const saved = await saveAiRequestLog(item.value);
  //       if (!props.param && saved?.id) {
  //         emit("parent:call", {
  //           method: "onItemClick",
  //           args: { id: saved.id },
  //         });
  //       }
  //       resetModificationState();
  //       emit("parent:call", { method: "reload" });
  //       if (!props.param) {
  //         emit("close:blade");
  //       }
  //     } else {
  //       showInfo(t("AI_REQUEST_LOG.PAGES.ALERTS.NOT_VALID"));
  //     }
  //   },
  //   disabled: computed(() => !(meta.value.valid && isModified.value)),
  // },
  // {
  //   id: "delete",
  //   title: t("AI_REQUEST_LOG.PAGES.DETAILS.TOOLBAR.DELETE"),
  //   icon: "material-delete",
  //   async clickHandler() {
  //     if (props.param && (await showConfirmation(t("AI_REQUEST_LOG.PAGES.ALERTS.DELETE")))) {
  //       await deleteAiRequestLog(props.param);
  //       emit("parent:call", { method: "reload" });
  //       emit("close:blade");
  //     }
  //   },
  //   isVisible: computed(() => !!props.param),
  // },
]);

watch(
  () => props.param,
  async (newParam) => {
    if (newParam) {
      await loadAiRequestLog(newParam);
    }
  },
  { immediate: true, deep: true },
);

onMounted(async () => {
  if (props.param) {
    await loadAiRequestLog(props.param);
  } else if (props.options?.item) {
    item.value = props.options.item;
    resetModificationState();
  }
});

onBeforeClose(async () => {
  if (isModified.value) {
    return await showConfirmation(t("AI_REQUEST_LOG.PAGES.ALERTS.CLOSE_CONFIRMATION"));
  }
  return true;
});

useBeforeUnload(computed(() => isModified.value));

defineExpose({
  title: bladeTitle,
});
</script>
