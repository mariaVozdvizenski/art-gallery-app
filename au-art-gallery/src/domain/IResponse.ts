export interface IResponse<TData> {
    statusCode: number;
    errorMessage?: string; // can be undefined
    data?: TData
}