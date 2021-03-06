import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IUserPaymentMethod} from 'domain/IUserPaymentMethod';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';



@autoinject
export class UserPaymentMethodService{
    
    constructor(private appState: AppState, private httpClient: HttpClient){
        this.httpClient.baseUrl = this.appState.baseUrl;

    }

    private readonly _baseUrl = 'UserPaymentMethods';

    async getUserPaymentMethods(): Promise<IFetchResponse<IUserPaymentMethod[]>> {

        try {
            const response = await this.httpClient
                .fetch(this._baseUrl, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IUserPaymentMethod[];
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getUserPaymentMethod(id: string): Promise<IFetchResponse<IUserPaymentMethod>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IUserPaymentMethod;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

  
}