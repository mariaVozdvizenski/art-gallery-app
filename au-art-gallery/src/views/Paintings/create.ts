import {autoinject} from 'aurelia-framework';
import { PaintingService } from 'service/painting-service';
import { IPaintingCreate } from 'domain/IPaintingCreate';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';

@autoinject
export class PaintingCreate{

    private _painting: IPaintingCreate | null =null;
    private _artists: IArtist[] = [];
    private _artistId = null;


    constructor (private paintingService: PaintingService, private router: Router, private artistService: ArtistService) {

    }

    attached() {
        this.artistService.getArtists().then(
            data => this._artists = data
        );
    }

    onSubmit(event: Event){
        this._painting!.artistId = this._artistId!

        this.paintingService.createPainting(this._painting!).then((response) => {
            console.log('redirect?', response);
            this.router.navigateToRoute('paintings');
        });
        event.preventDefault();
    }

}