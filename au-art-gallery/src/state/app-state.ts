

export class AppState {

    constructor() {

    }

    public readonly baseUrl = 'https://artgallery2020.azurewebsites.net/api/v1.0/';
    public readonly baseUrlForUploads = 'https://localhost:5001/api/v1.0/';
    // JSON Web Token to keep track of logged in status
    public jwt: string | null = null;
    public userName: string | null = null;
    public userRoles: string[] | null = null;
    public appUserId: string | null = null;

    UserIsAdmin(): boolean {

        if (this.userRoles != null) {
            return this.userRoles?.some(r => r.indexOf("admin") !== -1);
        }
        
        return false;
    }
}