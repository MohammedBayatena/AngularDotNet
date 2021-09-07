import { AddCharacterDto } from './models/AddCharacterDto';
import { Skill } from './models/Skill';
import { Character } from './models/Character';
import { createAction, props } from '@ngrx/store';
import { Update } from '@ngrx/entity';
import { SkillCharacter } from './models/SkillCharacter';

export const loadAllCharacters = createAction(
  '[Characters Resolver] Load Characters'
);

export const allCharactersLoaded = createAction(
  '[Load Characters Effect] All Characters Loaded',
  props<{ characters: Character[] }>()
);

export const characterUpdated = createAction(
  '[Edit Character Form] Character Updated',
  props<{ update: Update<Character>; skills: Array<SkillCharacter> }>()
  // Partial type only update what we need , no need to give all values
);

export const addCharacter = createAction(
  '[Add Character Form] Character Add',
  props<{ character: AddCharacterDto; skills: Array<number> }>()
);

export const characterAdded = createAction(
  '[Add Character Effect] Character Added',
  props<{ character: Character }>()
);

export const deleteCharacter = createAction(
  '[Character Info Card ] Character Delete',
  props<{ id: number }>()
);

export const characterDeleted = createAction(
  '[Delete Character Effect ] Character Deleted',
  props<{ character: Character }>()
);
