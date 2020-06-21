import { UserState } from './types'
import { Module } from 'vuex'
import { RootState } from '../types'
import { userMutations } from "./mutations";
import { actions } from "./actions";

const state: UserState = {
    id: '',
    name: '',
    avatar: '',
    mobile: '',
    isLogin: false,
    accessToken: '',
    lastLoginTime: undefined,
}
let namespaced = true;

export const user: Module<UserState, RootState> = {
    namespaced: namespaced,
    state: state,
    mutations: userMutations,
    actions: actions
}
export default state