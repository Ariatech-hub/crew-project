<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-card-section style="padding-top: 0px">
        <div>
          <q-toolbar style="padding: 0px">
            <q-toolbar-title>Buyer</q-toolbar-title>
            <q-input
              dense
              debounce="300"
              v-model="filter"
              placeholder="Search"
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
              icon="add"
              to="/customer/new"
            />
          </q-toolbar>
          <q-table
            square
            :rows="customers"
            :pagination="initialPagination"
            :filter="filter"
          >
            <template v-slot:header>
              <tr>
                <th class="text-left">S.No</th>
                <th class="text-left">Name</th>
                <th class="text-left">Email</th>
                <th class="text-center">Phone Number</th>
                <th class="text-center">IsActive</th>
                <th class="text-center">Actions</th>
              </tr>
            </template>
            <template v-slot:body="props">
              <tr :key="props.row.id">
                <td class="text-left">{{ props.rowIndex + 1 }}</td>
                <td class="text-left">{{ props.row.name }}</td>
                <td class="text-left">{{ props.row.email }}</td>
                <td class="text-center">{{ props.row.phoneNumber }}</td>

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
                <td class="text-center">
                  <q-btn
                    unelevated
                    dense
                    outline
                    round
                    size="xs"
                    color="warning"
                    icon="visibility"
                    :to="`/customer/${props.row.id}/view`"
                  >
                    <q-tooltip> View </q-tooltip>
                  </q-btn>
                  <!-- <q-btn
                    unelevated
                    round
                    size="sm"
                    dense
                    outline
                    color="negative"
                    class="q-ml-xs"
                    icon="delete"
                  >
                    <q-tooltip> Delete</q-tooltip>
                  </q-btn> -->
                </td>
              </tr>
            </template>
            <template v-slot:top-right> </template>
          </q-table>
        </div>
      </q-card-section>
    </q-card>
  </q-page>
</template>
<script>
import { defineComponent, ref, onMounted } from "vue";
import { handleError } from "boot/utility";
import { api } from "boot/axios";
import { useRouter, useRoute } from "vue-router";
import { useQuasar } from "quasar";
export default defineComponent({
  setup() {
    const $q = useQuasar();
    let customers = ref([]);
    const getCustomers = async () => {
      try {
        $q.loading.show();
        const response = await api.get("customers");
        customers.value = response.data;
        $q.loading.hide();
      } catch (ex) {
        $q.loading.hide();
        handleError(ex);
      }
    };
    onMounted(async () => {
      try {
        $q.loading.show();
        await getCustomers();
        $q.loading.hide();
      } catch (ex) {
        handleError(ex);
      }
    });
    return {
      customers,
      initialPagination: {
        rowsPerPage: 30,
        // rowsNumber: xx if getting data from a server
      },
      filter: ref(""),
    };
  },
});
</script>
<style>
.q-table__top.relative-position.row.items-center {
  display: none;
}
</style>
