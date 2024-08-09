import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../store';
import { TokenDecode, User } from '../types/user.type';
import { saveToken } from '../utils/auth';
import { jwtDecode } from 'jwt-decode';

export interface AuthState {
    tokenDecode: TokenDecode;
    user: User;
}

const initUser: User = {
    counterNumber: 0,
    email: '',
    fullName: '',
    gender: '',
    phoneNumber: '',
    roleName: 'Guest',
    status: false,
    userId: '',
    username: '',
    counterId: '',
};

const initToken: TokenDecode = {
    aud: '',
    email: '',
    exp: 0,
    iat: 0,
    Id: '',
    iss: '',
    jti: '',
    nbf: 0,
    role: '',
    sub: '',
    nameid: 'test',
};

const initialState: AuthState = {
    tokenDecode: initToken,
    user: initUser,
};

export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setToken: (state, action: PayloadAction<string>) => {
            state.tokenDecode = jwtDecode<TokenDecode>(action.payload);
            saveToken(action.payload);
        },
        setUser: (state, action: PayloadAction<User>) => {
            state.user = action.payload;
        },
    },
});

export const selectAuth = (state: RootState) => state.auth;

export const { setToken, setUser } = authSlice.actions;

export default authSlice.reducer;
