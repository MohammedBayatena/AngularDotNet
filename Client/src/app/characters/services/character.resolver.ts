import { CharactersService } from './../../services/characters.service';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable } from 'rxjs';
import { Character } from './../models/Character';
import { first } from 'rxjs/operators';

@Injectable()
export class CharacterResolver implements Resolve<Character> {
  constructor(private _characterService: CharactersService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Character | Observable<Character> | Promise<Character> | Character {
    const id = route.paramMap.get('id');
    return this._characterService
      .getCharacterById(id ? id : '0')
      .pipe
      // first() //In case observable doesnt complete and keeps sending forever
      ();
  }
}
