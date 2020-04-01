import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {ICategory} from 'domain/ICategory';

@autoinject
export class CategoryService {

    private readonly _baseUrl = 'https://localhost:5001/api/Categories';

    constructor(private httpClient: HttpClient) {

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
}