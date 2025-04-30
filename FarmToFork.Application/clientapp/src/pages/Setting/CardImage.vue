<template>
  <q-page>
    <q-card flat >
      
        <q-toolbar>
          <q-toolbar-title>Card Image</q-toolbar-title>
          <q-btn
            unelevated
            size="ms"
            color="light-green-8"
           
            label="New"
            icon="add"
            @click="onNewCardImage()"
          />
        </q-toolbar>
        <q-card-section style="padding-top:0;">
        <q-table square :rows="cardImages" :pagination="initialPagination">
          <template v-slot:header>
            <tr>
              <th class="text-left">S.No</th>
              <th class="text-left">Title</th>
              <th >Actions</th>
            </tr>
          </template>
          <template v-slot:body="props">
            <tr :key="props.row.id">
              <td class="text-left">{{ props.rowIndex + 1 }}</td>
              <td>{{ props.row.name }}</td>
              <td class="text-center">
                <q-btn
                  unelevated
                  round
                  dense
                  outline
                  size="xs"
                  color="primary"
                  icon="mdi-pencil"
                  @click="onUpdateCardImage(props.row)"
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
                  @click="deleteCardImage(props.row)"
                >
                  <q-tooltip> Delete </q-tooltip>
                </q-btn>
              </td>
            </tr>
          </template>
        </q-table>

        
      </q-card-section>

      <q-dialog v-model="openDialog" persistent position="top">
        <q-card class="q-mt-lg">
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
          <q-separator spaced="" />
          <q-card-section>
            <q-form @submit="onSubmit">
              <div>
                <q-input
                  label="Name *"
                  square
                  dense
                  outlined
                  v-model="cardImage.name"
                  lazy-rules
                  :rules="[
                    (val) => (val && val.length > 0) || 'Name is required',
                  ]"
                ></q-input>
              </div>
              <div>
                <q-file
                  label="Upload File *"
                  square
                  dense
                  outlined
                  lazy-rules
                  v-model="cardImage.fileName"
                  :rules="[(val) => val !== null || 'File is required']"
                  accept=".png, .jpg, .jpeg "
                >
                  <template v-slot:prepend>
                    <q-icon name="attach_file" />
                  </template>
                </q-file>
              </div>
              <div>
                <q-img
                  :src="cardImage.filePath"
                  spinner-color="white"
                  style="height: 140px; max-width: 200px"
                  v-if="cardImage.id != 0"
                />
              </div>
           
              <div>
                <q-btn label="Submit" color="primary" type="submit" />
              </div>
            </q-form>
          </q-card-section>
        </q-card>
      </q-dialog>
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
    let cardImages = ref([]);
    const $q = useQuasar();
    let openDialog = ref(false);
    let cardImage = ref({
      id: 0,
      name: "",
      fileName: "",
      filePath: "",
    });
    let dialogName = ref("");
    const getCardImages = async () => {
      try {
        $q.loading.show();
        const response = await api.get("setting/card-images");
        cardImages.value = response.data;
        $q.loading.hide();
      } catch (error) {
        $q.loading.hide();
        handleError();
      }
    };
    const onNewCardImage = () => {
      openDialog.value = true;
      cardImage.value.name = "";
      cardImage.value.fileName = "";
      cardImage.value.id = 0;
      dialogName.value = "Add Card Image";
    };
    const onSubmit = async () => {
      try {
        let formData = new FormData();
        let response = "";
        if (cardImage.value.id == 0) {
          formData.append("Title", cardImage.value.name);
          formData.append("Image", cardImage.value.fileName);
          response = await api.post("setting/card-image/add", formData, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          });
        } else {
          formData.append("Id", cardImage.value.id);
          formData.append("Title", cardImage.value.name);
          formData.append("Image", cardImage.value.fileName);
          response = await api.post("setting/card-image/update", formData, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          });
        }
        openDialog.value = false;
        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        await getCardImages();
      } catch (error) {
        handleError(error);
      }
    };
    const onUpdateCardImage = (card) => {
      openDialog.value = true;
      cardImage.value.id = card.id;
      cardImage.value.name = card.name;
      cardImage.value.fileName = card.fileName;
      cardImage.value.filePath = card.filePath;
      dialogName.value = "Edit Card Image";
    };
    const deleteCardImage = async (card) => {
      try {
        $q.dialog({
          title: "Confirm",
          message: `Are you sure you want to delete the community ${card.name} ?`,
          cancel: true,
          persistent: true,
        }).onOk(async () => {
          $q.loading.show();
          try {
            let response = await api.post("setting/card-image/delete", {
              id: card.id,
            });
            $q.notify({
              type: "positive",
              message: `The card image ${response.data.name} has been deleted`,
            });
            await getCardImages();
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
    onMounted(async () => {
      $q.loading.show({});
      await getCardImages();
      $q.loading.hide({});
    });
    return {
      cardImages,
      filter: ref(""),
      initialPagination: {
        rowsPerPage: 30,
      },
      openDialog,
      dialogName,
      cardImage,
      onNewCardImage,
      onSubmit,
      onUpdateCardImage,
      deleteCardImage,
    };
  },
});
</script>
