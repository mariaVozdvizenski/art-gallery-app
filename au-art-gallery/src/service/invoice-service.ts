import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IInvoice } from 'domain/IInvoice';
import { IInvoiceCreate } from 'domain/IInvoiceCreate';
import { data } from 'jquery';


@autoinject
export class InvoiceService {
    constructor(private appState: AppState, private httpClient: HttpClient){
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'Invoices';
  
    async getInvoices(orderId: string): Promise<IFetchResponse<IInvoice[]>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + "?orderId=" + orderId, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IInvoice[];
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

    async downloadInvoice(id: string, fileName: string): Promise<IFetchResponse<Blob>>  {
        try{
            const response = await this.httpClient
            .get(this._baseUrl + "/" + id + "/" + fileName, {
                cache: 'no-store',
                headers: {
                    authorization: "Bearer " + this.appState.jwt
                }
            })

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.blob()) as Blob;
                return {
                    statusCode: response.status,
                    data: data
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


    async getInvoice(id: string): Promise<IFetchResponse<IInvoice>> {
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
                const data = (await response.json()) as IInvoice;
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

    async updateInvoice(invoice: IInvoice): Promise<IFetchResponse<string>>{
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + invoice.id, JSON.stringify(invoice), {
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

    async createInvoice(invoice: IInvoiceCreate): Promise<IFetchResponse<string>> {
        try{
            const response = await this.httpClient
            .post(this._baseUrl, JSON.stringify(invoice), {
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

    async deleteInvoice(id: string): Promise<IFetchResponse<string>> {
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