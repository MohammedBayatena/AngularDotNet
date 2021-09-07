import { authUser } from './../authResponseDto';
import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createReducer,
  createSelector,
  MetaReducer,
  on,
} from '@ngrx/store';
import { AuthActions } from '../action-types';

export const authFeatureKey = 'auth';

export interface AuthState {
  user: authUser | undefined;
}

export const initialAuthState: AuthState = {
  user: undefined,
};

export const AuthReducer = createReducer(
  initialAuthState,
  on(AuthActions.login, (state, action) => {
    return {
      user: action.user,
    };
  }),
  on(AuthActions.logout, (state, action) => {
    return {
      user: undefined,
    };
  })
);
