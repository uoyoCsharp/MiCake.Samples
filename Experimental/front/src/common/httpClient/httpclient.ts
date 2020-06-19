/* 
    Base On:https://github.com/John0King/uni-HttpClient.git
*/

export type InitHttpRequestDelegate = (request: IntercepterRequestContext) => Promise<IntercepterResponseContext>;

export interface HttpClientIntercepter {
    handle(request: IntercepterRequestContext, next: InitHttpRequestDelegate): Promise<IntercepterResponseContext>;
}

export interface HttpResultFilter {
    /**
     *结果过滤器的Order，当Http请求成功后，将根据order的从小到大的顺序来执行.
     *
     * @type {number}
     * @memberof HttpResultFilter
     */
    order: number;

    handle(context: ResponseData): void;
}

export interface IntercepterRequestContext {
    url: string;
    readonly method:
    | "OPTIONS"
    | "GET"
    | "HEAD"
    | "POST"
    | "PUT"
    | "DELETE"
    | "TRACE"
    | "CONNECT";
    header?: any;
    data?: any;
    responseType?: "text" | "arraybuffer";
}

export interface IntercepterResponseContext {
    httpClient: HttpClient;
    statusCode: number;
    data: any;
    error?: any;
    header: any;
}

export interface ResponseData<T = any> {
    statusCode: number;
    data: T;
    error?: any;
    header: any;
}

export class HttpClient {

    /**
     * Http发起请求时的拦截器管道
     *
     * @static
     * @type {HttpClientIntercepter[]}
     * @memberof HttpClient
     */
    static readonly intercepters: HttpClientIntercepter[] = [];

    /**
     *Http请求成功过后的结果过滤器
     *
     * @static
     * @type {HttpResultFilter[]}
     * @memberof HttpClient
     */
    static readonly resultFilters: HttpResultFilter[] = [];

    /**
     *  当Http请求失败（断网等）的时候所做的操作请求
     *
     * @static
     * @memberof HttpClient
     */
    static httpErrorFilter: (errorRes: any) => void;

    get<T = any>(url: string, query?: object | string, header?: any, options?: { responseType?: "text" | "arraybuffer"; }): Promise<T> {
        return this.request(url, "GET", query, header, options)
            .then(x => x.data as T)
            .catch(e => {
                throw e;
            });
    }

    post<T = any>(url: string, data?: object | string, header?: any, options?: { responseType?: "text" | "arraybuffer"; }): Promise<T> {
        return this.request(url, "POST", data, header, options)
            .then(x => x.data)
            .catch(e => {
                throw e;
            });
    }

    put<T = any>(url: string, data: object | string, header?: any, options?: { responseType?: "text" | "arraybuffer"; }): Promise<T> {
        return this.request(url, "PUT", data, header, options)
            .then(x => x.data)
            .catch(e => {
                throw e;
            });
    }

    form<T = any>(url: string, data: object | string, header?: any, options?: { responseType?: "text" | "arraybuffer"; }): Promise<T> {
        return this.request(url, "POST", data, { ...header, "Content-Type": "application/x-www-form-urlencoded" }, options)
            .then(x => x.data)
            .catch(e => {
                throw e;
            });
    }

    delete<T = any>(url: string, data?: object | string, header?: any, options?: { responseType?: "text" | "arraybuffer"; }): Promise<T> {
        return this.request(url, "DELETE", data, header, options)
            .then(x => x.data)
            .catch(e => {
                throw e;
            });
    }

    uploadFile<T = any>(
        url: string,
        data: {
            files?: { name: string; uri: string }[];
            fileType?: "image" | "audio" | "video";
            filePath?: string;
            name?: string;
            formData?: any;
        },
        header?: any
    ): Promise<ResponseData<T>> {
        return this.do_intercepte(
            {
                url,
                method: "POST",
                header
            },
            new FinalIntercepter(rcontext => {
                return this.internal_uploadFile(rcontext, {
                    files: data.files,
                    fileType: data.fileType,
                    filePath: data.filePath,
                    name: data.name,
                    formData: data.formData
                });
            }, this)
        )
            .then(x => {
                return {
                    statusCode: x.statusCode,
                    data: x.data,
                    header: x.header
                };
            })
            .catch(e => {
                throw e;
            });
    }

    /**
     * 全能的请求
     * @param url 地址
     * @param method 方法
     * @param data 数据
     * @param header 请求头
     * @param options 其他参数
     */
    request(
        url: string,
        method:
            | "OPTIONS"
            | "GET"
            | "HEAD"
            | "POST"
            | "PUT"
            | "DELETE"
            | "TRACE"
            | "CONNECT",
        data?: any,
        header?: any,
        options?: {
            responseType?: "text" | "arraybuffer";
        }
    ): Promise<ResponseData> {
        return this.do_intercepte(
            {
                url,
                method,
                data,
                header,
                responseType: options?.responseType ?? undefined
            },
            new FinalIntercepter(rcontext => {
                return this.internal_request(rcontext);
            }, this)
        )
            .then(x => {
                let reData = { statusCode: x.statusCode, data: x.data, header: x.header } as ResponseData;

                let filters = HttpClient.resultFilters.sort(s => s.order);
                filters.forEach(filter => {
                    filter.handle(reData);
                });

                return reData;
            })
            .catch(e => {
                throw e;
            });
    }

    /**
     * 最基础的 @see uni.request 封装
     * @param req 请求上下文
     */
    private internal_request(req: IntercepterRequestContext) {
        const p = new Promise<any>((resolve, reject) => {
            uni.request({
                url: req.url,
                method: req.method,
                data: req.data,
                header: req.header,
                responseType: req.responseType ?? "application/json",
                success: x => {
                    //     console.log(x);
                    //   if (
                    //     (x.header["content-type"] as string).toLowerCase().indexOf("json") >
                    //     -1
                    //   ) {
                    //     x.data = JSON.parse(x.data!) as any;
                    //   }
                    resolve({ ...x, httpClient: this });
                },
                fail: e => {
                    reject(e);
                    HttpClient.httpErrorFilter(e);
                }
            });
        });
        return p;
    }

    private internal_uploadFile(
        req: IntercepterRequestContext,
        upload: {
            files?: any[];
            fileType?: "image" | "audio" | "video";
            filePath?: string;
            name?: string;
            formData?: FormData;
        }
    ): Promise<IntercepterResponseContext> {
        const p = new Promise<any>((resovle, reject) => {
            uni.uploadFile({
                url: req.url,
                files: upload.files,
                fileType: upload.fileType,
                filePath: upload.filePath,
                name: upload.name,
                header: req.header,
                formData: upload.formData,

                success: x => {
                    let data = x.data;
                    let x2 = x as any;
                    let ct = x2.header?.["Content-Type"] as string;
                    try {
                        data = JSON.parse(data!);
                    } catch (e) {
                        data = data;
                        // 说明不是json, 操蛋的api没法监测header
                    }
                    x2.data = data;
                    resovle({
                        ...x2,
                        httpClient: this
                    });
                },
                fail: e => {
                    reject(e);
                    HttpClient.httpErrorFilter(e);
                }
            });
        });
        return p;
    }

    /**
     * 在 @see  internal_request 的基础上包裹拦截器
     * @param context  拦截请求上下文
     */
    private do_intercepte(context: IntercepterRequestContext, finalIntercepter: FinalIntercepter): Promise<ResponseData> {
        let pipe = [...HttpClient.intercepters, finalIntercepter];
        let i = -1;

        return chain(context);

        function chain(context: IntercepterRequestContext): Promise<IntercepterResponseContext> {
            i++;
            let p = pipe[i].handle(context, chain);
            return p;
        }
    }
}

class FinalIntercepter implements HttpClientIntercepter {
    constructor(
        protected requestHandler: (context: {
            url: string;
            method:
            | "OPTIONS"
            | "GET"
            | "HEAD"
            | "POST"
            | "PUT"
            | "DELETE"
            | "TRACE"
            | "CONNECT";
            data?: any;
            header?: any;
            options?: {
                responseType?: "text" | "arraybuffer";
            };
        }) => Promise<UniApp.RequestSuccessCallbackResult>,
        protected httpClient: HttpClient
    ) { }

    public handle(request: IntercepterRequestContext, next: InitHttpRequestDelegate): Promise<IntercepterResponseContext> {
        return this.requestHandler(request)
            .then(r => {
                return {
                    httpClient: this.httpClient,
                    statusCode: r?.statusCode ?? 0,
                    header: r.header,
                    data: r.data,
                    error: null
                };
            });
    }
}

export const httpClient = new HttpClient();
