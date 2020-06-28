import {autoinject} from 'aurelia-framework';
import { PaymentMethodService } from 'service/payment-method-service';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IPaymentMethod } from 'domain/IPaymentMethod';

@autoinject
export class PaymentMethodsDelete{

    private _alert: IAlertData | null = null;
    private _paymentMethod: IPaymentMethod | null = null;

    constructor(private paymentMethodService: PaymentMethodService, private router: Router){

    }

    activate (params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.paymentMethodService.getPaymentMethod(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._paymentMethod = response.data!;
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
        this.paymentMethodService
        .deletePaymentMethod(this._paymentMethod!.id)
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