import { autoinject } from 'aurelia-framework';
import { OrderService } from 'service/order-service';
import { IOrder } from 'domain/IOrder';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AddressService } from 'service/address-service';
import { IAddress } from 'domain/IAddress';
import { OrderStatusCodeService } from 'service/order-status-code-service';
import { IOrderStatusCode } from 'domain/IOrderStatusCode';
import { InvoiceService } from 'service/invoice-service';
import { IInvoice } from 'domain/IInvoice';


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
        private orderStatusCodeService: OrderStatusCodeService, private invoiceService: InvoiceService) {

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
                order.total! += parseInt(orderItem.paintingPrice)
            });
        });
    }

    attached() {
        this.orderService.getOrders(this._condition, this._orderStatusCodesString).then(
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
        )
        .then(response => this.getOrderStatusCodes())
        .then(response => this.getInvoices())
        .then(response => this.calculateTotal());
    }

    downloadInvoice(orderInvoice: IInvoice) {
        this.invoiceService.downloadInvoice(orderInvoice.id, String(orderInvoice.invoiceNumber))
        .then(response =>
             URL.createObjectURL(response.data))
        .then(url => {
            window.open(url);
            URL.revokeObjectURL(url);
        });
    }


    getInvoices() {
        this._orders.forEach(order => {
            this.invoiceService.getInvoices(order.id!).then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    order.invoice = response.data![0];                     
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });         
        });
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
        this.changeShowDetails();

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
        this.attached();
    }

    changeStatus(orderStatusCodeId: string, order: IOrder) {


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