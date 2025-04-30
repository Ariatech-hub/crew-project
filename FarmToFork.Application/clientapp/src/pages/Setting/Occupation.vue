<template>
  <q-page>
    <q-card flat class="no-border-radius">

        <q-toolbar >
        <q-toolbar-title >Occupations</q-toolbar-title>
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
          @click="openNewOccupationDialog"
          icon="add"
              
        />
      </q-toolbar>
            
     
      <q-card-section style="padding-top:0px;">
        <q-table
          square
          :rows="occupations"
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
              <th class="text-left">OrderNo</th>
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
                  dense
                  outline
                  size="xs"
                  color="primary"
                  icon="mdi-pencil"
                  @click="openOccupationEditDialog(props.row)"
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
                  @click="deleteOccupation(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right>
           
          </template>
        </q-table>

        <q-dialog v-model="occupationDialog" persistent position="top">
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
                  v-model="occupation.name"
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
                  v-model="occupation.nepaliName"
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
                  v-model="occupation.code"
                  outlined
                  lazy-rules
             
                  label="Code "
                  dense
                >
                </q-input>
                <q-input
                  v-model="occupation.orderNo"
                  outlined
                  type="number"
                  lazy-rules
                  class="q-mt-md"
        
                  label="Order No. "
                  dense
                >
                </q-input>

                <q-checkbox
                  class="q-mt-md"
                  v-if="occupation.id"
                  v-model="occupation.isActive"
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
    let occupations = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let occupation = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      orderNo: null,
      isActive: false,
    });
    let occupationDialog = ref(false);
    let dialogName = ref(null);
    const getOccupations = async () => {
      try {
        const response = await api.get("setting/occupations");
        occupations.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewOccupationDialog = () => {
      $q.loading.show({});
      occupation.value.id = 0;
      occupation.value.name = null;
      occupation.value.nepaliName = null;
      occupation.value.code = null;
      occupation.value.orderNo = null;
      dialogName.value = "New Occupation";
      occupationDialog.value = true;
      $q.loading.hide();
    };
    const openOccupationEditDialog = (selectedOccupation) => {
      $q.loading.show({});
      occupation.value.id = selectedOccupation.id;
      occupation.value.name = selectedOccupation.name;
      occupation.value.isActive = selectedOccupation.isActive;
      occupation.value.nepaliName = selectedOccupation.nepaliName;
      occupation.value.code = selectedOccupation.code;
      occupation.value.orderNo = selectedOccupation.orderNo;
      dialogName.value = "Update Occupation";
      occupationDialog.value = true;
      $q.loading.hide();
    };
    const deleteOccupation = async (occupation) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the Occupation ${occupation.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/occupation/delete", {
              id: occupation.id,
              name: occupation.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getOccupations();
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
      if (occupation.value.orderNo == "") {
        occupation.value.orderNo = null;
      }
      let response = "";
      try {
        if (occupation.value.id === 0) {
          response = await api.post(
            "setting/occupation/insert",
            occupation.value
          );
        } else {
          response = await api.post(
            "setting/occupation/update",
            occupation.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getOccupations();
        occupationDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getOccupations();
      $q.loading.hide({});
    });
    return {
      occupations,
      occupation,
      openNewOccupationDialog,
      openOccupationEditDialog,
      deleteOccupation,
      tableLoading,
      onSubmit,
      dialogName,
      occupationDialog,
      initialPagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
    };
  },
});
</script>
<style scoped>
.q-table__top.relative-position.row.items-center {
    display: none;
}
</style>