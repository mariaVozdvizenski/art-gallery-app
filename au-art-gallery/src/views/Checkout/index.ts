import { autoinject } from 'aurelia-framework';
import { BasketService } from 'service/basket-service';
import { BasketItemService } from 'service/basket-item-service';
import { Router } from 'aurelia-router';
import { IBasket } from 'domain/IBasket';
import { IBasketItem } from 'domain/IBasketItem';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { PaymentMethodService } from 'service/payment-method-service';
import { IPaymentMethod } from 'domain/IPaymentMethod';
import { IAddress } from 'domain/IAddress';
import { AddressService } from 'service/address-service';
import { IFetchResponse } from 'types/IFetchResponse';
import { IAddressCreate } from 'domain/IAddressCreate';
import { OrderService } from 'service/order-service';
import { IOrderCreate } from 'domain/IOrderCreate';
import { AppState } from 'state/app-state';
import { IOrderItem } from 'domain/IOrderItem';
import { IOrderItemCreate } from 'domain/IOrderItemCreate';
import { OrderItemService } from 'service/order-item-service';
import { IInvoiceCreate } from 'domain/IInvoiceCreate';
import { InvoiceService } from 'service/invoice-service';

@autoinject
export class CheckoutIndex {

    private _baskets: IBasket[] = []
    private _alert: IAlertData | null = null;
    private _success: string | null = null;
    private _total: number = 0;
    private _paymentMethods: IPaymentMethod[] = [];
    private _paymentMethod: string = 'Credit Card';
    private _imgVisa: string = 'visa.jpg';
    private _imgMaster: string = 'mastercard.jpg';
    private _imgAmex: string = 'amex.jpg';
    private _addressId: string | null = null;
    private _addresses: IAddress[] = [];
    private _createNew: boolean = false;
    private _newAddress: IAddressCreate | null = null;
    private _orderDetails: string | undefined = undefined;
    private _isInStock: boolean = false;
    private _invoiceDetails: IInvoiceCreate | null = null;
    private _invoiceExtraDetails: string | null = null;

    constructor(private basketService: BasketService, private basketItemService: BasketItemService, private router: Router,
        private paymentMethodService: PaymentMethodService, private addressService: AddressService, private orderService: OrderService,
        private appState: AppState, private orderItemService: OrderItemService, private invoiceService: InvoiceService) {

    }

    changeCreateNew() {
        if (this._createNew == false) {
            this._createNew = true;
        } else {
            this._createNew = false;
        }
    }


    attached() {
        this.basketService.getBaskets().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._baskets = response.data!;
                    this._isInStock = this.isInStock();
                    this.calculateTotal();
                } else {
                    // show error message
                    this.createAlert<IBasket>(response)
                }
            }
        );

        this.addressService.getAddresses().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._addresses = response.data!;
                } else {
                    this.createAlert<IAddress>(response)
                }
            }
        );

        this.paymentMethodService.getPaymentMethods().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._paymentMethods = response.data!;
                } else {
                    // show error message
                    this.createAlert<IPaymentMethod>(response)
                }
            }
        );

        this._success = null;
    }

    calculateTotal() {
        this._total = 0;
        let basketItems: IBasketItem[] = this._baskets[0].basketItems;

        basketItems.forEach(element => {
            this._total! += element.paintingPrice * element.quantity
        });
    }

    createAlert<TData>(response: IFetchResponse<TData[]>) {
        this._alert = {
            message: response.statusCode.toString() + ' - ' + response.errorMessage,
            type: AlertType.Danger,
            dismissable: true,
        }
    }

    addNewAddress() {
        this._newAddress!.zip = Number(this._newAddress!.zip)

        this.addressService.createAddress(this._newAddress!).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._success = "New address created!";
                    this.addressService.getAddresses().then(
                        response => {
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._alert = null;
                                this._addresses = response.data!;
                            } else {
                                this.createAlert<IAddress>(response)
                            }
                        }
                    )
                    this._createNew = false;

                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }
    confirmPurchase() {

        var order: IOrderCreate = <IOrderCreate>{
            orderDetails: this._orderDetails,
            appUserId: this.appState.appUserId,
            addressId: this._addressId,
            orderStatusCodeId: "00000000-0000-0000-0000-000000000002"
        }

        if (this.isInStock()) {
            this.orderService.createOrder(order).then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    var orderId = response.data!

                    this._baskets[0].basketItems.forEach(basketItem => {
                        if (basketItem.paintingQuantity > 0) {
                            this.createOrderItems(basketItem, orderId)
                        }
                    });

                    if (this._paymentMethod == "Invoice") {
                        this.createInvoice(orderId);
                    }

                    this.emptyBasket();
                    this.router.navigateToRoute('checkoutSuccess');
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
        }
    }

    async emptyBasket() {
        this._baskets[0].basketItems.forEach(basketItem => {
            if (basketItem.paintingQuantity > 0) {
                this.basketItemService.deleteBasketItem(basketItem.id)
            }
        });
    }

    async createInvoice(orderId: string) {

        if (this._invoiceExtraDetails == null) {
            this._invoiceExtraDetails = "None"
        }

        var invoice: IInvoiceCreate = <IInvoiceCreate>{
            orderId: orderId,
            telephoneNumber: this._invoiceDetails?.telephoneNumber,
            firstName: this._invoiceDetails?.firstName,
            lastName: this._invoiceDetails?.lastName,
            country: this._invoiceDetails?.country,
            city: this._invoiceDetails?.city,
            address: this._invoiceDetails?.address,
            invoiceDetails: this._invoiceExtraDetails
        }

        this.invoiceService.createInvoice(invoice).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,
                }
            }
        });
    }

    isInStock(): boolean {
        if (this._baskets[0].basketItems) {
            return this._baskets[0].basketItems.some(element => element.paintingQuantity > 0)
        }
        return false;
    }

    async createOrderItems(basketItem: IBasketItem, orderId: string) {

        var orderItem: IOrderItemCreate = <IOrderItemCreate>{
            paintingId: basketItem.paintingId,
            orderId: orderId,
            quantity: basketItem.quantity
        }
        await this.orderItemService.createOrderItem(orderItem).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,
                }
            }
        });
    }
}