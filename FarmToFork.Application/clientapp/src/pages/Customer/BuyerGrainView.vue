<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-toolbar>
        <q-toolbar-title>Buyer Grain View</q-toolbar-title>
      </q-toolbar>
      <q-card-section>
        <q-table square :rows="grainCycleData">
          <template v-slot:header>
            <tr>
              <th class="text-left">Grain</th>
              <th class="text-left">Grain Season</th>
              <th class="text-left">Estimated Quantity (kg)</th>
              <th class="text-left">Estimated Price (NPR)</th>
              <th class="text-left">Total Price (NPR)</th>
              <th class="text-center">Status</th>
              <th class="text-center">Created Date</th>
              <th class="text-left">Remarks</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">
                {{ props.row.customerGrainCycleGrainCycleGrainName }}
              </td>
              <td class="text-left">
                {{ props.row.customerGrainCycleGrainCycleName }}
              </td>
              <td class="text-left">{{ props.row.quantity }}</td>
              <td class="text-left">{{ props.row.price }}</td>
              <td class="text-left">
                {{ props.row.quantity * props.row.price }}
              </td>
              <td class="text-center">
                <q-chip
                  dense
                  color="positive"
                  text-color="white"
                  size="12px"
                  v-if="props.row.statusCode == 'ST'"
                >
                  {{ props.row.statusName }}
                </q-chip>
                <q-chip
                  dense
                  color="teal"
                  text-color="white"
                  size="12px"
                  v-else-if="props.row.statusCode == 'CT'"
                >
                  {{ props.row.statusName }}
                </q-chip>
                <q-chip
                  dense
                  color="orange"
                  text-color="white"
                  size="12px"
                  v-else-if="props.row.statusCode == 'SL'"
                >
                  {{ props.row.statusName }}
                </q-chip>
                <q-chip
                  dense
                  color="primary"
                  text-color="white"
                  v-else-if="props.row.statusCode == 'PR'"
                  size="12px"
                >
                  {{ props.row.statusName }}
                </q-chip>
              </td>
              <td class="text-center">
                {{
                  props.row.createdDate ? dateFormat(props.row.createdDate) : ""
                }}
              </td>
              <td class="text-left">{{ props.row.remarks }}</td>
            </tr>
          </template>
        </q-table>
      </q-card-section>
    </q-card>
  </q-page>
</template>
<script>
import { defineComponent, ref, onMounted } from "vue";
import { handleError, dateFormat } from "boot/utility";
import { api } from "boot/axios";
import { useRouter, useRoute } from "vue-router";
import { useQuasar } from "quasar";

export default defineComponent({
  setup() {
    let grainCycleData = ref([]);
    let route = useRoute();
    let $q = useQuasar();

    const getGrainData = async () => {
      try {
        const response = await api.get(
          `customer/grain-cycle/${route.params.id}/status`
        );
        grainCycleData.value = response.data;
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };

    onMounted(async () => {
      try {
        $q.loading.show();
        await getGrainData();
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    });
    return {
      grainCycleData,
      dateFormat,
    };
  },
});
</script>
