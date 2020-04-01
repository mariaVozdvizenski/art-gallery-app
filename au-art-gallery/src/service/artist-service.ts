import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {IArtist} from 'domain/IArtist';

@autoinject
export class ArtistService {
    constructor(private httpClient: HttpClient){

    }

    private readonly _baseUrl = 'https://localhost:5001/api/Artists';

    getArtists(): Promise<IArtist []>{
        return this.httpClient
        .fetch(this._baseUrl)
        .then(response => response.json())
        .then((data: IArtist[]) => data)
        .catch(reason => {
            console.log(reason); 
            return [];
        });
    }

    getArtist(id: string): Promise<IArtist | null> {
        return this.httpClient
        .fetch(this._baseUrl + '/' + id)
        .then(response => response.json())
        .then((data: IArtist) => data)
        .catch(reason => {
            console.log(reason); 
            return null;
        });
    
    }
}