import {autoinject} from 'aurelia-framework';
import { CategoryService } from 'service/category-service';
import { ICategoryCreate } from 'domain/ICategoryCreate';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CategoryCreate{

    private _categoryName = "";
    private _alert: IAlertData | null = null;

    constructor(private categoryService: CategoryService, private router: Router){

    }

    onSubmit(event: Event) {
        this.categoryService
        .createCategory({categoryName: this._categoryName})
        .then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this.router.navigateToRoute("categories");
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