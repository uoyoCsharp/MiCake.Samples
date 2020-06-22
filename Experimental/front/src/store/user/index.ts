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
    isLogin: '' || uni.getStorageSync('accessToken') ? true : false,
    accessToken: '' || uni.getStorageSync('accessToken'),
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