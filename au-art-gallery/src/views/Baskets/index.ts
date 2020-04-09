import { autoinject } from 'aurelia-framework';
import { BasketService } from 'service/basket-service';
import { IBasket } from 'domain/IBasket';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class BasketsIndex {

    private _baskets: IBasket[] = []

    private _alert: IAlertData | null = null;


    constructor(private basketService: BasketService) {

    }

    attached() {
        this.basketService.getBaskets().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._baskets = response.data!;
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