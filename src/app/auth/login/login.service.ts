import { tap, map } from 'rxjs/operators';
import { authUser } from './../authResponseDto';
import { LoginUserDto } from './LoginUserDto';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private _http: HttpClient) {}

  /** POST: Login and return the autorization token if loggd in **/
  login(user: LoginUserDto): Observable<authUser> {
    return this._http
      .post<any>('https://localhost:5001/Auth/Login', user)
      .pipe(map((result) => result.data));
  }
}
