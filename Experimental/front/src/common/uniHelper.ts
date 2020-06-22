import FormValidation from "./formValidation";

export default class uniHelper {
    /**
     *常用的表单验证方案
     *
     * @static
     * @type {FormValidation}
     * @memberof uniHelper
     */
    public static validator: FormValidation = new FormValidation();

    /**
     *弹出Toast提示
     *
     * @param {string} title  title信息
     * @param {number} [duration=2000] 持续时间，默认为2秒
     * @param {boolean} [success=true] 是否显示成功的图标，如果iconType指定了类型，那么将使用自定义图标
     * @param {string} [iconType='none'] 自定义显示图标类型
     */
    public static showToast(title: string, duration: number = 2000, success: boolean = true, iconType: string | null = null) {
        uni.showToast({
            title: title || "出错啦~",
            icon: iconType ?? success ? 'success' : 'none',
            duration: duration,
        });
    }

    /**
     *弹出loading框
     *
     * @static
     * @param {string} [title='加载中']
     * @param {boolean} [showMask=true]
     * @memberof uniHelper
     */
    public static showLoading(title: string = '加载中', showMask: boolean = true) {
        uni.showLoading({
            title: title,
            mask: showMask
        });
    }

    /**
     * 关闭loading框,请使用异步等待
     *
     * @static
     * @param {boolean} delay 延迟取消时间，单位毫秒
     * @memberof uniHelper
     */
    public static hideLoading(delay: number = 0): Promise<any> {
        if (delay > 0) {
            return new Promise((resolve) => setTimeout(() => {
                uni.hideLoading();
                resolve();
            }, delay));
        } else {
            return new Promise((resolve) => {
                uni.hideLoading();
                resolve();
            });
        }
    }
}

export class thorUiHelper {
    /**
     *弹出tips提醒
     *
     * @static
     * @param {*} tipsInstance 页面上的thor-ui tips实例
     * @param {string} title
     * @param {number} [duration=2000]
     * @param {(string | null)} [iconType='danger']
     * @memberof uniHelper
     */
    public static showTips(tipsInstance: any, title: string, duration: number = 2000, iconType: string | null = 'danger') {
        if (tipsInstance) {
            tipsInstance.showTips({
                msg: title,
                duration: duration,
                type: iconType
            });
        }
    }
}