import Vuex, { StoreOptions } from "vuex";
import Vue from "vue";
import { RootState } from './types';
import { user } from "./user/index";

Vue.use(Vuex);

const store: StoreOptions<RootState> = {
    modules: {
        user: user
    }
};

export default new Vuex.Store<RootState>(store);