import { Character } from './../models/Character';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { from, Observable } from 'rxjs';
import { CharactersService } from 'src/app/services/characters.service';

@Injectable()
export class CharactersFormResolver implements Resolve<any> {
  constructor(private _characterService: CharactersService) {}
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<Character> | null {
    const id = route.paramMap.get('id');
    if (id) {
      return this._characterService.getCharacterById(id).pipe();
    } else {
      return null;
    }
  }
}
