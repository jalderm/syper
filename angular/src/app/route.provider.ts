import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/clients',
        name: '::Menu:Clients',
        iconClass: 'fas fa-person-running',
        layout: eLayoutType.application,
        requiredPolicy: 'Syper.Clients',
      },
      {
        path: '/workouts',
        name: '::Menu:Workouts',
        iconClass: 'fas fa-heart-pulse',
        layout: eLayoutType.application,
        requiredPolicy: 'Syper.Workouts',
      },
      {
        path: '/training-plans',
        name: '::Menu:TrainingPlans',
        iconClass: 'fas fa-clipboard-list',
        layout: eLayoutType.application,
        requiredPolicy: 'Syper.TrainingPlans',
      },
  ]);
}
