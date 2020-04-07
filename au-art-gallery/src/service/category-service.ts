import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {ICategory} from 'domain/ICategory';
import { ICategoryCreate } from 'domain/ICategoryCreate';
import { ICategoryEdit } from 'domain/ICategoryEdit';
import { AppState } from 'state/app-state';

@autoinject
export class CategoryService {

    private readonly _baseUrl = 'Categories';

    constructor(private appState: AppState, private httpClient: HttpClient) {
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    getCategories(): Promise<ICategory[]> {
        return this.httpClient
        .fetch(this._baseUrl)
        .then(response => response.json())
        .then((data: ICategory[]) => data)
        .catch(reason => {
            console.log(reason); 
            return [];
        });
    }

    getCategory(id: string): Promise<ICategory | null> {
        return this.httpClient
        .fetch(this._baseUrl + '/' + id)
        .then(response => response.json())
        .then((data: ICategory) => data)
        .catch(reason => {
            console.log(reason); 
            return null;
        });

    }

    createCategory(createCategory: ICategoryCreate): Promise<string> {
        return this.httpClient.post(this._baseUrl, JSON.stringify(createCategory),{
            cache: 'no-store'
        }).then(
            response => {
                console.log('createCategory response', response);
                return response.statusText;
            }
        );
    }

    updateCategory(category: ICategoryEdit): Promise<string>{
        return this.httpClient.put(this._baseUrl + '/' + category.id, JSON.stringify(category), {
            cache: 'no-store'
        }).then (
            response => {
                console.log('updateCategory response', response);
                return response.statusText;
            }
        )
    }

    deleteCategory(id: string): Promise<string> {
        return this.httpClient.delete(this._baseUrl + '/' + id)
        .then(
            response => {
                console.log('deleteCategory response', response);
                return response.statusText;
            }
        )
    }

    
}