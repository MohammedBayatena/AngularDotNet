import { RegisterService } from './register/register.service';
import { EffectsModule } from '@ngrx/effects';
import { AuthReducer } from './reducers/index';
import { StoreModule } from '@ngrx/store';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LoginService } from './login/login.service';
import { AuthService } from '../auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ForbiddenValidatorDirective } from '../shared/forbidden-name.directive';
import { MustMatchDirective } from '../shared/must-match.directive';
import * as fromAuth from './reducers';
import { AuthGuard } from './auth.guard';
import { AuthEffects } from './auth.effects';
import { AuthRoutingModule } from './auth-routing.module';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    AuthRoutingModule,
    StoreModule.forFeature(fromAuth.authFeatureKey, AuthReducer),
    EffectsModule.forFeature([AuthEffects]),
  ],
  declarations: [
    LoginComponent,
    RegisterComponent,
    ForbiddenValidatorDirective,
    MustMatchDirective,
  ],
  exports: [LoginComponent, RegisterComponent],
})
export class AuthModule {
  static forRoot(): ModuleWithProviders<AuthModule> {
    return {
      ngModule: AuthModule,
      providers: [LoginService, AuthService, AuthGuard , RegisterService],
    };
  }
}
