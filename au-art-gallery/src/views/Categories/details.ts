import {autoinject} from 'aurelia-framework';
import { ICategory } from 'domain/ICategory';
import { CategoryService } from 'service/category-service';
import {RouteConfig, NavigationInstruction} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class CategoryDetails {
    private _category: ICategory | null = null;
    private _alert: IAlertData | null = null;

    constructor(private categoryService: CategoryService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string") {
            this.categoryService.getCategory(params.id)
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._category = response.data!;
                    this._alert = null;
                }
                else {
                    this._alert = {
                        message: response.statusCode + '-' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true
                    }
                }
            })
        }
    }
}