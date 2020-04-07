import { autoinject } from 'aurelia-framework';
import { BasketService } from 'service/basket-service';
import { IBasket } from 'domain/IBasket';


@autoinject
export class BasketsIndex {
    private _baskets: IBasket[] = []

    constructor(private basketService: BasketService) {

    }

    attached() {
        this.basketService.getBaskets().then(
            data => this._baskets = data
        );
    }

}