<template>
  <q-page>
    <q-card flat class="no-border-radius">
      <q-card-section style="padding-top:0;">
        <q-toolbar style="padding:0px;">
          <q-toolbar-title >Districts</q-toolbar-title>
          <q-input
            dense
            debounce="300"
            v-model="filter"
            squared
            placeholder="Search"
            outlined
          >
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
        </q-toolbar>
        <q-table
          square
          :rows="districts"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left" style="padding-left:10px;">S.No</th>
              <th class="text-left">Province</th>
              <th class="text-left">Name</th>
              <th class="text-left">Nepali Name</th>
              <th class="text-center">Active</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">{{ props.rowIndex + 1 }}</td>
              <td class="text-left">{{ props.row.provinceName }}</td>
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
    let districts = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    const getdistricts = async () => {
      try {
        const response = await api.get("general/districts");
        districts.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };

    const onToggleClicked = async (evt) => {
      $q.loading.show({});
      try {
        let response = await api.post(`general/update-district`, {
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
      await getdistricts();
      $q.loading.hide({});
    });
    return {
      districts,
      tableLoading,
      initialPagination: {
        rowsPerPage: 30,
        // rowsNumber: xx if getting data from a server
      },
      filter: ref(""),
      onToggleClicked,
    };
  },
});
</script>
