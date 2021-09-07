import { allCharactersLoaded, characterAdded } from './../character.actions';
import { createEntityAdapter, EntityState } from '@ngrx/entity';
import { createReducer, on } from '@ngrx/store';
import { CharacterActions } from '../action-types';
import { Character } from './../models/Character';

export interface CharacterState extends EntityState<Character> {
  allCharactersLoaded: boolean; // We added this to prevent the router from laoding the data from the back end if its already there
  // entities: {
  //   [key: number]: Character;
  // };
  // ids: number[]; same as NGRX Entites
}

export const adapter = createEntityAdapter<Character>();
// {sortComparer= //Compare Data and sorts}

export const initialCharactersState = adapter.getInitialState({
  allCharactersLoaded: false,
}); // inhitialize

export const charctersReducer = createReducer(
  initialCharactersState,
  on(
    CharacterActions.allCharactersLoaded,
    (state, action) =>
      adapter.setAll(action.characters, { ...state, allCharactersLoaded: true })
    //Copied state and changed the characters to loaded true after the router transition
  ),
  on(CharacterActions.characterUpdated, (state, action) =>
    adapter.updateOne(action.update, state)
  ),

  on(CharacterActions.characterAdded, (state, action) =>
    adapter.addOne(action.character as Character, state)
  ),
  on(CharacterActions.characterDeleted, (state, action) =>
    adapter.removeOne(action.character.id, state)
  )
);

export const { selectAll } = adapter.getSelectors();
