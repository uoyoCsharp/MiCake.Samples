import Vue from 'vue'
import App from './App.vue'
import { HttpClient, JwtTokenIntercepter, AutoDomainIntercepter, MiCakeResponseNoAuthorizeFilter } from "@/common/httpClient";
import VuexStore from "./store";
import { ServerUrl } from "@/common/environment";


/* 添加全局Http请求 */
//url拦截
var urlIntercept = new AutoDomainIntercepter((url) => ServerUrl);
HttpClient.intercepters.push(urlIntercept);
//全局网络失败
HttpClient.httpErrorFilter = (err) => {
    console.log('httpClient 请求失败');
    uni.navigateTo({ url: '/pages/no-network' });
};
//全局jwt header
var jwtIntercept = new JwtTokenIntercepter(url => {
    if (VuexStore.state.user.isLogin) {
        return VuexStore.state.user.accessToken;
    }
    return null;
});
HttpClient.intercepters.push(jwtIntercept);
//全局权限处理
let authorizeFilter = new MiCakeResponseNoAuthorizeFilter(() => {
    uni.navigateTo({
        url: '/pages/no-authorize'
    });
});
HttpClient.resultFilters.push(authorizeFilter);

let httpClient = new HttpClient();

Vue.config.productionTip = false;
Vue.prototype.$httpClient = httpClient;
let vueApp = new App({
    store: VuexStore
});
vueApp.$mount();

