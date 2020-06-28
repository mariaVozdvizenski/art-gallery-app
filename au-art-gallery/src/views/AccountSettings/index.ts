import { autoinject } from 'aurelia-framework';
import { IAddress } from 'domain/IAddress';
import { IAlertData } from 'types/IAlertData';
import {AddressService} from 'service/address-service'
import { AppState } from 'state/app-state';
import { AlertType } from 'types/AlertType';
import { Router } from 'aurelia-router';


@autoinject
export class AccountSettingsIndex {

    private _addresses: IAddress[] = []
    private _alert: IAlertData | null = null

    constructor(private addressService: AddressService, private appState: AppState, private router: Router) {

    }

    attached() {
        this.addressService.getAddresses().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._addresses = response.data!;
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

    toCreateAddress() {
        this.router.navigateToRoute('accountSettingsCreate');
    }
    
    onDelete(id: string) {
        this.addressService.deleteAddress(id).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.addressService.getAddresses().then(
                        response => {
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._alert = null;
                                this._addresses = response.data!;
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