import { createApp } from 'vue'
import App from './App.vue'
import vClickOutside from "click-outside-vue3"
import RButton from "./components/base/RButton.vue";
import RDialog from "./components/base/RDialog.vue";
import RInput from "./components/base/RInput.vue";
import RCombobox from "./components/base/RCombobox.vue";

import RResource from './js/RResource';
import RHelper from './js/RHelper';
import * as ApiConFig from "./js/ApiConfig.js";
import router from './routers/router';
import { createStore } from "vuex";
import employee from "./store/employee/index";
import apps from "./store/app/index";
import department from "./store/department/index";
import customer from "./store/customer/index";

import "./assets/font/icofont/icofont.min.css"

const app = createApp(App);

app.component("r-button", RButton);
app.component("r-dialog", RDialog);
app.component("r-input", RInput);
app.component("r-combobox",RCombobox);
app.use(vClickOutside);

app.use(router);


app.config.globalProperties.$RResource = RResource;
app.config.globalProperties.$ApiConFig = ApiConFig;
app.config.globalProperties.$RHelper = RHelper;

const stores = createStore({
    modules: {
      employee,
      apps,
      department,
      customer,
    },
});

app.use(stores);


app.mount('#app')
