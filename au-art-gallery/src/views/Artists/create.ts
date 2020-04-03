import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import { IArtistCreate } from 'domain/IArtistCreate';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';


@autoinject
export class ArtistCreate{

    private _firstName = "";
    private _lastName = "";
    private _country = "";
    private _bio = "";
    private _placeOfBirth = "";
    private _dateOfBirth = "";

    constructor(private artistsService: ArtistService, private router: Router){
    }

    onSubmit(event: Event){
        this.artistsService
        .createArtist({
            firstName: this._firstName, lastName: this._lastName, country: this._country, bio: this._bio, 
            placeOfBirth: this._placeOfBirth, dateOfBirth: this._dateOfBirth})
        .then((response) => {
            console.log('redirect?', response);
            this.router.navigateToRoute('artists');
        });

        event.preventDefault();
    }
}