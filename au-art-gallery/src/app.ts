import {RouterConfiguration, Router, NavigationInstruction, Next, PipelineStep, Redirect, activationStrategy } from 'aurelia-router';
import {autoinject, PLATFORM} from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { configure } from 'main';

@autoinject
export class App {

  router?: Router

  constructor(private appState: AppState){

  }

  configureRouter(config: RouterConfiguration, router: Router): void{
    this.router = router;

    config.title = "ArtGallery";
    config.addAuthorizeStep(AuthorizeStep);

    config.map([
      {route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/index'), nav: true, title:'Home'},

      {route: ['about', 'about/index'], name: 'about', moduleId: PLATFORM.moduleName('views/AboutUs/index'), nav: true, title:'Sell a painting', settings: {roles: []}},

      {route: ['accountSettings', 'accountSettings/index'], name: 'accountSettings', moduleId: PLATFORM.moduleName('views/AccountSettings/index'), nav: true, title:'Account Settings',  settings: { roles: ['user', 'admin'] }},
      {route: ['accountSettings/create'], name: 'accountSettingsCreate', moduleId: PLATFORM.moduleName('views/AccountSettings/create'), nav: false, title:'Account Settings Create',  settings: { roles: ['user', 'admin'] }},
      {route: ['accountSettings/edit/:id'], name: 'accountSettingsEdit', moduleId: PLATFORM.moduleName('views/AccountSettings/edit'), nav: false, title:'Account Settings Edit',  settings: { roles: ['user', 'admin'] }},


      {route: ['artists', 'artists/index'], name: 'artists', moduleId: PLATFORM.moduleName('views/Artists/index'), nav: true, title:'Artists', settings: { roles: [] }},
      {route: ['artists/details/:id'], name: 'artistDetails', moduleId: PLATFORM.moduleName('views/Artists/details'), nav: false, title:'Artist Details', settings: { roles: [] }},
      {route: ['artists/edit/:id'], name: 'artistEdit', moduleId: PLATFORM.moduleName('views/Artists/edit'), nav: false, title:'Artist Edit', settings: { roles: ['admin'] }},
      {route: ['artists/create'], name: 'artistCreate', moduleId: PLATFORM.moduleName('views/Artists/create'), nav: false, title:'Artist Create', settings: { roles: ['admin'] }},
      {route: ['artists/delete/:id'], name: 'artistDelete', moduleId: PLATFORM.moduleName('views/Artists/delete'), nav: false, title:'Artist Delete', settings: { roles: ['admin'] }},

      {route: ['comments/delete/:id'], name: 'commentDelete', moduleId: PLATFORM.moduleName('views/Comments/delete'), nav: false, title:'Comment Delete', settings: { roles: ['admin'] }},

      {route: ['checkout', 'checkout/index'], name: 'checkout', moduleId: PLATFORM.moduleName('views/Checkout/index'), nav: false, title:'Checkout', activationStrategy: activationStrategy.replace, settings: { roles: ['user','admin'] }},
      {route: ['checkout/success'], name: 'checkoutSuccess', moduleId: PLATFORM.moduleName('views/Checkout/success'), nav: false, title:'Checkout Success', settings: { roles: ['user','admin'] }},

      {route: ['PaymentMethods', 'PaymentMethods/index'], name: 'paymentMethods', moduleId: PLATFORM.moduleName('views/PaymentMethods/index'), nav: false, title:'Payment Methods', settings: { roles: ['admin'] }},
      {route: ['PaymentMethods/create'], name: 'paymentMethodsCreate', moduleId: PLATFORM.moduleName('views/PaymentMethods/create'), nav: false, title:'Payment Methods Create', settings: { roles: ['admin'] }},
      {route: ['PaymentMethods/edit/:id'], name: 'paymentMethodsEdit', moduleId: PLATFORM.moduleName('views/PaymentMethods/edit'), nav: false, title:'Payment Methods Edit', settings: { roles: ['admin'] }},
      {route: ['PaymentMethods/delete/:id'], name: 'paymentMethodsDelete', moduleId: PLATFORM.moduleName('views/PaymentMethods/delete'), nav: false, title:'Payment Methods Delete', settings: { roles: ['admin'] }},

      {route: ['ordersAdmin', 'orders/admin'], name: 'ordersAdmin', moduleId: PLATFORM.moduleName('views/Orders/admin/index'), nav: true, title:'Orders Admin', settings: { roles: ['admin'] }},
      {route: ['orders', 'orders/customer'], name: 'ordersCustomer', moduleId: PLATFORM.moduleName('views/Orders/customer/index'), nav: true, title:'Orders', settings: { roles: ['user'] }},

      {route: ['paintings', 'paintings/index'], name: 'paintings', moduleId: PLATFORM.moduleName('views/Paintings/index'), nav: true, title:'Paintings', settings: { roles: [] }},
      {route: ['paintings/details/:id'], name: 'paintingDetails', moduleId: PLATFORM.moduleName('views/Paintings/details'), nav: false, title:'Painting Details', activationStrategy: activationStrategy.invokeLifecycle
      ,settings: { roles: [] }},
      {route: ['paintings/create'], name: 'paintingCreate', moduleId: PLATFORM.moduleName('views/Paintings/create'), nav: false, title:'Painting Create', settings: { roles: ['admin'] }},
      {route: ['paintings/edit/:id'], name: 'paintingEdit', moduleId: PLATFORM.moduleName('views/Paintings/edit'), nav: false, title:'Painting Edit', settings: { roles: ['admin'] }},
      {route: ['paintings/delete/:id'], name: 'paintingDelete', moduleId: PLATFORM.moduleName('views/Paintings/delete'), nav: false, title:'Painting Delete', settings: { roles: ['admin'] }},

      {route: ['baskets', 'baskets/index'], name: 'baskets', moduleId: PLATFORM.moduleName('views/Baskets/index'), nav: true, title:'Cart', activationStrategy: activationStrategy.replace, settings: { roles: ['user', 'admin'] }},

      {route: ['categories', 'categories/index'], name: 'categories', moduleId: PLATFORM.moduleName('views/Categories/index'), nav: true, title:'Categories', settings: { roles: ['admin'] }},
      {route: ['categories/delete/:id'], name: 'categoryDelete', moduleId: PLATFORM.moduleName('views/Categories/delete'), nav: false, title:'Category Delete', settings: { roles: ['admin'] }},
      {route: ['categories/edit/:id'], name: 'categoryEdit', moduleId: PLATFORM.moduleName('views/Categories/edit'), nav: false, title:'Category Edit', settings: { roles: ['admin'] }},
      {route: ['categories/create'], name: 'categoryCreate', moduleId: PLATFORM.moduleName('views/Categories/create'), nav: false, title:'Category Create', settings: { roles: ['admin'] }},

      { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/Account/login'), nav: false, title: 'Login', settings: { roles: [] }},
      { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/Account/register'), nav: false, title: 'Register', settings: { roles: [] }}

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
