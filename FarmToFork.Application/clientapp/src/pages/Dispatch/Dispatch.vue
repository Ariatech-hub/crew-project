<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-toolbar>
        <q-toolbar-title>Sale</q-toolbar-title>
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
        <div>
          <q-table
            square
            :rows="dispatchList"
            :loading="tableLoading"
            :pagination="initialPagination"
            :filter="filter"
          >
            <template v-slot:header>
              <tr>
                <th class="text-left" style="padding-left: 10px">S.No</th>
                <th class="text-left">Name</th>
                <th class="text-left">Phone Number</th>
                <th class="text-left">Email</th>
                <th class="text-left">Grain Season</th>
                <th class="text-left">Grain</th>
                <th class="text-center">Quantity (kg)</th>
                <th class="text-center">Unit Price (NPR)</th>
                <th class="text-center">Total (NPR)</th>
                <th class="text-center">Dispatched Date</th>
              </tr>
            </template>
            <template v-slot:body="props">
              <tr :key="props.row.id">
                <td class="text-left">{{ props.rowIndex + 1 }}</td>
                <td class="text-left">
                  {{ props.row.customerGrainCycleCustomerName }}
                </td>
                <td class="text-left">
                  {{ props.row.customerGrainCycleCustomerPhoneNumber }}
                </td>
                <td class="text-left">
                  {{ props.row.customerGrainCycleCustomerEmail }}
                </td>
                <td class="text-left">
                  {{ props.row.customerGrainCycleGrainCycleName }}
                </td>
                <td class="text-left">
                  {{ props.row.customerGrainCycleGrainCycleGrainName }}
                </td>
                <td class="text-center">{{ props.row.quantity }}</td>
                <td class="text-center">{{ props.row.unitPrice }}</td>
                <td class="text-center">{{ props.row.total }}</td>
                <td class="text-center">
                  {{ dateFormat(props.row.createdDate) }}
                </td>
              </tr>
            </template>
            <template v-slot:top-right> </template>
          </q-table>
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
    let dispatchList = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let selectedWard = ref(0);

    const getAllDispatch = async () => {
      try {
        const response = await api.get("dispatch/list");
        dispatchList.value = response.data;
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
          link.setAttribute("download", "Sales_Report.xlsx");
          document.body.appendChild(link);
          link.click();
        })
        .catch((error) => handleError(error))
        .finally(() => $q.loading.hide());
    };

    onMounted(async () => {
      $q.loading.show();

      await getAllDispatch();
      $q.loading.hide();
    });
    return {
      tableLoading,
      dateFormat,
      dispatchList,
      initialPagination: {
        rowsPerPage: 30,
        // rowsNumber: xx if getting data from a server
      },
      filter: ref(""),
      onDownloadClicked,
    };
  },
});
</script>
<style>
.q-table__top.relative-position.row.items-center {
  display: none;
}
</style>
