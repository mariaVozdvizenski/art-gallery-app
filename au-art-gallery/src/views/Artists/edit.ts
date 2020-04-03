import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IArtistEdit } from 'domain/IArtistEdit';

@autoinject
export class ArtistEdit{
    private _artist: IArtist | null = null;
    private _id = "";

    constructor(private artistService: ArtistService, private router: Router) {

    }

    attached() {  
    }
    
    activate (params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.artistService.getArtist(params.id).then(
                data => this._artist = data)
            this._id = params.id;   
        }
    }

    onSubmit(event: Event){
        console.log(this._id);

        let artist: IArtistEdit = <IArtistEdit> {
            firstName: this._artist!.firstName,
            lastName: this._artist!.lastName,
            country: this._artist!.country,
            id: this._id
        }

        this.artistService.updateArtist(artist)
        .then((response) => {
            console.log('redirect?', response);
            this.router.navigateToRoute('artists');
        });

        event.preventDefault();
    }

}