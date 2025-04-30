<template>
  <q-page>
    <q-card flat class="no-border-radius q-mb-none">
      <q-card-section class="q-mb-none">
        <Header
          :headerNameProps="'Grain'"
          :buttons="buttons"
          :filter="filter"
          v-on:onButtonClick="onButtonClick"
          v-on:onSearch="onSearch"
        ></Header>
        <div class="row q-mt-sm">
          <div class="col-md-4 offset-md-8">
            <q-input
              dense
              outlined
              square
              debounce="300"
              v-model="filter"
              placeholder="Search"
              class="q-mr-md"
            >
              <template v-slot:append>
                <q-icon name="search" />
              </template>
            </q-input>
          </div>
        </div>

        <div class="row q-mt-md">
          <div class="col-md-12">
            <q-table
              square
              dense
              :rows="grains"
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
                      unelevated
                      round
                      name="check"
                      color="positive"
                      class="active-icon-q"
                      size="xs"
                      dense
                      v-if="props.row.isActive"
                    />
                    <q-icon
                      name="highlight_off"
                      color="negative"
                      class="active-icon-q"
                      size="xs"
                      v-else
                    />
                  </td>
                  <td class="text-right">
                    <q-btn
                      unelevated
                      round
                      outline
                      size="xs"
                      dense
                      color="primary"
                      icon="mdi-pencil-outline"
                      @click="openGrainEditDialog(props.row)"
                    >
                      <q-tooltip> Edit </q-tooltip>
                    </q-btn>
                    <q-btn
                      unelevated
                      round
                      outline
                      class="q-ml-xs"
                      size="xs"
                      dense
                      color="negative"
                      icon="mdi-delete"
                      @click="deleteGrain(props.row)"
                    >
                      <q-tooltip> Delete </q-tooltip>
                    </q-btn>
                  </td>
                </tr>
              </template>
            </q-table>
          </div>
        </div>

        <q-dialog v-model="grainDialog" persistent position="top">
          <q-card class="q-mt-lg" square>
            <q-toolbar>
              <q-toolbar-title class="text-dark">{{
                dialogName
              }}</q-toolbar-title>
              <q-btn color="dark" flat round dense icon="close" v-close-popup />
            </q-toolbar>
            <q-separator spaced />

            <q-card-section class="form-card" style="padding-top: 15px">
              <q-form @submit="onSubmit">
                <q-input
                  v-model="grain.name"
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
                  v-model="grain.nepaliName"
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
                  v-model="grain.code"
                  outlined
                  lazy-rules
                  label="Code "
                  dense
                  square
                >
                </q-input>
                <q-input
                  class="q-mt-md"
                  v-model="grain.orderNumber"
                  outlined
                  type="number"
                  square
                  label="Order Number "
                  dense
                >
                </q-input>
                <q-checkbox
                  class="q-mt-md"
                  v-if="grain.id"
                  v-model="grain.isActive"
                  outlined
                  lazy-rules
                  square
                  color="secondary"
                  label="Is Active"
                  dense
                ></q-checkbox>

                <q-separator class="q-mt-lg" spaced />
                <div class="q-mt-lg">
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
import { default as Header } from "src/components/General/Header.vue";

export default defineComponent({
  components: {
    Header,
  },
  setup() {
    let grains = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let buttons = ref([]);
    let grain = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      isActive: false,
      orderNumber: null,
    });
    let grainDialog = ref(false);
    let dialogName = ref(null);
    let grainNewDialog = ref(false);
    let grainEditDialog = ref(false);
    const getGrains = async () => {
      try {
        const response = await api.get("setting/grains");
        grains.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewGrainDialog = () => {
      $q.loading.show({});
      grainNewDialog.value = true;
      grain.value.id = 0;
      grain.value.name = null;
      grain.value.nepaliName = null;
      grain.value.code = null;
      grain.value.orderNumber = null;
      dialogName.value = "New Grain";
      grainDialog.value = true;
      $q.loading.hide();
    };
    const openGrainEditDialog = (selectedgrain) => {
      $q.loading.show({});
      grain.value.id = selectedgrain.id;
      grain.value.name = selectedgrain.name;
      grain.value.isActive = selectedgrain.isActive;
      grain.value.nepaliName = selectedgrain.nepaliName;
      grain.value.code = selectedgrain.code;
      grain.value.orderNumber = selectedgrain.orderNumber;
      dialogName.value = "Update grain";
      grainDialog.value = true;
      $q.loading.hide();
    };
    const deleteGrain = async (grain) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the grain ${grain.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/grain/delete", {
              id: grain.id,
              name: grain.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getGrains();
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
      if (grain.value.orderNumber == "") {
        grain.value.orderNumber = null;
      }
      try {
        if (grain.value.id === 0) {
          response = await api.post("setting/grain/insert", grain.value);
        } else {
          response = await api.post("setting/grain/update", grain.value);
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getGrains();
        grainDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    function onSearch(value) {
      alert("her");
      filter.value = value;
    }

    const addButtonForNewGrain = () => {
      let _temp = {};
      _temp.color = "light-green-8";
      _temp.label = "New";
      _temp.icon = "add";
      _temp.click = openNewGrainDialog;
      _temp.to = null;
      buttons.value.push(_temp);
    };
    function onButtonClick(item) {
      item.click();
    }

    onMounted(async () => {
      $q.loading.show({});
      await getGrains();
      addButtonForNewGrain();
      $q.loading.hide({});
    });
    return {
      grains,
      grain,
      grainNewDialog,
      grainEditDialog,
      openNewGrainDialog,
      openGrainEditDialog,
      deleteGrain,
      tableLoading,
      onSubmit,
      dialogName,
      buttons,
      grainDialog,
      onSearch,
      initialPagination: {
        rowsPerPage: 30,
        // rowsNumber: xx if getting data from a server
      },
      filter: ref(""),
      onButtonClick,
      hide: ref(true),
    };
  },
});
</script>
