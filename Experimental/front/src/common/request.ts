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
    public request(url: string, method: HttpMethod, data: RequestData): void {
        var result = new Promise((reslove, reject) => {
            uni.request({
                url: url,
                method: method,
                data: data,
                success: res => {
                    //request ok
                    if (res.statusCode == 200) {
                        reslove(res.data);
                    }
                    //authorize fail
                    if (res.statusCode == 401) {

                    }
                    //request bad
                    if (res.statusCode == 500) {

                    }
                },
                fail: error => {
                    uni.showToast({
                        title: "网络出了一点点小差，稍后再试试哦~",
                        icon: 'none',
                        duration: 2000
                    });
                    reject(error);
                }
            });
        })
    }
}

type RequestData = string | AnyObject | ArrayBuffer;

enum HttpMethod {
    Options = "OPTIONS",
    Get = "GET",
    HEAD = "HEAD",
    POST = "POST",
    PUT = "PUT",
    DELETE = "DELETE",
    TRACE = "TRACE",
    CONNECT = "CONNECT"
}