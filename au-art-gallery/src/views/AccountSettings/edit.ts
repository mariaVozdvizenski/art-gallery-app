import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IAddress } from 'domain/IAddress';
import { AddressService } from 'service/address-service';


@autoinject
export class AccountSettingsEdit {

    private _address: IAddress | null = null;
    private _id = "";
    private _alert: IAlertData | null = null;


    constructor(private addressService: AddressService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == "string") {
            this.addressService.getAddress(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._address = response.data!;
                        console.log(this._address);
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

    onSubmit(event: Event) {
        console.log(this._id);

        this._address!.zip = Number(this._address!.zip)

        this.addressService.updateAddress(this._address!)
            .then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
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