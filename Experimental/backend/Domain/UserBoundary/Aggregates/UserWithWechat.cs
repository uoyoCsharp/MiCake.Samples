using MiCake.DDD.Domain;

namespace MiCakeDemoApplication.Domain.UserBoundary.Aggregates
{
    public class UserWithWechat : AggregateRoot<long>
    {
        public long UserID { get; private set; }
        public string WeChatOpenID { get; private set; }

        public UserWithWechat()
        {
        }

        public UserWithWechat(long userID, string wechatOpenID)
        {
        }
    }
}
