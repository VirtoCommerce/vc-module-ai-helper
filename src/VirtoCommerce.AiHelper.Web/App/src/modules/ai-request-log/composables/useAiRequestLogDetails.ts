import { computed, ref, ComputedRef, Ref, reactive } from "vue";
import { useAsync, useApiClient, useModificationTracker, useLoading } from "@vc-shell/framework";

import { IAiRequestLog, AiHelperLogClient, AiHelperCommonClient } from "../../../api_client/virtocommerce.aihelper";



export interface IUseAiRequestLogDetails {
  item: Ref<IAiRequestLog>;
  loading: ComputedRef<boolean>;
  logLevel: ComputedRef<string>;
  loadAiRequestLog: (id: string) => Promise<void>;
  saveAiRequestLog: (data?: IAiRequestLog) => Promise<IAiRequestLog | undefined>;
  deleteAiRequestLog: (id: string) => Promise<void>;

  // Modification tracking
  isModified: Readonly<Ref<boolean>>;
  resetModificationState: () => void;
}

export function useAiRequestLogDetails(): IUseAiRequestLogDetails {
  const { getApiClient: getAiHelperLogApiClient } = useApiClient(AiHelperLogClient);
  const { getApiClient: getAiHelperCommonApiClient } = useApiClient(AiHelperCommonClient);

  const item = ref<IAiRequestLog>(reactive({} as IAiRequestLog));
  const logLevelValue = ref<string>("minimal");

  // Use modification tracker - КАК В useOrderDetailsNew.ts
  const { currentValue, isModified, resetModificationState } = useModificationTracker(item);

  const { action: loadLogLevel } = useAsync(async () => {
    const commonApiClient = await getAiHelperCommonApiClient();
    const settings = await commonApiClient.getSettings();
    logLevelValue.value = settings.logLevel?.toLocaleLowerCase() ?? "minimal";
  });

  const { action: loadAiRequestLog, loading: loadingAiRequestLog } = useAsync<string>(async (id) => {
    if (id) {
      await loadLogLevel();

      const logApiClient = await getAiHelperLogApiClient();
      const data = await logApiClient.getAiRequestLogById(id, logLevelValue.value);

      currentValue.value = reactive(data);
      resetModificationState();
    }
  });

  const { action: saveAiRequestLog, loading: savingAiRequestLog } = useAsync<
    IAiRequestLog | undefined,
    IAiRequestLog | undefined
   >(/*async*/ (data): Promise<IAiRequestLog | undefined> => {
    //if (!data) return;

    // const apiClient = await getApiClient();
    // let result: IAiRequestLog;

    // if (data.id) {
    //   result = await apiClient.updateAiRequestLog(data);
    // } else {
    //   result = await apiClient.createAiRequestLog(data);
    // }

    // if (result) {
    //   currentValue.value = reactive(result);
    //   resetModificationState();
    // }

    // return result;
    return Promise.resolve(data);
  });

  const { action: deleteAiRequestLog, loading: deletingAiRequestLog } = useAsync<string>(async (id) => {
    // const apiClient = await getApiClient();
    // await apiClient.deleteAiRequestLog(id);
  });

  const logLevel = computed(() => logLevelValue.value);

  return {
    item: currentValue, // ВАЖНО: возвращаем currentValue, а не item
    loading: useLoading(loadingAiRequestLog, savingAiRequestLog, deletingAiRequestLog),
    logLevel,
    loadAiRequestLog,
    saveAiRequestLog,
    deleteAiRequestLog,
    isModified,
    resetModificationState,
  };
}
