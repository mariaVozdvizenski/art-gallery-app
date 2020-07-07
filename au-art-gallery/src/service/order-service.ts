import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IOrder} from 'domain/IOrder';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IOrderCreate } from 'domain/IOrderCreate';



@autoinject
export class OrderService {
    
    constructor(private appState: AppState, private httpClient: HttpClient){
    }

    private readonly _baseUrl = 'Orders';

    constructUrl(condition:string | null, statusCodes: string[] | null): string {
        var url = this._baseUrl;

        if (condition != null && statusCodes != null) {
            return url = url + "?condition=" + condition + "&statusCodes=" + this.extractStatusCodes(statusCodes);
        } else if (condition != null && statusCodes == null) {
            return url = url + "?condition=" + condition;
        } else if (condition == null && statusCodes != null) {
            return url = url + "?statusCodes=" + this.extractStatusCodes(statusCodes);
        } 
        return url;
    }

    extractStatusCodes(statusCodes: string[]): string {
        var statusCodeString: string = "";

        if (statusCodes) {

            statusCodes.forEach(code => {
                statusCodeString += code + "_"
            });
        }
        return statusCodeString;
    }
  

    async getOrders(condition: string | null, statusCodes: string[] | null): Promise<IFetchResponse<IOrder[]>> {

        try {
            var url = this.constructUrl(condition, statusCodes);
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .fetch(url, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IOrder[];
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

    async getOrder(id: string): Promise<IFetchResponse<IOrder>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IOrder;
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

    async updateOrder(order: IOrder): Promise<IFetchResponse<string>>{
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .put(this._baseUrl + '/' + order.id, JSON.stringify(order), {
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

    async createOrder(order: IOrderCreate): Promise<IFetchResponse<string>> {
        try{
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
            .post(this._baseUrl, JSON.stringify(order), {
                cache: 'no-store',
                headers: {
                    authorization: "Bearer " + this.appState.jwt

                }
            });

            if (response.status >= 200 && response.status < 300) {
                const data = (await (response.json()) as IOrder)
                return {
                    statusCode: response.status,
                    data: data.id
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

    async deleteOrder(id: string): Promise<IFetchResponse<string>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
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