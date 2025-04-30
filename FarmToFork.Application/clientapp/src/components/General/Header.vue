<template>
  <q-toolbar class="bg-white q-mt-xs">
    <q-toolbar-title class="text-primary">{{ headerName }}</q-toolbar-title>
    <q-space></q-space>
    <div v-for="item in buttons" :key="item">
      <q-btn
        unelevated
        no-caps
        class="q-mr-sm"
        size="sm"
        :color="item.color"
        :label="item.label"
        :icon="item.icon"
        @click="onButtonClick(item)"
      />
    </div>
  </q-toolbar>
</template>

<script>
import { useQuasar } from "quasar";
import { defineComponent, ref, onMounted } from "vue";
export default defineComponent({
  props: {
    headerNameProps: String,
    buttons: Array,
    filterProps: String,
  },
  setup(context, props) {
    let headerName = ref(null);
    let buttons = ref([]);
    let filter = ref("");
    const onButtonClick = function (item) {
      props.emit("onButtonClick", item);
    };
    const onSearch = function (value) {
      props.emit("onSearch", value);
    };
    onMounted(async () => {
      headerName.value = context.headerNameProps;
      buttons.value = context.buttons;
      filter.value = context.filterProps;
    });
    return {
      headerName,
      onButtonClick,
      filter,
      onSearch,
    };
  },
});
</script>
