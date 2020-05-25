import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import {RouteConfig, NavigationInstruction} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';



@autoinject
export class ArtistsDetails{
    
    private _artist?: IArtist | null;
    private _id = ""
    private _alert: IAlertData | null = null;

    constructor(private artistService: ArtistService) {

    }

    attached(){

    }


    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.artistService.getArtist(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._artist = response.data!;
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
    }
}
