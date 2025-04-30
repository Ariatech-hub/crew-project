<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-toolbar>
        <q-toolbar-title>Members</q-toolbar-title>
        <q-btn
          icon="cloud_download"
          unelevated
          round
          size="sm"
          @click="onDownloadClicked"
          color="primary"
        >
          <q-tooltip anchor="bottom start"> Download </q-tooltip>
        </q-btn>
      </q-toolbar>
      <q-card-section style="padding-top: 0px">
        <div>
          <div class="row justify-center bg-white q-py-md q-pe-md q-mb-md">
            <div class="col-md-4 col-xs-12 q-pr-md q-pl-md">
              <q-select
                dense
                outlined
                v-model="selectedProvince"
                label="Provinces"
                clearable
                input-debounce="0"
                :options="provinces"
                @update:model-value="onProvinceChanged"
                emit-value
                map-options
                behavior="menu"
                option-label="name"
                option-value="id"
                lazy-rules
              >
              </q-select>
            </div>
            <div class="col-md-4 col-xs-12 q-pr-xs q-pl-xs">
              <q-select
                dense
                outlined
                v-model="selectedDistrict"
                label="Districts"
                clearable
                use-input
                input-debounce="0"
                :options="districtOptions"
                option-label="name"
                emit-value
                @filter="filterDistrictFn"
                map-options
                :disable="selectedProvince === null"
                behavior="menu"
                @update:model-value="onDistrictChanged"
                option-value="id"
                lazy-rules
              >
                <template v-slot:no-option>
                  <q-item>
                    <q-item-section class="text-grey">
                      No results
                    </q-item-section>
                  </q-item>
                </template>
              </q-select>
            </div>
            <div class="col-md-4 col-xs-12 q-pr-md q-pl-md">
              <q-select
                dense
                outlined
                v-model="selectedPalika"
                label="Palika"
                clearable
                input-debounce="0"
                :options="palikaOptions"
                option-label="name"
                emit-value
                @update:model-value="onPalikaChanged"
                :disable="selectedDistrict === null"
                map-options
                behavior="menu"
                option-value="id"
                lazy-rules
              />
            </div>
          </div>
          <q-table
            square
            :rows="farmersOptions"
            :loading="tableLoading"
            :pagination="initialPagination"
            :filter="filter"
          >
            <template v-slot:header>
              <tr>
                <th class="text-left">S.No</th>
                <th class="text-left">Name</th>
                <th class="text-left">Province</th>
                <th class="text-left">District</th>
                <th class="text-left">Palika</th>
                <th class="text-left" style="padding-left: 0">Ward</th>
                <th class="text-left">Community</th>
                <th class="text-center">Active</th>
                <th class="text-right">Actions</th>
              </tr>
            </template>
            <template v-slot:body="props">
              <tr :key="props.row.id">
                <td class="text-left">{{ props.rowIndex + 1 }}</td>
                <td class="text-left">{{ props.row.fullName }}</td>
                <td class="text-left">{{ props.row.provinceName }}</td>
                <td class="text-left">{{ props.row.districtName }}</td>
                <td class="text-left">{{ props.row.palikaName }}</td>
                <td class="text-left">{{ props.row.ward }}</td>
                <td class="text-left">{{ props.row.communityName }}</td>
                <td class="text-center">
                  <q-toggle
                    v-model="props.row.isActive"
                    color="positive"
                    size="xs"
                    dense
                    @update:model-value="onToggleClicked(props.row.id)"
                  />
                </td>
                <td class="text-right">
                  <q-btn
                    unelevated
                    round
                    size="xs"
                    dense
                    outline
                    color="warning"
                    class="q-ml-xs"
                    icon="visibility"
                    :to="`/farmer/${props.row.id}/view`"
                  >
                    <q-tooltip> View</q-tooltip>
                  </q-btn>
                  <q-btn
                    unelevated
                    round
                    dense
                    outline
                    class="q-ml-xs"
                    size="xs"
                    color="primary"
                    icon="mdi-pencil"
                  >
                    <q-tooltip> Edit </q-tooltip>
                  </q-btn>
                </td>
              </tr>
            </template>
          </q-table>
        </div>
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
    let selectedProvince = ref(null);
    let provinceOptions = ref([]);
    let selectedDistrict = ref(null);
    let selectedPalika = ref(null);
    let farmers = ref([]);
    let farmersOptions = ref([]);
    let provinces = ref([]);
    let districts = ref([]);
    let districtOptions = ref([]);
    let districtOptionFilter = ref([]);
    let palikas = ref([]);
    let palikaOptions = ref([]);
    let palikaOptionFilter = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let selectedWard = ref(0);

    const getAddressFilterData = async () => {
      try {
        const response = await api.get("general/address-for-filter");
        provinces.value = response.data.provinces;
        provinceOptions.value = response.data.provinces;
        districts.value = response.data.districts;
        districtOptions.value = response.data.districts;
        palikas.value = response.data.palikas;
        palikaOptions.value = response.data.palikas;
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };

    const onToggleClicked = async (farmerId) => {
      try {
        $q.loading.show();
        const response = await api.post(`farmer/change-status`, {
          Id: farmerId,
        });
        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };
    const getAllFarmers = async () => {
      try {
        const response = await api.get("farmers");
        farmers.value = response.data;
        farmersOptions.value = response.data;
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const filterProvinceFn = (val, update) => {
      if (val === "") {
        update(() => {
          provinces.value = provinceOptions.value;
        });
        return;
      }

      update(() => {
        const needle = val.toLowerCase();

        provinces.value = provinceOptions.value.filter(
          (v) => v.name.toLowerCase().indexOf(needle) > -1
        );
      });
    };

    const filterDistrictFn = (val, update) => {
      if (val === "") {
        update(() => {
          districtOptions.value = districtOptionFilter.value;
        });
        return;
      }

      update(() => {
        const needle = val.toLowerCase();

        districtOptions.value = districtOptionFilter.value.filter(
          (v) => v.name.toLowerCase().indexOf(needle) > -1
        );
      });
    };

    const onProvinceChanged = () => {
      $q.loading.show();
      districtOptions.value = [];
      palikaOptions.value = [];
      selectedDistrict.value = null;
      selectedPalika.value = null;
      selectedWard.value = null;
      if (selectedProvince.value == null) {
        farmersOptions.value = farmers.value;
        $q.loading.hide();
        return;
      }

      districtOptions.value = districts.value.filter(
        (v) => v.provinceId === selectedProvince.value
      );
      farmerFilter(selectedProvince.value);
      districtOptionFilter.value = districtOptions.value;
      $q.loading.hide();
    };

    const farmerFilter = (
      provinceId,
      districtId = 0,
      palikaId = 0,
      ward = null
    ) => {
      farmersOptions.value = farmers.value.filter(
        (v) => v.provinceId === provinceId
      );
      if (districtId > 0) {
        farmersOptions.value = farmersOptions.value.filter(
          (v) => v.districtId === districtId
        );
      }
      if (palikaId > 0) {
        farmersOptions.value = farmersOptions.value.filter(
          (v) => v.palikaId === palikaId
        );
      }
      if (ward != null && ward != "") {
        farmersOptions.value = farmersOptions.value.filter(
          (v) => v.ward === ward
        );
      }
    };

    const onDistrictChanged = () => {
      $q.loading.show();
      palikaOptions.value = [];
      selectedPalika.value = null;
      selectedWard.value = null;
      if (selectedDistrict.value == null) {
        farmerFilter(selectedProvince.value);
        $q.loading.hide();
        return;
      }
      farmerFilter(selectedProvince.value, selectedDistrict.value);
      palikaOptions.value = palikas.value.filter(
        (v) => v.districtId === selectedDistrict.value
      );
      palikaOptionFilter.value = palikaOptions.value;
      $q.loading.hide();
    };

    const onWardChanged = () => {
      $q.loading.show();

      if (selectedWard.value === null || selectedWard.value === "") {
        farmerFilter(
          selectedProvince.value,
          selectedDistrict.value,
          selectedPalika.value
        );
        $q.loading.hide();
        return;
      }

      farmerFilter(
        selectedProvince.value,
        selectedDistrict.value,
        selectedPalika.value,
        selectedWard.value
      );
      $q.loading.hide();
    };

    const onDownloadClicked = () => {
      $q.loading.show();
      const method = "GET";
      let theDistrictId = 0;
      let thePalikaId = 0;
      let theProvinceId = 0;
      if (selectedProvince.value != null) {
        theProvinceId = selectedProvince.value;
      }

      if (selectedDistrict.value != null) {
        theDistrictId = selectedDistrict.value;
      }
      if (selectedPalika.value != null) {
        thePalikaId = selectedPalika.value;
      }

      const url = `report/farmer?provinceId=${theProvinceId}&&districtId=${theDistrictId}&palikaId=${thePalikaId}&ward=0&communityId=0`;
      api
        .request({
          url,
          method,
          headers: {
            "Content-Disposition": "attachment; filename=template.xlsx",
            "Content-Type":
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          },
          responseType: "arraybuffer",
        })
        .then((response) => {
          const url = window.URL.createObjectURL(new Blob([response.data]));
          const link = document.createElement("a");
          link.href = url;
          link.setAttribute("download", "Farmer_Report.xlsx");
          document.body.appendChild(link);
          link.click();
        })
        .catch((error) => handleError(error))
        .finally(() => $q.loading.hide());
    };

    const onPalikaChanged = () => {
      $q.loading.show();
      selectedWard.value = null;
      if (selectedPalika.value == null) {
        farmerFilter(selectedProvince.value, selectedDistrict.value);
        $q.loading.hide();
        return;
      }
      farmerFilter(
        selectedProvince.value,
        selectedDistrict.value,
        selectedPalika.value
      );
      $q.loading.hide();
    };
    onMounted(async () => {
      $q.loading.show();
      await getAddressFilterData();
      await getAllFarmers();
      $q.loading.hide();
    });
    return {
      selectedProvince,
      selectedDistrict,
      provinces,
      districtOptions,
      palikaOptions,
      selectedPalika,
      filterProvinceFn,
      onProvinceChanged,
      filterDistrictFn,
      onDistrictChanged,
      farmersOptions,
      tableLoading,
      initialPagination: {
        rowsPerPage: 30,
        // rowsNumber: xx if getting data from a server
      },
      filter: ref(""),
      onPalikaChanged,
      onToggleClicked,
      selectedWard,
      onWardChanged,
      onDownloadClicked,
    };
  },
});
</script>
