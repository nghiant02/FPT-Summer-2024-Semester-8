

import { Counter } from '../types/counter.type';
import { baseQueryWithReauth } from './baseApi';
import { createApi } from '@reduxjs/toolkit/query/react';

export const counterApi = createApi({
    baseQuery: baseQueryWithReauth,
    tagTypes: ['counter'],
    refetchOnMountOrArgChange: true,
    endpoints: (builder) => ({
        getAvailableCounters: builder.query<Counter[], void>({
            query: () => ({
                url: `api/Counter/GetAvailableCounters`,
                method: 'GET',
            }),
            extraOptions:{
                hasHeader: false
            }
        }),
    }),
    reducerPath: 'counterApi',
});

export default counterApi;