<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-card-section>
        <q-toolbar class="bg-white q-mt-xs">
          <q-toolbar-title class="text-primary">Stock</q-toolbar-title>
          <q-input
            dense
            debounce="300"
            v-model="filter"
            placeholder="Search"
            class="q-mr-md"
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
        <div class="row q-col-gutter-md q-my-md">
          <div class="col-md-4 col-xs-12 col-sm-4 col-lg-4">
            <q-select
              label="Grain"
              square
              outlined
              dense
              v-model="selectedGrain"
              :options="grains"
              option-label="nepaliName"
              option-value="id"
              map-options
              emit-value
              @update:model-value="getGrainsByGrainId()"
            ></q-select>
          </div>
          <div class="col-md-4 col-xs-12 col-sm-4 col-lg-4">
            <q-select
              label="Grain Season"
              square
              outlined
              dense
              v-model="selectedGrainCycle"
              :options="grainCycles"
              option-label="nepaliName"
              option-value="id"
              emit-value
              map-options
              :disable="selectedGrain == null"
              @update:model-value="getStocks()"
            ></q-select>
          </div>
        </div>
        <q-table
          square
          :rows="stocks"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left">S.No</th>
              <th class="text-left">Grain</th>
              <th class="text-left">Grain Nepali Name</th>
              <th class="text-left">Grain Season</th>
              <th class="text-left">Grain Season Nepali Name</th>
              <th class="text-left">Quantity (kg)</th>
              <th class="text-left">Remaining Quantity (kg)</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">{{ props.rowIndex + 1 }}</td>
              <td class="text-left">{{ props.row.grainCycleGrainName }}</td>
              <td class="text-left">
                {{ props.row.grainCycleGrainNepaliName }}
              </td>
              <td class="text-left">{{ props.row.grainCycleName }}</td>
              <td class="text-left">{{ props.row.grainCycleNepaliName }}</td>

              <td class="text-left">{{ props.row.quantity.toFixed(2) }}</td>
              <td class="text-left">
                {{ props.row.remainingQuantity.toFixed(2) }}
              </td>
            </tr>
          </template>
        </q-table>
      </q-card-section>
    </q-card>
  </q-page>
</template>
<script>
import { defineComponent, ref, onMounted } from "vue";
import { handleError } from "boot/utility";
import { api } from "boot/axios";
import { useRouter, useRoute } from "vue-router";
import { useQuasar } from "quasar";

export default defineComponent({
  setup() {
    let $q = useQuasar();
    let stocks = ref([]);
    let grains = ref([]);
    let grainCycles = ref([]);
    let selectedGrain = ref(null);
    let selectedGrainCycle = ref(null);
    const getStocks = async () => {
      try {
        let grainCycleId = 0;
        if (selectedGrainCycle.value != null) {
          grainCycleId = selectedGrainCycle.value;
        }
        const response = await api.get(
          `stock/list?grainCycleId=${grainCycleId}`
        );
        stocks.value = response.data;
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
    const getGrainsByGrainId = async () => {
      try {
        if (selectedGrain.value == null) {
          return;
        }
        $q.loading.show();
        const response = await api.get(
          `grain/${selectedGrain.value}/grain-cycles`
        );
        grainCycles.value = response.data;
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const onDownloadClicked = async () => {
      $q.loading.show();
      const method = "GET";
      const url = `report/stock/excel`;
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
    onMounted(async () => {
      try {
        $q.loading.show();
        await getGrains();
        await getStocks();
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    });
    return {
      stocks,
      filter: ref(""),
      initialPagination: {
        rowsPerPag: 30,
      },
      grains,
      grainCycles,
      onDownloadClicked,
      selectedGrain,
      selectedGrainCycle,
      getGrainsByGrainId,
      getStocks,
    };
  },
});
</script>
