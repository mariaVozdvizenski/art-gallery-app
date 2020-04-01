import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';

@autoinject
export class ArtistEdit{
    private _artist: IArtist | null = null;

    constructor(private artistService: ArtistService, private router: Router) {

    }

    attached() {
        
    }
    
    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params)
        if (params.id && typeof(params) == "string") {
            this.artistService.getArtist(params).then(
                data => data = this._artist)
        }
    }

}