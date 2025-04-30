<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-card-section>
        <q-toolbar class="bg-white">
          <q-toolbar-title>View</q-toolbar-title>
          <q-spacer></q-spacer>
          <q-tabs
            v-model="tab"
            dense
            class="text-dark bg-white"
            active-color="primary"
            indicator-color="primary"
            narrow-indicator
            style="width: 90%"
          >
            <q-tab name="farmer" label="Farmer" />
            <q-tab name="farmerMember" label="Farmer Member" />
            <q-tab name="lastSeasonProduction" label="Last Season Production" />
            <q-tab name="ProductionPlan" label="Production Plan" />
            <q-tab name="Research" label="Research" />
          </q-tabs>
        </q-toolbar>
        <q-card flat class="q-mt-md">
          <!-- Farmer Info -->
          <q-tab-panels v-model="tab" animated>
            <q-tab-panel name="farmer">
              <div class="row q-pt-sm">
                <div class="col-md-8">
                  <div class="row justify-center">
                    <div class="col-md-6">
                      <p>
                        <strong>Citizenship Number : </strong>
                        {{ farmer.citizenshipNumber }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Province</strong> :
                        {{ farmer.provinceName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>District</strong> :
                        {{ farmer.districtName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p><strong>Palika</strong> : {{ farmer.palikaName }}</p>
                    </div>
                    <div class="col-md-6">
                      <p><strong>Ward</strong> : {{ farmer.ward }}</p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Community</strong> :
                        {{ farmer.communityName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p><strong>DOB</strong> : {{ farmer.dateOfBirth }}</p>
                    </div>
                    <div class="col-md-6">
                      <p><strong>Age</strong> : {{ farmer.age }}</p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Husband/Father</strong> :
                        {{ farmer.husbandOrFatherName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Grandfather/father-in-law</strong> :
                        {{ farmer.grandFatherOrFatherInLawName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Education</strong> :
                        {{ farmer.educationLevelName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Ethnicity</strong> :
                        {{ farmer.ethnicityName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Occupation</strong> :
                        {{ farmer.occupationName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Tole</strong> :
                        {{ farmer.tole }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Latitude</strong> :
                        {{ farmer.latitude }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Longitude</strong> :
                        {{ farmer.longitude }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Bank Balance Valuation</strong> :
                        {{ farmer.bankBalanceValuationAmount }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Business Valuation</strong> :
                        {{ farmer.businessValuationAmount }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Cattle Valuation</strong> :
                        {{ farmer.cattleValuationAmount }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>House Valuation</strong> :
                        {{ farmer.houseValuationAmount }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Jewelry Valuation</strong> :
                        {{ farmer.jewelryValuationAmount }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Other Valuation</strong> :
                        {{ farmer.otherValuationAmount }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Marital Status </strong> :
                        {{ farmer.maritalStatusName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Nominee</strong> :
                        {{ farmer.nomineeName }}
                      </p>
                    </div>
                    <div class="col-md-6">
                      <p>
                        <strong>Nominee Relationship</strong> :
                        {{ farmer.nomineeRelationName }}
                      </p>
                    </div>

                    <div class="col-md-6">
                      <p>
                        <strong>Own Smartphone</strong> :
                        <q-icon
                          name="check_circle_outline"
                          color="positive"
                          size="20px"
                          v-if="farmer.ownSmartPhone"
                        />
                        <q-icon
                          name="highlight_off"
                          color="negative"
                          size="sm"
                          v-else
                        />
                      </p>
                    </div>
                  </div>
                </div>
                <div class="col-md-2 text-left">
                  <q-img
                    :src="src"
                    style="
                      max-width: 110px;
                      height: 110px;
                      border-radius: 50%;
                      margin-bottom: 25px;
                    "
                  />
                  <p>
                    <q-file
                      outlined
                      square
                      dense
                      v-model="file"
                      label="Upload Image"
                      accept="image/*"
                      filled
                      @update:model-value="onFileUpload()"
                    />
                  </p>
                  <p>
                    <strong
                      v-if="farmer.isActive"
                      style="color: #17832e; font-size: 14px"
                      >Active
                    </strong>
                    <!-- <q-icon
                          name="check_circle_outline"
                          color="positive"
                          size="20px"
                          label="Active"

                        /> -->
                    <strong
                      v-else
                      label="Inactive"
                      style="color: #c10015; font-size: 14px"
                      >In Active</strong
                    >
                  </p>
                  <p>{{ farmer.fullName }}</p>
                  <p>{{ farmer.mobileNumber }}</p>
                  <p>{{ farmer.genderName }}</p>
                </div>
              </div>
            </q-tab-panel>
            <!-- Farmer Member -->
            <q-tab-panel name="farmerMember">
              <div class="row">
                <div
                  class="col-md-12"
                  v-for="item in farmer.farmerMembers"
                  :key="item.id"
                >
                  <div>
                    <q-card
                      class="card-box-q"
                      style="border:1px solid rgb(211, 227, 253"
                    >
                      <q-card-section>
                        <div class="row q-ml-md q-mr-md">
                          <div class="col-md-3">
                            <p><strong>Name</strong> {{ item.name }}</p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Relation</strong> {{ item.relationName }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p><strong>Gender</strong> {{ item.genderName }}</p>
                          </div>
                          <div class="col-md-3">
                            <p><strong>Age</strong> {{ item.age }}</p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Education</strong>
                              {{ item.educationLevelName }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Occupation</strong>
                              {{ item.occupationName }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Own Smartphone</strong> :
                              <q-icon
                                name="check_circle_outline"
                                color="positive"
                                size="20px"
                                v-if="item.ownSmartPhone"
                              />
                              <q-icon
                                name="highlight_off"
                                color="negative"
                                size="sm"
                                v-else
                              />
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Location</strong>
                              {{
                                item.locationName
                                  ? item.locationName
                                  : "not set"
                              }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p><strong>Remarks</strong> {{ item.remarks }}</p>
                          </div>
                        </div>

                        <div
                          class="q-mt-md q-ml-md q-mr-md"
                          v-if="item.farmerMemberProperties.length > 0"
                        >
                          <p style="font-size: 18px">
                            <strong>Properties</strong>
                          </p>
                          <div
                            v-for="property in item.farmerMemberProperties"
                            :key="property.id"
                          >
                            <div class="row q-mt-md">
                              <div class="col-md-3">
                                <p>
                                  <strong>Province</strong>
                                  {{ property.provinceName }}
                                </p>
                              </div>
                              <div class="col-md-3">
                                <p>
                                  <strong>District</strong>
                                  {{ property.districtName }}
                                </p>
                              </div>
                              <div class="col-md-3">
                                <p>
                                  <strong>PalikaName</strong>
                                  {{ property.palikaName }}
                                </p>
                              </div>
                              <div class="col-md-3">
                                <p>
                                  <strong>Ward</strong>
                                  {{ property.ward }}
                                </p>
                              </div>
                              <div class="col-md-3">
                                <p>
                                  <strong>Kitta Number</strong>
                                  {{ property.kittaNumber }}
                                </p>
                              </div>
                              <div class="col-md-3">
                                <p>
                                  <strong>Land Area</strong>
                                  {{ property.landArea }}
                                </p>
                              </div>
                              <div class="col-md-3">
                                <p>
                                  <strong>land Valuation</strong>
                                  {{ property.landValuation }}
                                </p>
                              </div>
                            </div>
                          </div>
                        </div>

                        <div
                          class="q-mt-md"
                          v-if="item.farmerMemberTransactions.length > 0"
                        >
                          <p style="font-size: 18px">
                            <strong>Transactions</strong>
                          </p>
                          <div
                            v-for="transaction in item.farmerMemberTransactions"
                            :key="transaction.id"
                          >
                            <div class="row q-mt-md">
                              <div class="col-md-3">
                                <p>
                                  <strong>Expense Source</strong>
                                  {{ transaction.expenseSource }}
                                </p>
                              </div>
                              <div class="col-md-3">
                                <p>
                                  <strong>Expense Amount</strong>
                                  {{ transaction.expenseAmount }}
                                </p>
                              </div>
                              <div class="col-md-3">
                                <p>
                                  <strong>Income Source</strong>
                                  {{ transaction.incomeSource }}
                                </p>
                              </div>
                              <div class="col-md-3">
                                <p>
                                  <strong>Income Amount</strong>
                                  {{ transaction.incomeAmount }}
                                </p>
                              </div>
                            </div>
                          </div>
                        </div>
                      </q-card-section>
                    </q-card>
                  </div>
                </div>
              </div>
            </q-tab-panel>

            <!-- Last Season Production -->

            <q-tab-panel name="lastSeasonProduction">
              <div class="row">
                <div
                  class="col-md-6"
                  v-for="item in farmer.lastSeasonProductions"
                  :key="item.id"
                >
                  <div>
                    <q-card class="q-mt-md" style="width: 750px">
                      <q-card-section>
                        <div class="row">
                          <div class="col-md-3">
                            <p>
                              <strong>Grain Name</strong>
                              {{ item.grainNepaliName }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Production Land Area</strong>
                              {{ item.productionLandArea }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Total Production</strong>
                              {{ item.totalProduction }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Total Sales</strong> {{ item.totalSales }}
                            </p>
                          </div>
                        </div>
                        <div class="row">
                          <div class="col-md-3">
                            <p>
                              <strong>Total Sales Amount</strong>
                              {{ item.totalSalesAmount }}
                            </p>
                          </div>
                          <q-separator></q-separator>
                        </div>
                      </q-card-section>
                    </q-card>
                  </div>
                </div>
              </div>
            </q-tab-panel>
            <!-- Production  Plan -->
            <q-tab-panel name="ProductionPlan">
              <div class="row">
                <div
                  class="col-md-6"
                  v-for="item in farmer.productionPlans"
                  :key="item.id"
                >
                  <div>
                    <q-card class="q-mt-md" style="width: 750px">
                      <q-card-section>
                        <div class="row">
                          <div class="col-md-3">
                            <p>
                              <strong>Grain Season Name</strong>
                              {{ item.grainCycleNepaliName }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Grain Name</strong>
                              {{ item.grainNepaliName }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Home Use</strong>
                              {{ item.homeUse }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Land Area</strong> {{ item.landArea }}
                            </p>
                          </div>
                        </div>
                        <div class="row">
                          <div class="col-md-3">
                            <p>
                              <strong>Seed Only</strong>
                              {{ item.seedOnly }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Total Production</strong>
                              {{ item.totalProduction }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Total Sales</strong>
                              {{ item.totalSales }}
                            </p>
                          </div>
                          <div class="col-md-3">
                            <p>
                              <strong>Total Sales Through Cooperative</strong>
                              {{ item.totalSalesThroughCooperative }}
                            </p>
                          </div>
                        </div>
                      </q-card-section>
                    </q-card>
                  </div>
                </div>
              </div>
            </q-tab-panel>
            <!-- Research -->
            <q-tab-panel name="Research">
              <div v-for="item in farmer.researches" :key="item.id">
                <q-card class="q-mt-md">
                  <q-card-section>
                    <div class="row q-mb-md">
                      <div class="col-md-3">
                        <p>
                          <strong>Economically Active People</strong>
                          {{ item.economicallyActivePeople }}
                        </p>
                      </div>
                      <div class="col-md-3">
                        <p>
                          <strong>Land Area For Bean Production</strong>
                          {{ item.landAreaForBeanProduction }}
                        </p>
                      </div>
                      <div class="col-md-3">
                        <p>
                          <strong>Total Land Area</strong>
                          {{ item.totalLandArea }}
                        </p>
                      </div>

                      <div class="col-md-3">
                        <p>
                          <strong>Has Internet Connection : </strong>
                          <q-icon
                            name="check_circle_outline"
                            color="positive"
                            size="sm"
                            v-if="item.hasInternetConnection"
                          />
                          <q-icon
                            name="highlight_off"
                            color="negative"
                            size="sm"
                            v-else
                          />
                        </p>
                      </div>
                      <div class="col-md-3">
                        <p>
                          <strong>Type Of Internet Name: </strong>
                          {{ item.internetTypeName }}
                        </p>
                      </div>
                      <div class="col-md-3">
                        <p>
                          <strong
                            >Harvesting and Storing After Bean Production
                            :</strong
                          >
                          {{
                            item.harvestingAndStoringAfterBeanProductionLabourDivisionName
                          }}
                        </p>
                      </div>

                      <div class="col-md-3">
                        <p>
                          <strong> Land Ownership For Bean Production</strong>
                          {{
                            item.makingLandReadyForBeanProductionLabourDivisionName
                          }}
                        </p>
                      </div>

                      <div class="col-md-3">
                        <p>
                          <strong>
                            Market Land Ready For Bean Production</strong
                          >
                          {{
                            item.landOwnershipForBeanProductionLabourDivisionName
                          }}
                        </p>
                      </div>

                      <div class="col-md-3">
                        <p>
                          <strong> Plant Seed For Bean Production</strong>
                          {{
                            item.plantSeedForBeanProductionLabourDivisionName
                          }}
                        </p>
                      </div>

                      <div class="col-md-3">
                        <p>
                          <strong
                            >Selling And Marketing After Bean Production</strong
                          >
                          {{
                            item.sellingAndMarketingAfterBeanProductionLabourDivisionName
                          }}
                        </p>
                      </div>

                      <div class="col-md-3">
                        <p>
                          <strong>Taking Care of Bean Production</strong>
                          {{
                            item.takingCareForBeanProductionLabourDivisionName
                          }}
                        </p>
                      </div>
                    </div>

                    <div v-if="item.researchInternetUses.length > 0">
                      <div class="row">
                        <div class="col-md-3 q-mt-xs">
                          <strong>Internet Uses :</strong>
                          <p
                            class="q-mt-xs"
                            v-for="internetUse in item.researchInternetUses"
                            :key="internetUse.useOfInternetName"
                          >
                            {{ internetUse.useOfInternetName }}
                          </p>
                        </div>
                      </div>
                    </div>

                    <div v-if="item.marketStatuses.length > 0">
                      <q-markup-table flat>
                        <caption>
                          <h6 class="q-mt-md">
                            <strong>Market Status</strong>
                          </h6>
                        </caption>
                        <thead>
                          <tr>
                            <th class="text-left"><strong>Grain</strong></th>
                            <th class="text-left"><strong>Month</strong></th>
                            <th class="text-center"><strong>Week</strong></th>
                            <th class="text-center">
                              <strong>Sales Rate</strong>
                            </th>
                            <th class="text-center">
                              <strong>Is From Home</strong>
                            </th>
                          </tr>
                        </thead>
                        <tbody
                          v-for="marketStatus in item.marketStatuses"
                          :key="marketStatus.id"
                        >
                          <td class="text-left">
                            {{ marketStatus.grainName }}
                          </td>
                          <td class="text-left">
                            {{ marketStatus.monthName }}
                          </td>
                          <td class="text-center">{{ marketStatus.week }}</td>
                          <td class="text-center">
                            {{ marketStatus.saleRate }}
                          </td>
                          <td class="text-center">
                            <q-icon
                              name="check_circle_outline"
                              color="positive"
                              size="sm"
                              v-if="marketStatus.isFromHome"
                            />
                            <q-icon
                              name="highlight_off"
                              color="negative"
                              size="sm"
                              v-else
                            />
                          </td>
                        </tbody>
                      </q-markup-table>
                    </div>

                    <div v-if="item.researchObstacles.length > 0">
                      <q-markup-table flat>
                        <caption>
                          <h6>
                            <strong>Research Obstacles</strong>
                          </h6>
                        </caption>
                        <thead>
                          <tr>
                            <th class="text-left"><strong>Name</strong></th>
                            <th class="text-left">
                              <strong>Is Production</strong>
                            </th>
                            <th class="text-left"><strong>Is Sale</strong></th>
                          </tr>
                        </thead>
                        <tbody
                          v-for="obstacles in item.researchObstacles"
                          :key="obstacles.obstacleName"
                        >
                          <td class="text-left">
                            {{ obstacles.obstacleName }}
                          </td>
                          <td class="text-left">
                            <q-icon
                              name="check_circle_outline"
                              color="positive"
                              size="sm"
                              v-if="obstacles.isProduction"
                            />
                            <q-icon
                              name="highlight_off"
                              color="negative"
                              size="sm"
                              v-else
                            />
                          </td>
                          <td class="text-left">
                            <q-icon
                              name="check_circle_outline"
                              color="positive"
                              size="sm"
                              v-if="obstacles.isSale"
                            />
                            <q-icon
                              name="highlight_off"
                              color="negative"
                              size="sm"
                              v-else
                            />
                          </td>
                        </tbody>
                      </q-markup-table>
                    </div>

                    <div
                      v-if="item.farmerMemberAgricultureParticipants.length > 0"
                    >
                      <q-markup-table flat>
                        <caption>
                          <h6 style="margin-bottom: 2px; margin-top: 2px">
                            <strong>Agriculture Participant</strong>
                          </h6>
                        </caption>
                        <thead>
                          <tr>
                            <th class="text-left">
                              <strong>Member Name</strong>
                            </th>
                            <th class="text-left">
                              <strong>Participant Option</strong>
                            </th>
                          </tr>
                        </thead>
                        <tbody
                          v-for="agricultureParticipant in item.farmerMemberAgricultureParticipants"
                          :key="agricultureParticipant.id"
                        >
                          <td class="text-left">
                            {{ agricultureParticipant.farmerMemberName }}
                          </td>

                          <td class="text-left">
                            {{ agricultureParticipant.participantOptionName }}
                          </td>
                        </tbody>
                      </q-markup-table>
                    </div>
                  </q-card-section>
                </q-card>
                <q-separator></q-separator>
              </div>
            </q-tab-panel>
          </q-tab-panels>
        </q-card>
      </q-card-section>
    </q-card>
  </q-page>
</template>

<script>
import { defineComponent, onMounted, ref } from "vue";
import { api } from "boot/axios";
import { handleError } from "boot/utility";
import { useQuasar } from "quasar";
import { useRouter, useRoute } from "vue-router";

export default defineComponent({
  setup() {
    const $q = useQuasar();
    const router = useRouter();
    const route = useRoute();
    const file = ref(null);

    const farmer = ref({
      fullname: "",
      farmerMembers: [
        {
          farmerMemberProperties: [],
          farmerMemberProperties: [],
        },
      ],
      researches: [
        {
          marketStatuses: [],
          researchInternetUses: [],
          researchObstacles: [],
          farmerMemberAgricultureParticipants: [],
        },
      ],
    });
    const farmerId = ref(null);
    let src = ref(null);

    const getFarmerById = async () => {
      try {
        const response = await api.get(`farmer/view/${farmerId.value}`);
        farmer.value = response.data;

        src.value = farmer.value.photoPath;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };

    const onFileUpload = async () => {
      if (file.value) {
        const formData = new FormData();
        formData.append("file", file.value);
        formData.append("farmerId", farmerId.value);
        try {
          const response = await api.post("farmer/change-picture", formData);
          $q.notify({
            color: "positive",
            message: `${response.data}`,
          });
          await getFarmerById();
          file.value = null;
        } catch (error) {
          handleError(error);
        }
      }
    };

    onMounted(async () => {
      $q.loading.show();
      farmerId.value = route.params.id;
      await getFarmerById();
      $q.loading.hide();
    });
    return {
      tab: ref("farmer"),
      farmer,
      src,
      onFileUpload,
      file,
    };
  },
});
</script>
<style>
.card-box-q {
  box-shadow: none;
}
</style>
