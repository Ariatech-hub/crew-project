<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-card-section>
        <q-toolbar class="bg-white q-mt-xs">
          <q-toolbar-title class="text-primary">Dispatch Buyer</q-toolbar-title>
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
              option-label="name"
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
              option-label="name"
              option-value="id"
              emit-value
              map-options
              :disable="selectedGrain == null"
              @update:model-value="getDispatchedCustomer()"
            ></q-select>
          </div>
          <div class="col-md-4 col-xs-12 col-sm-4 col-lg-4">
            <q-select
              label="Customer"
              square
              outlined
              dense
              v-model="selectedCustomer"
              :options="customers"
              option-label="name"
              option-value="id"
              emit-value
              map-options
              @update:model-value="getDispatchedCustomer()"
            ></q-select>
          </div>
        </div>
        <q-table
          square
          :rows="dispatchedCustomers"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left">S.No</th>
              <th class="text-left">Customer</th>
              <th class="text-left">Grain</th>
              <th class="text-left">Grain Season</th>
              <th class="text-left">Quantity (kg)</th>
              <th class="text-left">Available Quantity (kg)</th>
              <th class="text-center">Action</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">{{ props.rowIndex + 1 }}</td>
              <td class="text-left">{{ props.row.customerName }}</td>
              <td class="text-left">{{ props.row.grainCycleGrainName }}</td>
              <td class="text-left">{{ props.row.grainCycleName }}</td>
              <td class="text-left">{{ props.row.quantity.toFixed(2) }}</td>
              <td class="text-left">
                {{ props.row.availableQuantity.toFixed(2) }}
              </td>
              <td class="text-center">
                <q-btn
                  icon="send_time_extension"
                  round
                  flat
                  unelevated
                  color="primary"
                  @click="onSend(props.row)"
                ></q-btn>
              </td>
            </tr>
          </template>
        </q-table>
        <q-dialog v-model="showDialog" persistent>
          <q-card square>
            <q-toolbar>
              <q-toolbar-title class="text-primary"
                >Are you sure you want to dispatch ?</q-toolbar-title
              >
              <q-btn
                color="primary"
                flat
                round
                dense
                icon="close"
                v-close-popup
              />
            </q-toolbar>
            <q-card-section>
              <q-form @submit="onDispatched">
                <div class="row q-col-gutter-md">
                  <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                    <q-input
                      label="Unit Price"
                      v-model="dispatch.unitPrice"
                      square
                      outlined
                      dense
                    ></q-input>
                  </div>
                  <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                    <q-input
                      square
                      outlined
                      dense
                      label="Quantity (kg)"
                      v-model="dispatch.quantity"
                    ></q-input>
                  </div>
                  <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                    <q-input
                      label="Total (NPR)"
                      readonly
                      v-model="totalAmount"
                      lazy-rules
                      outlined
                      square
                      dense
                    >
                    </q-input>
                  </div>
                </div>
                <div class="row q-mt-md">
                  <q-btn label="Add" color="primary" type="submit"></q-btn>
                </div>
              </q-form>
            </q-card-section> </q-card
        ></q-dialog>
      </q-card-section>
    </q-card>
  </q-page>
</template>
<script>
import { defineComponent, ref, onMounted, computed } from "vue";
import { handleError } from "boot/utility";
import { api } from "boot/axios";
import { useRouter, useRoute } from "vue-router";
import { useQuasar } from "quasar";

export default defineComponent({
  setup() {
    let $q = useQuasar();
    let dispatchedCustomers = ref([]);
    let selectedGrainCycle = ref(null);
    let selectedCustomer = ref(null);
    let selectedGrain = ref(null);
    let grains = ref([]);
    let grainCycles = ref([]);
    let customers = ref([]);
    let showDialog = ref(false);
    let dispatch = ref({
      unitPrice: 1,
      quantity: null,
      totalAmount: null,
      customerGrainCycleId: null,
      grainCycleId: null,
      customerId: null,
    });

    const totalAmount = computed(() => {
      return (dispatch.value.unitPrice * dispatch.value.quantity).toFixed(2);
    });
    const getDispatchedCustomer = async () => {
      try {
        $q.loading.show();
        let grainCycleId = 0;
        let customerId = 0;
        if (selectedCustomer.value) {
          customerId = selectedCustomer.value;
        }
        if (selectedGrainCycle.value) {
          grainCycleId = selectedGrainCycle.value;
        }
        const response = await api.get(
          `dispatch/available?grainCycleId=${grainCycleId}&customerId=${customerId}`
        );
        dispatchedCustomers.value = response.data;
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
    const getCustomers = async () => {
      try {
        const response = await api.get("customers");
        customers.value = response.data;
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    function onSend(dispatchCustomer) {
      showDialog.value = true;
      dispatch.value.quantity = dispatchCustomer.quantity;
      dispatch.value.customerGrainCycleId = dispatchCustomer.id;
      dispatch.value.grainCycleId = dispatchCustomer.grainCycleId;
      dispatch.value.customerId = dispatchCustomer.customerId;
      console.log(dispatchCustomer, "dispatch");
    }
    const onDispatched = async () => {
      $q.loading.show();
      try {
        console.log(dispatch.value, "dispatch");
        let response = await api.post("dispatch/process", dispatch.value);
        $q.notify({
          message: "Dispatched successfully",
          color: "positive",
          icon: "check",
        });
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };
    onMounted(async () => {
      try {
        $q.loading.show();
        await getGrains();
        await getCustomers();
        await getDispatchedCustomer();
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    });
    return {
      dispatchedCustomers,
      filter: ref(""),
      selectedCustomer,
      selectedGrainCycle,
      initialPagination: {
        rowsPerPage: 30,
      },
      selectedGrain,
      grains,
      grainCycles,
      getGrainsByGrainId,
      getDispatchedCustomer,
      customers,
      showDialog,
      dispatch,
      onSend,
      totalAmount,
      onDispatched,
    };
  },
});
</script>
