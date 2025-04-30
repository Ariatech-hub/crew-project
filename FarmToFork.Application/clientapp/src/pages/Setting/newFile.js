import { defineComponent, onMounted, ref } from "vue";
import { api } from "boot/axios";
import { handleError } from "boot/utility";
import { useQuasar } from "quasar";

export default defineComponent({
components: {
Header,
},
setup() {
let grains = ref([]);
const $q = useQuasar();
let tableLoading = ref(false);
let buttons = ref([]);
let grain = ref({
id: 0,
name: null,
nepaliName: null,
code: null,
isActive: false,
orderNumber: null,
});
let grainDialog = ref(false);
let dialogName = ref(null);
let grainNewDialog = ref(false);
let grainEditDialog = ref(false);
const getgrains = async () => {
try {
const response = await api.get("setting/grains");
grains.value = response.data;
} catch (error) {
$q.loading.hide({});
handleError(error);
}
};
const openNewgrainDialog = () => {
$q.loading.show({});
grainNewDialog.value = true;
grain.value.id = 0;
grain.value.name = null;
grain.value.nepaliName = null;
grain.value.code = null;
grain.value.orderNumber = null;
dialogName.value = "New Grain";
grainDialog.value = true;
$q.loading.hide();
};
const opengrainEditDialog = (selectedgrain) => {
$q.loading.show({});
grain.value.id = selectedgrain.id;
grain.value.name = selectedgrain.name;
grain.value.isActive = selectedgrain.isActive;
grain.value.nepaliName = selectedgrain.nepaliName;
grain.value.code = selectedgrain.code;
grain.value.orderNumber = selectedgrain.orderNumber;
dialogName.value = "Update grain";
grainDialog.value = true;
$q.loading.hide();
};
const deletegrain = async (grain) => {
try {
$q.dialog({
title: "Confirm",
message: `Are you sure you want to delete the grain ${grain.name} ?`,
cancel: true,
persistent: true,
}).onOk(async () => {
$q.loading.show();
try {
let response = await api.post("setting/grain/delete", {
id: grain.id,
name: grain.name,
});
$q.notify({
type: "positive",
message: `${response.data}`,
});
await getgrains();
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
if (grain.value.orderNumber == "") {
grain.value.orderNumber = null;
}
try {
if (grain.value.id === 0) {
response = await api.post("setting/grain/insert", grain.value);
} else {
response = await api.post("setting/grain/update", grain.value);
}

$q.notify({
type: "positive",
message: `${response.data}`,
});
await getgrains();
grainDialog.value = false;
} catch (error) {
handleError(error);
} finally {
$q.loading.hide();
}
};

onMounted(async () => {
$q.loading.show({});
await getgrains();
$q.loading.hide({});
});
return {
grains,
grain,
grainNewDialog,
grainEditDialog,
openNewgrainDialog,
opengrainEditDialog,
deletegrain,
tableLoading,
onSubmit,
dialogName,
grainDialog,
initialPagination: {
rowsPerPage: 30,
// rowsNumber: xx if getting data from a server
},
filter: ref(""),
};
},
});
