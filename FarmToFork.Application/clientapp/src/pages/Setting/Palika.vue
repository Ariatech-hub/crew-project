<template>
  <q-page>
    <q-card flat class="no-border-radius">
        <q-toolbar >
        <q-toolbar-title >Palikas</q-toolbar-title>
        <q-input
              dense
              debounce="300"
              v-model="filter"
              placeholder="Search"
              outlined
            >
              <template v-slot:append>
                <q-icon name="search" />
              </template>
            </q-input>
      </q-toolbar>
      <q-card-section style="padding-top:0px">
        <q-table
          square
          
          :rows="palikas"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left"  style="padding-left:10px">S.No</th>
              <th class="text-left">District</th>
              <th class="text-left">Name</th>
              <th class="text-left">Nepali Name</th>
              <th class="text-center">Active</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">{{ props.rowIndex + 1 }}</td>
              <td class="text-left">{{ props.row.districtName }}</td>
              <td class="text-left">{{ props.row.name }}</td>
              <td class="text-left">{{ props.row.nepaliName }}</td>
              <td class="text-center">
                <q-toggle
                  v-model="props.row.isActive"
                  color="positive"
                  size="xs"
                  dense
                  @update:model-value="onToggleClicked(props.row)"
                />
              </td>
            </tr>
          </template>
          <template v-slot:top-right>
           
          </template>
        </q-table>
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
    let palikas = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    const getpalikas = async () => {
      try {
        const response = await api.get("general/palikas");
        palikas.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };

    const onToggleClicked = async (evt) => {
      $q.loading.show({});
      try {
        let response = await api.post(`general/update-palikass`, {
          id: evt.id,
        });
        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide({});
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getpalikas();
      $q.loading.hide({});
    });
    return {
      palikas,
      tableLoading,
      initialPagination: {
        rowsPerPage: 30,
      },
      filter: ref(""),
      onToggleClicked,
    };
  },
});
</script>
