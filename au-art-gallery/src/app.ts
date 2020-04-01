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

      {route: ['paintings', 'paintings/index'], name: 'paintings', moduleId: PLATFORM.moduleName('views/paintings/index'), nav: true, title:'Paintings'},
      {route: ['paintings/details/:id'], name: 'paintingDetails', moduleId: PLATFORM.moduleName('views/paintings/details'), nav: false, title:'Painting Details'},

      {route: ['categories', 'categories/index'], name: 'categories', moduleId: PLATFORM.moduleName('views/categories/index'), nav: true, title:'Categories'},
      {route: ['categories/details/:id'], name: 'categoryDetails', moduleId: PLATFORM.moduleName('views/categories/details'), nav: false, title:'Category Details'}
    ]);

    config.mapUnknownRoutes('views/home/index')

  }

}
