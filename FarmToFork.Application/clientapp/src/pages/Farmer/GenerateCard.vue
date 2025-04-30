<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-toolbar>
        <q-toolbar-title>Generate Card</q-toolbar-title>
      </q-toolbar>
      <q-card-section style="padding-top: 0px">
        <div class="row justify-center bg-white q-py-md bordered">
          <div class="col-md-4 q-pl-md">
            <q-select
              dense
              outlined
              v-model="selectedProvince"
              label="Provinces"
              clearable
              input-debounce="0"
              :options="provinces"
              option-label="name"
              @update:model-value="onProvinceChanged"
              emit-value
              map-options
              behavior="menu"
              option-value="id"
              lazy-rules
            >
            </q-select>
          </div>
          <div class="col-md-4 q-px-md">
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
          <div class="col-md-4 q-pr-md">
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
        <div class="row q-mt-md q-col-gutter-md" style="margin-top: 0px">
          <div class="col-6">
            <q-table
              square
              :rows="farmersOptions"
              :pagination="pagination"
              flat
              dense
              :filter="filter"
              bordered
            >
              <template v-slot:header>
                <tr>
                  <th class="text-left" style="padding: 7px 16px">
                    <q-checkbox
                      v-model="selectAll"
                      @update:model-value="onSelectAll()"
                      size="25px"
                      :val="onSelectAll"
                    ></q-checkbox>
                  </th>
                  <th class="text-left" style="padding: 7px 8px">
                    <span class="text-weight-bold">Name</span>
                  </th>
                </tr>
              </template>
              <template v-slot:body="props">
                <tr>
                  <td class="text-left">
                    <q-checkbox
                      v-model="props.row.isSelected"
                      @update:model-value="onSelect(props.row)"
                      :val="props.row.id"
                      size="25px"
                    ></q-checkbox>
                  </td>
                  <td>{{ props.row.fullName }}</td>
                </tr>
              </template>
              <template v-slot:top-right>
                <q-input
                  square
                  outlined
                  dense
                  debounce="300"
                  clearable
                  v-model="filter"
                  placeholder="Search"
                >
                  <template v-slot:append>
                    <q-icon name="search" />
                  </template>
                </q-input>
              </template>
            </q-table>
          </div>
          <div class="col-6" v-if="farmersOptions.length > 0">
            <div class="row">
              <q-card
                v-for="item in cardImages"
                :key="item.id"
                class="col-4 q-mr-md q-mb-md bg-white"
                :style="
                  item.isSelected
                    ? 'border:solid 3px blue; cursor:pointer'
                    : 'border:solid 1px rgb(211 227 253); cursor:pointer'
                "
                @click="onCardSelected(item)"
              >
                <q-img :src="item.filePath" spinner-color="white" />
                <small style="padding-left: 30%">{{ item.name }}</small>
              </q-card>
            </div>
          </div>
        </div>
        <div class="row q-mt-md" v-if="farmersOptions.length > 0">
          <q-checkbox
            size="sm"
            label="Card Flipped"
            v-model="isCardFlipped"
            dense
          />
        </div>
        <div class="row q-mt-md">
          <q-btn
            label="Generate Card"
            color="primary"
            @click="onGenerateCard()"
          ></q-btn>
        </div>
      </q-card-section>
    </q-card>
  </q-page>
</template>
<script>
import { defineComponent, onMounted, ref } from "vue";
import { api, baseURL } from "boot/axios";
import { handleError } from "boot/utility";
import { useQuasar } from "quasar";

export default defineComponent({
  setup() {
    let provinces = ref([]);
    let districts = ref([]);
    let palikas = ref([]);
    let provinceOptions = ref([]);
    let districtOptions = ref([]);
    let palikaOptions = ref([]);
    let selectedProvince = ref(null);
    let selectedDistrict = ref(null);
    let selectedPalika = ref(null);
    let palikaOptionFilter = ref([]);
    let districtOptionFilter = ref([]);
    let farmers = ref([]);
    let farmersOptions = ref([]);
    let selectAll = ref(false);
    let cardImages = ref([]);
    let isCardFlipped = ref(false);
    const $q = useQuasar();
    const getAllAddress = async () => {
      try {
        const response = await api.get("general/address-for-filter");
        provinces.value = response.data.provinces;
        provinceOptions.value = response.data.provinces;
        districts.value = response.data.districts;
        districtOptions.value = response.data.districts;
        palikas.value = response.data.palikas;
        palikaOptions.value = response.data.palikas;
      } catch (error) {
        handleError(error);
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
          districts.value = districtOptions.value;
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
    const onDistrictChanged = () => {
      $q.loading.show();
      palikaOptions.value = [];
      selectedPalika.value = null;
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
    const onPalikaChanged = () => {
      $q.loading.show();
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
    const getAllFarmers = async () => {
      try {
        const response = await api.get("farmers/card");
        farmers.value = response.data;
        farmersOptions.value = response.data;
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    const onSelectAll = () => {
      if (selectAll.value) {
        farmersOptions.value.forEach(function (item) {
          item.isSelected = true;
        });
      } else {
        farmersOptions.value.forEach(function (item) {
          item.isSelected = false;
        });
      }
    };
    const onSelect = (farmer) => {
      if (farmer.isSelected === false) {
        selectAll.value = false;
      } else {
        selectAll.value = true;
        for (let i = 0; i < farmersOptions.value.length; i++) {
          if (farmersOptions.value[i].isSelected === false) {
            selectAll.value = false;
            break;
          }
        }
      }
    };
    const getAllCardImages = async () => {
      try {
        const response = await api.get("setting/card-images");
        cardImages.value = response.data;
      } catch (error) {
        handleError(error);
      }
    };
    const onCardSelected = (image) => {
      image.isSelected = !image.isSelected;
      cardImages.value.forEach(function (item) {
        if (item.id == image.id) {
          item.isSelected = image.isSelected;
        } else {
          item.isSelected = false;
        }
      });
    };
    const onGenerateCard = async () => {
      let selectedFarmers = farmersOptions.value.filter(function (item) {
        return item.isSelected == true;
      });

      if (selectedFarmers.length <= 0) {
        $q.notify({
          type: "negative",
          message: "Farmer is required",
        });
        return;
      }

      let selectedFarmerIds = selectedFarmers.map(function (item) {
        return item.id;
      });
      let selectedImageId = 0;
      cardImages.value.forEach(function (item) {
        if (item.isSelected == true) {
          selectedImageId = item.id;
        }
      });
      if (selectedImageId === 0) {
        $q.notify({
          type: "negative",
          message: `Card design is required.`,
        });
        return;
      }
      try {
        let formData = document.createElement("form");
        formData.action = baseURL + "/generate-card";
        formData.method = "POST";
        formData.target = "_blank";

        let inputData = document.createElement("input");
        inputData.type = "hidden";
        inputData.name = "FarmerIds";
        inputData.value = JSON.stringify(selectedFarmerIds);
        formData.appendChild(inputData);

        let inputData2 = document.createElement("input");
        inputData2.type = "hidden";
        inputData2.name = "IsCardFlipped";
        inputData2.value = isCardFlipped.value;
        formData.appendChild(inputData2);

        let inputData3 = document.createElement("input");
        inputData3.type = "hidden";
        inputData3.name = "Id";
        inputData3.value = selectedImageId;
        formData.appendChild(inputData3);

        let the_body = document.getElementsByTagName("body")[0];
        the_body.appendChild(formData);
        formData.submit();
        $q.notify({
          type: "positive",
          message: `Card has been generated.`,
        });
        selectedImageId = 0;
      } catch (error) {
        handleError(error);
      }
    };
    onMounted(async () => {
      $q.loading.show();
      await getAllAddress();
      await getAllFarmers();
      await getAllCardImages();
      $q.loading.hide();
    });
    return {
      provinces,
      districts,
      palikas,
      provinceOptions,
      districtOptions,
      palikaOptions,
      selectedProvince,
      selectedDistrict,
      selectedPalika,
      farmers,
      farmersOptions,
      getAllAddress,
      filterProvinceFn,
      filterDistrictFn,
      onProvinceChanged,
      onPalikaChanged,
      onDistrictChanged,
      pagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
      selectAll,
      onSelectAll,
      onSelect,
      cardImages,
      onCardSelected,
      isCardFlipped,
      onGenerateCard,
    };
  },
});
</script>
<style>
.q-card {
  box-shadow: none;
}
</style>
