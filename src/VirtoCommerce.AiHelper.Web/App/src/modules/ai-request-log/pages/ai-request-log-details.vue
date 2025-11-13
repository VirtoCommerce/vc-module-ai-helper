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
    <template #actions>
      <div class="tw-flex tw-flex-row tw-items-center">
        <div class="tw-ml-4">
          <slot
            name="status-badge"
            :item="item"
          >
            <VcStatus :variant="item.isSuccess ? 'success' : 'danger'">
              {{ item.isSuccess ? "Success" : "Failed" }}
            </VcStatus>
          </slot>
        </div>
      </div>
    </template>

    <VcContainer class="tw-p-2">
      <VcRow class="tw-space-x-4">
        <VcCol :size="6">
          <!-- Main Form -->
          <div class="tw-space-y-4">
            <VcField
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.PROVIDER_NAME')"
              :model-value="item.providerName"
              orientation="horizontal"
              :aspect-ratio="[1, 3]"
            />

            <VcField
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.MODEL')"
              :model-value="item.model"
              orientation="horizontal"
              :aspect-ratio="[1, 3]"
            />

            <VcField
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_TYPE')"
              :model-value="item.requestType"
              orientation="horizontal"
              :aspect-ratio="[1, 3]"
            />

            <VcField
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.TASK_TYPE')"
              :model-value="item.taskType"
              orientation="horizontal"
              :aspect-ratio="[1, 3]"
            />

            <VcField
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.USER_ID')"
              :model-value="item.userId"
              orientation="horizontal"
              :aspect-ratio="[1, 3]"
            />
            <VcField
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.ENTITY_ID')"
              :model-value="item.entityId"
              orientation="horizontal"
              :aspect-ratio="[1, 3]"
            />
            <VcField
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.ENTITY_TYPE')"
              :model-value="item.entityType"
              orientation="horizontal"
              :aspect-ratio="[1, 3]"
            />

            <VcField
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_DURATION')"
              :model-value="formattedDuration"
              orientation="horizontal"
              :aspect-ratio="[1, 3]"
            />

            <!--<hr
              v-if="logLevel === 'verbose'"
              class="tw-my-4"
            />
            <VcButton
              v-if="logLevel === 'verbose'"
              :icon="copyIconRequest"
              icon-size="m"
              class="tw-float-right -tw-mt-10"
              text
              @click="copy(item.requestContext, 'request')"
            ></VcButton>
            <VcField
              v-if="logLevel === 'verbose'"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_CONTEXT')"
              :model-value="item.requestContext"
              orientation="vertical"
            />
            <hr
              v-if="logLevel === 'verbose'"
              class="tw-my-4"
            />
            <VcButton
              v-if="logLevel === 'verbose'"
              :icon="copyIconPrompt"
              icon-size="m"
              class="tw-float-right -tw-mt-10"
              text
              @click="copy(item.prompt, 'prompt')"
            ></VcButton>
            <VcField
              v-if="logLevel === 'verbose'"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.PROMPT')"
              :model-value="item.prompt"
              orientation="vertical"
            />
            <hr
              v-if="logLevel === 'verbose'"
              class="tw-my-4"
            />
            <VcButton
              v-if="item.isSuccess && logLevel === 'verbose'"
              :icon="copyIconResponse"
              icon-size="m"
              class="tw-float-right -tw-mt-10"
              text
              @click="copy(item.response, 'response')"
            ></VcButton>
            <VcField
              v-if="item.isSuccess && logLevel === 'verbose'"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.RESPONSE')"
              :model-value="item.response"
              orientation="vertical"
            />
            <VcButton
              v-if="!item.isSuccess && logLevel === 'verbose'"
              :icon="copyIconError"
              icon-size="m"
              class="tw-float-right -tw-mt-10"
              text
              @click="copy(item.errorText, 'error')"
            ></VcButton>
            <VcField
              v-if="!item.isSuccess && logLevel === 'verbose'"
              :label="$t('AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.ERROR_TEXT')"
              :model-value="item.errorText"
              orientation="vertical"
            />-->
            <VcAccordion
              v-if="logLevel === 'verbose'"
              :items="accordionItems"
              :collapsed-height="80"
              :max-expanded-height="300"
              :multiple="true"
              variant="default"
            />
          </div>
        </VcCol>
      </VcRow>
    </VcContainer>
  </VcBlade>
</template>

<script setup lang="ts">
import { computed, onMounted, watch, ref, Ref } from "vue";
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

const { item, loading, loadAiRequestLog, logLevel, isModified, resetModificationState } = useAiRequestLogDetails();

const bladeTitle = computed(() => {
  return t("AI_REQUEST_LOG.PAGES.DETAILS.TITLE");
});

const copyIconRequest = ref("material-content_copy");
const copyIconPrompt = ref("material-content_copy");
const copyIconResponse = ref("material-content_copy");
const copyIconError = ref("material-content_copy");

const iconRefs: Record<string, Ref<string>> = {
  request: copyIconRequest,
  prompt: copyIconPrompt,
  response: copyIconResponse,
  error: copyIconError,
};

function copy(value: string | undefined, iconKey: string) {
  if (!value) return;
  navigator.clipboard?.writeText(value);
  const iconRef = iconRefs[iconKey];
  if (iconRef) {
    iconRef.value = "material-check";
    setTimeout(() => {
      iconRef.value = "material-content_copy";
    }, 1000);
  }
}

const accordionItems = computed(() => {
  return [
    { id: 1, title: t("AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.REQUEST_CONTEXT"), content: item.value.requestContext },
    { id: 2, title: t("AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.PROMPT"), content: item.value.prompt },
    { id: 3, title: t("AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.RESPONSE"), content: item.value.response },
    { id: 4, title: t("AI_REQUEST_LOG.PAGES.DETAILS.FORM.INFO.ERROR_TEXT"), content: item.value.errorText },
  ];
});

const createdDate = computed(() => {
  const date = new Date(item.value?.createdDate ?? "");
  return moment(date).format("L LT");
});

const formattedDuration = computed(() => {
  const duration = item.value?.requestDuration ?? 0;
  if (duration < 1000) {
    return `${duration} ms`;
  } else {
    return `${Math.round(duration / 1000)} s`;
  }
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
