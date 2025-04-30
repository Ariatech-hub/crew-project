<template>
  <q-page>
    <q-card>
      <q-card-section style="padding-top: 0px">
        <q-toolbar style="padding: 0px 0px">
          <q-toolbar-title>Grain Cycle</q-toolbar-title>
          <q-input
            dense
            debounce="300"
            v-model="filter"
            placeholder="Search"
            class="q-mr-md"
            outlined=""
          >
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
          <q-btn
            unelevated
            size="md"
            color="light-green-8"
            label="New"
            to="/grain-cycle/new"
            icon="add"
          />
        </q-toolbar>

        <q-table
          square
          :rows="grainCycles"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left">S.No</th>
              <th class="text-left">Name</th>
              <th class="text-left">Nepali Name</th>
              <th class="text-left">Grain</th>
              <th class="text-left">Year</th>
              <th class="text-left">From Month</th>
              <th class="text-left">To Month</th>
              <th class="text-center">Active</th>
              <th class="text-right">Actions</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">{{ props.rowIndex + 1 }}</td>
              <td class="text-left">{{ props.row.name }}</td>
              <td class="text-left">{{ props.row.nepaliName }}</td>
              <td class="text-left">{{ props.row.grainName }}</td>
              <td class="text-left">{{ props.row.yearName }}</td>
              <td class="text-left">{{ props.row.fromMonthName }}</td>
              <td class="text-left">{{ props.row.toMonthName }}</td>

              <td class="text-center">
                <!-- <q-icon
                  name="check"
                  color="positive"
                  size="sm"
                  v-if="props.row.isActive"
                />
                <q-icon
                  name="highlight_off"
                  color="negative"
                  size="sm"
                  v-else
                /> -->
                <q-toggle
                  v-model="props.row.isActive"
                  color="primary"
                  size="xs"
                  @update:model-value="onToggleClicked(props.row)"
                ></q-toggle>
              </td>
              <td class="text-right">
                <q-btn
                  unelevated
                  round
                  dense
                  outline
                  size="xs"
                  color="primary"
                  icon="mdi-pencil"
                  :to="`/grain-cycle/${props.row.id}/update/`"
                >
                  <q-tooltip> Edit </q-tooltip>
                </q-btn>
                <q-btn
                  unelevated
                  round
                  dense
                  outline
                  class="q-ml-xs"
                  size="xs"
                  color="negative"
                  icon="mdi-delete"
                  @click="deleteGrainCycle(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right> </template>
        </q-table>

        <q-dialog v-model="grainCycleDialog" persistent position="top">
          <q-card square>
            <q-toolbar style="padding: 0 15px">
              <q-toolbar-title class="text-primary">{{
                dialogName
              }}</q-toolbar-title>
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
              <q-form @submit="onSubmit">
                <q-input
                  v-model="grainCycle.name"
                  outlined
                  lazy-rules
                  square
                  label="Name *"
                  dense
                  :rules="[
                    (val) => (val && val.length > 0) || 'Name is required',
                  ]"
                >
                </q-input>
                <q-input
                  v-model="grainCycle.nepaliName"
                  outlined
                  lazy-rules
                  square
                  label="Nepali Name *"
                  dense
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Nepali Name is required',
                  ]"
                >
                </q-input>

                <q-input
                  v-model="grainCycle.orderNo"
                  outlined
                  lazy-rules
                  square
                  label="Order Number "
                  dense
                >
                </q-input>
                <q-select
                  square
                  dense
                  outlined
                  multiple
                  use-chips
                  v-model="grainCycle.monthId"
                  :options="months"
                  option-label="name"
                  option-value="id"
                  label="From Month"
                  emit-value
                  map-options
                  clearable
                  :rules="[
                    (val) =>
                      (val !== null && val !== '') || 'Please Select Month',
                  ]"
                >
                </q-select>

                <q-select
                  square
                  dense
                  outlined
                  multiple
                  use-chips
                  v-model="grainCycle.monthId"
                  :options="months"
                  option-label="name"
                  option-value="id"
                  label="To Month"
                  emit-value
                  map-options
                  clearable
                  :rules="[
                    (val) =>
                      (val !== null && val !== '') || 'Please Select Month',
                  ]"
                >
                </q-select>

                <q-checkbox
                  class="q-mt-md"
                  v-if="grainCycle.id"
                  v-model="grainCycle.isActive"
                  outlined
                  lazy-rules
                  square
                  label="Is Active"
                  dense
                ></q-checkbox>
                <div class="q-mt-md">
                  <q-btn color="primary" type="submit" label="Submit" />
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
import { defineComponent, onMounted, ref } from "vue";
import { api } from "boot/axios";
import { handleError } from "boot/utility";
import { useQuasar } from "quasar";

export default defineComponent({
  setup() {
    let grainCycles = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let grainCycle = ref({
      id: 0,
      name: null,
      nepaliName: null,
      isActive: false,
      monthId: 0,
      fromMonthName: null,
      toMonthName: null,
      yearName: null,
      orderNo: 0,
    });
    let grainCycleDialog = ref(false);
    let dialogName = ref(null);
    //TODO Name should be camel case
    // TODO: Code Review
    const getGrainCycles = async () => {
      try {
        const response = await api.get("grain-cycles");
        grainCycles.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const getMonths = async () => {
      try {
        const response = await api.get("general/months");
        months = response.data;
      } catch {
        $q.loading.hide({});
        handleError(error);
      }
    };

    const deleteGrainCycle = async (grainCycle) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the grain cycle ${grainCycle.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("grain-cycle/delete", {
              id: grainCycle.id,
              name: grainCycle.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getGrainCycles();
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

    const onSubmit = async () => {
      $q.loading.show({});
      let response = "";
      if (grainCycle.value.orderNo == "") {
        grainCycle.value.orderNo = null;
      }
      try {
        if (grainCycle.value.id === 0) {
          response = await api.post("grain-cycle/insert", grainCycle.value);
        } else {
          response = await api.post("grain-cycle/update", grainCycle.value);
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getgrains();
        grainCycleDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };
    const onToggleClicked = async (grainCycle) => {
      $q.loading.show({});
      try {
        let response = await api.post("grain-cycle/update-status", {
          id: grainCycle.id,
          name: grainCycle.name,
          yearId: grainCycle.yearId,
          fromMonthId: grainCycle.fromMonthId,
          toMonthId: grainCycle.toMonthId,
          grainId: grainCycle.grainId,
        });
        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getGrainCycles();
      } catch (ex) {
        handleError(ex);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getGrainCycles();
      $q.loading.hide({});
    });
    return {
      grainCycles,
      grainCycle,
      deleteGrainCycle,
      tableLoading,
      onSubmit,
      dialogName,
      grainCycleDialog,
      monthId: null,
      months: [],
      initialPagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
      onToggleClicked,
    };
  },
});
</script>
<style>
.q-table__top.relative-position.row.items-center {
  display: none;
}
</style>
