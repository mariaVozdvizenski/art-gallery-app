import {autoinject} from 'aurelia-framework';
import { CategoryService } from 'service/category-service';
import { ICategory } from 'domain/ICategory';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ICategoryEdit } from 'domain/ICategoryEdit';

@autoinject
export class CategoryEdit{

    private _category: ICategory | null = null;
    private _id = "";

    constructor(private categoryService: CategoryService, private router: Router) {

    }

    attached() {  
    }
    
    activate (params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.categoryService.getCategory(params.id).then(
                data => this._category = data)
            this._id = params.id;   
        }
    }

    onSubmit(event: Event){
        console.log(this._id);

        let category: ICategoryEdit = <ICategoryEdit> {
            categoryName: this._category!.categoryName,
            id: this._id
        }

        this.categoryService.updateCategory(category)
        .then((response) => {
            console.log('redirect?', response);
            this.router.navigateToRoute('categories');
        });

        event.preventDefault();
    }


}