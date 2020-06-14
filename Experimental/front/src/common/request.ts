import { MiCakeApiModel } from './environment';

/* 
    Http Request请求辅助类
*/
export default class MiCakeRequest {
    /**
     *发起一个Http请求
     *
     * @param {string} url 请求地址
     * @param {HttpMethod} method Http请求的方法
     * @param {RequestData} data Http请求的数据
     */
    public request(url: string, method: HttpMethod, requestData: string | undefined): Promise<string | AnyObject | ArrayBuffer | undefined> {
        var result = new Promise<string | AnyObject | ArrayBuffer | undefined>((reslove, reject) => {
            uni.request({
                url: url,
                method: method,
                data: requestData,
                success: res => {
                    var result = this.handleResult(res);
                    if (result.isError) {
                        reject(res);
                    } else {
                        reslove(res.data);
                    }
                },
                fail: error => {
                    this.showErrorMsg();
                    reject(error);
                }
            });
        });
        return result;
    }

    /**
     *发起一个Http请求，该请求将返回@type {MiCakeApiModel} 包裹的结果模型。
    * @param {TApiModel} 期望接收到的数据模型
    * @param {string} url 请求地址
    * @param {HttpMethod} method Http请求的方法
    * @param {RequestData} requestData Http请求的数据
    */
    public requestWithModel<TApiModel>(url: string, method: HttpMethod, requestData: string | undefined): Promise<MiCakeApiModel<TApiModel>> {
        var result = new Promise((reslove: (value: MiCakeApiModel<TApiModel>) => void, reject) => {
            uni.request({
                url: url,
                method: method,
                data: requestData,
                success: res => {
                    try {
                        var result = this.handleResult(res);
                        let micakeApiModelResult = <MiCakeApiModel<TApiModel>>result.objectResult;

                        if (result.isError) {
                            reject(res);
                        } else {
                            if (micakeApiModelResult) {
                                reslove(micakeApiModelResult);
                            } else {
                                //如果存在错误，则toast错误信息。（这些错误信息属于业务异常）
                            }
                        }
                    } catch{ 
                        //如果出现类型转换错误等其他异常。
                        reject(res);
                    }
                },
                fail: error => {
                    this.showErrorMsg();
                    reject(error);
                }
            });
        });
        return result;
    }

    private handleResult(result: UniApp.RequestSuccessCallbackResult): { isError: boolean, objectResult: any } {
        let isError: boolean = false;
        let resultData: any;

        //request ok
        if (result.statusCode == 200) {
            resultData = JSON.parse(<string>result.data);
        }
        //authorize fail
        if (result.statusCode == 401) {
            this.authoricationFailedMsg();
            isError = true;
        }
        //request bad
        if (result.statusCode == 500) {
            this.serverErrorMsg();
            isError = true;
        }

        return { isError: isError, objectResult: resultData };
    }

    /**
     * 当http请求没有网络，请求失败的情况下所显示的提示。
     */
    private showErrorMsg() {
        uni.showToast({
            title: "网络出了一点点小差，稍后再试试哦~",
            icon: 'none',
            duration: 2000
        });
    }

    /**
     * 当服务端发生错误时，提示用户以及进行错误反馈。
     */
    private serverErrorMsg() {
        uni.showToast({
            title: "服务器返回错误~",
            icon: 'none',
            duration: 2000
        });
    }

    /**
     * 当用户身份验证失败时，所显示的提示
     */
    private authoricationFailedMsg() {
        //todo.
    }
}

export enum HttpMethod {
    Options = "OPTIONS",
    Get = "GET",
    HEAD = "HEAD",
    POST = "POST",
    PUT = "PUT",
    DELETE = "DELETE",
    TRACE = "TRACE",
    CONNECT = "CONNECT"
}