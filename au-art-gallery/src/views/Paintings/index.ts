
import { autoinject } from 'aurelia-framework';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import { ICategory } from 'domain/ICategory';
import { CategoryService } from 'service/category-service';
import { UploadsService } from 'service/uploads-service';

@autoinject
export class PaintingsIndex {

    private _paintings: IPainting[] | null = null;
    private _alert: IAlertData | null = null;
    private _filterOption: string | null = "ascending";
    private _inStock: boolean = false;
    private _categoryNames: string[] = [];
    private _categories: ICategory[] = [];

    constructor(private paintingService: PaintingService, private appState: AppState, 
        private categoryService: CategoryService, private uploadsService: UploadsService) {

    }

    async attached() {
        await this.paintingService.getPaintings(this._filterOption, this._inStock, this._categoryNames).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._paintings = response.data!;
                    console.log("attached");
                    console.log(this._inStock);
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
        await this.categoryService.getCategories().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._categories = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
        await this.getImages();
    }

    async filter(event: Event){
        console.log("in options");

        console.log(this._filterOption);
        console.log(this._inStock);
        console.log(this._categoryNames);

        this.attached();
    }

    async getImages(){
        this._paintings!.forEach(painting => {
              this.uploadsService.getUpload(painting.imageName).then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.createImgSource(painting, response.data!);
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
        });
    }

    createImgSource(painting: IPainting, file: Blob) {
        var objectURL = URL.createObjectURL(file);
        painting.imageUrl = objectURL;
    }
}