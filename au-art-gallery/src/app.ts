import {RouterConfiguration, Router} from 'aurelia-router';
import {autoinject, PLATFORM} from 'aurelia-framework';
import { AppState } from 'state/app-state';

@autoinject
export class App {

  router?: Router

  constructor(private appState: AppState){

  }

  configureRouter(config: RouterConfiguration, router: Router): void{
    this.router = router;

    config.title = "ArtGallery";

    config.map([
      {route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/index'), nav: true, title:'Home'},

      {route: ['artists', 'artists/index'], name: 'artists', moduleId: PLATFORM.moduleName('views/artists/index'), nav: true, title:'Artists'},
      {route: ['artists/details/:id'], name: 'artistDetails', moduleId: PLATFORM.moduleName('views/artists/details'), nav: false, title:'Artist Details'},
      {route: ['artists/edit/:id'], name: 'artistEdit', moduleId: PLATFORM.moduleName('views/artists/edit'), nav: false, title:'Artist Edit'},
      {route: ['artists/create'], name: 'artistCreate', moduleId: PLATFORM.moduleName('views/artists/create'), nav: false, title:'Artist Create'},
      {route: ['artists/delete/:id'], name: 'artistDelete', moduleId: PLATFORM.moduleName('views/artists/delete'), nav: false, title:'Artist Delete'},

      {route: ['comments', 'comments/index'], name: 'comments', moduleId: PLATFORM.moduleName('views/comments/index'), nav: true, title:'Comments'},
      {route: ['comments/edit/:id'], name: 'commentEdit', moduleId: PLATFORM.moduleName('views/comments/edit'), nav: false, title:'Comment Edit'},
      {route: ['comments/delete/:id'], name: 'commentDelete', moduleId: PLATFORM.moduleName('views/comments/delete'), nav: false, title:'Comment Delete'},
      {route: ['comments/create'], name: 'commentCreate', moduleId: PLATFORM.moduleName('views/comments/create'), nav: false, title:'Comment Create'},
      {route: ['comments/details/:id'], name: 'commentDetails', moduleId: PLATFORM.moduleName('views/comments/details'), nav: false, title:'Comment Details'},


      {route: ['orders', 'orders/index'], name: 'orders', moduleId: PLATFORM.moduleName('views/orders/index'), nav: true, title:'Orders'},

      {route: ['userPaymentMethods', 'userPaymentMethods/index'], name: 'userPaymentMethods', moduleId: PLATFORM.moduleName('views/UPMethods/index'), nav: true, title:'User Payment Methods'},

      {route: ['paintings', 'paintings/index'], name: 'paintings', moduleId: PLATFORM.moduleName('views/paintings/index'), nav: true, title:'Paintings'},
      {route: ['paintings/details/:id'], name: 'paintingDetails', moduleId: PLATFORM.moduleName('views/paintings/details'), nav: false, title:'Painting Details'},
      {route: ['paintings/create'], name: 'paintingCreate', moduleId: PLATFORM.moduleName('views/paintings/create'), nav: false, title:'Painting Create'},
      {route: ['paintings/edit/:id'], name: 'paintingEdit', moduleId: PLATFORM.moduleName('views/paintings/edit'), nav: false, title:'Painting Edit'},
      {route: ['paintings/delete/:id'], name: 'paintingDelete', moduleId: PLATFORM.moduleName('views/paintings/delete'), nav: false, title:'Painting Delete'},

      {route: ['baskets', 'baskets/index'], name: 'baskets', moduleId: PLATFORM.moduleName('views/baskets/index'), nav: true, title:'Baskets'},

      {route: ['categories', 'categories/index'], name: 'categories', moduleId: PLATFORM.moduleName('views/categories/index'), nav: true, title:'Categories'},
      {route: ['categories/delete/:id'], name: 'categoryDelete', moduleId: PLATFORM.moduleName('views/categories/delete'), nav: false, title:'Category Delete'},
      {route: ['categories/details/:id'], name: 'categoryDetails', moduleId: PLATFORM.moduleName('views/categories/details'), nav: false, title:'Category Details'},
      {route: ['categories/edit/:id'], name: 'categoryEdit', moduleId: PLATFORM.moduleName('views/categories/edit'), nav: false, title:'Category Edit'},
      {route: ['categories/create'], name: 'categoryCreate', moduleId: PLATFORM.moduleName('views/categories/create'), nav: false, title:'Category Create'},

      { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
      { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' }

    ]);

    config.mapUnknownRoutes('views/home/index')

  }

  logoutOnClick() {
    this.appState.jwt = null;
    this.router!.navigateToRoute('account-login');
  }

}
