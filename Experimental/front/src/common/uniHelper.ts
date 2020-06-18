export default class uniHelper {
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
}