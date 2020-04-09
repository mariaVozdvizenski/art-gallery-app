import { autoinject } from 'aurelia-framework';
import { OrderService } from 'service/order-service';
import { IOrder } from 'domain/IOrder';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class OrdersIndex {

    private _orders: IOrder[] = []

    private _alert: IAlertData | null = null;


    constructor(private orderService: OrderService) {

    }

    attached() {
        this.orderService.getOrders().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._orders = response.data!;
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