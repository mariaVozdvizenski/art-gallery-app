import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IOrderStatusCodeCreate} from 'domain/IOrderStatusCodeCreate';
import {IOrderStatusCode} from 'domain/IOrderStatusCode';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';


@autoinject
export class OrderStatusCodeService {
    constructor(private appState: AppState, private httpClient: HttpClient){
    }

    private readonly _baseUrl = 'OrderStatusCodes';
  
    async getOrderStatusCodes(): Promise<IFetchResponse<IOrderStatusCode[]>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .fetch(this._baseUrl, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IOrderStatusCode[];
                
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


    async getOrderStatusCode(id: string): Promise<IFetchResponse<IOrderStatusCode>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IOrderStatusCode;
                
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

    async updateOrderStatusCode(orderStatusCode: IOrderStatusCode): Promise<IFetchResponse<string>>{
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .put(this._baseUrl + '/' + orderStatusCode.id, JSON.stringify(orderStatusCode), {
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

    async createOrderStatusCode(orderStatusCode: IOrderStatusCodeCreate): Promise<IFetchResponse<string>> {
        try{
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
            .post(this._baseUrl, JSON.stringify(orderStatusCode), {
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

    async deleteOrderStatusCode(id: string): Promise<IFetchResponse<string>> {
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