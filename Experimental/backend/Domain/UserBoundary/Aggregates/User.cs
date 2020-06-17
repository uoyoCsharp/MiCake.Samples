using MiCake.AspNetCore.Identity;
using MiCake.Audit;
using MiCake.DDD.Domain;
using MiCake.Identity.Authentication;
using System;

namespace MiCakeDemoApplication.Domain.UserBoundary.Aggregates
{
    public class User : MiCakeUser<Guid>, IAggregateRoot<Guid>, IHasCreationTime, IHasModificationTime
    {
        [JwtClaim]
        public string Name { get; private set; }

        public string Avatar { get; private set; }

        public int Age { get; private set; }

        public DateTime CreationTime { get; set; }

        public DateTime? ModificationTime { get; set; }

        public User()
        {
        }

        public User(string name, string avatar, int age)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException($"用户的姓名不能为空");
            
            Id = Guid.NewGuid();
            Name = name;
            Avatar = avatar;
            Age = age;
        }

        public void SetAvatar(string avatar) => Avatar = avatar;
    }
}
