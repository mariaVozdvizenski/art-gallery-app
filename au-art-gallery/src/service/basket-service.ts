import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IBasket} from 'domain/IBasket';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';



@autoinject
export class BasketService {
    
    constructor(private appState: AppState, private httpClient: HttpClient){
    }

    private readonly _baseUrl = 'Baskets';

    async getBaskets(): Promise<IFetchResponse<IBasket[]>> {
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
                const data = (await response.json()) as IBasket[];
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

    async getBasket(id: string): Promise<IFetchResponse<IBasket>> {
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
                const data = (await response.json()) as IBasket;
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
