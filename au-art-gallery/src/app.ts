import {RouterConfiguration, Router} from 'aurelia-router';
import {autoinject, PLATFORM} from 'aurelia-framework';

@autoinject
export class App {

  router?: Router

  constructor(){

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

      
      {route: ['paintings', 'paintings/index'], name: 'paintings', moduleId: PLATFORM.moduleName('views/paintings/index'), nav: true, title:'Paintings'},
      {route: ['paintings/details/:id'], name: 'paintingDetails', moduleId: PLATFORM.moduleName('views/paintings/details'), nav: false, title:'Painting Details'},
      {route: ['paintings/create'], name: 'paintingCreate', moduleId: PLATFORM.moduleName('views/paintings/create'), nav: false, title:'Painting Create'},
      {route: ['paintings/edit/:id'], name: 'paintingEdit', moduleId: PLATFORM.moduleName('views/paintings/edit'), nav: false, title:'Painting Edit'},
      {route: ['paintings/delete/:id'], name: 'paintingDelete', moduleId: PLATFORM.moduleName('views/paintings/delete'), nav: false, title:'Painting Delete'},


      {route: ['categories', 'categories/index'], name: 'categories', moduleId: PLATFORM.moduleName('views/categories/index'), nav: true, title:'Categories'},
      {route: ['categories/delete/:id'], name: 'categoryDelete', moduleId: PLATFORM.moduleName('views/categories/delete'), nav: false, title:'Category Delete'},
      {route: ['categories/details/:id'], name: 'categoryDetails', moduleId: PLATFORM.moduleName('views/categories/details'), nav: false, title:'Category Details'},
      {route: ['categories/edit/:id'], name: 'categoryEdit', moduleId: PLATFORM.moduleName('views/categories/edit'), nav: false, title:'Category Edit'},
      {route: ['categories/create'], name: 'categoryCreate', moduleId: PLATFORM.moduleName('views/categories/create'), nav: false, title:'Category Create'},

      { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: true, title: 'Login' },
      { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: true, title: 'Register' }

    ]);

    config.mapUnknownRoutes('views/home/index')

  }

}
