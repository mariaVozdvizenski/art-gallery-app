import {autoinject} from 'aurelia-framework';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import { IPaintingEdit } from 'domain/IPaintingEdit';

@autoinject
export class PaintingEdit{

    private _painting: IPainting | null = null;
    private _artists: IArtist[] = [];
    private _artistId = null;


    constructor(private paintingService: PaintingService, private artistService: ArtistService, private router: Router) {

    }

    attached() {
        this.artistService.getArtists().then(
            data => this._artists = data
        );
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof(params.id) == "string") {
            this.paintingService.getPainting(params.id)
            .then(data => this._painting = data)
        }
    }

    onSubmit(event: Event){
        console.log(this._painting!.price);

        let painting: IPaintingEdit = <IPaintingEdit> {
            id: this._painting!.id,
            title: this._painting!.title,
            size: this._painting!.size,
            price: Number(this._painting!.price),
            artistId: this._artistId!
        }

        this.paintingService.updatePainting(painting).then((response) => {
            console.log('redirect?', response);
            this.router.navigateToRoute('paintings');
        });
        event.preventDefault();
    }

}