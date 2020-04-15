import {autoinject} from 'aurelia-framework';
import { PaintingService } from 'service/painting-service';
import { IPaintingCreate } from 'domain/IPaintingCreate';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class PaintingCreate{

    private _painting: IPaintingCreate | null =null;
    private _artists: IArtist[] = [];
    private _artistId = null;
    private _alert: IAlertData | null = null;


    constructor (private paintingService: PaintingService, private router: Router, private artistService: ArtistService) {

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

    onSubmit(event: Event){
        this._painting!.artistId = this._artistId!
        this._painting!.price = Number(this._painting!.price)

        this.paintingService.createPainting(this._painting!)
        .then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this.router.navigateToRoute('paintings', {});
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