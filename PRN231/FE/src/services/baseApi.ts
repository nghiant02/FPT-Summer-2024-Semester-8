import { fetchBaseQuery, FetchBaseQueryError } from '@reduxjs/toolkit/query/react';
import 'immer';
import { KEYS } from '../enums';
const baseQuery = fetchBaseQuery({
    baseUrl: 'http://localhost:5188/',
    prepareHeaders: async (headers) => {
        const token = await localStorage.getItem(KEYS.ACCESS_TOKEN);
        if (token) {
            headers.set('Authorization', `Bearer ${token}`);
        }
        return headers;
    },
});

const baseQueryNoHeader = fetchBaseQuery({
    baseUrl: 'http://localhost:5188/',
    prepareHeaders: async (headers) => {
       
        return headers;
    },
});

export const baseQueryWithReauth: (args: any, api: any, extraOptions: any) => any = async (
    args: any,
    api: any,
    extraOptions: any,
) => {
    const hasHeader = extraOptions?.hasHeader ?? true;
  let response;
  if (hasHeader) {
    response = await baseQuery(args, api, extraOptions);
  } else {
    response = await baseQueryNoHeader(args, api, extraOptions);
  }

    if (response.error && (response.error as FetchBaseQueryError).status === 401) {
        
    }

    return response;
};
