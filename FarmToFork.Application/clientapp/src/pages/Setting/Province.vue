<template>
  <q-page>
    <q-card flat class="no-border-radius">
   
        <q-toolbar >
          <q-toolbar-title >Provinces</q-toolbar-title>

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
        <q-card-section style="padding-top:0;">
        <q-table
          square
          class="q-pa-none"
          :rows="provinces"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left" style="padding-left: 10px">S.No</th>
              <th class="text-left">Name</th>
              <th class="text-left">Nepali Name</th>
              <th class="text-center">Active</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">{{ props.rowIndex + 1 }}</td>
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
          <template v-slot:top-right> </template>
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
    let provinces = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    const getProvinces = async () => {
      try {
        const response = await api.get("general/provinces");
        provinces.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };

    const onToggleClicked = async (evt) => {
      $q.loading.show({});
      try {
        let response = await api.post(`general/update-province`, {
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
      await getProvinces();
      $q.loading.hide({});
    });
    return {
      provinces,
      tableLoading,
      initialPagination: {
        rowsPerPage: 7,
      },
      filter: ref(""),
      onToggleClicked,
    };
  },
});
</script>
<style>
.q-table__top.relative-position.row.items-center {
    display: none;
}
</style>
