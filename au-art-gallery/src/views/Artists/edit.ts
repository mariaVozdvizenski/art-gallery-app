import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class ArtistEdit{
    private _artist: IArtist | null = null;
    private _id = "";
    private _alert: IAlertData | null = null;


    constructor(private artistService: ArtistService, private router: Router) {

    }

    attached() {  
    }
    
    activate (params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
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

    onSubmit(event: Event){
        console.log(this._id);

        let artist: IArtist = <IArtist> {
            firstName: this._artist!.firstName,
            lastName: this._artist!.lastName,
            country: this._artist!.country,
            id: this._artist!.id,
            dateOfBirth: this._artist!.dateOfBirth,
            bio: this._artist!.bio,
            placeOfBirth: this._artist!.placeOfBirth
        };

        console.log(artist.dateOfBirth)

        this.artistService.updateArtist(artist)
        .then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this.router.navigateToRoute('artists', {});
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