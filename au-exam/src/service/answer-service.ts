import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IAnswerCreate } from 'domain/IAnswerCreate';
import { IAnswer } from 'domain/IAnswer';


@autoinject
export class AnswerService {
    constructor(private appState: AppState, private httpClient: HttpClient){
    }

    private readonly _baseUrl = 'Answers';
  
    async createAnswer(answer: IAnswerCreate): Promise<IFetchResponse<string>> {
        try{
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
            .post(this._baseUrl, JSON.stringify(answer), {
                cache: 'no-store',
                headers: {
                    authorization: "Bearer " + this.appState.jwt

                }
            })

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IAnswer;
                return {
                    statusCode: response.status,
                    data: data.id
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

}