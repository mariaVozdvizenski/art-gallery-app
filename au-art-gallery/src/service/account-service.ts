
import {autoinject} from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { HttpClient } from 'aurelia-fetch-client';
import { IResponse } from 'domain/IResponse';
import { ILoginResponse } from 'domain/ILoginResponse';
import { IRegisterResponse } from 'domain/IRegisterResponse';


@autoinject
export class AccountService {

    constructor(private appState: AppState, private httpClient: HttpClient) {
    }

    async login(email: string, password: string): Promise<IResponse<ILoginResponse>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient.post('account/login', JSON.stringify({
                email: email,
                password: password,
            }), {
                cache: 'no-store'
            });

            // happy case
            if (response.status >= 200 && response.status < 300){
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data as ILoginResponse
                }
            } 

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }
    async register (email: string, password: string): Promise<IResponse<ILoginResponse>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient.post('account/register', JSON.stringify({
                email: email,
                password: password}), 

                {cache: 'no-store'});

            // happy case
            if (response.status >= 200 && response.status < 300){
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data as ILoginResponse
                }
            } 

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

}