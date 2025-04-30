<template>
  <q-page>
    <q-card flat class="no-border-radius">
   
     
        <q-toolbar >
        <q-toolbar-title >Internet Types</q-toolbar-title>
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
          @click="openNewinternetTypeDialog"
          icon="add"
              
        />
      </q-toolbar>
      <q-card-section style="padding-top:0px;">
        <q-table
          square
          :rows="internetTypes"
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
              <th class="text-center">Order Number</th>
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
              <td class="text-center">{{ props.row.orderNumber }}</td>
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
                  @click="openinternetTypeEditDialog(props.row)"
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
                  @click="deleteinternetType(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right>
         
          </template>
        </q-table>

        <q-dialog v-model="internetTypeDialog" persistent position="top" >
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
                  v-model="internetType.name"
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
                  v-model="internetType.nepaliName"
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
                  v-model="internetType.code"
                  outlined
                  lazy-rules
                  label="Code "
                  dense
                >
                </q-input>
                <q-input
                  class="q-mt-md"
                  v-model="internetType.orderNumber"
                  outlined
                  type="number"
                  label="Order Number "
                  dense
                >
                </q-input>
                <q-checkbox
                  class="q-mt-md"
                  v-if="internetType.id"
                  v-model="internetType.isActive"
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
    let internetTypes = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let internetType = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      isActive: false,
      orderNumber: null,
    });
    let internetTypeDialog = ref(false);
    let dialogName = ref(null);
    let internetTypeNewDialog = ref(false);
    let internetTypeEditDialog = ref(false);
    const getinternetTypes = async () => {
      try {
        const response = await api.get("setting/internet-types");
        internetTypes.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewinternetTypeDialog = () => {
      $q.loading.show({});
      internetTypeNewDialog.value = true;
      internetType.value.id = 0;
      internetType.value.name = null;
      internetType.value.nepaliName = null;
      internetType.value.code = null;
      internetType.value.orderNumber = null;
      dialogName.value = "New Internet Type";
      internetTypeDialog.value = true;
      $q.loading.hide();
    };
    const openinternetTypeEditDialog = (selectedinternetType) => {
      $q.loading.show({});
      internetType.value.id = selectedinternetType.id;
      internetType.value.name = selectedinternetType.name;
      internetType.value.isActive = selectedinternetType.isActive;
      internetType.value.nepaliName = selectedinternetType.nepaliName;
      internetType.value.code = selectedinternetType.code;
      internetType.value.orderNumber = selectedinternetType.orderNumber;
      dialogName.value = "Update Internet Type";
      internetTypeDialog.value = true;
      $q.loading.hide();
    };
    const deleteinternetType = async (internetType) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the internet Type ${internetType.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/internet-type/delete", {
              id: internetType.id,
              name: internetType.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getinternetTypes();
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
      if (internetType.value.orderNumber == "") {
        internetType.value.orderNumber = null;
      }
      try {
        if (internetType.value.id === 0) {
          response = await api.post(
            "setting/internet-type/insert",
            internetType.value
          );
        } else {
          response = await api.post(
            "setting/internet-type/update",
            internetType.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getinternetTypes();
        internetTypeDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getinternetTypes();
      $q.loading.hide({});
    });
    return {
      internetTypes,
      internetType,
      internetTypeNewDialog,
      internetTypeEditDialog,
      openNewinternetTypeDialog,
      openinternetTypeEditDialog,
      deleteinternetType,
      tableLoading,
      onSubmit,
      dialogName,
      internetTypeDialog,
      initialPagination: {
        rowsPerPage: 30,
        // rowsNumber: xx if getting data from a server
      },
      filter: ref(""),
    };
  },
});
</script>
<style>
.q-table__top{
  display:none;
}
</style>