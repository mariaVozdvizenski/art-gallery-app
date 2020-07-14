import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IQuizType} from 'domain/IQuizType';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';


@autoinject
export class QuizTypeService {
    constructor(private appState: AppState, private httpClient: HttpClient){
    }

    private readonly _baseUrl = 'QuizTypes';
  
    async getQuizTypes(): Promise<IFetchResponse<IQuizType[]>> {
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
                const data = (await response.json()) as IQuizType[];
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


    async getQuizType(id: string): Promise<IFetchResponse<IQuizType>> {
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
                const data = (await response.json()) as IQuizType;
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