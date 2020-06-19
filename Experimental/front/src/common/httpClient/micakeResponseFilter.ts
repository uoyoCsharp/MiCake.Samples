import { HttpResultFilter, ResponseData } from "./httpclient";
import { MiCakeApiModel } from "@/common/environment";
import uniHelper from '../uniHelper';

export default class MiCakeResponseFilter implements HttpResultFilter {
    order: number = 0;  //普通级别

    handle(context: ResponseData<any>): void {
        var micakeApiResponse = context.data as MiCakeApiModel<any>;

        if(micakeApiResponse.isError){
            uniHelper.showToast(micakeApiResponse.message!);
        }
    }
}