<template>
  <q-page>
    <q-card flat class="no-border-radius">
    
      
        <q-toolbar >
        <q-toolbar-title >Ethnicities</q-toolbar-title>
        <q-input
              
              dense
              debounce="300"
              v-model="filter"
              placeholder="Search"
              outlined
             class="q-mr-md"
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
          @click="openNewethnicityDialog"
          icon="add"
          
        />
      </q-toolbar>
      <q-card-section style="padding-top:0px;">
        <q-table
          square
          :rows="ethnicities"
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
              <th class="text-left">Order No.</th>
              <th class="text-center">Active</th>
              <th class="text-right">Actions</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">{{ props.rowIndex + 1 }}</td>
              <td class="text-left">{{ props.row.name }}</td>
              <td class="text-left">{{ props.row.nepaliName }}</td>
              <td class="text-left">{{ props.row.code }}</td>
              <td class="text-left">{{ props.row.orderNo }}</td>
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
                  size="sm"
                  class="active-icon-q"
                  v-else
                />
              </td>
              <td class="text-right">
                <q-btn
                  unelevated
                  round
                  size="xs"
                  dense
                  outline
                  color="primary"
                  icon="mdi-pencil"
                  @click="openethnicityEditDialog(props.row)"
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
                  @click="deleteethnicity(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right>
           
          </template>
        </q-table>

        <q-dialog v-model="ethnicityDialog" persistent position="top" >
          <q-card class="q-mt-lg"
            square
           
          >
            <q-toolbar>
              <q-toolbar-title  >{{ dialogName }}</q-toolbar-title>
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
                  v-model="ethnicity.name"
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
                  v-model="ethnicity.nepaliName"
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
                  v-model="ethnicity.code"
                  outlined
                  lazy-rules
                  label="Code "
                  dense
                >
                </q-input>
                <q-input
                  v-model="ethnicity.orderNo"
                  outlined
                  class="q-mt-md"
                  lazy-rules
                  type="number"
                  label="Order No. "
                  dense
                >
                </q-input>
                <q-checkbox
                  class="q-mt-md"
                  v-if="ethnicity.id"
                  v-model="ethnicity.isActive"
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
    let ethnicities = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let ethnicity = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      orderNo: null,
      isActive: false,
    });
    let ethnicityDialog = ref(false);
    let dialogName = ref(null);
    let ethnicityNewDialog = ref(false);
    let ethnicityEditDialog = ref(false);
    const getethnicities = async () => {
      try {
        const response = await api.get("setting/ethnicities");
        ethnicities.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewethnicityDialog = () => {
      $q.loading.show({});
      ethnicityNewDialog.value = true;
      ethnicity.value.id = 0;
      ethnicity.value.name = null;
      ethnicity.value.nepaliName = null;
      ethnicity.value.code = null;
      ethnicity.value.orderNo = null;
      dialogName.value = "New ethnicity";
      ethnicityDialog.value = true;
      $q.loading.hide();
    };
    const openethnicityEditDialog = (selectedethnicity) => {
      $q.loading.show({});
      ethnicity.value.id = selectedethnicity.id;
      ethnicity.value.name = selectedethnicity.name;
      ethnicity.value.isActive = selectedethnicity.isActive;
      ethnicity.value.nepaliName = selectedethnicity.nepaliName;
      ethnicity.value.code = selectedethnicity.code;
      ethnicity.value.orderNo = selectedethnicity.orderNo;

      dialogName.value = "Update Ethnicity";
      ethnicityDialog.value = true;
      $q.loading.hide();
    };
    const deleteethnicity = async (ethnicity) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the ethnicity ${ethnicity.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/ethnicity-delete", {
              id: ethnicity.id,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getethnicities();
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
      if (ethnicity.value.orderNo == "") {
        ethnicity.value.orderNo = null;
      }
      try {
        if (ethnicity.value.id === 0) {
          response = await api.post(
            "setting/ethnicity/insert",
            ethnicity.value
          );
        } else {
          response = await api.post(
            "setting/ethnicity/update",
            ethnicity.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getethnicities();
        ethnicityDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getethnicities();
      $q.loading.hide({});
    });
    return {
      ethnicities,
      ethnicity,
      ethnicityNewDialog,
      ethnicityEditDialog,
      openNewethnicityDialog,
      openethnicityEditDialog,
      deleteethnicity,
      tableLoading,
      onSubmit,
      dialogName,
      ethnicityDialog,
      initialPagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
    };
  },
});
</script>
