import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IPainting } from 'domain/IPainting';
import { IPaintingCreate } from 'domain/IPaintingCreate';
import { IPaintingEdit } from 'domain/IPaintingEdit';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';


@autoinject
export class PaintingService {

    private readonly _baseUrl = 'Paintings';

    constructor(private appState: AppState, private httpClient: HttpClient) {
    }

    constructUrl(filterOption: string | null, inStock: boolean | null, categories: string[] | null): string {
        let url = this._baseUrl;

        if (filterOption != null && inStock != null && categories != null) {
            url = url + "?condition=" + filterOption + "&inStock=" + inStock + "&categories=" + this.extractCategories(categories);
        }
        if (filterOption != null && inStock == null && categories == null) {
            url = url + "?condition=" + filterOption;
        }
        if (filterOption == null && inStock != null && categories == null) {
            url = url + "?inStock=" + inStock;
        }
        if (filterOption == null && inStock != null && categories != null) {
            url = url + "?inStock=" + inStock + "&categories=" + this.extractCategories(categories);
        }
        if (filterOption != null && inStock == null && categories != null) {
            url = url + "?condition=" + filterOption + "&categories=" + this.extractCategories(categories);
        }
        if (filterOption == null && inStock == null && categories != null) {
            url = url + "?categories=" + this.extractCategories(categories);
        }
        return url
    }

    extractCategories(categories: string[]): string {
        var url: string = "";

        if (categories) {

            categories.forEach(category => {
                url += category + "_"
            });

        }
        return url;
    }

    async getPaintings(filterOption: string | null, inStock: boolean | null, categories: string[] | null): Promise<IFetchResponse<IPainting[]>> {
        try {

            this.httpClient.baseUrl = this.appState.baseUrl;
            let url = this.constructUrl(filterOption, inStock, categories);

            const response = await this.httpClient
                .fetch(url, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IPainting[];
                
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


    async getPainting(id: string): Promise<IFetchResponse<IPainting>> {
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
                const data = (await response.json()) as IPainting;
                
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

    async updatePainting(painting: IPaintingEdit): Promise<IFetchResponse<string>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .put(this._baseUrl + '/' + painting.id, JSON.stringify(painting), {
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

    async createPainting(painting: IPaintingCreate): Promise<IFetchResponse<string>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .post(this._baseUrl, JSON.stringify(painting), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IPainting;
                return {
                    statusCode: response.status,
                    data: data!.id
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

    async deletePainting(id: string): Promise<IFetchResponse<string>> {
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