<template>
  <q-page>
    <q-card flat class="no-border-radius">
    

      
        <q-toolbar >
        <q-toolbar-title >IncomeSources</q-toolbar-title>
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
          unelevated
          size="md"
          
          color="light-green-8"
          label="New"
          @click="openNewIncomeSourceDialog"
          icon="add"
          
        />
      </q-toolbar>
      <q-card-section style="padding-top:0px">
        <q-table
          square
          :rows="incomeSources"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left" style="padding-left:10px">S.No</th>
              <th class="text-left">Name</th>
              <th class="text-left">Nepali Name</th>
              <th class="text-left">Code</th>
              <th class="text-center">Order No.</th>
              <th class="text-center">Active</th>
              <th class="text-right">Actions</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">{{ props.rowIndex + 1 }}</td>
              <td
                style="
                  max-width: 210px;
                  min-width: 50px;
                  overflow: hidden;
                  text-overflow: ellipsis;
                "
                class="text-left"
              >
                {{ props.row.name }}
              </td>
              <td
                style="
                  max-width: 210px;
                  min-width: 50px;
                  overflow: hidden;
                  text-overflow: ellipsis;
                "
                class="text-left"
              >
                {{ props.row.nepaliName }}
              </td>
              <td class="text-left">{{ props.row.code }}</td>
              <td class="text-center">{{ props.row.orderNo }}</td>
              <td class="text-center">
                <q-icon
                  name="check"
                  color="positive"
                  class="active-icon-q"
                  size="sm"
                  v-if="props.row.isActive"
                />
                <q-icon
                  name="highlight_off"
                  color="negative"
                  class="active-icon-q"
                  size="sm"
                  v-else
                />
              </td>
              <td class="text-right">
                <q-btn
                  unelevated
                  round
                  outline
                  dense
                  size="xs"
                  color="primary"
                  icon="mdi-pencil"
                  @click="openIncomeSourceEditDialog(props.row)"
                >
                  <q-tooltip> Edit </q-tooltip>
                </q-btn>
                <q-btn
                  unelevated
                  round
                  outline
                  dense
                  class="q-ml-xs"
                  size="xs"
                  color="negative"
                  icon="mdi-delete"
                  @click="deleteIncomeSource(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
         
        </q-table>

        <q-dialog v-model="incomeSourceDialog" persistent position="top">
          <q-card class="q-mt-lg"
            square
          
          >
            <q-toolbar>
              <q-toolbar-title >{{ dialogName }}</q-toolbar-title>
              <q-btn
                color="primary"
                flat
                round
                dense
                icon="close"
                v-close-popup
              />
            </q-toolbar>
            <q-separator spaced="" />
            <q-card-section class="form-card" style="padding-top:15px;">
              <q-form @submit="onSubmit">
                <q-input
                  v-model="incomeSource.name"
                  outlined
                  lazy-rules
                  label="Name *"
                  dense
                  :rules="[
                    (val) => (val && val.length > 0) || 'Name is required',
                  ]"
                >
                </q-input>
                <q-input
                  v-model="incomeSource.nepaliName"
                  outlined
                  lazy-rules
                  label="Nepali Name *"
                  dense
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Nepali Name is required',
                  ]"
                >
                </q-input>
                <q-input
                  v-model="incomeSource.code"
                  outlined
                  lazy-rules
                  label="Code "
                  dense
                >
                </q-input>

                <q-input
                  v-model="incomeSource.orderNo"
                  outlined
                  type="number"
                  class="q-mt-md"
                  lazy-rules
                  label="Order No. "
                  dense
                >
                </q-input>

                <q-checkbox
                  class="q-mt-md"
                  v-if="incomeSource.id"
                  v-model="incomeSource.isActive"
                  outlined
                  lazy-rules
                  square
                  label="Is Active"
                  dense
                ></q-checkbox>
                <q-separator spaced="" />
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
    let incomeSources = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let incomeSource = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      orderNo: null,
      isActive: false,
    });
    let incomeSourceDialog = ref(false);
    let dialogName = ref(null);
    const getIncomeSources = async () => {
      try {
        const response = await api.get("setting/income-source");
        incomeSources.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewIncomeSourceDialog = () => {
      $q.loading.show({});
      incomeSource.value.id = 0;
      incomeSource.value.name = null;
      incomeSource.value.nepaliName = null;
      incomeSource.value.code = null;
      incomeSource.value.orderNo = null;
      dialogName.value = "New IncomeSource";
      incomeSourceDialog.value = true;
      $q.loading.hide();
    };
    const openIncomeSourceEditDialog = (selectedIncomeSource) => {
      $q.loading.show({});
      incomeSource.value.id = selectedIncomeSource.id;
      incomeSource.value.name = selectedIncomeSource.name;
      incomeSource.value.isActive = selectedIncomeSource.isActive;
      incomeSource.value.nepaliName = selectedIncomeSource.nepaliName;
      incomeSource.value.code = selectedIncomeSource.code;
      incomeSource.value.orderNo = selectedIncomeSource.orderNo;
      dialogName.value = "Update IncomeSource";
      incomeSourceDialog.value = true;
      $q.loading.hide();
    };
    const deleteIncomeSource = async (incomeSource) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the IncomeSource ${incomeSource.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/income-source/delete", {
              id: incomeSource.id,
              name: incomeSource.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getIncomeSources();
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
      if (incomeSource.value.orderNo == "") {
        incomeSource.value.orderNo = null;
      }
      let response = "";
      try {
        if (incomeSource.value.id === 0) {
          response = await api.post(
            "setting/income-source/insert",
            incomeSource.value
          );
        } else {
          response = await api.post(
            "setting/income-source/update",
            incomeSource.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getIncomeSources();
        incomeSourceDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getIncomeSources();
      $q.loading.hide({});
    });
    return {
      incomeSources,
      incomeSource,
      openNewIncomeSourceDialog,
      openIncomeSourceEditDialog,
      deleteIncomeSource,
      tableLoading,
      onSubmit,
      dialogName,
      incomeSourceDialog,
      initialPagination: {
        rowsPerPage: 15,
      },
      filter: ref(""),
    };
  },
});
</script>
