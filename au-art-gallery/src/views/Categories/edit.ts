import {autoinject} from 'aurelia-framework';
import { CategoryService } from 'service/category-service';
import { ICategory } from 'domain/ICategory';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ICategoryEdit } from 'domain/ICategoryEdit';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CategoryEdit{

    private _category: ICategory | null = null;
    private _id = "";
    private _alert: IAlertData | null = null;

    constructor(private categoryService: CategoryService, private router: Router) {

    }

    attached() {  
    }
    
    activate (params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.categoryService.getCategory(params.id)
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._category = response.data!,
                    this._alert = null
                } 
                else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
        }

    }

    onSubmit(event: Event){
        console.log(this._id);

        let category: ICategoryEdit = <ICategoryEdit> {
            categoryName: this._category!.categoryName,
            id: this._category!.id
        }

        this.categoryService.updateCategory(category)
        .then((response) => {
            if(response.statusCode >= 200 && response.statusCode < 300){
                this._alert = null;
                this.router.navigateToRoute('categories', {});
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