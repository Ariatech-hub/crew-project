<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-toolbar>
        <q-toolbar-title> Buyer View</q-toolbar-title>
        <q-btn
          unelevated
          size="sm"
          color="primary"
          label="List"
          to="/customer/list"
          icon="list"
        />
      </q-toolbar>
      <q-card-section class="bg-white q-mt-xs">
        <h6 class="q-mb-md q-mt-none">Buyer Details</h6>
        <div class="row q-col-gutter-md text-subtitle2">
          <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
            <span>Name: </span> <span>{{ customer.name }}</span>
          </div>
          <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
            <span>Email: </span> <span>{{ customer.email }}</span>
          </div>
          <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
            <span>Phone Number: </span> <span>{{ customer.phoneNumber }}</span>
          </div>
        </div>
        <div class="row q-col-gutter-md text-subtitle2 q-my-sm">
          <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
            <span>Active: </span>
            <span>
              <q-icon
                name="check_circle_outline"
                color="positive"
                size="xs"
                v-if="customer.isActive" />
              <q-icon name="highlight_off" color="negative" size="xs" v-else
            /></span>
          </div>
        </div>
        <q-separator />
        <h6 class="q-mt-md q-mb-none">Buyer Grain</h6>
        <div class="row">
          <q-btn
            label="Add"
            class="q-ml-auto q-mb-md"
            color="primary"
            icon="mdi-plus"
            size="sm"
            unelevated
            @click="onAdd()"
          ></q-btn>
        </div>
        <q-dialog v-model="showDialog" position="top" persistent>
          <q-card square>
            <q-toolbar>
              <q-toolbar-title class="text-primary"
                >Add Grain Season</q-toolbar-title
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
              <q-form @submit="onAddCustomerGrainCycle">
                <div class="row q-col-gutter-md">
                  <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                    <q-select
                      square
                      dense
                      outlined
                      v-model="selectedGrain"
                      label="Grain *"
                      clearable
                      :options="grains"
                      option-label="nepaliName"
                      emit-value
                      map-options
                      option-value="id"
                      @update:model-value="getGrainsByGrainId()"
                      lazy-rules
                      :rules="[
                        (val) =>
                          (val !== null && val !== '') || 'Grain  is required',
                      ]"
                    />
                  </div>
                  <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                    <q-select
                      label="Grain Season *"
                      square
                      outlined
                      dense
                      v-model="selectedGrainCycle"
                      :options="grainCycles"
                      option-label="nepaliName"
                      option-value="id"
                      map-options
                      emit-value
                      clearable
                      lazy-rules
                      :disable="selectedGrain == null"
                      :rules="[
                        (val) =>
                          (val !== null && val !== '') ||
                          'Grain Season is required',
                      ]"
                    ></q-select>
                  </div>
                </div>
                <div class="row q-col-gutter-md">
                  <!-- <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                    <q-select
                      label="Status *"
                      square
                      outlined
                      dense
                      clearable
                      v-model="selectedCustomerGrainCycleStatus"
                      :options="customerGrainCycleStatuses"
                      option-label="name"
                      option-value="id"
                      map-options
                      emit-value
                      lazy-rules
                      :rules="[
                        (val) =>
                          (val !== null && val !== '') || 'Status is required',
                      ]"
                    ></q-select>
                  </div> -->
                  <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                    <q-input
                      label="Estimated Quantity (kg)*"
                      type="number"
                      step="0.01"
                      square
                      outlined
                      dense
                      v-model="customerGrainCycle.estimatedQuantity"
                      lazy-rules
                      :rules="[
                        (val) =>
                          (val && val > 0) || 'Estimated Quantity is required',
                      ]"
                    ></q-input>
                  </div>
                  <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                    <q-input
                      label="Estimated Price (NPR)*"
                      type="number"
                      step="0.01"
                      square
                      outlined
                      dense
                      v-model="customerGrainCycle.estimatedCost"
                      lazy-rules
                      :rules="[
                        (val) =>
                          (val && val > 0) || 'Estimated Price is required',
                      ]"
                    ></q-input>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                    <q-input
                      label="Remarks"
                      square
                      dense
                      outlined
                      type="textarea"
                      v-model="customerGrainCycle.remarks"
                    ></q-input>
                  </div>
                </div>
                <div class="row q-mt-md">
                  <q-btn label="Add" color="primary" type="submit"></q-btn>
                </div>
              </q-form>
            </q-card-section>
          </q-card>
        </q-dialog>
        <q-table
          square
          :rows="customerGrainCycles"
          :pagination="initialPagination"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left">S.No</th>
              <th class="text-left">Grain</th>
              <th class="text-left">Grain Nepali Name</th>
              <th class="text-left">Grain Season</th>
              <th class="text-left">Grain Season Nepali Name</th>
              <th class="text-left">Estimated Quantity (kg)</th>
              <th class="text-left">Estimated Price (NPR)</th>
              <th class="text-left">Total Price (NPR)</th>
              <th class="text-center">Status</th>
              <th class="text-center">Created Date</th>
              <th class="text-center">Action</th>
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
              <td class="text-left">{{ props.row.quantity }}</td>
              <td class="text-left">{{ props.row.price }}</td>
              <td class="text-left">
                {{ (props.row.quantity * props.row.price).toFixed(2) }}
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
              <!-- TODO: Code Review -->
              <td class="text-center">
                <q-btn
                  size="xs"
                  icon="mdi-pencil"
                  round
                  @click="updateDialogFunction(props.row)"
                  color="primary"
                >
                  <q-tooltip>Update Status</q-tooltip>
                </q-btn>
                <q-btn
                  class="q-ml-xs"
                  icon="visibility"
                  color="positive"
                  round
                  size="xs"
                  :to="`/customer/${customerId}/buyer-grain/${props.row.id}`"
                >
                  <q-tooltip>View</q-tooltip>
                </q-btn>
                <q-btn
                  class="q-ml-xs"
                  icon="delete"
                  color="negative"
                  round
                  size="xs"
                  @click="deleteCustomerGrainCycle(props.row)"
                  v-if="
                    props.row.statusCode == 'ST' || props.row.statusCode == 'CT'
                  "
                >
                  <q-tooltip>Delete</q-tooltip>
                </q-btn>

                <q-btn-dropdown
                  rounded
                  color="primary"
                  label="More"
                  size="xs"
                  class="q-ml-xs"
                  v-if="
                    props.row.statusCode == 'SL' || props.row.statusCode == 'PR'
                  "
                >
                  <q-list>
                    <q-item
                      clickable
                      v-close-popup
                      @click="onPrintReceipt(props.row)"
                    >
                      <q-item-section>
                        <q-item-label>Print Receipt</q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-list>
                </q-btn-dropdown>
                <!-- <q-btn
                  size="xs"
                  label="Print Receipt"
                  color="primary"
                  class="q-ml-sm"
                  v-if="props.row.statusId == 3 || props.row.statusId == 5"
                  @click="onPrintReceipt(props.row)"
                ></q-btn> -->
              </td>
            </tr>
          </template>
        </q-table>
        <q-dialog v-model="updateStatusDialog" position="top" persistent>
          <q-card square>
            <q-toolbar>
              <q-toolbar-title class="text-primary"
                >Update Grain Season</q-toolbar-title
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
              <q-form @submit="onUpdateGrainCycleStatus">
                <div class="row q-col-gutter-md">
                  <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                    <q-select
                      label="Status *"
                      square
                      outlined
                      dense
                      clearable
                      v-model="selectedCustomerGrainCycleStatus"
                      :options="customerGrainCycleStatuses"
                      option-label="name"
                      option-value="id"
                      map-options
                      emit-value
                      lazy-rules
                      :rules="[
                        (val) =>
                          (val !== null && val !== '') || 'Status is required',
                      ]"
                    ></q-select>
                  </div>

                  <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                    <q-input
                      label="Estimated Quantity (kg)*"
                      type="number"
                      step="0.01"
                      square
                      outlined
                      dense
                      v-model="updateStatusModel.estimatedQuantity"
                      lazy-rules
                      :rules="[
                        (val) =>
                          (val && val > 0) || 'Estimated Quantity is required',
                      ]"
                      v-if="selectedCustomerGrainCycleStatus != 5"
                    ></q-input>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                    <q-input
                      label="Estimated Price (NPR)*"
                      type="number"
                      step="0.01"
                      square
                      outlined
                      dense
                      v-model="updateStatusModel.estimatedCost"
                      lazy-rules
                      :rules="[
                        (val) =>
                          (val && val > 0) || 'Estimated Cost is required',
                      ]"
                      v-if="selectedCustomerGrainCycleStatus != 5"
                    ></q-input>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                    <q-input
                      label="Remarks"
                      square
                      dense
                      outlined
                      v-model="updateStatusModel.remarks"
                      type="textarea"
                    ></q-input>
                  </div>
                </div>
                <div class="row q-mt-md">
                  <q-btn label="Update" color="primary" type="submit"></q-btn>
                </div>
              </q-form>
            </q-card-section>
          </q-card>
        </q-dialog>
      </q-card-section>
    </q-card>
  </q-page>
</template>
<script>
import { defineComponent, ref, onMounted } from "vue";
import { handleError, dateFormat } from "boot/utility";
import { api, baseURL } from "boot/axios";
import { useRouter, useRoute } from "vue-router";
import { useQuasar } from "quasar";
export default defineComponent({
  setup() {
    const $q = useQuasar();
    const route = useRoute();
    const router = useRouter();
    let customer = ref({
      name: "",
      id: 0,
      email: "",
      phoneNumber: "",
      statusId: 0,
      isActive: false,
    });
    let updateStatusDialog = ref(false);
    let customerGrainCycles = ref([]);
    let selectedCustomerStatus = ref(null);
    let selectedCustomerGrainCycleStatus = ref(null);
    let customerStatuses = ref([]);
    let customerGrainCycleStatuses = ref([]);
    let selectedGrain = ref(null);
    let selectedGrainCycle = ref(null);
    let grains = ref([]);
    let grainCycles = ref([]);
    let customerGrainCycle = ref({
      estimatedQuantity: null,
      remarks: "",
      estimatedCost: null,
    });
    let updateStatusModel = ref({
      id: 0,
      statusId: 0,
      estimatedQuantity: 0,
      estimatedCost: 0,
      remarks: "",
      customerGrainCycleId: 0,
      grainCycleId: 0,
    });
    let showDialog = ref(false);
    let customerId = ref(null);
    const getCustomerById = async () => {
      try {
        const response = await api.get(`customer/${route.params.id}`);
        customer.value.id = response.data.id;
        customer.value.name = response.data.name;
        customer.value.email = response.data.email;
        customer.value.phoneNumber = response.data.phoneNumber;
        customer.value.isActive = response.data.isActive;
        selectedCustomerStatus.value = response.data.statusId;
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };

    const updateDialogFunction = (buyerGrainData) => {
      updateStatusDialog.value = true;
      updateStatusModel.value.id = buyerGrainData.id;
      updateStatusModel.value.estimatedQuantity = buyerGrainData.quantity;
      updateStatusModel.value.estimatedCost = buyerGrainData.price;
      selectedCustomerGrainCycleStatus.value = buyerGrainData.statusId;
      updateStatusModel.value.remarks = buyerGrainData.remarks;
      updateStatusModel.value.customerGrainCycleId =
        buyerGrainData.customerGrainCycleId;
      updateStatusModel.value.grainCycleId = buyerGrainData.grainCycleId;
    };

    const getStatuses = async () => {
      try {
        $q.loading.show();
        const response = await api.get("setting/statuses");
        customerGrainCycleStatuses.value = response.data;
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };

    const getGrains = async () => {
      try {
        $q.loading.show();
        const response = await api.get("setting/grains");
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
    const getGrainCycleByCustomerId = async () => {
      try {
        const response = await api.get(
          `customer/${route.params.id}/grain-cycle`
        );
        customerGrainCycles.value = response.data;
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };

    const onAddCustomerGrainCycle = async () => {
      try {
        let response = await api.post("customer-grain-cycle/insert", {
          customerId: parseInt(route.params.id),
          grainCycleId: selectedGrainCycle.value,
          estimatedQuantity: parseFloat(
            customerGrainCycle.value.estimatedQuantity
          ),
          statusId: selectedCustomerGrainCycleStatus.value,
          remarks: customerGrainCycle.value.remarks,
          estimatedCost: parseFloat(customerGrainCycle.value.estimatedCost),
        });
        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        showDialog.value = false;
        await getGrainCycleByCustomerId();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };

    const onUpdateGrainCycleStatus = async () => {
      try {
        let response = await api.post(
          "customer/customer-grain-cycle-status-update",
          {
            customerGrainCycleId: updateStatusModel.value.customerGrainCycleId,
            statusId: selectedCustomerGrainCycleStatus.value,
            remarks: updateStatusModel.value.remarks,
            quantity: parseFloat(updateStatusModel.value.estimatedQuantity),
            price: parseFloat(updateStatusModel.value.estimatedCost),
            grainCycleId: parseInt(updateStatusModel.value.grainCycleId),
          }
        );
        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        updateStatusDialog.value = false;
        await getGrainCycleByCustomerId();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    function onAdd() {
      showDialog.value = true;
      selectedGrain.value = null;
      selectedGrainCycle.value = null;
      customerGrainCycle.value.estimatedQuantity = null;
      selectedCustomerGrainCycleStatus.value = null;
      customerGrainCycle.value.remarks = "";
      customerGrainCycle.value.estimatedCost = null;
    }
    //TODO: Code Review
    const onPrintReceipt = async (grainCycle) => {
      try {
        $q.loading.show();
        // await api.get(
        //   `generate-sales-receipt/pdf/${grainCycle.id}/status/${grainCycle.statusId}`
        // );
        const url = `${baseURL}generate-sales-receipt/pdf/${grainCycle.id}/status/${grainCycle.statusId}`;
        window.open(url, "_blank").focus();
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const deleteCustomerGrainCycle = async (grainCycle) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the grain season ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("customer/delete-grain-cycle", {
              id: grainCycle.id,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getGrainCycleByCustomerId();
            $q.loading.hide();
          } catch (error) {
            $q.loading.hide();
            handleError(error);
          }
        });
      } catch (error) {
        $q.loading.hide();
        handleError(error);
      }
    };
    onMounted(async () => {
      try {
        $q.loading.show();
        customerId.value = route.params.id;
        await getCustomerById();
        await getStatuses();
        await getGrains();
        await getGrainCycleByCustomerId();
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    });
    return {
      customer,
      selectedCustomerStatus,
      customerStatuses,
      customerGrainCycleStatuses,
      selectedCustomerGrainCycleStatus,
      grains,
      selectedGrain,
      selectedGrainCycle,
      customerGrainCycles,
      getGrainsByGrainId,
      grainCycles,

      customerGrainCycle,
      initialPagination: {
        rowsPerPage: 5,
      },
      showDialog,
      onAddCustomerGrainCycle,
      onAdd,
      updateStatusDialog,
      updateStatusModel,
      updateDialogFunction,
      onUpdateGrainCycleStatus,
      dateFormat,
      onPrintReceipt,
      customerId,
      deleteCustomerGrainCycle,
    };
  },
});
</script>
