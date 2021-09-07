import { Injectable } from '@angular/core';
import { getToken } from './shared/inmemory';
/** Mock client-side authentication/authorization service */
@Injectable()
export class AuthService {
  getAuthorizationToken() {
    let mytoken = getToken();
    return mytoken ? mytoken : 'Access Forbidden';
  }
}
