import Vue from "vue";
import { HttpClient } from "@/common/httpClient";

declare module 'vue/types/vue' {
    // 可以使用 `VueConstructor` 接口
    // 来声明全局 property
    interface Vue {
        $httpClient: HttpClient
    }
}