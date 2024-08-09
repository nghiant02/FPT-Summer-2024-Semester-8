import { Promotion } from '../types/promotion.type';
import { baseQueryWithReauth } from './baseApi';
import { createApi } from '@reduxjs/toolkit/query/react';

export const promotionApi = createApi({
    baseQuery: baseQueryWithReauth,
    tagTypes: ['promotion'],
    refetchOnMountOrArgChange: true,
    endpoints: (builder) => ({
        getJewelries: builder.query<Promotion[], void>({
            query: () => ({
                url: `api/Promotion/GetPromotions`,
                method: 'GET',
            }),
        }),
    }),
    reducerPath: 'promotionApi',
});

export default promotionApi;
