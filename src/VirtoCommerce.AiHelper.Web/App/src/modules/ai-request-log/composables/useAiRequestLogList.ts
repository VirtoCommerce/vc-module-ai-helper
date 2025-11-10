import { computed, ref, ComputedRef, Ref } from "vue";
import { useAsync, useApiClient } from "@vc-shell/framework";
import { useI18n } from "vue-i18n";

import { ISearchAiRequestLogQuery, SearchAiRequestLogQuery, IAiRequestLog, AiRequestLogSearchResult, AiHelperLogClient } from "../../../api_client/virtocommerce.aihelper";

interface FilterState {
  isSuccess?: boolean;
  startDate?: string;
  endDate?: string;
  [key: string]: string[] | string | boolean | undefined;
}

export interface IUseAiRequestLogList {
  items: ComputedRef<IAiRequestLog[]>;
  totalCount: ComputedRef<number>;
  pages: ComputedRef<number>;
  currentPage: ComputedRef<number>;
  searchQuery: Ref<SearchAiRequestLogQuery>;
  loadAiRequestLogs: (query?: ISearchAiRequestLogQuery) => Promise<void>;
  loading: ComputedRef<boolean>;
  statuses: ComputedRef<Array<{ value: string | undefined; displayValue: string | undefined }>>;

  // Filters - staged/applied architecture
  stagedFilters: Ref<FilterState>;
  appliedFilters: Ref<FilterState>;
  hasFilterChanges: ComputedRef<boolean>;
  hasFiltersApplied: ComputedRef<boolean>;
  activeFilterCount: ComputedRef<number>;
  toggleFilter: (filterType: keyof FilterState, value: string, checked: boolean) => void;
  applyFilters: () => Promise<void>;
  resetFilters: () => Promise<void>;
  resetSearch: () => Promise<void>;
}

export interface UseAiRequestLogListOptions {
  pageSize?: number;
  sort?: string;
}

export enum AiRequestLogStatus {
  Success = "Success",
  Failed = "Failed",
}

export function useAiRequestLogList(options?: UseAiRequestLogListOptions): IUseAiRequestLogList {
  const { getApiClient } = useApiClient(AiHelperLogClient);
  const { t } = useI18n({ useScope: "global" });

  const pageSize = options?.pageSize || 20;
  const searchQuery = ref<SearchAiRequestLogQuery>(new SearchAiRequestLogQuery({
    take: pageSize,
    sort: options?.sort,
  }));
  const searchResult = ref<AiRequestLogSearchResult>();

  const { action: loadAiRequestLogs, loading: loadingAiRequestLogs } = useAsync<ISearchAiRequestLogQuery>(
    async (_query) => {
      searchQuery.value = new SearchAiRequestLogQuery({ ...searchQuery.value, ...(_query || {}) });

      const apiClient = await getApiClient();
      searchResult.value = await apiClient.searchLog(searchQuery.value);
    },
  );

  // Filter state with staged/applied architecture
  const stagedFilters = ref<FilterState>({});
  const appliedFilters = ref<FilterState>({});

  const hasFilterChanges = computed((): boolean => {
    return (
      stagedFilters.value?.isSuccess !== appliedFilters.value?.isSuccess ||
      stagedFilters.value?.startDate !== appliedFilters.value?.startDate ||
      stagedFilters.value?.endDate !== appliedFilters.value?.endDate
    );
  });

  const hasFiltersApplied = computed((): boolean => {
    return appliedFilters.value?.isSuccess || !!appliedFilters.value?.startDate || !!appliedFilters.value?.endDate;
  });

  const activeFilterCount = computed((): number => {
    let count = 0;
    if (appliedFilters.value?.isSuccess) count++;
    if (appliedFilters.value?.startDate) count++;
    if (appliedFilters.value?.endDate) count++;
    return count;
  });

  const toggleFilter = (filterType: keyof FilterState, value: string, checked: boolean) => {
    if (filterType === "isSuccess") {
      if (value === "Success" && checked) {
        stagedFilters.value = {
          ...stagedFilters.value,
          isSuccess: true,
        };
      } else if (value === "Failed" && checked) {
        stagedFilters.value = {
          ...stagedFilters.value,
          isSuccess: false,
        };
      }
    }else if (filterType === "startDate" || filterType === "endDate") {
      stagedFilters.value = {
        ...stagedFilters.value,
        [filterType]: value || undefined,
      };
    }
  }

  const applyFilters = async () => {
    // Deep copy staged to applied
    appliedFilters.value = {
      isSuccess: stagedFilters.value?.isSuccess,
      startDate: stagedFilters.value?.startDate,
      endDate: stagedFilters.value?.endDate,
    };

    // Convert to API query format with proper Date types
    const queryWithFilters = {
      ...searchQuery.value,
      isSuccess: appliedFilters.value.isSuccess,
      startDate: appliedFilters.value.startDate ? new Date(appliedFilters.value.startDate) : undefined,
      endDate: appliedFilters.value.endDate ? new Date(appliedFilters.value.endDate) : undefined,
      skip: 0, // Reset pagination
    };

    await loadAiRequestLogs(queryWithFilters);
  };

  const resetFilters = async () => {
    stagedFilters.value = { };
    appliedFilters.value = { };

    const queryWithoutFilters = {
      ...searchQuery.value,
      isSuccess: undefined,
      startDate: undefined,
      endDate: undefined,
      skip: 0,
    };

    await loadAiRequestLogs(queryWithoutFilters);
  };

  const resetSearch = async () => {
    stagedFilters.value = { };
    appliedFilters.value = { };

    const resetQuery = {
      take: pageSize,
      sort: options?.sort,
      skip: 0,
      keyword: "",
    };

    await loadAiRequestLogs(resetQuery);
  };

  // Computed properties
  const items = computed(() => searchResult.value?.results || []);
  const totalCount = computed(() => searchResult.value?.totalCount || 0);
  const pages = computed(() => Math.ceil(totalCount.value / pageSize));
  const currentPage = computed(() => Math.floor((searchQuery.value.skip || 0) / pageSize) + 1);
  const loading = computed(() => loadingAiRequestLogs.value);

  // Example statuses - customize for your entity
  const statuses = computed(() => {
    const statusEntries = Object.entries(AiRequestLogStatus);
    return statusEntries.map(([value, displayValue]) => ({
      value,
      displayValue: t(`AI_REQUEST_LOG.PAGES.LIST.TABLE.FILTER.STATUS.${displayValue}`),
    }));
  });

  return {
    items,
    totalCount,
    pages,
    currentPage,
    searchQuery,
    loadAiRequestLogs,
    loading,
    statuses,

    // Filters
    stagedFilters,
    appliedFilters,
    hasFilterChanges,
    hasFiltersApplied,
    activeFilterCount,
    toggleFilter,
    applyFilters,
    resetFilters,
    resetSearch,
  };
}
