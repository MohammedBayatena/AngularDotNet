import { CharacterslistComponent } from './characterslist/characterslist.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainComponent } from './../main/main.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CharacterComponent } from './character/character.component';
import { AuthGuard } from '../auth/auth.guard';
import { CharacterResolver } from './services/character.resolver';
import { CharactersResolver } from './services/characters.resolver';
import { CharacterformComponent } from './characterform/characterform.component';
import { CharactersFormResolver } from './services/characterform.resolver';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    component: MainComponent,
    resolve: {
      characters: CharactersResolver, // Accessed by main component
    },
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        component: DashboardComponent,
        children: [
          {
            path: 'character/new',
            component: CharacterformComponent,
            resolve: { character: CharactersFormResolver },
          },
          {
            path: 'character/edit/:id',
            component: CharacterformComponent,
            resolve: { character: CharactersFormResolver },
          },
          {
            path: 'character/:id',
            component: CharacterComponent,
            resolve: { character: CharacterResolver },
          },
        ],
      },
      { path: 'list', component: CharacterslistComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [CharacterResolver],
})
export class CharactersRoutingModule {}
