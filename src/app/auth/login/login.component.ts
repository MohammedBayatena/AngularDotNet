import { AppState } from './../../reducers/index';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LoginService } from './login.service';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { tap, map } from 'rxjs/operators';
import { login } from '../auth.actions';
import { noop } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(
    private _loginService: LoginService,
    private _router: Router,
    private store: Store<AppState>
  ) {}

  ngOnInit(): void {}
  errors: Array<string> = [];

  onSubmit(form: NgForm) {
    const { username, passwordLogin } = form.value;
    if (form.valid) {
      this.login(username, passwordLogin);
    }
  }

  login(username: any, passwordLogin: any) {
    this._loginService
      .login({ username: username, password: passwordLogin })
      .pipe(
        tap((user) => {
          this.store.dispatch(login({ user }));
          this._router.navigateByUrl('/characters');
        })
      )
      .subscribe(noop, () => {
        this.errors = [
          ...this.errors,
          "Invalid Credintials or Username Doesn't Exist.",
        ];
      });
  }
}
