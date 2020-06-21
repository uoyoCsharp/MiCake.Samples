export interface UserState {
    id: string;
    name: string;
    avatar: string;
    mobile: string;
    isLogin: boolean;
    accessToken: string;
    lastLoginTime?: number;
}