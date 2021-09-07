import { selectUserCharacters } from './../characters.selectors';
import { Character } from './../models/Character';
import { isLoggedOut } from '../../auth/auth.selectors';
import { AppState } from '../../reducers/index';
import { map, shareReplay } from 'rxjs/operators';
import { AuthState } from '../../auth/reducers/index';
import { select, Store } from '@ngrx/store';
import { CharactersService } from '../../services/characters.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
// import { logout } from '../shared/inmemory';
import { Observable, of } from 'rxjs';
import { isLoggedIn } from '../../auth/auth.selectors';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  constructor(private _store: Store<AppState>) {}

  characters$!: Observable<Array<Character>>;

  ngOnInit(): void {
    this.characters$ = this._store.pipe(select(selectUserCharacters));
  }
}
