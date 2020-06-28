import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IPaymentMethodCreate} from 'domain/IPaymentMethodCreate';
import {IPaymentMethod} from 'domain/IPaymentMethod';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';


@autoinject
export class PaymentMethodService {
    constructor(private appState: AppState, private httpClient: HttpClient){
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'PaymentMethods';
  
    async getPaymentMethods(): Promise<IFetchResponse<IPaymentMethod[]>> {
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
                const data = (await response.json()) as IPaymentMethod[];
                console.log(data);
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


    async getPaymentMethod(id: string): Promise<IFetchResponse<IPaymentMethod>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IPaymentMethod;
                console.log(data);
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

    async updatePaymentMethod(paymentMethod: IPaymentMethod): Promise<IFetchResponse<string>>{
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + paymentMethod.id, JSON.stringify(paymentMethod), {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
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

    async createPaymentMethod(paymentMethod: IPaymentMethodCreate): Promise<IFetchResponse<string>> {
        try{
            const response = await this.httpClient
            .post(this._baseUrl, JSON.stringify(paymentMethod), {
                cache: 'no-store',
                headers: {
                    authorization: "Bearer " + this.appState.jwt

                }
            })

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }

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

    async deletePaymentMethod(id: string): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .delete(this._baseUrl + '/' + id, null, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
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

}