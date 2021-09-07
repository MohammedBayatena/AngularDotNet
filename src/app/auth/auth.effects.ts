import { Router } from '@angular/router';
import { saveToken } from 'src/app/shared/inmemory';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap } from 'rxjs/operators';
import { AuthActions } from './action-types';

@Injectable()
export class AuthEffects {
  constructor(private actions$: Actions, private _router: Router) {}

  login$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.login),
        tap((action) => {
          localStorage.setItem('user', JSON.stringify(action.user));
          saveToken(action.user.token, false);
        })
      ),
    { dispatch: false }
  );

  logout$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.logout),
        tap((action) => {
          localStorage.removeItem('user');
          localStorage.removeItem('token');
          this._router.navigateByUrl('/login');
        })
      ),
    {
      dispatch: false,
    }
  );
}
