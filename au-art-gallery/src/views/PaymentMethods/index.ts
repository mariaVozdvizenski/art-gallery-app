import { autoinject } from 'aurelia-framework';
import { PaymentMethodService } from 'service/payment-method-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IPaymentMethod } from 'domain/IPaymentMethod';


@autoinject
export class PaymentMethodsIndex {

    private _paymentMethods: IPaymentMethod[] = []

    private _alert: IAlertData | null = null;

    constructor(private paymentMethodService: PaymentMethodService) {

    }

    attached() {
        this.paymentMethodService.getPaymentMethods().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._paymentMethods = response.data!;
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