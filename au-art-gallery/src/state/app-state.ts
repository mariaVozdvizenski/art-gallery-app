

export class AppState {
    constructor() {

    }
    public readonly baseUrl = 'https://localhost:5001/api/v1.0/';

    // JSON Web Token to keep track of logged in status
    public jwt: string | null = null;
}