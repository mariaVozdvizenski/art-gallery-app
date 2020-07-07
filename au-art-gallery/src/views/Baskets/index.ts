import { autoinject } from 'aurelia-framework';
import { BasketService } from 'service/basket-service';
import { BasketItemService } from 'service/basket-item-service';
import {Router} from 'aurelia-router';
import { IBasket } from 'domain/IBasket';
import {IBasketItem} from 'domain/IBasketItem';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { OrderService } from 'service/order-service';
import { UploadsService } from 'service/uploads-service';


@autoinject
export class BasketsIndex {

    private _baskets: IBasket[] = []
    private _alert: IAlertData | null = null;
    private _success: string | null = null;
    private _total: number  = 0;

    constructor(private basketService: BasketService, private basketItemService: BasketItemService, private router: Router,
        private orderService: OrderService, private uploadsService: UploadsService) {

    }

    attached() {
        this.basketService.getBaskets().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._baskets = response.data!;
                    this.calculateTotal()
                    this.getImages();
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

    onDelete(id: string){
        this.basketItemService.deleteBasketItem(id).then(
            (response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this.attached();                         
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,

                }
            }
        });
    }

    onUpdate(id: string, quantity: string) {
        let basketItem: IBasketItem | null;    
        this.basketItemService.getBasketItem(id).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    basketItem = response.data!;
                    basketItem.quantity = Number(quantity)   
                    this.basketItemService.updateBasketItem(basketItem).then(
                        (response) => {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            this.attached();                    
                        } else {
                            this._alert = {
                                message: response.statusCode.toString() + ' - ' + response.errorMessage,
                                type: AlertType.Danger,
                                dismissable: true        
                            }
                        }
                    });

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

    isInStock(): boolean {
        return this._baskets[0].basketItems.some(element => element.paintingQuantity > 0);
    }

    async getImages(){

        this._baskets[0].basketItems!.forEach(basketItem => {
              this.uploadsService.getUpload(basketItem.paintingImageUrl).then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    basketItem.imageUrl = URL.createObjectURL(response.data)
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
        });
    }


    calculateTotal(){
        this._total = 0;
        let basketItems: IBasketItem[] = this._baskets[0].basketItems;

        basketItems.forEach(element => {
            if (element.paintingQuantity > 0) {
                this._total! += element.paintingPrice * element.quantity
            }
        });
    }

    proceedToCheckout() {
        if (this.isInStock()) {
            this.router.navigateToRoute('checkout', {});
        }
    }
}