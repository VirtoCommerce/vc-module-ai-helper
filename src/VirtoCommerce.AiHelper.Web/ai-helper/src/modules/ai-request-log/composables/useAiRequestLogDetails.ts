import { computed, ref, ComputedRef, Ref, reactive } from "vue";
import { useAsync, useApiClient, useModificationTracker, useLoading } from "@vc-shell/framework";

import { IAiRequestLog, AiHelperLogClient } from "../../../api_client/virtocommerce.aihelper";



export interface IUseAiRequestLogDetails {
  item: Ref<IAiRequestLog>;
  loading: ComputedRef<boolean>;
  loadAiRequestLog: (id: string) => Promise<void>;
  saveAiRequestLog: (data?: IAiRequestLog) => Promise<IAiRequestLog | undefined>;
  deleteAiRequestLog: (id: string) => Promise<void>;

  // Modification tracking
  isModified: Readonly<Ref<boolean>>;
  resetModificationState: () => void;
}

export function useAiRequestLogDetails(): IUseAiRequestLogDetails {
  const { getApiClient } = useApiClient(AiHelperLogClient);

  const item = ref<IAiRequestLog>(reactive({} as IAiRequestLog));

  // Use modification tracker - КАК В useOrderDetailsNew.ts
  const { currentValue, isModified, resetModificationState } = useModificationTracker(item);

  const { action: loadAiRequestLog, loading: loadingAiRequestLog } = useAsync<string>(async (id) => {
    if (id) {
      const apiClient = await getApiClient();
      const data = await apiClient.getAiRequestLogById(id);

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

  return {
    item: currentValue, // ВАЖНО: возвращаем currentValue, а не item
    loading: useLoading(loadingAiRequestLog, savingAiRequestLog, deletingAiRequestLog),
    loadAiRequestLog,
    saveAiRequestLog,
    deleteAiRequestLog,
    isModified,
    resetModificationState,
  };
}
