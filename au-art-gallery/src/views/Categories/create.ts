import {autoinject} from 'aurelia-framework';
import { CategoryService } from 'service/category-service';
import { ICategoryCreate } from 'domain/ICategoryCreate';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';

@autoinject
export class CategoryCreate{

    private _categoryName = "";

    constructor(private categoryService: CategoryService, private router: Router){

    }

    onSubmit(event: Event) {
        this.categoryService
        .createCategory({categoryName: this._categoryName})
        .then((response) => {
            console.log('redirect?', response);
            this.router.navigateToRoute('categories');
        });
        event.preventDefault();
    }

}