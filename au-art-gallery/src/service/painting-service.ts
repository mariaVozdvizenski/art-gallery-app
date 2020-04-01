import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {IPainting} from 'domain/IPainting';

@autoinject
export class PaintingService {

    private readonly _baseUrl = 'https://localhost:5001/api/paintings';

    constructor(private httpClient: HttpClient){

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
}