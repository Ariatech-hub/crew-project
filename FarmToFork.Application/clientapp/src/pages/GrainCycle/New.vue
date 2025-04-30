<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-toolbar>
        <q-toolbar-title>New Grain Season</q-toolbar-title>
        <q-btn
          unelevated
          size="md"
          color="light-green-8"
          label="List"
          to="/grain-cycle/list"
          icon="list"
        />
      </q-toolbar>
      <q-card-section style="padding-top: 0px">
        <div class="bg-white q-pa-md">
          <q-form @submit="onSubmit">
            <div class="row q-col-gutter-lg">
              <div class="col-md-4 col-xs-12">
                <q-input
                  outlined
                  v-model="grainCycle.name"
                  label="Name *"
                  lazy-rules
                  :rules="[
                    (val) => (val && val.length > 0) || 'Name is required',
                  ]"
                />
              </div>
              <div class="col-md-4">
                <q-input
                  v-model="grainCycle.nepaliName"
                  outlined
                  label="Nepali Name *"
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Nepali Name is required',
                  ]"
                >
                </q-input>
              </div>
              <div class="col-md-4 col-xs-12">
                <q-select
                  outlined
                  v-model="grainCycle.grainId"
                  label="Grain *"
                  clearable
                  input-debounce="0"
                  :options="grains"
                  option-label="name"
                  emit-value
                  map-options
                  @clear="onCleargrain"
                  behavior="menu"
                  :loading="grainLoading"
                  option-value="id"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val !== null && val !== '') || 'Grain  is required',
                  ]"
                />
              </div>
            </div>

            <div class="row q-col-gutter-lg">
              <div class="col-md-4 col-xs-6">
                <q-select
                  outlined
                  v-model="grainCycle.yearId"
                  label="Year *"
                  clearable
                  input-debounce="0"
                  :options="years"
                  option-label="name"
                  emit-value
                  map-options
                  behavior="menu"
                  :loading="yearLoading"
                  option-value="id"
                  lazy-rules
                  :rules="[
                    (val) => (val !== null && val !== '') || 'Year is required',
                  ]"
                />
              </div>
              <div class="col-md-4 col-xs-12">
                <q-select
                  outlined
                  v-model="grainCycle.fromMonthId"
                  label="From Month *"
                  clearable
                  input-debounce="0"
                  :options="months"
                  option-label="name"
                  emit-value
                  map-options
                  behavior="menu"
                  :loading="monthLoading"
                  option-value="id"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val !== null && val !== '') || 'From Month is required',
                  ]"
                />
              </div>

              <div class="col-md-4 col-xs-12">
                <q-select
                  outlined
                  v-model="grainCycle.toMonthId"
                  label="To Month *"
                  clearable
                  input-debounce="0"
                  :options="months"
                  option-label="name"
                  emit-value
                  map-options
                  behavior="menu"
                  :loading="monthLoading"
                  option-value="id"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val !== null && val !== '') || 'To Month is required',
                  ]"
                />
              </div>
            </div>

            <div class="row q-col-gutter-md"></div>

            <div class="q-mt-md">
              <q-btn color="primary" type="submit" label="Submit" />
            </div>
          </q-form>
        </div>
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
  name: "grainCycleNew",
  setup() {
    const router = useRouter();
    const $q = useQuasar();
    const route = useRoute();
    let grains = ref([]);
    let grainLoading = ref(false);
    let yearLoading = ref(false);
    let monthLoading = ref(false);
    let months = ref([]);
    let years = ref([]);
    let grainCycle = ref({
      id: route.params.id,
      name: "",
      grainId: null,
      yearId: null,
      nepaliName: null,
      toMonthId: null,
      fromMonthId: null,
      code: null,
      isActive: false,
      orderNo: null,
    });

    const onSubmit = async () => {
      try {
        //grainCycle.value.id = parseInt(grainCycle.value.id);
        const response = await api.post("grain-cycle/insert", grainCycle.value);

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        router.push("/grain-cycle/list");
      } catch (ex) {
        handleError(ex);
      }
    };
    const getgrains = async () => {
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
    const getMonths = async () => {
      try {
        $q.loading.show();
        const response = await api.get("general/months");
        months.value = response.data;
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };

    const getYears = async () => {
      try {
        $q.loading.show();
        const response = await api.get("setting/years");
        years.value = response.data;
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const onCleargrain = () => {
      grainCycle.value.grainId = null;
    };

    // const getgrainCycleById = async () => {
    //   try {
    //     const response = await api.get(
    //       `grain-cycle/get-by-id?id=${route.params.id}`
    //     );
    //     grainCycle.value.grainId = response.data.grainId;
    //     await getGrains(response.data.grainId);
    //     grainCycle.value = response.data;
    //   } catch (ex) {
    //     handleError(ex);
    //   }
    // };

    onMounted(async () => {
      try {
        $q.loading.show();
        //await getgrainCycleById();
        await getgrains();
        await getMonths();
        await getYears();

        $q.loading.hide();
      } catch (ex) {
        handleError(ex);
      }
    });
    return {
      grainCycle,
      onSubmit,
      grains,
      years,
      grainLoading,
      monthLoading,
      yearLoading,
      onCleargrain,
      months,
    };
  },
});
</script>
