import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IBasket} from 'domain/IBasket';
import { AppState } from 'state/app-state';



@autoinject
export class BasketService {
    
    constructor(private appState: AppState, private httpClient: HttpClient){
        this.httpClient.baseUrl = this.appState.baseUrl;

    }

    private readonly _baseUrl = 'Baskets';
  

    getBaskets(): Promise<IBasket []>{
        return this.httpClient
        .fetch(this._baseUrl, {cache: "no-store", headers: {
            authorization: "Bearer " + this.appState.jwt
        }})
        .then(response => response.json())
        .then((data: IBasket[]) => data)
        .catch(reason => {
            console.log(reason); 
            return [];
        });
    }

    getBasket(id: string): Promise<IBasket | null> {
        return this.httpClient
        .fetch(this._baseUrl + '/' + id)
        .then(response => response.json())
        .then((data: IBasket) => data)
        .catch(reason => {
            console.log(reason); 
            return null;
        });

    }
}
