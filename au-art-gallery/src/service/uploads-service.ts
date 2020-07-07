import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IUploadResponse } from 'domain/IUploadResponse';


@autoinject
export class UploadsService {
    constructor(private appState: AppState, private httpClient: HttpClient){
    }

    private readonly _baseUrl = 'Uploads';
  

    async getUpload(fileName: string): Promise<IFetchResponse<Blob>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrlForUploads;
            const response = await this.httpClient
                .fetch(this._baseUrl + "/" + fileName, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.blob()) as Blob;
                
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


    async createUpload(formData: FormData): Promise<IFetchResponse<IUploadResponse>> {
        try{
            this.httpClient.baseUrl = this.appState.baseUrlForUploads;
            const response = await this.httpClient
            .post(this._baseUrl, formData, {
                cache: 'no-store',
                headers: {
                    authorization: "Bearer " + this.appState.jwt

                }
            });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IUploadResponse;
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

    async deleteUpload(fileName: string): Promise<IFetchResponse<string>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrlForUploads;
            const response = await this.httpClient
                .delete(this._baseUrl + "/" + fileName, null, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                var data = (await response.json())
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

}