<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-toolbar class="bg-grey-2">
        <q-toolbar-title class="text-primary">Locations</q-toolbar-title>
        <q-btn
          unelevated
          size="sm"
          color="primary"
          label="New"
          @click="openNewLocationDialog"
          icon="add"
        />
      </q-toolbar>
      <q-separator />
      <q-card-section>
        <q-table
          square
          :rows="locations"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left">S.No</th>
              <th class="text-left">Name</th>
              <th class="text-left">Nepali Name</th>
              <th class="text-left">Code</th>
              <th class="text-left">Order No</th>
              <th class="text-center">Is Active</th>
              <th class="text-center">Action</th>
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
              <td class="text-left">{{ props.row.orderId }}</td>
              <td class="text-center">
                <q-icon
                  name="check_circle_outline"
                  color="positive"
                  size="sm"
                  v-if="props.row.isActive"
                />
                <q-icon
                  name="highlight_off"
                  color="negative"
                  size="sm"
                  v-else
                />
              </td>
              <td class="text-center">
                <q-btn
                  unelevated
                  round
                  size="xs"
                  color="primary"
                  icon="mdi-circle-edit-outline"
                  @click="openLocationEditDialog(props.row)"
                >
                  <q-tooltip> Edit </q-tooltip>
                </q-btn>
                <q-btn
                  unelevated
                  round
                  class="q-ml-xs"
                  size="xs"
                  color="negative"
                  icon="mdi-delete-circle-outline"
                  @click="deleteLocation(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right>
            <q-input
              borderless
              dense
              debounce="300"
              v-model="filter"
              placeholder="Search"
            >
              <template v-slot:append>
                <q-icon name="search" />
              </template>
            </q-input>
          </template>
        </q-table>

        <q-dialog v-model="locationDialog" persistent position="top">
          <q-card
            square
           
          >
            <q-toolbar class="bg-primary text-white">
              <q-toolbar-title>{{ dialogName }}</q-toolbar-title>
              <q-btn
                color="white"
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
                  v-model="location.name"
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
                  v-model="location.nepaliName"
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
                  v-model="location.code"
                  outlined
                  lazy-rules
                  square
                  label="Code "
                  dense
                >
                </q-input>
                <q-input
                  v-model="location.orderId"
                  outlined
                  type="number"
                  lazy-rules
                  class="q-mt-md"
                  square
                  label="Order No. "
                  dense
                >
                </q-input>

                <q-checkbox
                  class="q-mt-md"
                  v-if="location.id"
                  v-model="location.isActive"
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
    let locations = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let location = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      orderId: null,
      isActive: false,
    });
    let locationDialog = ref(false);
    let dialogName = ref(null);
    const getLocations = async () => {
      try {
        const response = await api.get("setting/locations");
        locations.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewLocationDialog = () => {
      $q.loading.show({});
      location.value.id = 0;
      location.value.name = null;
      location.value.nepaliName = null;
      location.value.code = null;
      location.value.orderId = null;
      dialogName.value = "New location";
      locationDialog.value = true;
      $q.loading.hide();
    };
    const openLocationEditDialog = (selectedLocation) => {
      $q.loading.show({});
      location.value.id = selectedLocation.id;
      location.value.name = selectedLocation.name;
      location.value.isActive = selectedLocation.isActive;
      location.value.nepaliName = selectedLocation.nepaliName;
      location.value.code = selectedLocation.code;
      location.value.orderId = selectedLocation.orderId;
      dialogName.value = "Update location";
      locationDialog.value = true;
      $q.loading.hide();
    };
    const deleteLocation = async (location) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the location ${location.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/location/delete", {
              id: location.id,
              name: location.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getLocations();
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
      if (location.value.orderId == "") {
        location.value.orderId = null;
      }
      let response = "";
      try {
        if (location.value.id === 0) {
          response = await api.post("setting/location/insert", location.value);
        } else {
          response = await api.post("setting/location/update", location.value);
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getLocations();
        locationDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getLocations();
      $q.loading.hide({});
    });
    return {
      locations,
      location,
      openNewLocationDialog,
      openLocationEditDialog,
      deleteLocation,
      tableLoading,
      onSubmit,
      dialogName,
      locationDialog,
      initialPagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
    };
  },
});
</script>
