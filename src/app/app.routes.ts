// import { MainComponent } from './main/main.component';
// import { CharacterslistComponent } from './characters/characterslist/characterslist.component';
// import { DashboardComponent } from './characters/dashboard/dashboard.component';
// import { RegisterComponent } from './auth/register/register.component';
// // app.routes.ts (Route Configurations)
// import { Routes, RouterModule } from '@angular/router';
// import { CharacterComponent } from './characters/character/character.component';
// import { LoginComponent } from './auth/login/login.component';
// import { AuthGuard } from './auth/auth.guard';

// const APP_ROUTES: Routes = [
//   { path: '', redirectTo: '/login', pathMatch: 'full' },
//   { path: 'login', component: LoginComponent },
//   { path: 'register', component: RegisterComponent },

//   {
//     path: 'main',
//     component: MainComponent,
//     // canActivate: [AuthGuard],
//     loadChildren: () => import('./characters/characters.module').then(m => m.CharactersModule),
//     // children: [
//     //   {
//     //     path: '',
//     //     redirectTo: 'dashboard',
//     //     pathMatch: 'full',
//     //   },
//     //   {
//     //     path: 'dashboard',
//     //     component: DashboardComponent,
//     //     children: [{ path: 'character', component: CharacterComponent }],
//     //   },
//     //   { path: 'characters', component: CharacterslistComponent },
//     // ],
//   },
// ];
// export const routing = RouterModule.forRoot(APP_ROUTES);
