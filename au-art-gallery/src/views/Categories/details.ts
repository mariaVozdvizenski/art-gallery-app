import {autoinject} from 'aurelia-framework';
import { ICategory } from 'domain/ICategory';
import { CategoryService } from 'service/category-service';
import {RouteConfig, NavigationInstruction} from 'aurelia-router';


@autoinject
export class CategoryDetails {
    private _category: ICategory | null = null;

    constructor(private categoryService: CategoryService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string") {
            this.categoryService.getCategory(params.id)
            .then(data => this._category = data)
        }
    }
}