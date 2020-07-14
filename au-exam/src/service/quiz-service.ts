import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IQuizCreate} from 'domain/IQuizCreate';
import {IQuiz} from 'domain/IQuiz';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';


@autoinject
export class QuizService {
    constructor(private appState: AppState, private httpClient: HttpClient){
    }

    private readonly _baseUrl = 'Quizzes';

    extractCategories(categories: string[]): string {
        var url: string = "";

        if (categories) {

            categories.forEach(category => {
                url += category + "_"
            });

        }
        return url;
    }
    
    constructUrl(categories: string[] | null): string {
        let url = this._baseUrl;
        url = url + "?categories=" + this.extractCategories(categories);
        return url
    }
  
    async getQuizzes(categories: string[]): Promise<IFetchResponse<IQuiz[]>> {
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            let url = this.constructUrl(categories);
            console.log(url);

            const response = await this.httpClient
                .fetch(url, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IQuiz[];
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

    


    async getQuiz(id: string): Promise<IFetchResponse<IQuiz>> {
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
                const data = (await response.json()) as IQuiz;
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

    async updateQuiz(quiz: IQuiz): Promise<IFetchResponse<string>>{
        try {
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
                .put(this._baseUrl + '/' + quiz.id, JSON.stringify(quiz), {
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

    async createQuiz(quiz: IQuizCreate): Promise<IFetchResponse<string>> {
        try{
            this.httpClient.baseUrl = this.appState.baseUrl;
            const response = await this.httpClient
            .post(this._baseUrl, JSON.stringify(quiz), {
                cache: 'no-store',
                headers: {
                    authorization: "Bearer " + this.appState.jwt

                }
            })

    
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IQuiz;
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

    async deleteQuiz(id: string): Promise<IFetchResponse<string>> {
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