export const ServerUrl: string = 'https://127.0.0.1:5001';

export class MiCakeApiModel<TDataType> {
    statusCode: number = 0;
    isError: boolean = false;
    errorCode?: string;
    message?: string;
    result?: TDataType;
}