import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';

@autoinject
export class ArtistDelete{

    private _artist: IArtist | null = null;
    private _id = ""


    constructor(private artistService: ArtistService, private router: Router){

    }
    
    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.artistService.getArtist(params.id).then(
                data => this._artist = data)
                this._id = params.id;
        }    
    }

    onSubmit(event: Event) {
        this.artistService.deleteArtist(this._id)
        .then((response) => {
            console.log('redirect?', response);
            this.router.navigateToRoute('artists');
        });
    }

}