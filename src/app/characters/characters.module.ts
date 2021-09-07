import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { CharactersResolver } from './services/characters.resolver';
import { CharactersService } from './../services/characters.service';
import { SkillsService } from './../services/skills.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CharacterslistComponent } from './characterslist/characterslist.component';
import { MainComponent } from '../main/main.component';
import { CharacterComponent } from './character/character.component';
import { WeaponsService } from '../services/weapons.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { CharactersRoutingModule } from './characters-routing.module';
import { HttpErrorHandler } from '../http-error-handler.service';
import { MessageService } from '../message.service';
import { AuthInterceptor } from '../http-interceptors/auth-interceptor';
import { CharactersEffects } from './characters.effects';
import { charctersReducer } from './reducers/character.reducers';
import { CharacterformComponent } from './characterform/characterform.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { CharactersFormResolver } from './services/characterform.resolver';

@NgModule({
  declarations: [
    DashboardComponent,
    CharacterslistComponent,
    MainComponent,
    CharacterComponent,
    CharacterformComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    NgMultiSelectDropDownModule.forRoot(),
    CharactersRoutingModule,
    EffectsModule.forFeature([CharactersEffects]),
    StoreModule.forFeature('characters', charctersReducer),
  ],
  providers: [
    WeaponsService,
    SkillsService,
    CharactersService,
    HttpErrorHandler,
    MessageService,
    CharactersResolver,
    CharactersFormResolver,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  exports: [MainComponent,CharacterformComponent],
})
export class CharactersModule {}
