import {RouterConfiguration, Router, NavigationInstruction, Next, PipelineStep, Redirect, activationStrategy } from 'aurelia-router';
import {autoinject, PLATFORM} from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { configure } from 'main';

@autoinject
export class App {

  router?: Router
  private message: string = "isAdmin";

  constructor(private appState: AppState){

  }

  configureRouter(config: RouterConfiguration, router: Router): void{
    this.router = router;

    config.title = "ArtGallery";
    config.addAuthorizeStep(AuthorizeStep);

    config.map([
      {route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/index'), nav: true, title:'Home'},

      {route: ['artists', 'artists/index'], name: 'artists', moduleId: PLATFORM.moduleName('views/artists/index'), nav: true, title:'Artists', settings: { roles: [] }},
      {route: ['artists/details/:id'], name: 'artistDetails', moduleId: PLATFORM.moduleName('views/artists/details'), nav: false, title:'Artist Details', settings: { roles: [] }},
      {route: ['artists/edit/:id'], name: 'artistEdit', moduleId: PLATFORM.moduleName('views/artists/edit'), nav: false, title:'Artist Edit', settings: { roles: ['admin'] }},
      {route: ['artists/create'], name: 'artistCreate', moduleId: PLATFORM.moduleName('views/artists/create'), nav: false, title:'Artist Create', settings: { roles: ['admin'] }},
      {route: ['artists/delete/:id'], name: 'artistDelete', moduleId: PLATFORM.moduleName('views/artists/delete'), nav: false, title:'Artist Delete', settings: { roles: ['admin'] }},

      {route: ['comments', 'comments/index'], name: 'comments', moduleId: PLATFORM.moduleName('views/comments/index'), nav: true, title:'Comments', settings: { roles: ['admin'] }},
      {route: ['comments/edit/:id'], name: 'commentEdit', moduleId: PLATFORM.moduleName('views/comments/edit'), nav: false, title:'Comment Edit', settings: { roles: ['user', 'admin'] }},
      {route: ['comments/delete/:id'], name: 'commentDelete', moduleId: PLATFORM.moduleName('views/comments/delete'), nav: false, title:'Comment Delete', settings: { roles: ['admin'] }},
      {route: ['comments/create'], name: 'commentCreate', moduleId: PLATFORM.moduleName('views/comments/create'), nav: false, title:'Comment Create', settings: { roles: ['user', 'admin'] }},
      {route: ['comments/details/:id'], name: 'commentDetails', moduleId: PLATFORM.moduleName('views/comments/details'), nav: false, title:'Comment Details', settings: { roles: [] }},


      {route: ['orders', 'orders/index'], name: 'orders', moduleId: PLATFORM.moduleName('views/orders/index'), nav: true, title:'Orders', settings: { roles: ['user', 'admin'] }},

      {route: ['userPaymentMethods', 'userPaymentMethods/index'], name: 'userPaymentMethods', moduleId: PLATFORM.moduleName('views/UPMethods/index'), nav: true, title:'User Payment Methods', settings: { roles: ['user', 'admin'] }},

      {route: ['paintings', 'paintings/index'], name: 'paintings', moduleId: PLATFORM.moduleName('views/paintings/index'), nav: true, title:'Paintings', settings: { roles: [] }},
      {route: ['paintings/details/:id'], name: 'paintingDetails', moduleId: PLATFORM.moduleName('views/paintings/details'), nav: false, title:'Painting Details', activationStrategy: activationStrategy.invokeLifecycle
      ,settings: { roles: [] }},
      {route: ['paintings/create'], name: 'paintingCreate', moduleId: PLATFORM.moduleName('views/paintings/create'), nav: false, title:'Painting Create', settings: { roles: ['admin'] }},
      {route: ['paintings/edit/:id'], name: 'paintingEdit', moduleId: PLATFORM.moduleName('views/paintings/edit'), nav: false, title:'Painting Edit', settings: { roles: ['admin'] }},
      {route: ['paintings/delete/:id'], name: 'paintingDelete', moduleId: PLATFORM.moduleName('views/paintings/delete'), nav: false, title:'Painting Delete', settings: { roles: ['admin'] }},

      {route: ['baskets', 'baskets/index'], name: 'baskets', moduleId: PLATFORM.moduleName('views/baskets/index'), nav: true, title:'Baskets', settings: { roles: ['admin'] }},

      {route: ['categories', 'categories/index'], name: 'categories', moduleId: PLATFORM.moduleName('views/categories/index'), nav: true, title:'Categories', settings: { roles: ['admin'] }},
      {route: ['categories/delete/:id'], name: 'categoryDelete', moduleId: PLATFORM.moduleName('views/categories/delete'), nav: false, title:'Category Delete', settings: { roles: ['admin'] }},
      {route: ['categories/details/:id'], name: 'categoryDetails', moduleId: PLATFORM.moduleName('views/categories/details'), nav: false, title:'Category Details', settings: { roles: ['admin'] }},
      {route: ['categories/edit/:id'], name: 'categoryEdit', moduleId: PLATFORM.moduleName('views/categories/edit'), nav: false, title:'Category Edit', settings: { roles: ['admin'] }},
      {route: ['categories/create'], name: 'categoryCreate', moduleId: PLATFORM.moduleName('views/categories/create'), nav: false, title:'Category Create', settings: { roles: ['admin'] }},

      { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login', settings: { roles: [] }},
      { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register', settings: { roles: [] }}

    ]);

    config.mapUnknownRoutes('views/home/index')

  }

  logoutOnClick() {
    this.appState.jwt = null;
    this.appState.userName = null;
    this.appState.userRoles = null;
    this.appState.appUserId = null;
    this.router!.navigateToRoute('account-login');
  }
}

@autoinject
class AuthorizeStep implements PipelineStep {

  constructor(private appState: AppState){

  }

  public run(navigationInstruction: NavigationInstruction, next: Next): Promise<any> {

    if (navigationInstruction.getAllInstructions().some(i => i.config.settings.roles && i.config.settings.roles.indexOf('user') !== -1 )) {
      
      if (this.appState.jwt !== null) {
        return next();
      }
      return next.cancel(new Redirect('account/login'));
 

    } else if (navigationInstruction.getAllInstructions().some(i => i.config.settings.roles && i.config.settings.roles.indexOf('admin') !== -1 )) {
      
      var isAdmin = false;

      if (this.appState.userRoles !== null) {
        isAdmin = (this.appState.userRoles.some(r => r == "admin"));
      }

      if (!isAdmin) {
        return next.cancel(new Redirect('account/login'));
      }

      return next();
    }

    return next();
  }
}
