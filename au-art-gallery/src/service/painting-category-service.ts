import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IPaintingCategoryCreate} from 'domain/IPaintingCategoryCreate';
import {IPaintingCategory} from 'domain/IPaintingCategory';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';


@autoinject
export class PaintingCategoryService {
    constructor(private appState: AppState, private httpClient: HttpClient){
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'PaintingCategories';
  
    async getPaintingCategories(): Promise<IFetchResponse<IPaintingCategory[]>> {
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
                const data = (await response.json()) as IPaintingCategory[];
                
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


    async getPaintingCategory(id: string): Promise<IFetchResponse<IPaintingCategory>> {
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
                const data = (await response.json()) as IPaintingCategory;
                
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

    async updatePaintingCategory(paintingCategory: IPaintingCategory): Promise<IFetchResponse<string>>{
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .put(this._baseUrl + '/' + paintingCategory.id, JSON.stringify(paintingCategory), {
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

    async createPaintingCategory(paintingCategory: IPaintingCategoryCreate): Promise<IFetchResponse<string>> {
        
        try{
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
            .post(this._baseUrl, JSON.stringify(paintingCategory), {
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

    async deletePaintingCategory(id: string): Promise<IFetchResponse<string>> {
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