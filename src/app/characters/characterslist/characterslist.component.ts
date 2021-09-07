import { selectAllCharacters } from './../characters.selectors';
import { AppState } from './../../reducers/index';
import { Store, select } from '@ngrx/store';
import { Character } from './../models/Character';
import { CharactersService } from '../../services/characters.service';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-characterslist',
  templateUrl: './characterslist.component.html',
  styleUrls: ['./characterslist.component.css'],
})
export class CharacterslistComponent implements OnInit {
  constructor(private _store: Store<AppState>) {}

  characters$!: Observable<Array<Character>>;

  ngOnInit(): void {
    this.characters$ = this._store.pipe(select(selectAllCharacters));
  }
}
