import {autoinject} from 'aurelia-framework';
import { PaymentMethodService } from 'service/payment-method-service';
import { IPaymentMethodCreate } from 'domain/IPaymentMethodCreate';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class PaymentMethodsCreate{

    private _alert: IAlertData | null = null;
    private _paymentMethod: IPaymentMethodCreate | null = null;

    constructor(private paymentMethodService: PaymentMethodService, private router: Router){

    }

    onSubmit(event: Event) {
        this.paymentMethodService
        .createPaymentMethod(this._paymentMethod!)
        .then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this.router.navigateToRoute("paymentMethods");
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