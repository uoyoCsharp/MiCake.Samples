import Vue from 'vue'
import App from './App.vue'
import { HttpClient, JwtTokenIntercepter, AutoDomainIntercepter } from "@/common/httpClient";
import VuexStore from "./store";
import { ServerUrl } from "@/common/environment";


//配置全局Http请求
var urlIntercept = new AutoDomainIntercepter((url) => ServerUrl);
HttpClient.intercepters.push(urlIntercept);

HttpClient.httpErrorFilter = (err)=>{
    console.log('httpClient 请求失败');
    uni.navigateTo({url:'/pages/no-network'});
};

let httpClient = new HttpClient();

Vue.config.productionTip = false;
Vue.prototype.$httpClient = httpClient;

let vueApp = new App({
    store: VuexStore
});
vueApp.$mount();

