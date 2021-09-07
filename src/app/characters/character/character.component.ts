import { map, tap } from 'rxjs/operators';
import { Character } from './../models/Character';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/reducers';
import { deleteCharacter } from '../character.actions';

@Component({
  selector: 'app-character',
  templateUrl: './character.component.html',
  styleUrls: ['./character.component.css'],
})
export class CharacterComponent implements OnInit {
  character$!: Observable<Character>;
  // (!) This mean character will have value in runtime so skip initializing
  // ($) This mean that this is an observable

  constructor(
    private _store: Store<AppState>,
    private _route: ActivatedRoute,
    private _router: Router
  ) {}

  doDelete(id: number) {
    this._store.dispatch(deleteCharacter({ id: id }));
    this._router.navigateByUrl("/characters/dashboard");
  }

  ngOnInit(): void {
    this.character$ = this._route.data.pipe(map((data) => data['character']));
  }
}
