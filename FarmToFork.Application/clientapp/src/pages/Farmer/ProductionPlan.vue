<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-toolbar>
        <q-toolbar-title>Production Plan</q-toolbar-title>
        <q-input
          dense
          debounce="300"
          v-model="filter"
          placeholder="Search"
          class="q-mr-md"
          outlined
        >
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </q-input>
        <q-btn
          icon="download"
          color="primary"
          size="sm"
          @click="onDownloadClicked()"
        ></q-btn>
      </q-toolbar>
      <q-card-section style="padding-top: 0px">
        <div class="row q-col-gutter-md">
          <div class="col-md-4">
            <q-select
              outlined
              square
              dense
              v-model="selectedYears"
              :options="years"
              behavior="menu"
              option-label="name"
              option-value="id"
              label="Year "
              clearable
              multiple
              emit-value
              map-options
            >
              <template
                v-slot:option="{ itemProps, opt, selected, toggleOption }"
              >
                <q-item v-bind="itemProps">
                  <q-item-section>
                    <q-item-label v-html="opt.name" />
                  </q-item-section>
                  <q-item-section side>
                    <q-toggle
                      :model-value="selected"
                      @update:model-value="toggleOption(opt)"
                    />
                  </q-item-section>
                </q-item>
              </template>
            </q-select>
          </div>
          <div class="col-md-4">
            <q-select
              outlined
              square
              clearable
              dense
              v-model="selectedGrain"
              :options="grains"
              behavior="menu"
              option-label="name"
              option-value="id"
              label="Grain "
              multiple
              emit-value
              map-options
            >
              <template
                v-slot:option="{ itemProps, opt, selected, toggleOption }"
              >
                <q-item v-bind="itemProps">
                  <q-item-section>
                    <q-item-label v-html="opt.name" />
                  </q-item-section>
                  <q-item-section side>
                    <q-toggle
                      :model-value="selected"
                      @update:model-value="toggleOption(opt)"
                    />
                  </q-item-section>
                </q-item>
              </template>
            </q-select>
          </div>
          <div class="col-md-4">
            <q-select
              outlined
              square
              dense
              clearable
              v-model="selectedCommunity"
              :options="community"
              behavior="menu"
              option-label="name"
              option-value="id"
              label="Community "
              multiple
              emit-value
              map-options
            >
              <template
                v-slot:option="{ itemProps, opt, selected, toggleOption }"
              >
                <q-item v-bind="itemProps">
                  <q-item-section>
                    <q-item-label v-html="opt.name" />
                  </q-item-section>
                  <q-item-section side>
                    <q-toggle
                      :model-value="selected"
                      @update:model-value="toggleOption(opt)"
                    />
                  </q-item-section>
                </q-item>
              </template>
            </q-select>
          </div>
        </div>
        <div class="row q-mt-sm">
          <div class="col-md-4">
            <q-btn
              label="Search"
              @click="getAllDispatch()"
              no-caps
              color="primary"
            ></q-btn>
          </div>
        </div>
        <div class="row q-mt-sm">
          <div class="col-md-12">
            <q-table
              square
              :rows="productionPlanList"
              :loading="tableLoading"
              :pagination="initialPagination"
              :filter="filter"
            >
              <template v-slot:header>
                <tr>
                  <th class="text-left" style="padding-left: 10px">S.No</th>
                  <th class="text-left">Name</th>
                  <th class="text-left">Code</th>
                  <th class="text-left">Tol</th>
                  <th class="text-left">Grain</th>
                  <th class="text-center">Total Production(Kg)</th>
                  <th class="text-center">
                    Total Sale plan to Cooperative (Kg)
                  </th>
                </tr>
              </template>
              <template v-slot:body="props">
                <tr :key="props.row.id">
                  <td class="text-left">{{ props.rowIndex + 1 }}</td>
                  <td class="text-left">{{ props.row.farmerFullName }}</td>
                  <td class="text-left">{{ props.row.farmerCode }}</td>
                  <td class="text-left">{{ props.row.farmerCommunityName }}</td>
                  <td class="text-left">{{ props.row.grainName }}</td>
                  <td class="text-center">{{ props.row.totalProduction }}</td>
                  <td class="text-center">
                    {{ props.row.totalSalesThroughCooperative }}
                  </td>
                </tr>
              </template>
              <template v-slot:top-right> </template>
            </q-table>
          </div>
        </div>
      </q-card-section>
    </q-card>
  </q-page>
</template>

<script>
import { defineComponent, onMounted, ref } from "vue";
import { api } from "boot/axios";
import { handleError, dateFormat } from "boot/utility";
import { useQuasar } from "quasar";

export default defineComponent({
  setup() {
    let productionPlanList = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    const grainCycles = ref([]);
    const grains = ref([]);
    let selectedGrainCycle = ref([]);
    let selectedGrain = ref([]);
    const years = ref([]);
    const selectedYears = ref([]);
    const community = ref([]);
    const selectedCommunity = ref([]);

    const getAllDispatch = async () => {
      try {
        const response = await api.post("production-plan/list", {
          YearIds: selectedYears.value,
          GrainIds: selectedGrain.value,
          CommunityIds: selectedCommunity.value,
        });
        productionPlanList.value = response.data;
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const onDownloadClicked = async () => {
      $q.loading.show();
      const method = "GET";
      const url = `report/dispatch/excel`;
      api
        .request({
          url,
          method,
          headers: {
            "Content-Disposition": "attachment; filename=template.xlsx",
            "Content-Type":
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          },
          responseType: "arraybuffer",
        })
        .then((response) => {
          const url = window.URL.createObjectURL(new Blob([response.data]));
          const link = document.createElement("a");
          link.href = url;
          link.setAttribute("download", "Farmer_Report.xlsx");
          document.body.appendChild(link);
          link.click();
        })
        .catch((error) => handleError(error))
        .finally(() => $q.loading.hide());
    };
    const getYears = async () => {
      try {
        $q.loading.show();
        const response = await api.get(`setting/years`);
        years.value = response.data;
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const getGrains = async () => {
      try {
        $q.loading.show();
        const response = await api.get(`setting/grains`);
        grains.value = response.data;
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const getCommunity = async () => {
      try {
        $q.loading.show();
        const response = await api.get(`setting/communities`);
        community.value = response.data;
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };

    onMounted(async () => {
      $q.loading.show();
      await getAllDispatch();
      await getYears();
      await getGrains();
      await getCommunity();
      $q.loading.hide();
    });
    return {
      tableLoading,
      dateFormat,
      productionPlanList,
      initialPagination: {
        rowsPerPage: 30,
      },
      years,
      grains,
      filter: ref(""),
      onDownloadClicked,
      selectedGrainCycle,
      selectedGrain,
      community,
      selectedYears,
      selectedCommunity,
      getAllDispatch,
    };
  },
});
</script>
<style>
.q-table__top.relative-position.row.items-center {
  display: none;
}
</style>
