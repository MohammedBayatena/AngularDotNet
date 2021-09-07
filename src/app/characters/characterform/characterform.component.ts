import { CharactersService } from 'src/app/services/characters.service';
import { SkillCharacter } from './../models/SkillCharacter';
import {
  characterAdded,
  characterUpdated,
  addCharacter,
} from './../character.actions';
import { Update } from '@ngrx/entity';
import { AppState } from './../../reducers/index';
import { Weapon } from './../models/Weapon';
import { Skill } from './../models/Skill';
import { Character } from './../models/Character';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { Observable, Subject } from 'rxjs';
import { first, map, take, tap, filter } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { AddCharacterDto } from '../models/AddCharacterDto';

@Component({
  selector: 'app-characterform',
  templateUrl: './characterform.component.html',
  styleUrls: ['./characterform.component.css'],
})
export class CharacterformComponent implements OnInit {
  // dropdownList = [{}];
  selectedItems: Array<Skill> = [];
  dropdownSettings = {};
  isnewForm = true;
  title = 'New Form';
  character!: Character;
  character$!: Observable<Character> | null;
  skills$!: Observable<Skill[]> | null;

  constructor(
    private _route: ActivatedRoute,
    private _store: Store<AppState>,
    private changeDetector: ChangeDetectorRef,
    private _characterService: CharactersService
  ) {}

  ngAfterContentChecked(): void {
    this.changeDetector.detectChanges();
  }

  ngOnInit() {
    // this.dropdownList = [];
    this.selectedItems = [];
    this.skills$ = this._characterService.getSkills();
    this.character$ = this._route.data.pipe(
      map((data) => data['character']),
      tap((data) => {
        if (data != null) {
          this.isnewForm = false;
          this.title = 'Edit Form';
          this.character = data;
          const { skills } = data;
          this.selectedItems = [];
          skills.map((skill) => {
            this.selectedItems.push({
              id: skill.id,
              name: skill.name,
              damage: skill.damage,
            });
          });
        }
      })
    );

    // console.log(this.dropdownList);

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 1,
      allowSearchFilter: true,
    };
  }

  onItemSelect(item: any) {
    // console.log(item);
  }

  onSelectAll(items: any) {
    // console.log(items);
  }

  onSubmit(form: NgForm) {
    // console.log(form.value);
    const {
      charactername,
      characterhitpoints,
      characterstrength,
      characterdefence,
      type,
      weapon,
      selectedItems,
    } = form.value;

    if (form.valid) {
      if (this.isnewForm) {
        const skills: Array<number> = selectedItems.map(
          (skill: Skill) => skill.id
        );
        const toBeInserted: AddCharacterDto = {
          name: charactername,
          strength: characterstrength,
          defence: characterdefence,
          hitPoints: characterhitpoints,
          intelligence: 10,
          weapon: { name: weapon },
          type: type,
        };
        this._store.dispatch(
          addCharacter({ character: toBeInserted, skills: skills })
        );
      } else {
        const updated: Character = {
          ...this.character,
          name: charactername,
          strength: characterstrength,
          defence: characterdefence,
          hitPoints: characterhitpoints,
          weapon: { name: weapon, damage: 10 },
          type: type,
        };
        const update: Update<Character> = {
          id: updated.id,
          changes: updated,
        };
        const skills: Array<SkillCharacter> = selectedItems.map(
          (skill: Skill) => ({ characterId: updated.id, skillId: skill.id })
        );
        this._store.dispatch(
          characterUpdated({ update: update, skills: skills })
        );
      }
    }
  }
}
