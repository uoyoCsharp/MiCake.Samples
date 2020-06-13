import Vue from 'vue'
import App from './App.vue'
import MiCakeRequest from "./common/request";

Vue.config.productionTip = false;
Vue.prototype.$httpClient = new MiCakeRequest();

let vueApp = new App();
vueApp.$mount();

