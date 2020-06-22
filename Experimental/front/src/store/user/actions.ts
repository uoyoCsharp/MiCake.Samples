import { ActionTree } from 'vuex';
import { UserState } from './types';
import { RootState } from '../types';

export const actions: ActionTree<UserState, RootState> = {
    async loginSuccess({ commit }, token) {
        if (token != null || token != '') {
            await uni.setStorage({
                key: 'accessToken',
                data: token
            });

            commit('loginSuccess', token);
        }
    },
    async loginOut({ commit }){
        await uni.removeStorage({key: 'accessToken'});
        commit('loginOut');
    }
}