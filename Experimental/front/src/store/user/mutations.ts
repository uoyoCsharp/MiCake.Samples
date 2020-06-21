import { MutationTree } from 'vuex';
import { UserState } from './types';

export const userMutations: MutationTree<UserState> = {

    /**
     *保存当前登录的用户信息
     *
     * @param {*} state
     * @param {{ id: string; name: string; avatar: string; mobile: string; }} userInfo
     */
    saveUserInfo(state, userInfo: { id: string; name: string; avatar: string; mobile: string; }) {
        state.name = userInfo.name;
        state.id = userInfo.id;
        state.avatar = userInfo.avatar;
        state.mobile = userInfo.mobile;
    },

    /**
     *保存当前Token，并标记用户登录成功
     *
     * @param {*} state
     * @param {*} token
     */
    loginSuccess(state,token) {
        state.isLogin = true;
        state.accessToken = token;
        state.lastLoginTime = Date.now();
    }
}