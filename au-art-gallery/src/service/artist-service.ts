import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {IArtistCreate} from 'domain/IArtistCreate';
import {IArtistEdit } from 'domain/IArtistEdit';
import {IArtist} from 'domain/IArtist';
import { AppState } from 'state/app-state';



@autoinject
export class ArtistService {
    constructor(private appState: AppState, private httpClient: HttpClient){
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'Artists';
  


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

    getCreateArtist(id: string): Promise<IArtistCreate | null> {
        return this.httpClient
        .fetch(this._baseUrl + '/' + id)
        .then(response => response.json())
        .then((data: IArtistCreate) => data)
        .catch(reason => {
            console.log(reason); 
            return null;
        });
    }

    updateArtist(artist: IArtistEdit): Promise<string>{
        return this.httpClient.put(this._baseUrl + '/' + artist.id, JSON.stringify(artist), {
            cache: 'no-store'
        }).then (
            response => {
                console.log('updateArtist response', response);
                return response.statusText;
            }
        )
    }

    createArtist(artist: IArtistCreate): Promise<string> {
        return this.httpClient.post(this._baseUrl, JSON.stringify(artist),{
            cache: 'no-store'
        }).then(
            response => {
                console.log('createArtist response', response);
                return response.statusText;
            }
        );
    }

    deleteArtist(id: string): Promise<string> {
        return this.httpClient.delete(this._baseUrl + '/' + id)
        .then(
            response => {
                console.log('createArtist response', response);
                return response.statusText;
            }
        )
    }

}