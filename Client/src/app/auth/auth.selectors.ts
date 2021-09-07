import { AuthState } from './reducers/index';
import { createFeatureSelector, createSelector } from '@ngrx/store';

export const selectAuthstate = createFeatureSelector<AuthState>('auth');

export const isLoggedIn = createSelector(
  selectAuthstate,
  (auth) => !!auth.user
);

export const isLoggedOut = createSelector(isLoggedIn, (loggedIn) => !loggedIn);

export const currentUser = createSelector(
  selectAuthstate,
  (auth) => auth.user?.username
);
