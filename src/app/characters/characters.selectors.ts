import { currentUser } from './../auth/auth.selectors';
import { createFeatureSelector, createSelector, select } from '@ngrx/store';
import { CharacterState } from './reducers/character.reducers';
import * as fromCharacters from './reducers/character.reducers';

export const selectCharactersState =
  createFeatureSelector<CharacterState>('characters');

export const selectAllCharacters = createSelector(
  selectCharactersState,
  fromCharacters.selectAll
);

export const selectCharacter = createSelector(
  selectCharactersState,
  fromCharacters.selectAll
);

export const selectUserCharacters = createSelector(
  selectAllCharacters,
  currentUser,
  (characters, user) =>
    characters.filter((character) => character.user.username == user)
);

export const selectKnightCharacters = createSelector(
  selectAllCharacters,
  (characters) => characters.filter((character) => character.type == 'Knight')
);

export const selectHealerCharacters = createSelector(
  selectAllCharacters,
  (characters) => characters.filter((character) => character.type == 'Healer')
);

export const selectWizardCharacters = createSelector(
  selectAllCharacters,
  (characters) => characters.filter((character) => character.type == 'Wizard')
);

export const areCharactersLoaded = createSelector(
  selectCharactersState,
  (state) => state.allCharactersLoaded
);
