import { autoinject } from 'aurelia-framework';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import { IPaintingEdit } from 'domain/IPaintingEdit';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { ICategory } from 'domain/ICategory';
import { CategoryService } from 'service/category-service';
import { PaintingCategoryService } from 'service/painting-category-service';
import { IPaintingCategoryCreate } from 'domain/IPaintingCategoryCreate';
import { UploadsService } from 'service/uploads-service';


@autoinject
export class PaintingEdit {

    private _painting: IPainting | null = null;
    private _artists: IArtist[] = [];
    private _artistId = null;
    private _alert: IAlertData | null = null;
    private _categories: ICategory[] = [];
    private _categoryIds: string[] = [];
    private _selectedFile: FileList | null = null;


    constructor(private paintingService: PaintingService, private artistService: ArtistService, private router: Router,
        private categoryService: CategoryService, private paintingCategoryService: PaintingCategoryService, private uploadsService: UploadsService) {

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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == "string") {
            this.paintingService.getPainting(params.id)
                .then(
                    response => {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            this._alert = null;
                            this._painting = response.data!;
                        } else {
                            // show error message
                            this._alert = {
                                message: response.statusCode.toString() + ' - ' + response.errorMessage,
                                type: AlertType.Danger,
                                dismissable: true
                            }
                        }
                    }
                );
        }
    }

    revertToPrevious(){ 
        if (this._selectedFile) {
            this._selectedFile = null;
        }
    }

    async doUpload() {
        var formData = new FormData();

        formData.append(`file`, this._selectedFile![0], this._selectedFile![0].name);

        await this.uploadsService.createUpload(formData).then((response) => {

            if (response.statusCode >= 200 && response.statusCode < 300) {
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

    async uploadDeleteCreate() {

        if (this._selectedFile != null) {

            await this.uploadsService.deleteUpload(this._painting!.imageName).then((response) => {

                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;                   
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });

            await this.doUpload();
        }
    }

    async onSubmit(event: Event) {

        await this.uploadDeleteCreate();

        let painting: IPaintingEdit = <IPaintingEdit>{
            id: this._painting!.id,
            title: this._painting!.title,
            size: this._painting!.size,
            price: Number(this._painting!.price),
            artistId: this._artistId!,
            description: this._painting!.description,
            quantity: Number(this._painting!.quantity),
            imageName: (this._painting!.imageName)
        }

        await this.deletePaintingCategories();

        this._categoryIds.forEach(categoryId => {

            let paintingCategory: IPaintingCategoryCreate = <IPaintingCategoryCreate>
                {
                    paintingId: this._painting!.id,
                    categoryId: categoryId
                }

            this.submitPaintingCategory(paintingCategory);
        });

        this.paintingService.updatePainting(painting)
        
            .then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.router.navigateToRoute('paintingDetails', { id: this._painting!.id });
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

    async deletePaintingCategories() {
        if (this._painting!.paintingCategories) {

            this._painting!.paintingCategories.forEach(paintingCategory => {
                this.paintingCategoryService.deletePaintingCategory(paintingCategory.id).then(
                    response => {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            this._alert = null;
                        } else {
                            // show error message
                            this._alert = {
                                message: response.statusCode.toString() + ' - ' + response.errorMessage,
                                type: AlertType.Danger,
                                dismissable: true,
                            }
                        }
                    });
            });
        }
    }

    async submitPaintingCategory(paintingCategory: IPaintingCategoryCreate) {

        await this.paintingCategoryService.createPaintingCategory(paintingCategory).then((response) => {
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