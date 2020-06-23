import { HttpResultFilter, ResponseData } from "./httpclient";

export class MiCakeResponseNoAuthorizeFilter implements HttpResultFilter {
    order: number = 0;  //普通级别

    constructor(private redirectTo: () => void) {

    }

    handle(context: ResponseData<any>): void {
        console.log(context.statusCode);
        if ([401, 403].indexOf(context.statusCode) > -1) {
            this.redirectTo();
        }
    }
}