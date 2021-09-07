import { Observable } from 'rxjs';
import { authUser } from './../auth/authResponseDto';
import { login, logout } from './../auth/auth.actions';
import { AppState } from './../reducers/index';
import { Store, select } from '@ngrx/store';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { currentUser } from '../auth/auth.selectors';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  constructor(private _store: Store<AppState>, private _router: Router) {}

  user$!: Observable<string | undefined>;

  logout = () => {
    this._store.dispatch(logout());
    this._router.navigate(['login']);
  };

  ngOnInit(): void {
    this.user$ = this._store.pipe(
      select(currentUser),
      first() // End observation
    );
  }
}
