export interface ILoginResponse {
    token: string;
    status: string;
    userName: string;
    userRoles: string[];
    appUserId: string;
}
