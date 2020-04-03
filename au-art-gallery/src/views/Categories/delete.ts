import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {autoinject} from 'aurelia-framework';
import { CategoryService } from 'service/category-service';
import { ICategory } from 'domain/ICategory';

@autoinject
export class ArtistDelete{

    private _category: ICategory | null = null;
    private _id = ""


    constructor(private categoryService: CategoryService, private router: Router){

    }
    
    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.categoryService.getCategory(params.id).then(
                data => this._category = data)
                this._id = params.id;
        }    
    }

    onSubmit(event: Event) {
        this.categoryService.deleteCategory(this._id)
        .then((response) => {
            console.log('redirect?', response);
            this.router.navigateToRoute('categories');
        });
    }

}