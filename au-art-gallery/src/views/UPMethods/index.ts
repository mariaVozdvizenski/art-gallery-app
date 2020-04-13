import { autoinject } from 'aurelia-framework';
import { IUserPaymentMethod } from 'domain/IUserPaymentMethod';
import { UserPaymentMethodService } from 'service/user-payment-method-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class UPMethodsIndex{

    private _userPaymentMethods: IUserPaymentMethod[] | null = null
    private _alert: IAlertData | null = null

    constructor(private userPaymentMethodService: UserPaymentMethodService){

    }

    attached(){
        this.userPaymentMethodService.getUserPaymentMethods().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._userPaymentMethods = response.data!;
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