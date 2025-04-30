<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-toolbar>
        <q-toolbar-title>New Buyer</q-toolbar-title>
        <q-btn
          unelevated
          size="md"
          color="primary"
          label="List"
          to="/customer/list"
          icon="list"
        />
      </q-toolbar>
      <q-card-section style="padding-top: 0px">
        <q-form>
          <q-stepper
            v-model="step"
            ref="stepper"
            color="primary"
            animated
            :done="done1"
            style="box-shadow: none; border: 1px solid rgba(0, 0, 0, 0.12)"
          >
            <q-step :name="1" title="Buyer">
              <div class="row q-col-gutter-md">
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-input
                    label="Name *"
                    square
                    outlined
                    dense
                    v-model="customer.name"
                    lazy-rules
                    :rules="[
                      (val) => (val && val.length > 0) || 'Name is required',
                    ]"
                  ></q-input>
                </div>
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-input
                    label="Email *"
                    square
                    outlined
                    dense
                    v-model="customer.email"
                    lazy-rules
                    :rules="[
                      (val) => (val && val.length > 0) || 'Email is required',
                    ]"
                  ></q-input>
                </div>
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-input
                    label="Phone Number *"
                    square
                    outlined
                    dense
                    v-model="customer.phoneNumber"
                    mask="##########"
                    lazy-rules
                    :rules="[
                      (val) =>
                        (val && val.length > 0) || 'Phone Number is required',
                    ]"
                  ></q-input>
                </div>
              </div>
              <div class="row">
                <q-btn
                  label="Submit"
                  class="q-mt-md"
                  color="primary"
                  @click="submitCustomer"
                ></q-btn>
              </div>
            </q-step>
            <q-step :name="2" title="Buyer Contact Details" :done="done2">
              <div class="row q-mb-md">
                <q-btn
                  square
                  outline
                  size="sm"
                  color="secondary"
                  icon="mdi-plus"
                  class="q-ml-auto"
                  @click="onAddOption"
                  >Add Contact Details</q-btn
                >
              </div>
              <div
                class="row q-col-gutter-md"
                v-for="item in customerContactDetails"
                :key="item"
              >
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-input
                    label="Name *"
                    square
                    outlined
                    dense
                    v-model="item.name"
                    lazy-rules
                    :rules="[
                      (val) => (val && val.length > 0) || 'Name is required',
                    ]"
                  ></q-input>
                </div>
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-input
                    label="Email *"
                    square
                    outlined
                    dense
                    v-model="item.email"
                    lazy-rules
                    :rules="[
                      (val) => (val && val.length > 0) || 'Email is required',
                    ]"
                  ></q-input>
                </div>
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-input
                    label="Phone Number *"
                    square
                    outlined
                    dense
                    mask="##########"
                    v-model="item.phoneNumber"
                    lazy-rules
                    :rules="[
                      (val) =>
                        (val && val.length > 0) || 'Phone Number is required',
                    ]"
                  ></q-input>
                </div>
              </div>
              <div class="row" v-if="customerContactDetails.length > 0">
                <q-btn
                  label="Submit"
                  class="q-mt-md"
                  color="primary"
                  @click="submitCustomerContactDetails"
                ></q-btn>
              </div>
            </q-step>
            <q-step :name="3" title="Customer Grain Season" :done="done3">
              <div class="row q-col-gutter-md">
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-select
                    square
                    dense
                    outlined
                    v-model="selectedGrain"
                    label="Grain *"
                    clearable
                    :options="grains"
                    option-label="name"
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
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-select
                    label="Grain Season *"
                    square
                    outlined
                    dense
                    v-model="selectedGrainCycle"
                    :options="grainCycles"
                    option-label="name"
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
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
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
                        (val && val.length > 0) ||
                        'Estimated Quantity is required',
                    ]"
                  ></q-input>
                </div>
              </div>
              <div class="row q-col-gutter-md">
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-input
                    label="Estimated Price (NPR)*"
                    type="number"
                    step="0.01"
                    square
                    outlined
                    dense
                    v-model="customerGrainCycle.estimatedPrice"
                    lazy-rules
                    :rules="[
                      (val) =>
                        (val && val.length > 0) ||
                        'Estimated Price is required',
                    ]"
                  ></q-input>
                </div>
                <div class="col-md-4 col-sm-4 col-lg-4 col-xs-12">
                  <q-input
                    label="Remarks"
                    square
                    dense
                    outlined
                    v-model="customerGrainCycleStatus.remarks"
                  ></q-input>
                </div>
              </div>
              <div class="row">
                <q-btn
                  label="Submit"
                  class="q-mt-md"
                  color="primary"
                  @click="submitCustomerGrainCycle"
                ></q-btn>
              </div>
            </q-step>
          </q-stepper>
        </q-form>
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
    const $q = useQuasar();
    let step = ref(1);
    let done1 = ref(false);
    let done2 = ref(false);
    let done3 = ref(false);
    const router = useRouter();
    let customer = ref({
      name: "",
      email: "",
      phoneNumber: "",
    });
    let customerContactDetail = ref({
      name: "",
      phoneNumber: "",
      email: "",
    });
    let customerGrainCycle = ref({
      grainCycleId: 0,
      estimatedQuantity: null,
      estimatedPrice: null,
    });
    let customerGrainCycleStatus = ref({
      customerGrainCycleId: 0,
      StatusId: 0,
      remarks: "",
    });
    let customerId = ref(null);
    let grains = ref([]);
    let grainCycles = ref([]);
    let statuses = ref([]);
    let selectedGrain = ref(null);
    let selectedGrainCycle = ref(null);
    let selectedStatus = ref(null);
    let selectedCustomerStatus = ref(null);
    let customerContactDetails = ref([]);
    let customerStatuses = ref([]);
    let customerGrainCycleStatuses = ref([]);
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
    const submitCustomer = async () => {
      try {
        $q.loading.show();
        const response = await api.post("customer/insert", {
          name: customer.value.name,
          email: customer.value.email,
          phoneNumber: customer.value.phoneNumber,
          customerStatusId: selectedCustomerStatus.value,
        });
        customerId.value = response.data.id;
        done1.value = true;
        step.value = 2;
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const submitCustomerContactDetails = async () => {
      try {
        $q.loading.show();
        const response = await api.post(
          "customer-contact-detail/insert",
          customerContactDetails.value
        );
        done2.value = true;
        step.value = 3;
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const getStatuses = async () => {
      try {
        $q.loading.show();
        const response = await api.get("setting/statuses");
        statuses.value = response.data;
        statuses.value.forEach(function (item) {
          if (item.isCustomerStatus == true) {
            customerStatuses.value.push(item);
          }
          if (item.isCustomerGrainCycleStatus == true) {
            customerGrainCycleStatuses.value.push(item);
          }
        });
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
    function onAddOption() {
      customerContactDetails.value.push({
        name: "",
        phoneNumber: "",
        email: "",
        customerId: customerId.value,
      });
    }
    const submitCustomerGrainCycle = async () => {
      try {
        $q.loading.show();
        const response = await api.post("customer-grain-cycle/insert", {
          customerId: customerId.value,
          grainCycleId: selectedGrainCycle.value,
          estimatedQuantity: customerGrainCycle.value.estimatedQuantity,
          statusId: selectedStatus.value,
          remarks: customerGrainCycleStatus.value.remarks,
          estimatedCost: customerGrainCycle.value.estimatedPrice,
        });
        $q.loading.hide();
        router.push("/customer/list");
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    onMounted(async () => {
      try {
        $q.loading.show();
        await getGrains();
        await getStatuses();
        $q.loading.hide();
      } catch (ex) {
        handleError(ex);
      }
    });
    return {
      step,
      done1,
      done2,
      done3,
      customer,
      customerContactDetail,
      customerGrainCycle,
      statuses,
      customerGrainCycleStatus,
      grains,
      grainCycles,
      getGrains,
      selectedGrain,
      selectedGrainCycle,
      submitCustomer,
      submitCustomerContactDetails,
      getStatuses,
      selectedStatus,
      getGrainsByGrainId,
      customerStatuses,
      customerGrainCycleStatuses,
      selectedCustomerStatus,
      customerContactDetails,
      onAddOption,
      customerId,
      submitCustomerGrainCycle,
    };
  },
});
</script>
