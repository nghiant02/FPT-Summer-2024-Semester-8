export interface PaggingRespone<T> {
    pageNumber: number;
    pageSize: number;
    totalPage: number;
    totalRecord: number;
    data: T[];
}

export interface PaggingRequest<T> {
    pageNumber: number;
    pageSize: number;
    data: T;
}

export type Mode = 'Update' | 'Create' | 'View';
