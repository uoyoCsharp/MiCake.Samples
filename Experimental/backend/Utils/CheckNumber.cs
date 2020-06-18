using System.Text.RegularExpressions;

namespace MiCakeDemoApplication.Utils
{
    public static class CheckNumber
    {
        /// <summary>
        /// 判断当前输入的字符串是否为手机号码
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string no)
            => Regex.IsMatch(no, @"^[1]+[3,5,7,8,9]+\d{9}");
    }
}
