<template>
  <q-page>
     <q-card flat class="no-border-radius">
        <q-toolbar >
          <q-toolbar-title >Communities</q-toolbar-title>
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
            @click="openNewcommunityDialog"
            icon="add"
          />
        </q-toolbar>
        <q-card-section  style="padding-top:0;">
        <q-table
          square
          :rows="communities"
          :loading="tableLoading"
          :pagination="initialPagination"
          :filter="filter"
        >
          <template v-slot:header>
            <tr>
              <th class="text-left" style="padding-left: 10px">S.No</th>
              <th class="text-left">Name</th>
              <th class="text-left">Nepali Name</th>
              <th class="text-left">Code</th>
              <th class="text-center">Order Number</th>

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
              <td class="text-left">{{ props.row.orderNo }}</td>

              <td class="text-center">
                <q-icon
                  name="check"
                  outline
                  color="positive"
                  class="active-icon-q"
                  size="xs"
                  v-if="props.row.isActive"
                />
                <q-icon
                  name="highlight_off"
                  color="negative"
                  outline
                  class="active-icon-q"
                  dense
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
                  @click="opencommunityEditDialog(props.row)"
                >
                  <q-tooltip> Edit </q-tooltip>
                </q-btn>
                <q-btn
                  unelevated
                  round
                  outline
                  dense
                  class="q-ml-xs"
                  size="xs"
                  color="negative"
                  icon="mdi-delete"
                  @click="deletecommunity(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
          <template v-slot:top-right> </template>
        </q-table>

        <q-dialog v-model="communityDialog" persistent position="top" >
          <q-card class="q-mt-lg"
            square
           
          >
            <q-toolbar>
              <q-toolbar-title >{{
                dialogName
              }}</q-toolbar-title>
              <q-btn
                color="primary"
                flat
                round
                dense
                icon="close"
                v-close-popup
              />
            </q-toolbar>
            <q-separator spaced=""/>
            <q-card-section class="form-card" style="padding-top:15px;">
              <q-form @submit="onSubmit">
                <q-input
                  v-model="community.name"
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
                  v-model="community.nepaliName"
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
                  v-model="community.code"
                  outlined
                  lazy-rules
                  label="Code "
                  dense
                >
                </q-input>
                <q-input
                  class="q-mt-md"
                  v-model="community.orderNo"
                  outlined
                  type="number"
                  label="Order Number "
                  dense
                >
                </q-input>
                <q-checkbox
                  class="q-mt-md"
                  v-if="community.id"
                  v-model="community.isActive"
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
import { handleError } from "boot/utility";
import { useQuasar } from "quasar";

export default defineComponent({
  setup() {
    let communities = ref([]);
    const $q = useQuasar();
    let tableLoading = ref(false);
    let community = ref({
      id: 0,
      name: null,
      nepaliName: null,
      code: null,
      isActive: false,
      orderNo: null,
    });
    let communityDialog = ref(false);
    let dialogName = ref(null);
    let communityNewDialog = ref(false);
    let communityEditDialog = ref(false);
    const getcommunities = async () => {
      try {
        const response = await api.get("setting/communities");
        communities.value = response.data;
      } catch (error) {
        $q.loading.hide({});
        handleError(error);
      }
    };
    const openNewcommunityDialog = () => {
      $q.loading.show({});
      communityNewDialog.value = true;
      community.value.id = 0;
      community.value.name = null;
      community.value.nepaliName = null;
      community.value.code = null;
      community.value.orderNo = null;
      dialogName.value = "New Community";
      communityDialog.value = true;
      $q.loading.hide();
    };
    const opencommunityEditDialog = (selectedcommunity) => {
      $q.loading.show({});
      community.value.id = selectedcommunity.id;
      community.value.name = selectedcommunity.name;
      community.value.isActive = selectedcommunity.isActive;
      community.value.nepaliName = selectedcommunity.nepaliName;
      community.value.code = selectedcommunity.code;
      community.value.orderNo = selectedcommunity.orderNo;
      dialogName.value = "Update Community";
      communityDialog.value = true;
      $q.loading.hide();
    };
    const deletecommunity = async (community) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the community ${community.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/community/delete", {
              id: community.id,
            });
            $q.notify({
              type: "positive",
              message: `${response.data}`,
            });
            await getcommunities();
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
      let response = "";
      if (community.value.orderNo == "") {
        community.value.orderNo = null;
      }
      try {
        if (community.value.id === 0) {
          response = await api.post(
            "setting/community/insert",
            community.value
          );
        } else {
          response = await api.post(
            "setting/community/update",
            community.value
          );
        }

        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getcommunities();
        communityDialog.value = false;
      } catch (error) {
        handleError(error);
      } finally {
        $q.loading.hide();
      }
    };

    onMounted(async () => {
      $q.loading.show({});
      await getcommunities();
      $q.loading.hide({});
    });
    return {
      communities,
      community,
      communityNewDialog,
      communityEditDialog,
      openNewcommunityDialog,
      opencommunityEditDialog,
      deletecommunity,
      tableLoading,
      onSubmit,
      dialogName,
      communityDialog,
      initialPagination: {
        rowsPerPage: 30,
        // rowsNumber: xx if getting data from a server
      },
      filter: ref(""),
    };
  },
});
</script>
