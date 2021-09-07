import { AddCharacterDto } from './../characters/models/AddCharacterDto';
import { SkillCharacter } from './../characters/models/SkillCharacter';
import { Character } from './../characters/models/Character';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, ReplaySubject, Subject } from 'rxjs';
import { shareReplay, tap, map, filter } from 'rxjs/operators';
import { Skill } from '../characters/models/Skill';

@Injectable({
  providedIn: 'root',
})
export class CharactersService {
  constructor(private _http: HttpClient, private _router: Router) {}

  /** GET: GET ALL characters from database. */
  getAllCharacters(): Observable<Array<Character>> {
    return this._http.get<any>('https://localhost:5001/Character').pipe(
      map((result) => result.data),
      shareReplay()
      // catchError(this.handleError(''))
    );
  }

  /** GET: GET Certain User characters from database. */
  getUserCharacters(): Observable<Array<Character>> {
    return this._http
      .get<any>('https://localhost:5001/Character/GetUserCharacters')
      .pipe(
        map((result) => result.data),
        shareReplay()
        // catchError(this.handleError(''))
      );
  }

  /** GET: GET a character by id from database. */
  getCharacterById(id: string): Observable<Character> {
    return this._http.get<any>(`https://localhost:5001/Character/${id}`).pipe(
      map((response) => response?.data),
      shareReplay()
      // catchError(this.handleError(''))
    );
  }

  /** PUT: Update a character by id in database. */
  updateCharacter(id: number, body: Partial<Character>) {
    return this._http
      .put<any>(`https://localhost:5001/Character/${id}`, body)
      .pipe(map((response) => response?.data));
  }

  /** POST: ADD  a a group of skills to a character by ids pair in database. */
  addSkills(body: Array<SkillCharacter>) {
    return this._http
      .post<any>(`https://localhost:5001/Character/skill`, body)
      .pipe(map((response) => response?.data));
  }

  /** POST: ADD  a Character to the database. */
  addCharacter(body: AddCharacterDto) {
    return this._http
      .post<any>(`https://localhost:5001/Character`, body)
      .pipe(map((response) => response?.data));
  }

  /** GET: Get All Skills in the Database */
  getSkills(): Observable<Array<Skill>> {
    return this._http
      .get<any>(`https://localhost:5001/Skill`)
      .pipe(map((response) => response?.data));
  }

  /** DELETE: Delete a character from the Database */
  deleteCharacter(id: number): Observable<any> {
    return this._http
      .delete<any>(`https://localhost:5001/Character/id?id=${id}`)
      .pipe(map((response) => response?.data));
  }
}
