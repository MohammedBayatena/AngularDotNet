import { authUser } from './authResponseDto';
import { createAction, props } from '@ngrx/store';

export const login = createAction(
  '[Login Page] User Login',
  props<{ user: authUser }>()
);

export const logout = createAction('[Dashboard Page] User Logout');
