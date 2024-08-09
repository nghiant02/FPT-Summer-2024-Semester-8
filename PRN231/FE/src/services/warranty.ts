
import { AddWarrantyRequest } from '../types/warranty.type';
import { baseQueryWithReauth } from './baseApi';
import { createApi } from '@reduxjs/toolkit/query/react';

export const warrantyApi = createApi({
    baseQuery: baseQueryWithReauth,
    tagTypes: ['warranty'],
    refetchOnMountOrArgChange: true,
    endpoints: (builder) => ({
        addWarranty: builder.mutation<void, AddWarrantyRequest>({
            query: (body) => ({
                url: `api/Warranty/CreateWarranty`,
                method: 'POST',
                body
            }),
        }),
    }),
    reducerPath: 'warrantyApi',
});

export default warrantyApi;