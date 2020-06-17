import Vue from 'vue'
import App from './App.vue'
import { HttpClient, JwtTokenIntercepter, AutoDomainIntercepter } from "@/common/httpClient";
import { vuexStore } from "./store";
import { ServerUrl } from "@/common/environment";


//配置全局Http请求
var urlIntercept = new AutoDomainIntercepter((url) => ServerUrl);
HttpClient.intercepters.push(urlIntercept);
let httpClient = new HttpClient();

Vue.config.productionTip = false;
Vue.prototype.$httpClient = httpClient;

let vueApp = new App({
    store: vuexStore
});
vueApp.$mount();

