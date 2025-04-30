import { boot } from "quasar/wrappers";
import axios from "axios";

let baseURL = "/api";

if (process.env.DEV) {
  baseURL = "https://localhost:44301/api/";
  // baseURL = "https://localhost:7233/api/";
}

// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
const api = axios.create({ baseURL: baseURL, });
api.interceptors.request.use(function (config) {
  const token = window.localStorage.getItem("token");
  if(token){
  config.headers.common["Authorization"] = `Bearer ${token}`;
  }
else {
  delete config.headers.common["Authorization"];
}
  return config;
}, function (error) {

  return Promise.reject(error);
});
api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error?.response == null) return Promise.reject(error);
    if (error.response.status == 401) {
      localStorage.removeItem("token");
      delete api.defaults.headers.common["Authorization"];
      route.push("/login");
    }
    return Promise.reject(error);
  }
);

export default boot(({ app }) => {
  // for use inside Vue files (Options API) through this.$axios and this.$api

  app.config.globalProperties.$axios = axios;
  // ^ ^ ^ this will allow you to use this.$axios (for Vue Options API form)
  //       so you won't necessarily have to import axios in each vue file

  app.config.globalProperties.$api = api;
  // ^ ^ ^ this will allow you to use this.$api (for Vue Options API form)
  //       so you can easily perform requests against your app's API
});

export { api, baseURL };
