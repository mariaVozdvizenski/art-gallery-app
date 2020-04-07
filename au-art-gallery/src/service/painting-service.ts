import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {IPainting} from 'domain/IPainting';
import { IPaintingCreate } from 'domain/IPaintingCreate';
import { IPaintingEdit } from 'domain/IPaintingEdit';
import { AppState } from 'state/app-state';

@autoinject
export class PaintingService {

    private readonly _baseUrl = 'Paintings';

    constructor(private appState: AppState, private httpClient: HttpClient){
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    getPaintings(): Promise<IPainting[]>{
        return this.httpClient.fetch(this._baseUrl)
        .then(response => response.json())
        .then((data: IPainting[]) => data)
        .catch(reason => {
            console.log(reason); 
            return []
        });
    }

    getPainting(id: string): Promise<IPainting | null>{
        return this.httpClient.fetch(this._baseUrl + '/' + id)
        .then(response => response.json())
        .then((data: IPainting) => data)
        .catch(reason => {
            console.log(reason); 
            return null
        });
    }

    createPainting(painting: IPaintingCreate): Promise<string> {
        return this.httpClient.post(this._baseUrl, JSON.stringify(painting),{
            cache: 'no-store'
        }).then(
            response => {
                console.log('createArtist response', response);
                return response.statusText;
            }
        );
    }

    updatePainting(painting: IPaintingEdit): Promise<string>{
        return this.httpClient.put(this._baseUrl + '/' + painting.id, JSON.stringify(painting), {
            cache: 'no-store'
        }).then (
            response => {
                console.log('updatePainting response', response);
                return response.statusText;
            }
        )

    }

    deletePainting(id: string): Promise<string>{
        return this.httpClient.delete(this._baseUrl + '/' + id)
        .then(
            response => {
                console.log('deletePainting response', response);
                return response.statusText;
            }
        )
    }
}