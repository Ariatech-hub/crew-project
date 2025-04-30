<template>
  <q-page>
    <q-card flat>
      <q-card-section>
        <div class="row q-col-gutter-md">
          <div class="col-xs-12 col-sm-6 col-md-3">
            <q-item
              class="bg-green-3 bg-dash-b text-black q-py-lg ripple"
              clickable
              to="/farmer/list"
            >
              <q-item-section class="bg-indigo icon-q-d" avatar>
                <q-icon color="white" size="xl" name="mdi-account-group" />
              </q-item-section>

              <q-item-section class="q-pl-lg">
                <p class="q-my-none text-subtitle1">Farmer</p>
                <h4 class="q-my-none text-weight-bold text-indigo">
                  {{ dashboardData.farmerCount }}
                </h4>
              </q-item-section>
            </q-item>
          </div>
          <div class="col-xs-12 col-sm-6 col-md-3">
            <q-item
              class="bg-light-green-3 bg-dash-b text-black q-py-lg"
              clickable
              to="/customer/list"
            >
              <q-item-section bordered class="bg-deep-purple icon-q-d" avatar>
                <q-icon color="white" size="xl" name="mdi-account-group" />
              </q-item-section>

              <q-item-section class="q-pl-md">
                <p class="q-my-none text-subtitle1">Customer</p>
                <h4 class="q-my-none text-weight-bold text-indigo">
                  {{ dashboardData.customerCount }}
                </h4>
              </q-item-section>
            </q-item>
          </div>
          <div class="col-xs-12 col-sm-6 col-md-3">
            <q-item
              class="bg-brown-3 bg-dash-b text-black q-py-lg"
              clickable
              to="/setting/grains"
            >
              <q-item-section class="bg-purple icon-q-d" avatar>
                <q-icon color="white" size="xl" name="mdi-grain" />
              </q-item-section>

              <q-item-section class="q-pl-md">
                <p class="q-my-none text-subtitle1">Grain</p>
                <h4 class="q-my-none text-weight-bold text-indigo">
                  {{ dashboardData.grainCount }}
                </h4>
              </q-item-section>
            </q-item>
          </div>
          <div class="col-xs-12 col-sm-6 col-md-3">
            <q-item
              class="bg-blue-grey-3 bg-dash-b text-black q-py-lg"
              clickable
              to="/grain-cycle/list"
            >
              <q-item-section class="bg-deep-purple-10 icon-q-d" avatar>
                <q-icon color="white" size="xl" name="mdi-recycle" />
              </q-item-section>

              <q-item-section class="q-pl-md">
                <p class="q-my-none text-subtitle1">Grain Season</p>
                <h4 class="q-my-none text-weight-bold text-indigo">
                  {{ dashboardData.grainCycleCount }}
                </h4>
              </q-item-section>
            </q-item>
          </div>
        </div>
        <div class="q-mt-sm">
          <q-btn
            label="Dashboard"
            no-caps
            color="primary"
            target="_blank"
            href="https://app.powerbi.com/view?r=eyJrIjoiZGVhNjI4NDYtOGJiMS00Y2RkLTkyZGEtYzYxNmIzN2U0NDg2IiwidCI6ImQyMWExMDkzLTgwYWUtNDgxOS05NjU1LWFjYTNjZmIxZjBiZiIsImMiOjEwfQ%3D%3D"
          ></q-btn>
        </div>
      </q-card-section>
      <!-- <q-card-section style="padding-top:0;">
        <div class="row q-col-gutter-md">
          <div class=" col-sm-12 col-md-6">
            <q-markup-table flat bordered class="table-bg-q">
              <thead>
                <tr >
                  <th colspan="4" style="background-color: #21ba45;">
                    <div class="row no-wrap items-center">
                      <div class="text-subtitle2 text-white">
                        Top Ten Buyers
                      </div>
                    </div>
                  </th>
                </tr>
                <tr>
                  <th class="text-left bg-white">#</th>
                  <th class="text-left  bg-white">Name</th>
                  <th class="text-left  bg-white">Grain</th>
                  <th class="text-center  bg-white">Quantity</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td class="text-left">1</td>
                  <td class="text-left">Gopi Banjade</td>
                  <td class="text-left">Rice</td>
                  <td class="text-center">90</td>
                </tr>
                <tr>
                  <td class="text-left">2</td>
                  <td class="text-left">Ram Tahapa</td>
                  <td class="text-left">Wheat</td>
                  <td class="text-center">80</td>
                </tr>
                <tr>
                  <td class="text-left">3</td>
                  <td class="text-left">Janak Thapa Magar</td>
                  <td class="text-left">oats</td>
                  <td class="text-center">60</td>
                </tr>
                <tr>
                  <td class="text-left">4</td>
                  <td class="text-left">Gopal dhami</td>
                  <td class="text-left">Rice</td>
                  <td class="text-center">50</td>
                </tr>
                <tr>
                  <td class="text-left">5</td>
                  <td class="text-left">Biki Dhami</td>
                  <td class="text-left">Corn</td>
                  <td class="text-center">40</td>
                </tr>
                <tr>
                  <td class="text-left">6</td>
                  <td class="text-left">Lakhan Chaudhari</td>
                  <td class="text-left">Wheat</td>
                  <td class="text-center">30</td>
                </tr>
                <tr>
                  <td class="text-left">7</td>
                  <td class="text-left">Guddu Rana</td>
                  <td class="text-left">oats</td>
                  <td class="text-center">20</td>
                </tr>
                <tr>
                  <td class="text-left">8</td>
                  <td class="text-left">Newson Shrestha</td>
                  <td class="text-left">Corn</td>
                  <td class="text-center">15</td>
                </tr>
                <tr>
                  <td class="text-left">9</td>
                  <td class="text-left">Anuskha Banjade</td>
                  <td class="text-left">oats</td>
                  <td class="text-center">14</td>
                </tr>
                <tr>
                  <td class="text-left">10</td>
                  <td class="text-left">Janak Thapa Magar</td>
                  <td class="text-left">oats</td>
                  <td class="text-center">10</td>
                </tr>
              </tbody>
            </q-markup-table>
          </div>
          <div class="col-sm-12 col-md-6">
            <q-markup-table flat bordered class="table-bg-q">
              <thead>
                <tr >
                  <th colspan="4" style="background-color: #21ba45;">
                    <div class="row no-wrap items-center">
                      <div class="text-subtitle2 q-ml-md text-white">
                        Top Ten Seller
                      </div>
                    </div>
                  </th>
                </tr>
                <tr>
                  <th class="text-left bg-white">#</th>
                  <th class="text-left  bg-white">Name</th>
                  <th class="text-left  bg-white">Grain</th>
                  <th class="text-center  bg-white">Quantity</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td class="text-left">1</td>
                  <td class="text-left">Amir Giri</td>
                  <td class="text-left">Wheat </td>
                  <td class="text-center">50</td>
                </tr>
                <tr>
                  <td class="text-left">2</td>
                  <td class="text-left">Abril Thapa</td>
                  <td class="text-left">oats </td>
                  <td class="text-center">48</td>
                </tr>
                <tr>
                  <td class="text-left">3</td>
                  <td class="text-left">Alina Chanda</td>
                  <td class="text-left">Rye </td>
                  <td class="text-center">42</td>
                </tr>
                <tr>
                  <td class="text-left">4</td>
                  <td class="text-left">
                    Bibek Banjade
                  </td>
                  <td class="text-left">Corn </td>
                  <td class="text-center">34</td>
                </tr>
                <tr>
                  <td class="text-left">5</td>
                  <td class="text-left">Bisna Shai</td>
                  <td class="text-left">buckwheat</td>
                  <td class="text-center">32</td>
                </tr>
                <tr>
                  <td class="text-left">6</td>
                  <td class="text-left">Chimini Banjade</td>
                  <td class="text-left">Rye</td>
                  <td class="text-center">31</td>
                </tr>
                <tr>
                  <td class="text-left">7</td>
                  <td class="text-left">Newson Shrestha</td>
                  <td class="text-left">buckwheat</td>
                  <td class="text-center">31</td>
                </tr>
                <tr>
                  <td class="text-left">8</td>
                  <td class="text-left">Gopi Kuwar</td>
                  <td class="text-left">Corn</td>
                  <td class="text-center">30</td>
                </tr>
                <tr>
                  <td class="text-left">9</td>
                  <td class="text-left">Dexa Dhami</td>
                  <td class="text-left">Wheat</td>
                  <td class="text-center">29</td>
                </tr>
                <tr>
                  <td class="text-left">10</td>
                  <td class="text-left">Alina Chanda</td>
                  <td class="text-left">Rye </td>
                  <td class="text-center">20</td>
                </tr>
              </tbody>
            </q-markup-table>
          </div>
        </div>
      </q-card-section> -->
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
    const $q = useQuasar();
    let dashboardData = ref({});

    const getDashboardData = async () => {
      try {
        const response = await api.get("dashboard/dashboard");
        dashboardData.value = response.data;
        console.log(dashboardData.value);
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    onMounted(async () => {
      $q.loading.show({});
      await getDashboardData();
      $q.loading.hide({});
    });
    return {
      dashboardData,
    };
  },
});
</script>
