<template>
  <div class="bg-login-height">
    <div class="at-login" id="q-app">
      <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-9 col-lg-9 q-xs-none">
          <div class="full-width login-img">
            <img src="./../../public/login.png"    alt="login-img" />
          </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
          <div class="login-content">
            <q-card flat rounded class="q-px-lg q-sm-no-padding login-cart-w">
              <q-card-section>
                <div class="text-green text-h5 text-weight-bold q-pb-md">
                  Sign in
                </div>
                <div class="text-grey-8">
                  Sign in below to access your account
                </div>
              </q-card-section>
              <q-card-section class="text-center">
                <q-form @submit="onLoginFormSubmit" class="form-login-q">
                  <q-input
                    v-model="user.username"
                    label="Username"
                    outlined
                    class="q-login-f"



                    dense
                    :rules="[(val) => !!val || 'Username is required']"
                  >
                    <template v-slot:prepend class="sssdfasf">
                      <q-icon name="mdi-account-box" />
                    </template>
                  </q-input>

                  <q-input
                    :type="isPwd ? 'password' : 'text'"
                    class="q-mt-xs q-login-f"
                    v-model="user.password"
                    label="Password"
                    outlined
                    dense
                    :rules="[(val) => !!val || 'Password is required']"
                  >
                    <template v-slot:prepend>
                      <q-icon name="mdi-lock" />
                    </template>
                    <template v-slot:append>
                      <q-icon
                        :name="isPwd ? 'visibility_off' : 'visibility'"
                        class="cursor-pointer"
                        @click="isPwd = !isPwd"
                      />
                    </template>
                  </q-input>
                  <q-btn
                    :disabled="formPosting"
                    :loading="formPosting"
                    type="submit"
                    color="green"
                    size="md"
                    label="Sign in"
                    no-caps
                    class="full-width l-g-btn"
                    square
                  ></q-btn>
                </q-form>
              </q-card-section>
            </q-card>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent, ref, onMounted } from "vue";
import { handleError } from "boot/utility";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import { useQuasar } from "quasar";

export default defineComponent({
  name: "PageIndex",
  setup() {
    const store = useStore();
    const router = useRouter();
    const $q = useQuasar();

    let user = ref({
      username: "",
      password: "",
    });
    let formPosting = ref(false);

    const onLoginFormSubmit = async () => {
      try {
        formPosting.value = true;
        await store.dispatch("auth/login", user.value);
        $q.notify({
          type: "positive",
          message: `Logged In Successfully!`,
        });
        router.push("/dashboard");
      } catch (ex) {
        formPosting.value = false;
        handleError(ex);
      }
    };

    return {
      isPwd: ref(true),
      user,
      onLoginFormSubmit,
      formPosting,
    };
  },
});
</script>

<style>
.form-login-q .q-field--dense .q-field__control {
  height: 50px;
  background: white;
}
.form-login-q .q-field--dense .q-field__marginal {
  height: 50px;
  background: white;
}
.l-g-btn {
  height: 50px;
}
.sssdfasf {
  background-color: red;
}
.q-field--outlined .q-field__control:hover:before {
  border-color: #4caf50;
}
body {
  overflow: hidden;
}
</style>
