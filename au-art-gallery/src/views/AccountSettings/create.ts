import {autoinject} from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IAddressCreate } from 'domain/IAddressCreate';
import { AddressService } from 'service/address-service';


@autoinject
export class AccountSettingsCreate{

    private _address : IAddressCreate | null = null;
    private _alert : IAlertData | null = null;
    private _addressesCount: Number | null = null;

    constructor(private addressService: AddressService, private router: Router){
    }

    attached() {
        this.addressService.getAddresses().then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null,
                this._addressesCount = response.data!.length
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,
                }
            }
        });
    }

    onSubmit(event: Event){
        this._address!.zip = Number(this._address!.zip)  
        this.addressService
        .createAddress(this._address!)
        .then(response => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null,
                this.router.navigateToRoute('accountSettings', {});
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