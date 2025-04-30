<template>
  <q-page>
    <q-card flat class="no-border-radius">
     
        <q-toolbar >
          <q-toolbar-title 
            >Labour Divisions</q-toolbar-title
          >
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
            @click="openNewLabourDivisionDialog"
            icon="add"
          />
        </q-toolbar>
        <q-card-section style="padding-top:0px;">
        <q-table
          square
          :rows="labourDivisions"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left " style="padding-left:10px;">S.No</th>
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
              <td class="text-center">{{ props.row.orderId }}</td>
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
                  @click="openLabourDivisionEditDialog(props.row)"
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
                  @click="deleteLabourDivision(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right> </template>
        </q-table>

        <q-dialog v-model="labourDivisionDialog" persistent position="top" >
          <q-card class="q-mt-lg" square>
            <q-toolbar>
              <q-toolbar-title >{{
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
            <q-separator spaced="" />
            <q-card-section class="form-card" style="padding-top:15px;">
              <q-form @submit="onSubmit">
                <q-input
                  v-model="labourDivision.name"
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
                  v-model="labourDivision.nepaliName"
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
                  v-model="labourDivision.code"
                  outlined
                  lazy-rules
                  label="Code "
                  dense
                >
                </q-input>

                <q-input
                  v-model="labourDivision.orderId"
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
                  v-if="labourDivision.id"
                  v-model="labourDivision.isActive"
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
    let labourDivisions = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let labourDivision = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      orderId: null,
      isActive: false,
    });
    let labourDivisionDialog = ref(false);
    let dialogName = ref(null);
    const getLabourDivisions = async () => {
      try {
        const response = await api.get("setting/labour-divisions");
        labourDivisions.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewLabourDivisionDialog = () => {
      $q.loading.show({});
      labourDivision.value.id = 0;
      labourDivision.value.name = null;
      labourDivision.value.nepaliName = null;
      labourDivision.value.code = null;
      labourDivision.value.orderId = null;
      dialogName.value = "New Labour Division";
      labourDivisionDialog.value = true;
      $q.loading.hide();
    };
    const openLabourDivisionEditDialog = (selectedLabourDivision) => {
      $q.loading.show({});
      labourDivision.value.id = selectedLabourDivision.id;
      labourDivision.value.name = selectedLabourDivision.name;
      labourDivision.value.isActive = selectedLabourDivision.isActive;
      labourDivision.value.nepaliName = selectedLabourDivision.nepaliName;
      labourDivision.value.code = selectedLabourDivision.code;
      labourDivision.value.orderId = selectedLabourDivision.orderId;
      dialogName.value = "Update Labour Division";
      labourDivisionDialog.value = true;
      $q.loading.hide();
    };
    const deleteLabourDivision = async (labourDivision) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the Labour Division ${labourDivision.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/labour-division/delete", {
              id: labourDivision.id,
              name: labourDivision.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getLabourDivisions();
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
      if (labourDivision.value.orderId == "") {
        labourDivision.value.orderId = null;
      }

      let response = "";
      try {
        if (labourDivision.value.id === 0) {
          response = await api.post(
            "setting/labour-division/insert",
            labourDivision.value
          );
        } else {
          response = await api.post(
            "setting/labour-division/update",
            labourDivision.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getLabourDivisions();
        labourDivisionDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getLabourDivisions();
      $q.loading.hide({});
    });
    return {
      labourDivisions,
      labourDivision,
      openNewLabourDivisionDialog,
      openLabourDivisionEditDialog,
      deleteLabourDivision,
      tableLoading,
      onSubmit,
      dialogName,
      labourDivisionDialog,
      initialPagination: {
        rowsPerPage: 15,
      },
      filter: ref(""),
    };
  },
});
</script>
