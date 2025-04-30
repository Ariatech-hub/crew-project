<template>
  <q-page>
    <q-card flat class="no-border-radius">
        <q-toolbar>
        <q-toolbar-title >Internet Uses</q-toolbar-title>
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
          @click="openNewinternetUseDialog"
          icon="add"
              
        />
      </q-toolbar>

      <q-card-section style="padding-top:0px;">
        <q-table
          square
          :rows="internetUses"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
           
              <th class="text-left" style="padding-left:10px;">S.No</th>
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
                  dense
                  outline
                  size="xs"
                  color="primary"
                  icon="mdi-pencil"
                  @click="openinternetUseEditDialog(props.row)"
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
                  @click="deleteinternetUse(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
       
        </q-table>

        <q-dialog v-model="internetUseDialog" persistent position="top" >
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
                  v-model="internetUse.name"
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
                  v-model="internetUse.nepaliName"
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
                  v-model="internetUse.code"
                  outlined
                  lazy-rules
           
                  label="Code "
                  dense
                >
                </q-input>
                <q-input
                  class="q-mt-md"
                  v-model="internetUse.orderNumber"
                  outlined
                  type="number"
                
                  label="Order Number "
                  dense
                >
                </q-input>
                <q-checkbox
                  class="q-mt-md"
                  v-if="internetUse.id"
                  v-model="internetUse.isActive"
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
    let internetUses = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let internetUse = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      isActive: false,
      orderNumber: null,
    });
    let internetUseDialog = ref(false);
    let dialogName = ref(null);
    let internetUseNewDialog = ref(false);
    let internetUseEditDialog = ref(false);
    const getinternetUses = async () => {
      try {
        const response = await api.get("setting/internet-uses");
        internetUses.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewinternetUseDialog = () => {
      $q.loading.show({});
      internetUseNewDialog.value = true;
      internetUse.value.id = 0;
      internetUse.value.name = null;
      internetUse.value.nepaliName = null;
      internetUse.value.code = null;
      internetUse.value.orderNumber = null;
      dialogName.value = "New Internet Use";
      internetUseDialog.value = true;
      $q.loading.hide();
    };
    const openinternetUseEditDialog = (selectedinternetUse) => {
      $q.loading.show({});
      internetUse.value.id = selectedinternetUse.id;
      internetUse.value.name = selectedinternetUse.name;
      internetUse.value.isActive = selectedinternetUse.isActive;
      internetUse.value.nepaliName = selectedinternetUse.nepaliName;
      internetUse.value.code = selectedinternetUse.code;
      internetUse.value.orderNumber = selectedinternetUse.orderNumber;
      dialogName.value = "Update internet Use";
      internetUseDialog.value = true;
      $q.loading.hide();
    };
    const deleteinternetUse = async (internetUse) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the Internet Use ${internetUse.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/internet-use/delete", {
              id: internetUse.id,
              name: internetUse.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getinternetUses();
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
      if (internetUse.value.orderNumber == "") {
        internetUse.value.orderNumber = null;
      }
      try {
        if (internetUse.value.id === 0) {
          response = await api.post(
            "setting/internet-use/insert",
            internetUse.value
          );
        } else {
          response = await api.post(
            "setting/internet-use/update",
            internetUse.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getinternetUses();
        internetUseDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getinternetUses();
      $q.loading.hide({});
    });
    return {
      internetUses,
      internetUse,
      internetUseNewDialog,
      internetUseEditDialog,
      openNewinternetUseDialog,
      openinternetUseEditDialog,
      deleteinternetUse,
      tableLoading,
      onSubmit,
      dialogName,
      internetUseDialog,
      initialPagination: {
        rowsPerPage: 30,
        // rowsNumber: xx if getting data from a server
      },
      filter: ref(""),
    };
  },
});
</script>
