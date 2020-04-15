import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {autoinject} from 'aurelia-framework';
import { CategoryService } from 'service/category-service';
import { ICategory } from 'domain/ICategory';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class categoryDelete{

    private _category: ICategory | null = null;
    private _id = ""
    private _alert: IAlertData | null = null;


    constructor(private categoryService: CategoryService, private router: Router){

    }
    
    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
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
            }); 
        }
    }

    onSubmit(event: Event) {
        this.categoryService.deleteCategory(this._category!.id)
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