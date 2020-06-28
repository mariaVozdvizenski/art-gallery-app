import { autoinject } from 'aurelia-framework';
import { PaintingService } from 'service/painting-service';
import { IPaintingCreate } from 'domain/IPaintingCreate';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ArtistService } from 'service/artist-service';
import { PaintingCategoryService } from 'service/painting-category-service';
import { IArtist } from 'domain/IArtist';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { CategoryService } from 'service/category-service';
import { ICategory } from 'domain/ICategory';
import { IPaintingCategoryCreate } from 'domain/IPaintingCategoryCreate';
import { IPaintingCategory } from 'domain/IPaintingCategory';
import { UploadsService } from 'service/uploads-service';

@autoinject
export class PaintingCreate {

    private _painting: IPaintingCreate | null = null;
    private _selectedFile: FileList | null = null;
    private _fileName: string | null = null;
    private _artists: IArtist[] = [];
    private _artistId = null;
    private _alert: IAlertData | null = null;
    private _categories: ICategory[] = [];
    private _categoryIds: string[] = [];
    private _price: number | null = null;
    private _quantity: number | null = null;

    constructor(private paintingService: PaintingService, private router: Router, private artistService: ArtistService,
        private categoryService: CategoryService, private paintingCategoryService: PaintingCategoryService,
        private uploadsService: UploadsService) {

    }

    attached() {
        this.artistService.getArtists().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._artists = response.data!;
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

        this.categoryService.getCategories().then(
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
    }

    async onSubmit(event: Event) {

        await this.doUpload();

        this._painting!.artistId = this._artistId!
        var createdPaintingId: string;

        this._painting!.price =  Number(this._price)
        this._painting!.quantity = Number(this._quantity)

        console.log(this._painting);

        await this.paintingService.createPainting(this._painting!).then((response) => {

            if (response.statusCode >= 200 && response.statusCode < 300) {

                this._alert = null;
                createdPaintingId = response.data!;
                console.log(createdPaintingId);

                this._categoryIds.forEach(categoryId => {

                    let paintingCategory: IPaintingCategoryCreate = <IPaintingCategoryCreate>
                        {
                            paintingId: createdPaintingId,
                            categoryId: categoryId
                        }

                    this.SubmitPaintingCategory(paintingCategory);
                });

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

    onClick() {
        console.log(this._categoryIds);
        return true;
    }

    async doUpload() {
        var formData = new FormData();

        formData.append(`file`, this._selectedFile![0], this._selectedFile![0].name);

        await this.uploadsService.createUpload(formData).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._fileName = response.data!.fileName;
                this._painting!.imageName = response.data!.fileName;
                this._alert = null;
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,
                }
            }
        });
    }

    SubmitPaintingCategory(paintingCategory: IPaintingCategoryCreate) {
        console.log(paintingCategory);
        this.paintingCategoryService.createPaintingCategory(paintingCategory).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this.router.navigateToRoute('paintings', {});
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,
                }
            }
        });
    }
}