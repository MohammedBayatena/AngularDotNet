import { Observable } from 'rxjs';
import { LoginUserDto } from './../login/LoginUserDto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  constructor(private _http: HttpClient) {

  }

  /** POST: Register a new User to database **/

  register(user: LoginUserDto): Observable<any> {
    return this._http
      .post<any>('https://localhost:5001/Auth/Register', user)
      .pipe(map((result) => result));
  }
}
