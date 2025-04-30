<template>
  <q-page>
    <q-card flat class="no-border-radius">

        <q-toolbar >
        <q-toolbar-title 
          >Participation Options</q-toolbar-title
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
          @click="openNewParticipationOptionDialog"
          icon="add"
             
        />
      </q-toolbar>
      <q-card-section style="padding-top:0px">
        <q-table
          square
          :rows="participationOptions"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
                <th class="text-left ">S.No</th>
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
                  size="xs"
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
                  dense
                  outline
                  size="xs"
                  color="primary"
                  icon="mdi-pencil"
                  @click="openParticipationOptionEditDialog(props.row)"
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
                  @click="deleteParticipationOption(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          
        </q-table>

        <q-dialog v-model="participationOptionDialog" persistent position="top">
          <q-card class="q-mt-lg"
            square
          
          >
            <q-toolbar>
              <q-toolbar-title>{{ dialogName }}</q-toolbar-title>
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
                  v-model="participationOption.name"
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
                  v-model="participationOption.nepaliName"
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
                  v-model="participationOption.code"
                  outlined
                  lazy-rules
                
                  label="Code "
                  dense
                >
                </q-input>

                <q-input
                  v-model="participationOption.orderNo"
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
                  v-if="participationOption.id"
                  v-model="participationOption.isActive"
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
    let participationOptions = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let participationOption = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      orderNo: null,
      isActive: false,
    });
    let participationOptionDialog = ref(false);
    let dialogName = ref(null);
    const getParticipationOptions = async () => {
      try {
        const response = await api.get("setting/participation-options");
        participationOptions.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewParticipationOptionDialog = () => {
      $q.loading.show({});
      participationOption.value.id = 0;
      participationOption.value.name = null;
      participationOption.value.nepaliName = null;
      participationOption.value.code = null;
      participationOption.value.orderNo = null;
      dialogName.value = "New Participation Option";
      participationOptionDialog.value = true;
      $q.loading.hide();
    };
    const openParticipationOptionEditDialog = (selectedParticipationOption) => {
      $q.loading.show({});
      participationOption.value.id = selectedParticipationOption.id;
      participationOption.value.name = selectedParticipationOption.name;
      participationOption.value.isActive = selectedParticipationOption.isActive;
      participationOption.value.nepaliName =
        selectedParticipationOption.nepaliName;
      participationOption.value.code = selectedParticipationOption.code;
      participationOption.value.orderNo = selectedParticipationOption.orderNo;
      dialogName.value = "Update Participation Option";
      participationOptionDialog.value = true;
      $q.loading.hide();
    };
    const deleteParticipationOption = async (participationOption) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the ParticipationOption ${participationOption.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post(
              "setting/participation-option/delete",
              {
                id: participationOption.id,
                name: participationOption.name,
              }
            );
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getParticipationOptions();
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
      if (participationOption.value.orderNo == "") {
        participationOption.value.orderNo = null;
      }
      let response = "";
      try {
        if (participationOption.value.id === 0) {
          response = await api.post(
            "setting/participation-option/insert",
            participationOption.value
          );
        } else {
          response = await api.post(
            "setting/participation-option/update",
            participationOption.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getParticipationOptions();
        participationOptionDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getParticipationOptions();
      $q.loading.hide({});
    });
    return {
      participationOptions,
      participationOption,
      openNewParticipationOptionDialog,
      openParticipationOptionEditDialog,
      deleteParticipationOption,
      tableLoading,
      onSubmit,
      dialogName,
      participationOptionDialog,
      initialPagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
    };
  },
});
</script>
