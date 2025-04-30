<template>
  <q-page>
    <q-card flat class="no-border-radius">
        <q-toolbar >
        <q-toolbar-title >Obstacles</q-toolbar-title>
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
          @click="openNewobstacleDialog"
          icon="add"
               
        />
      </q-toolbar>
      <q-card-section style="padding-top:0px">
        <q-table
          square
          :rows="obstacles"
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
              <th class="text-center">Order Number</th>
              <th class="text-center">Sale</th>
              <th class="text-center">Production</th>
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
              <td class="text-center">{{ props.row.orderId }}</td>
              <td class="text-center">
                <q-icon
                  name="check"
                  color="positive"
                  class="active-icon-q"
                  size="sm"
                  v-if="props.row.isSale"
                />
                <q-icon
                  name="highlight_off"
                  color="negative"
                  class="active-icon-q"
                  size="sm"
                  v-else
                />
              </td>
              <td class="text-center">
                <q-icon
                  name="check"
                  color="positive"
                  class="active-icon-q"
                  size="xs"
                  v-if="props.row.isProduction"
                />
                <q-icon
                  name="highlight_off"
                  color="negative"
                  class="active-icon-q"
                  size="xs"
                  v-else
                />
              </td>
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
                  @click="openobstacleEditDialog(props.row)"
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
                  @click="deleteobstacle(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right>
          
          </template>
        </q-table>

        <q-dialog v-model="obstacleDialog" persistent position="top">
          <q-card
            square
            style="width: 600px; max-width: 80vw; margin-top: 20px"
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
                  v-model="obstacle.name"
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
                  v-model="obstacle.nepaliName"
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
                  v-model="obstacle.code"
                  outlined
                  lazy-rules
                 
                  label="Code "
                  dense
                >
                </q-input>
                <q-input
                  class="q-mt-md"
                  v-model="obstacle.orderId"
                  outlined
                  type="number"
                 
                  label="Order Number "
                  dense
                >
                </q-input>
                <div class="q-mt-md">
                  <q-checkbox
                    dense
                    label="Is Sale"
                    @update:model-value="onCheckBoxClicked(1)"
                    v-model="obstacle.isSale"
                  ></q-checkbox>
                  <q-checkbox
                    dense
                    @update:model-value="onCheckBoxClicked(2)"
                    v-model="obstacle.isProduction"
                    class="q-ml-md"
                    label="Is Production"
                  ></q-checkbox>
                  <q-checkbox
                    v-if="obstacle.id"
                    v-model="obstacle.isActive"
                    outlined
                    class="q-ml-md"
                    lazy-rules
                    square
                    label="Is Active"
                    dense
                  ></q-checkbox>
                </div>
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
    let obstacles = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let obstacle = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      isActive: false,
      orderId: null,
      isSale: null,
      isProduction: null,
    });
    let obstacleDialog = ref(false);
    let dialogName = ref(null);
    const getobstacles = async () => {
      try {
        const response = await api.get("setting/obstacles");
        obstacles.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewobstacleDialog = () => {
      $q.loading.show({});
      obstacle.value.id = 0;
      obstacle.value.name = null;
      obstacle.value.nepaliName = null;
      obstacle.value.code = null;
      obstacle.value.orderId = null;
      obstacle.value.isSale = false;
      obstacle.value.isProduction = false;
      dialogName.value = "New Obstacle";
      obstacleDialog.value = true;
      $q.loading.hide();
    };
    const onCheckBoxClicked = (value) => {
      if (value === 1) {
        obstacle.value.isSale = true;
        obstacle.value.isProduction = false;
      } else if (value === 2) {
        obstacle.value.isSale = false;
        obstacle.value.isProduction = false;
      }
    };
    const openobstacleEditDialog = (selectedobstacle) => {
      $q.loading.show({});
      obstacle.value.id = selectedobstacle.id;
      obstacle.value.name = selectedobstacle.name;
      obstacle.value.isActive = selectedobstacle.isActive;
      obstacle.value.nepaliName = selectedobstacle.nepaliName;
      obstacle.value.code = selectedobstacle.code;
      obstacle.value.orderId = selectedobstacle.orderId;
      obstacle.value.isSale = selectedobstacle.isSale;
      obstacle.value.isProduction = selectedobstacle.isProduction;
      dialogName.value = "Update Obstacle";
      obstacleDialog.value = true;
      $q.loading.hide();
    };
    const deleteobstacle = async (obstacle) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the obstacle ${obstacle.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/obstacle/delete", {
              id: obstacle.id,
              name: obstacle.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getobstacles();
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

      if (!obstacle.value.isSale && !obstacle.value.isProduction) {
        $q.notify({
          type: "warning",
          message: `Please select the obstacle is either sale or production`,
        });
        $q.loading.hide();
        return;
      }

      let response = "";
      if (obstacle.value.orderId == "") {
        obstacle.value.orderId = null;
      }
      try {
        if (obstacle.value.id === 0) {
          response = await api.post("setting/obstacle/insert", obstacle.value);
        } else {
          response = await api.post("setting/obstacle/update", obstacle.value);
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getobstacles();
        obstacleDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getobstacles();
      $q.loading.hide({});
    });
    return {
      obstacles,
      obstacle,
      openNewobstacleDialog,
      onCheckBoxClicked,
      openobstacleEditDialog,
      deleteobstacle,
      tableLoading,
      onSubmit,
      dialogName,
      obstacleDialog,
      initialPagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
    };
  },
});
</script>
<style > 
.q-table__top.relative-position.row.items-center {
    display: none;
}
</style>
