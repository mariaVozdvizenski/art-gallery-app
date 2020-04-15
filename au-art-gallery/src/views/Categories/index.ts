import { autoinject } from 'aurelia-framework';
import { CategoryService } from 'service/category-service';
import { ICategory } from 'domain/ICategory';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class CategoriesIndex {
    private _categories: ICategory[] | null = null;
    private _alert: IAlertData | null = null;

    constructor(private categoryService: CategoryService) {

    }

    attached() {
        this.categoryService.getCategories()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300){
                    this._categories = response.data!
                    this._alert = null
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
}