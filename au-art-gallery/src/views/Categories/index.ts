import { autoinject } from 'aurelia-framework';
import { CategoryService } from 'service/category-service';
import { ICategory } from 'domain/ICategory';

@autoinject
export class CategoriesIndex {
    private _categories: ICategory[] | null = null;

    constructor(private categoryService: CategoryService) {

    }

    attached() {
        this.categoryService.getCategories()
            .then(data => this._categories = data);
    }
}