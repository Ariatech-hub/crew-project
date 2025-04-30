<template>
  <q-page>
    <q-card flat class="no-border-radius">
      
        <q-toolbar >
          <q-toolbar-title 
            >Education Levels</q-toolbar-title
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
            @click="openNeweducationLevelDialog"
            icon="add"
          />
        </q-toolbar>
        <q-card-section style="padding-top:0px">
        <q-table
          square
          :rows="educationLevels"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left" style="padding-left: 10px">S.No</th>
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
                  @click="openeducationLevelEditDialog(props.row)"
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
                  @click="deleteEducationLevel(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right> </template>
        </q-table>

        <q-dialog v-model="educationLevelDialog" persistent  position="top">
          <q-card class="q-mt-lg"
            square
         
          >
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
                  v-model="educationLevel.name"
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
                  v-model="educationLevel.nepaliName"
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
                  v-model="educationLevel.code"
                  outlined
                  lazy-rules
                  label="Code "
                  dense
                >
                </q-input>
                <q-input
                  v-model="educationLevel.orderNo"
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
                  v-if="educationLevel.id"
                  v-model="educationLevel.isActive"
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
        <q-dialog v-model="educationLevelEditDialog" persistent position="top">
          <q-card
            square
           
          >
            <q-toolbar>
              <q-toolbar-title color="dark">Update educationLevel</q-toolbar-title>
              
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
                  v-model="educationLevel.name"
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
                <div class="row">
                  <div class="col-md-4">
                    <q-checkbox
                      dense
                      v-model="educationLevel.isActive"
                      label="Is Active"
                    />
                  </div>
                </div>
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
    let educationLevels = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let educationLevel = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      orderNo: null,
      isActive: false,
    });
    let educationLevelDialog = ref(false);
    let dialogName = ref(null);
    let educationLevelNewDialog = ref(false);
    let educationLevelEditDialog = ref(false);
    const geteducationLevels = async () => {
      try {
        const response = await api.get("setting/education-levels");
        educationLevels.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNeweducationLevelDialog = () => {
      $q.loading.show({});
      educationLevelNewDialog.value = true;

      educationLevel.value.id = 0;
      educationLevel.value.name = null;
      educationLevel.value.nepaliName = null;
      educationLevel.value.code = null;
      educationLevel.value.orderNo = null;
      dialogName.value = "New Education Level";
      educationLevelDialog.value = true;
      $q.loading.hide();
    };
    const openeducationLevelEditDialog = (selectededucationLevel) => {
      $q.loading.show({});
      educationLevel.value.id = selectededucationLevel.id;
      educationLevel.value.name = selectededucationLevel.name;
      educationLevel.value.isActive = selectededucationLevel.isActive;
      educationLevel.value.nepaliName = selectededucationLevel.nepaliName;
      educationLevel.value.code = selectededucationLevel.code;
      educationLevel.value.orderNo = selectededucationLevel.orderNo;

      dialogName.value = "Update Education Level";
      educationLevelDialog.value = true;
      $q.loading.hide();
    };
    const deleteEducationLevel = async (educationLevel) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the educationLevel ${educationLevel.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/education-level/delete", {
              id: educationLevel.id,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await geteducationLevels();
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
      if (educationLevel.value.orderNo == "") {
        educationLevel.value.orderNo = null;
      }
      try {
        if (educationLevel.value.id === 0) {
          response = await api.post(
            "setting/education-level/insert",
            educationLevel.value
          );
        } else {
          response = await api.post(
            "setting/education-level/update",
            educationLevel.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await geteducationLevels();
        educationLevelDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await geteducationLevels();
      $q.loading.hide({});
    });
    return {
      educationLevels,
      educationLevel,
      educationLevelNewDialog,
      educationLevelEditDialog,
      openNeweducationLevelDialog,
      openeducationLevelEditDialog,
      deleteEducationLevel,
      tableLoading,
      onSubmit,
      dialogName,
      educationLevelDialog,
      initialPagination: {
        rowsPerPage: 30,
        // rowsNumber: xx if getting data from a server
      },
      filter: ref(""),
    };
  },
});
</script>
