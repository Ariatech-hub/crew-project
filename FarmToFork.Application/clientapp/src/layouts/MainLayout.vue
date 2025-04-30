<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn
          flat
          round
          icon="menu"
          aria-label="Menu"
          width="100%"
          size="md"
          color="white"
          @click="toggleLeftDrawer = !toggleLeftDrawer"
        />
        <q-toolbar-title>
          <div class="text-h6 text-white" style="font-size: 18px">
            Welcome {{ username() }}
          </div>
        </q-toolbar-title>
        <div>
          <q-btn
            outline
            no-caps
            flat
            size="md"
            color="white"
            style="font-size: 12px"
            label="Change Password"
            class="text-light bg-green-six"
            clickable
            v-close-popup
            v-on:click="changePasswordDialogOpen"
            icon="key"
          >
          </q-btn>
          <q-btn
            outline
            no-caps
            flat
            size="md"
            style="font-size: 12px"
            color="white"
            label="Logout"
            class="text-light bg-green-six q-ml-sm"
            clickable
            v-close-popup
            v-on:click="logout"
            icon="mdi-account-cog"
          >
          </q-btn>
        </div>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="toggleLeftDrawer"
      show-if-above
      :width="300"
      :breakpoint="500"
      side="left"
      style="border-right: 1px solid rgb(211 227 253)"
      borderless
    >
      <q-list style="border-bottom: 1px solid rgb(211 227 253)">
        <q-item
          class="q-item q-item-type row no-wrap text-center q-ml-xl q-mb-md"
        >
          <img
            src="./../../public/logo.png"
            class="q-mt-md"
            alt=""
            style="width: 120px; margin-left: 10px"
        /></q-item>
      </q-list>

      <q-list style="min-width: 100px; padding-top: 10px" color="white">
        <template v-for="item in menus()" :key="item">
          <q-expansion-item
            v-if="item.children.length > 0 && !item.isLink"
            :key="item"
            expand-separator
            group="somegroup"
            :icon="item.icon"
            :label="item.name"
          >
            <q-list v-for="childItem in item.children" :key="childItem.id">
              <q-item
                style="padding-left: 72px"
                dense
                clickable
                v-if="!childItem.isLink"
                v-close-popup
                :to="childItem.url"
              >
                <q-item-section>{{ childItem.name }}</q-item-section>
              </q-item>
            </q-list>
          </q-expansion-item>
          <q-item v-else :key="item.id" clickable v-close-popup :to="item.url">
            <q-item-section avatar v-if="!item.isLink">
              <q-icon :name="item.icon" />
            </q-item-section>
            <q-item-section v-if="!item.isLink">{{ item.name }}</q-item-section>
          </q-item>
        </template>
      </q-list>
    </q-drawer>

    <q-dialog v-model="changePasswordDialog" persistent position="top">
      <q-card class="q-mt-lg" square>
        <q-toolbar>
          <q-toolbar-title color="primary">Change Password</q-toolbar-title>
          <q-btn color="dark" flat round dense icon="close" v-close-popup />
        </q-toolbar>
        <q-separator></q-separator>
        <q-card-section>
          <q-form @submit="onChangePassword">
            <q-input
              v-model="changePassword.oldPassword"
              outlined
              lazy-rules
              label="Old Password *"
              dense
              square
              :rules="[
                (val) =>
                  (val && val.length > 0) || ' Old Password cannot be empty',
              ]"
              :type="oldConfirmPwd ? 'password' : 'text'"
            >
              <template v-slot:append>
                <q-icon
                  :name="oldConfirmPwd ? 'visibility_off' : 'visibility'"
                  class="cursor-pointer"
                  @click="oldConfirmPwd = !oldConfirmPwd"
                />
              </template>
            </q-input>
            <q-input
              v-model="changePassword.password"
              outlined
              lazy-rules
              square
              label="Password *"
              dense
              :rules="[
                (val) => (val && val.length > 0) || 'Password cannot be empty',
              ]"
              :type="isPwd ? 'password' : 'text'"
            >
              <template v-slot:append>
                <q-icon
                  :name="isPwd ? 'visibility_off' : 'visibility'"
                  class="cursor-pointer"
                  @click="isPwd = !isPwd"
                />
              </template>
            </q-input>
            <q-input
              v-model="changePassword.confirmPassword"
              outlined
              lazy-rules
              label="Confirm Password *"
              square
              dense
              :rules="[
                (val) =>
                  (val && val.length > 0) || 'Confirm Password cannot be empty',
              ]"
              :type="isConfirmPwd ? 'password' : 'text'"
            >
              <template v-slot:append>
                <q-icon
                  :name="isConfirmPwd ? 'visibility_off' : 'visibility'"
                  class="cursor-pointer"
                  @click="isConfirmPwd = !isConfirmPwd"
                />
              </template>
            </q-input>
            <q-btn
              color="primary"
              class="bg-primary q-mt-sm"
              type="submit"
              label="Submit"
            />
          </q-form>
        </q-card-section>
      </q-card>
    </q-dialog>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import { defineComponent, ref, onMounted } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import { api } from "boot/axios";
import { useQuasar } from "quasar";
import { handleError } from "src/boot/utility";

export default defineComponent({
  name: "MainLayout",
  setup() {
    const leftDrawerOpen = ref(false);
    const store = useStore();
    const router = useRouter();
    const $q = useQuasar();
    const left = ref(true);
    let isPwd = ref(false);
    let isConfirmPwd = ref(false);
    let oldConfirmPwd = ref(false);

    const changePasswordDialog = ref(false);

    let changePassword = ref({
      password: "",
      confirmPassword: "",
      oldPassword: "",
    });

    const logout = function () {
      store.dispatch("auth/logout");
      $q.notify({
        type: "negative",
        message: `Logged Out Successfully!`,
      });
      router.push("/login");
    };
    const changePasswordDialogOpen = () => {
      $q.loading.show();
      isPwd.value = true;
      isConfirmPwd.value = true;
      oldConfirmPwd.value = true;
      changePassword.value.password = "";
      changePassword.value.confirmPassword = "";
      changePasswordDialog.value = true;
      $q.loading.hide();
    };

    const onChangePassword = async () => {
      try {
        $q.loading.show();
        const response = await api.post(
          "change-password",
          changePassword.value
        );
        $q.notify({
          type: "positive",
          message: `${response.data}`,
        });
        changePasswordDialog.value = false;
        $q.loading.hide();
      } catch (error) {
        changePasswordDialog.value = false;
        $q.loading.hide();
        handleError(error);
      }
    };

    onMounted(async () => {
      $q.loading.show();
      $q.loading.hide();
    });

    return {
      toggleLeftDrawer: ref(false),
      left,
      logout,
      menus() {
        return store.state.auth.user.menuTree;
      },
      role() {
        return store.state.auth.user.role;
      },
      username() {
        return store.state.auth.user.username;
      },
      changePasswordDialog,
      changePasswordDialogOpen,
      changePassword,
      onChangePassword,
      isPwd,
      isConfirmPwd,
      oldConfirmPwd,
    };
  },
});
</script>
<style></style>
