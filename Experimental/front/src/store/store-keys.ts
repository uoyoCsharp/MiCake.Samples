import { UserState } from './user/types';

export class UserStoreKey {

    public static nameSpace = 'user';

    public static actions_loginSuccess = 'loginSuccess';
    public static actions_loginOut = 'loginOut';

    public static mutations_saveUserInfo = 'saveUserInfo';

    public static state_isLogin='isLogin';

    public static state_accessToken = 'accessToken';
}