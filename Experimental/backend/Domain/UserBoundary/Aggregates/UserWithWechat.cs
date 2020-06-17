using MiCake.DDD.Domain;
using System;

namespace MiCakeDemoApplication.Domain.UserBoundary.Aggregates
{
    public class UserWithWechat : AggregateRoot<long>
    {
        public Guid UserID { get; private set; }
        public string WeChatOpenID { get; private set; }

        public UserWithWechat()
        {
        }

        public UserWithWechat(Guid userID, string wechatOpenID)
        {
            if (userID == default)
                throw new DomainException($"传入的用户ID无效");

            if (string.IsNullOrWhiteSpace(wechatOpenID))
                throw new DomainException($"传入的OpenID无效");

            UserID = userID;
            WeChatOpenID = wechatOpenID;
        }
    }
}
