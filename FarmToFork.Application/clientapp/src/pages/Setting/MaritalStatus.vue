<template>
  <q-page>
    <q-card flat class="no-border-radius">
  
        <q-toolbar>
        <q-toolbar-title>Marital Status</q-toolbar-title>
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
          @click="openNewMaritalStatusDialog"
          icon="add"
             
        />
      </q-toolbar>
      <q-card-section style="padding-top:0px;">
        <q-table
          square
          :rows="maritalStatuses"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
                <th class="text-left " style="padding-left:10px">S.No</th>
              <th class="text-left">Name</th>
              <th class="text-left">Nepali Name</th>
              <th class="text-left">Order No</th>
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
              <td class="text-left">{{ props.row.orderId }}</td>
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
                  dense
                  outline
                  size="xs"
                  color="primary"
                  icon="mdi-pencil"
                  @click="openMaritalStatusEditDialog(props.row)"
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
                  @click="deleteMaritalStatus(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right>
          
          </template>
        </q-table>

        <q-dialog v-model="maritalStatusDialog" persistent position="top">
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
                  v-model="maritalStatus.name"
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
                  v-model="maritalStatus.nepaliName"
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
                  v-model="maritalStatus.orderId"
                  outlined
                  type="number"
                  lazy-rules
                  class="q-mt-xs"
                
                  label="Order No. "
                  dense
                >
                </q-input>

                <q-checkbox
                  class="q-mt-md"
                  v-if="maritalStatus.id"
                  v-model="maritalStatus.isActive"
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
    let maritalStatuses = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let maritalStatus = ref({
      id: 0,
      name: null,
      nepaliName: null,
      orderId: null,
      isActive: false,
    });
    let maritalStatusDialog = ref(false);
    let dialogName = ref(null);
    const getMaritalStatuses = async () => {
      try {
        const response = await api.get("setting/marital-statuses");
        maritalStatuses.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewMaritalStatusDialog = () => {
      $q.loading.show({});
      maritalStatus.value.id = 0;
      maritalStatus.value.name = null;
      maritalStatus.value.nepaliName = null;
      maritalStatus.value.orderId = null;
      dialogName.value = "New Marital Status";
      maritalStatusDialog.value = true;
      $q.loading.hide();
    };
    const openMaritalStatusEditDialog = (selectedMaritalStatus) => {
      $q.loading.show({});
      maritalStatus.value.id = selectedMaritalStatus.id;
      maritalStatus.value.name = selectedMaritalStatus.name;
      maritalStatus.value.isActive = selectedMaritalStatus.isActive;
      maritalStatus.value.nepaliName = selectedMaritalStatus.nepaliName;
      maritalStatus.value.orderId = selectedMaritalStatus.orderId;
      dialogName.value = "Update Marital Status";
      maritalStatusDialog.value = true;
      $q.loading.hide();
    };
    const deleteMaritalStatus = async (maritalStatus) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the Marital Status ${maritalStatus.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/marital-status/delete", {
              id: maritalStatus.id,
              name: maritalStatus.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getMaritalStatuses();
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
      if (maritalStatus.value.orderId == "") {
        maritalStatus.value.orderId = null;
      }
      let response = "";
      try {
        if (maritalStatus.value.id === 0) {
          response = await api.post(
            "setting/marital-status/insert",
            maritalStatus.value
          );
        } else {
          response = await api.post(
            "setting/marital-status/update",
            maritalStatus.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getMaritalStatuses();
        maritalStatusDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getMaritalStatuses();
      $q.loading.hide({});
    });
    return {
      maritalStatuses,
      maritalStatus,
      openNewMaritalStatusDialog,
      openMaritalStatusEditDialog,
      deleteMaritalStatus,
      tableLoading,
      onSubmit,
      dialogName,
      maritalStatusDialog,
      initialPagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
    };
  },
});
</script>
<style>
.q-table__top.relative-position.row.items-center {
    display: none;
}
</style>