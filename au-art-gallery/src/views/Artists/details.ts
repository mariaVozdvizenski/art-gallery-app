import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import {RouteConfig, NavigationInstruction} from 'aurelia-router';



@autoinject
export class ArtistsDetails{
    private _artist?: IArtist | null ;

    constructor(private artistService: ArtistService) {

    }

    attached(){

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.artistService.getArtist(params.id).then(
                data => this._artist = data)
        }
    }
}
