import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';


@autoinject
export class ArtistsIndex{
    private _artists: IArtist[] = []

    constructor(private artistService: ArtistService) {

    }

    attached() {
       this.artistService.getArtists().then(
           data => this._artists = data
       ); 
    }
    
}