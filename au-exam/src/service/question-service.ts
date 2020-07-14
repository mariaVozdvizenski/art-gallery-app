import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IQuestionCreate} from 'domain/IQuestionCreate';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IQuestion } from 'domain/IQuestion';


@autoinject
export class QuestionService {
    constructor(private appState: AppState, private httpClient: HttpClient){
    }

    private readonly _baseUrl = 'Questions';
  
    async createQuestion(question: IQuestionCreate): Promise<IFetchResponse<string>> {
        try{
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
            .post(this._baseUrl, JSON.stringify(question), {
                cache: 'no-store',
                headers: {
                    authorization: "Bearer " + this.appState.jwt

                }
            })

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IQuestion;
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