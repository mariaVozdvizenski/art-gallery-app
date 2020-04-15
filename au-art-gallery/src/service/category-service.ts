import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {ICategory} from 'domain/ICategory';
import { ICategoryCreate } from 'domain/ICategoryCreate';
import { ICategoryEdit } from 'domain/ICategoryEdit';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';


@autoinject
export class CategoryService {

    private readonly _baseUrl = 'Categories';

    constructor(private appState: AppState, private httpClient: HttpClient) {
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    async getCategories(): Promise<IFetchResponse<ICategory[]>> {
        try {
            const response = await this.httpClient.fetch(this._baseUrl, {
                cache: "no-store",
                headers: {
                    authorization: "Bearer " + this.appState.jwt
                }
            });
            if (response.status >= 200 && response.status < 300){
                const data = (await response.json()) as ICategory[];
                console.log(data);
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

    async getCategory(id: string): Promise<IFetchResponse<ICategory>> {
        try {
            const response = await this.httpClient.fetch(this._baseUrl + '/' + id, {
                cache: "no-store",
                headers: {
                    authorization: "Bearer " + this.appState.jwt
                }
            });

            if (response.status >= 200 && response.status < 300){
                const data = (await response.json()) as ICategory;
                console.log(data);
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }    
                

        } catch (reason){
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async createCategory(category: ICategoryCreate): Promise<IFetchResponse<string>> {
        try{
            const response = await this.httpClient
            .post(this._baseUrl, JSON.stringify(category), {
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

    async updateCategory(category: ICategoryEdit): Promise<IFetchResponse<ICategory>> {

        try {
            const response = await this.httpClient.put(this._baseUrl + '/' + category.id, JSON.stringify(category), {
                cache: "no-store",
                headers: {
                    authorization: "Bearer " + this.appState.jwt
                }
            });
            if (response.status >= 200 && response.status < 300){
                return {
                    statusCode: response.status,
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

    async deleteCategory(id: string): Promise<IFetchResponse<string>> {
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