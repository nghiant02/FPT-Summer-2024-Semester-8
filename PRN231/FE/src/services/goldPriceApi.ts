// services/goldPriceApi.ts
import { createApi } from '@reduxjs/toolkit/query/react';
import { GoldPrice } from '../types/goldPrice.type';
import { baseQueryWithReauth } from './baseApi';

export const goldPriceApi = createApi({
    baseQuery: baseQueryWithReauth,
    tagTypes: ['goldPrice'],
    refetchOnMountOrArgChange: true,
    endpoints: (builder) => ({
        getGoldPrices: builder.query<GoldPrice[], void>({
            query: () => ({
                url: `api/Price/GetGoldPrices`,
                method: 'GET',
            }),
        }),
    }),
    reducerPath: 'goldPriceApi',
});

export default goldPriceApi;
