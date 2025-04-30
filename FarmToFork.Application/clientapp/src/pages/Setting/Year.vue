<template>
  <q-page>
    <q-card flat class="no-border-radius">
        <q-toolbar>
        <q-toolbar-title >Years</q-toolbar-title>
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
          @click="openNewYearDialog"
          icon="add"
          
        />
      </q-toolbar>
          

      <q-card-section style="padding-top:0px;">
        <q-table
          square
          :rows="years"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
                <th class="text-left ">S.No</th>
              <th class="text-left">Name</th>
              <th class="text-left">Nepali Name</th>
              <th class="text-left">Start Date(A.D)</th>
              <th class="text-left">End Date(A.D)</th>
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
              <td
                style="
                  max-width: 210px;
                  min-width: 50px;
                  overflow: hidden;
                  text-overflow: ellipsis;
                "
                class="text-left"
              >
                {{ props.row.startDate }}
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
                {{ props.row.endDate }}
              </td>
              <td
                style="
                  max-width: 210px;
                  min-width: 50px;
                  overflow: hidden;
                  text-overflow: ellipsis;
                "
                class="text-center"
              >
                {{ props.row.orderNo }}
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
                  @click="openYearEditDialog(props.row)"
                >
                  <q-tooltip> Edit </q-tooltip>
                </q-btn>
                <q-btn
                  unelevated
                  round
                  class="q-ml-xs"
                  dense
                  outline
                  size="xs"
                  color="negative"
                  icon="mdi-delete"
                  @click="deleteYear(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
         
        </q-table>

        <q-dialog v-model="yearDialog" persistent position="top">
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
                  v-model="year.name"
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
                  v-model="year.nepaliName"
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
              
                  dense
                  outlined
                  class="q-mb-lg"
                  label="Start Date"
                  placeholder="yyyy/mm/dd"
                  v-model="year.startDate"
                  mask="date"
                  lazy-rules
                >
                  <template v-slot:append>
                    <q-icon name="event" class="cursor-pointer">
                      <q-popup-proxy
                        ref="qDateProxy"
                        transition-show="scale"
                        transition-hide="scale"
                      >
                        <q-date minimal v-model="year.startDate">
                          <div class="row items-center justify-end">
                            <q-btn
                              v-close-popup
                              label="Close"
                              color="primary"
                              flat
                            />
                          </div>
                        </q-date>
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                </q-input>

                <q-input
      
                  dense
                  outlined
                  label="End Date"
                  placeholder="yyyy/mm/dd"
                  v-model="year.endDate"
                  mask="date"
                  lazy-rules
                >
                  <template v-slot:append>
                    <q-icon name="event" class="cursor-pointer">
                      <q-popup-proxy
                        ref="qDateProxy"
                        transition-show="scale"
                        transition-hide="scale"
                      >
                        <q-date minimal v-model="year.endDate">
                          <div class="row items-center justify-end">
                            <q-btn
                              v-close-popup
                              label="Close"
                              color="primary"
                              flat
                            />
                          </div>
                        </q-date>
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                </q-input>
                <q-input
                  v-model="year.orderNo"
                  outlined
                  type="number"
                  class="q-mt-md"
                  lazy-rules
               
                  label="Order No."
                  dense
                >
                </q-input>

                <q-checkbox
                  class="q-mt-md"
                  v-if="year.id"
                  v-model="year.isActive"
                  te
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
import { handleError, dateFormat } from "boot/utility";
import { useQuasar } from "quasar";
import { parseStringStyle } from "@vue/shared";
export default defineComponent({
  setup() {
    let years = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let year = ref({
      id: 0,
      name: "",
      nepaliName: "",
      startDate: "",
      endDate: "",
      orderNo: "",
      isActive: false,
    });
    let yearDialog = ref(false);
    let dialogName = ref(null);
    const getYears = async () => {
      try {
        const response = await api.get("setting/years");
        years.value = response.data;
        years.value.forEach((item) => {
          item.endDate = dateFormat(item.endDateAd, "YYYY-MM-DD");
          item.startDate = dateFormat(item.startDateAd, "YYYY-MM-DD");
        });
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewYearDialog = () => {
      $q.loading.show({});
      year.value.id = 0;
      year.value.name = "";
      year.value.nepaliName = "";
      year.value.startDate = "";
      year.value.endDate = "";
      year.value.orderNo = "";
      dialogName.value = "New Year";
      yearDialog.value = true;
      $q.loading.hide();
    };
    const openYearEditDialog = (selectedYear) => {
      $q.loading.show({});
      year.value.id = selectedYear.id;
      year.value.name = selectedYear.name;
      year.value.isActive = selectedYear.isActive;
      year.value.nepaliName = selectedYear.nepaliName;
      year.value.startDate = selectedYear.startDate;
      year.value.endDate = selectedYear.endDate;
      year.value.orderNo = selectedYear.orderNo;

      dialogName.value = "Update Year";
      yearDialog.value = true;
      $q.loading.hide();
    };
    const deleteYear = async (year) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the Year ${year.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/year/delete", {
              id: year.id,
              name: year.name,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });

            await getYears();

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
      if (year.value.orderNo == "") {
        year.value.orderNo = null;
      }
      let response = "";

      try {
        if (year.value.id === 0) {
          response = await api.post("setting/year/insert", year.value);
        } else {
          response = await api.post("setting/year/update", year.value);
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getYears();
        yearDialog.value = false;
        $q.loading.hide();
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getYears();
      $q.loading.hide({});
    });
    return {
      years,
      year,
      openNewYearDialog,
      openYearEditDialog,
      deleteYear,
      tableLoading,
      onSubmit,
      dialogName,
      yearDialog,
      initialPagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
    };
  },
});
</script>
