import {autoinject} from 'aurelia-framework';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import { IPaintingEdit } from 'domain/IPaintingEdit';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class PaintingEdit{

    private _painting: IPainting | null = null;
    private _artists: IArtist[] = [];
    private _artistId = null;
    private _alert: IAlertData | null = null;


    constructor(private paintingService: PaintingService, private artistService: ArtistService, private router: Router) {

    }

    attached() {
        this.artistService.getArtists().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._artists = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof(params.id) == "string") {
            this.paintingService.getPainting(params.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._painting = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true
                        }
                    }
                }
            );}
    }

    onSubmit(event: Event){
        console.log(this._painting!.price);

        let painting: IPaintingEdit = <IPaintingEdit> {
            id: this._painting!.id,
            title: this._painting!.title,
            size: this._painting!.size,
            price: Number(this._painting!.price),
            artistId: this._artistId!,
            description: this._painting!.description    
        }

        this.paintingService.updatePainting(painting)
        .then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this.router.navigateToRoute('paintingDetails', {id: this._painting!.id});
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,

                }
            }
        });
        event.preventDefault();
    }

}