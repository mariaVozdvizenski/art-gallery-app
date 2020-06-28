import { autoinject } from 'aurelia-framework';
import { OrderService } from 'service/order-service';
import { IOrder } from 'domain/IOrder';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AddressService } from 'service/address-service';
import { IAddress } from 'domain/IAddress';
import { OrderStatusCodeService } from 'service/order-status-code-service';
import { IOrderStatusCode } from 'domain/IOrderStatusCode';


@autoinject
export class OrdersIndex {

    private _orders: IOrder[] = []
    private _alert: IAlertData | null = null;
    private _showDetails: boolean = false;
    private _address: IAddress | null = null;
    private _orderStatusCodes: IOrderStatusCode[] = [];
    private _orderStatusCodesString: string[] = [];
    private _condition: string | null = null;
    private _showItemDetails: boolean = false;


    constructor(private orderService: OrderService, private addressService: AddressService, 
        private orderStatusCodeService: OrderStatusCodeService) {

    }

    showItemDetails() {
        if (!this._showItemDetails){
            this._showItemDetails = true
        }
    }

    calculateTotal() {
        this._orders.forEach(order => {
            order.total = 0;
            order.orderItems.forEach(orderItem => {
                console.log(orderItem.paintingPrice);
                order.total! += parseInt(orderItem.paintingPrice)
                console.log(order.total);
            });
        });
    }

    attached() {
        this.orderService.getOrders(this._condition, this._orderStatusCodesString).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._orders = response.data!;
                    this.calculateTotal();
                    this.getOrderStatusCodes();              
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

    getOrderStatusCodes() {
        this.orderStatusCodeService.getOrderStatusCodes().then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this._orderStatusCodes = response.data!;      
            } else {
                // show error message
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,
                }
            }
        });     
    }

    getDetails(addressId: string) {
        console.log(addressId);

        this.changeShowDetails();

        console.log(this._showDetails);

        this.addressService.getAddress(addressId).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._address = response.data!;
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

    filter() {
        console.log(this._orderStatusCodesString);
        console.log(this._condition);
        this.attached();
    }

    changeStatus(orderStatusCodeId: string, order: IOrder) {

        console.log(orderStatusCodeId);
        console.log(order);


        var updateOrder: IOrder = <IOrder> {
            id: order.id,
            orderDate: order.orderDate,
            orderDetails: order.orderDetails,
            orderStatusCodeId: orderStatusCodeId,
            appUserId: order.appUserId,
            addressId: order.addressId
        }

        this.orderService.updateOrder(updateOrder).then((response) =>{
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this.attached();
            }
        })
    }

    changeShowDetails() {
        if (this._showDetails == false) {
            this._showDetails = true
        } 
    }

    hide() {
        if (this._showDetails == true || this._showItemDetails == true) {
            this._showDetails = false;
            this._showItemDetails = false;
        }
    }
}