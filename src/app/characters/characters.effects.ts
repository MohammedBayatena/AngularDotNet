import { SkillCharacter } from './models/SkillCharacter';
import { CharactersService } from './../services/characters.service';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { CharacterActions } from './action-types';
import {
  concatMap,
  exhaustMap,
  map,
  mergeMap,
  switchMap,
  tap,
} from 'rxjs/operators';
import {
  allCharactersLoaded,
  addCharacter,
  characterAdded,
  characterDeleted,
} from './character.actions';
import { forkJoin, pipe } from 'rxjs';
import { Skill } from './models/Skill';

@Injectable()
export class CharactersEffects {
  constructor(
    private $actions: Actions,
    private _charactersService: CharactersService
  ) {}

  loadCharacters$ = createEffect(() =>
    this.$actions.pipe(
      ofType(CharacterActions.loadAllCharacters),
      concatMap((action) => this._charactersService.getAllCharacters()),
      map((characters) => allCharactersLoaded({ characters: characters }))
    )
  );

  addCharacter$ = createEffect(() =>
    this.$actions.pipe(
      ofType(CharacterActions.addCharacter),
      concatMap((action) =>
        this._charactersService.addCharacter(action.character).pipe(
          tap((character) => {
            const skills = action.skills.map((skill) => ({
              characterId: character.id,
              skillId: skill.toString(),
            }));
            this._charactersService.addSkills(skills).subscribe();
          })
        )
      ),
      map((result) => characterAdded({ character: result }))
      // tap((result) => console.log(result))
    )
  );

  //This is saveCharacter but written like this by mistake
  saveCourse$ = createEffect(
    () =>
      this.$actions.pipe(
        ofType(CharacterActions.characterUpdated),
        concatMap((action) =>
          forkJoin([
            this._charactersService.updateCharacter(
              action.update.id as number,
              action.update.changes
            ),
            this._charactersService.addSkills(action.skills),
          ])
        ),
        tap((result) => alert('Character have been updated Successfully'))
      ),
    { dispatch: false }
  );

  deleteCharacter$ = createEffect(() =>
    this.$actions.pipe(
      ofType(CharacterActions.deleteCharacter),
      concatMap((action) => this._charactersService.deleteCharacter(action.id)),
      map((result) => characterDeleted({ character: result }))
      // tap((result) => alert('Character have been deleted Successfully'))
    )
  );
}
