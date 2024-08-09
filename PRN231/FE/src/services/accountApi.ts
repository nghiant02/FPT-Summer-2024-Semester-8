import { SignInRequest, Token, User } from '../types/user.type';
import { baseQueryWithReauth } from './baseApi';
import { createApi } from '@reduxjs/toolkit/query/react';

export const accountApi = createApi({
    baseQuery: baseQueryWithReauth,
    tagTypes: ['account'],
    refetchOnMountOrArgChange: true,
    endpoints: (builder) => ({
        // signUp: builder.mutation<ApiResponse<SignUpRespone>, SignUpRequest>({
        //   query: (signUpRequest) => ({
        //     url: 'api/users/sign-up',
        //     method: 'POST',
        //     body: signUpRequest,
        //   }),
        // }),
        signIn: builder.mutation<Token, SignInRequest>({
            query: (signInRequest) => ({
                url: 'api/User/Login',
                method: 'POST',
                body: signInRequest,
            }),
            extraOptions:{
                hasHeader: false
            }
        }),Logout: builder.mutation<void, string>({
            query: (id) => ({
                url: `api/User/Logout?userId=${id}`,
                method: 'POST',
       
            }),
            extraOptions:{
                hasHeader: false
            }
        }),

        getUserById: builder.query<User, string>({
            query: (id) => ({
                url: 'api/User/GetUserById/' + id,
                method: 'GET',
            }),
        }),

        refresh: builder.mutation<{ accessToken: string }, { refreshToken: string }>({
            query: (refreshToken) => ({
                url: 'auth/refresh',
                method: 'POST',
                body: { refreshToken },
            }),
        }),
    }),
    reducerPath: 'accountApi',
});

export default accountApi;
