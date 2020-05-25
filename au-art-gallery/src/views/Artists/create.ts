import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class ArtistCreate{

    private _firstName = "";
    private _lastName = "";
    private _country = "";
    private _bio = "";
    private _placeOfBirth = "";
    private _dateOfBirth = "";
    private _alert: IAlertData | null = null;

    constructor(private artistsService: ArtistService, private router: Router){
    }

    onSubmit(event: Event){
        
        this.artistsService
        .createArtist({
            firstName: this._firstName, lastName: this._lastName, country: this._country, bio: this._bio, 
            placeOfBirth: this._placeOfBirth, dateOfBirth: this._dateOfBirth})
        .then(response => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null,
                this.router.navigateToRoute('artists', {});
            } else {

                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,

                }
                console.log(this._alert);
            }
        });

        event.preventDefault();
    }
}