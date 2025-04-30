<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-card-section>
        <Header :headerNameProps="'Purchase'"></Header>
        <div class="row q-mt-sm">
          <div class="col-md-12">
            <q-table
              square
              :rows="receiptDetails"
              :pagination="initialPagination"
              :filter="filter"
            >
              <template v-slot:header>
                <tr>
                  <th class="text-left">S.No</th>
                  <th class="text-left">Grain Season</th>
                  <th class="text-left">Grain Season Nepali Name</th>
                  <th class="text-left">Grain</th>
                  <th class="text-left">Grain Name</th>
                  <th class="text-center">Actual Sales (kg)</th>
                  <th class="text-center">Unit Price (NPR)</th>
                  <th class="text-center">Total Price (NPR)</th>
                </tr>
              </template>
              <template v-slot:body="props">
                <tr :key="props.row.id">
                  <td class="text-left">{{ props.rowIndex + 1 }}</td>
                  <td class="text-left">
                    {{ props.row.productionPlanGrainCycleName }}
                  </td>
                  <td class="text-left">
                    {{ props.row.productionPlanGrainCycleNepaliName }}
                  </td>
                  <td class="text-left">
                    {{ props.row.productionPlanGrainName }}
                  </td>
                  <td class="text-left">
                    {{ props.row.productionPlanGrainNepaliName }}
                  </td>
                  <td class="text-center">
                    {{ props.row.productionPlanActualSalesThroughCooperative }}
                  </td>
                  <td class="text-center">{{ props.row.unitPrice }}</td>
                  <td class="text-center">{{ props.row.total }}</td>
                </tr>
              </template>
            </q-table>
          </div>
        </div>
      </q-card-section>
    </q-card>
  </q-page>
</template>
<script>
import { defineComponent, ref, onMounted } from "vue";
import { handleError } from "boot/utility";
import { api } from "boot/axios";
import { default as Header } from "src/components/General/Header.vue";
import { useQuasar } from "quasar";

export default defineComponent({
  components: {
    Header,
  },

  setup() {
    let $q = useQuasar();
    let stocks = ref([]);
    let grains = ref([]);
    let grainCycles = ref([]);
    let selectedGrain = ref(null);
    let selectedGrainCycle = ref(null);
    let receiptDetails = ref([]);

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
      alert("here");
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

    const getPurchase = async () => {
      try {
        $q.loading.show();
        const response = await api.get(`/report/receipt-details`);
        receiptDetails.value = response.data;
        console.log(response.data);
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };

    function onEmit(item) {
      console.log(item);
      alert("here");
    }

    onMounted(async () => {
      try {
        $q.loading.show();
        await getGrains();
        await getStocks();
        await getPurchase();
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
      receiptDetails,
      onEmit,
    };
  },
});
</script>
