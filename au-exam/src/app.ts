import { RouterConfiguration, Router, NavigationInstruction, Next, PipelineStep, Redirect, activationStrategy } from 'aurelia-router';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { configure } from 'main';
import { AppState } from 'state/app-state';

@autoinject
export class App {

  router?: Router

  constructor(private appState: AppState) {

  }

  configureRouter(config: RouterConfiguration, router: Router): void {
    this.router = router;

    config.title = "QuizApp";
    config.addAuthorizeStep(AuthorizeStep);

    config.map([
      { route: ['', 'quizzes', 'quizzes/index'], name: 'quizzes', moduleId: PLATFORM.moduleName('views/Quizzes/index'), nav: true, title: 'Quizzes' },
      { route: ['quizzes/details/'], name: 'quizDetails', moduleId: PLATFORM.moduleName('views/Quizzes/details'), nav: false, title: 'Quiz Details' },
      { route: ['quizzes/create/'], name: 'quizCreate', moduleId: PLATFORM.moduleName('views/Quizzes/create'), nav: false, title: 'Quiz Create', settings: { roles: ['admin'] } },


      { route: ['completed/index/'], name: 'completed', moduleId: PLATFORM.moduleName('views/Completed/index'), nav: false, title: 'Completed' },
      { route: ['completedPoll/index/'], name: 'completedPoll', moduleId: PLATFORM.moduleName('views/CompletedPoll/index'), nav: false, title: 'Completed Poll' },

      { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/Account/login'), nav: false, title: 'Login', settings: { roles: [] } },
      { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/Account/register'), nav: false, title: 'Register', settings: { roles: [] } }
    ]);

    config.mapUnknownRoutes('views/Quizzes/index')
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

  constructor(private appState: AppState) {

  }

  public run(navigationInstruction: NavigationInstruction, next: Next): Promise<any> {

    if (navigationInstruction.getAllInstructions().some(i => i.config.settings.roles && i.config.settings.roles.indexOf('user') !== -1)) {

      if (this.appState.jwt !== null) {
        return next();
      }
      return next.cancel(new Redirect('account/login'));


    } else if (navigationInstruction.getAllInstructions().some(i => i.config.settings.roles && i.config.settings.roles.indexOf('admin') !== -1)) {

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