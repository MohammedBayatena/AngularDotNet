import { login } from './auth.actions';
import { tap } from 'rxjs/operators';
import { AppState } from './../reducers/index';
import { select, Store } from '@ngrx/store';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  CanLoad,
  Route,
  Router,
  RouterStateSnapshot,
  UrlSegment,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { isLoggedIn } from './auth.selectors';
import { authUser } from './authResponseDto';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private _router: Router, private _store: Store<AppState>) {
    const user: string | null = localStorage.getItem('user');
    if (user) {
      const sessionUser: authUser = JSON.parse(user);
      this._store.dispatch(login({ user: sessionUser }));
    }
  }
  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean | UrlTree> {
    return this.checkIfAuthinticated();
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
    return this.checkIfAuthinticated();
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.checkIfAuthinticated();
  }

  private checkIfAuthinticated() {
    return this._store.pipe(
      select(isLoggedIn),
      tap((loggedIn) => {
        if (!loggedIn) {
          this._router.navigateByUrl('/login');
        }
      })
    );
  }
}
