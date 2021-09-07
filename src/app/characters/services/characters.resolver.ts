import { finalize, first, tap, filter } from 'rxjs/operators';
import { AppState } from '../../reducers/index';
import { Store, select } from '@ngrx/store';
import { Character } from '../models/Character';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable } from 'rxjs';
import { loadAllCharacters } from '../character.actions';
import { areCharactersLoaded } from '../characters.selectors';

@Injectable()
export class CharactersResolver implements Resolve<any> {
  loading = false;
  constructor(private _store: Store<AppState>) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    return this._store.pipe(
      select(areCharactersLoaded),
      tap((charactersLoaded) => {
        if (!this.loading && !charactersLoaded) {
          this.loading = true;
          this._store.dispatch(loadAllCharacters());
        }
      }),

      filter((charactersLoaded) => charactersLoaded),
      //mean that the observable will only terminate if the data is there
      first(),
      finalize(() => (this.loading = false)) //if observable complete or errors out finalize is called
    );
  }
}
