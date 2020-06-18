using MiCake.AspNetCore.Identity;
using MiCake.Audit;
using MiCake.DDD.Domain;
using MiCake.Identity.Authentication;
using MiCakeDemoApplication.Utils;
using System;

namespace MiCakeDemoApplication.Domain.UserBoundary.Aggregates
{
    public class User : MiCakeUser<Guid>, IAggregateRoot<Guid>, IHasCreationTime, IHasModificationTime
    {
        [JwtClaim]
        public string Name { get; private set; }

        public string Avatar { get; private set; }

        public int Age { get; private set; }

        public string Phone { get; private set; }

        //可能你需要加密存储密码等信息
        public string Password { get; private set; }

        public DateTime CreationTime { get; set; }

        public DateTime? ModificationTime { get; set; }

        public User()
        {
        }

        internal User(string name, string phone, string pwd, int age)
        {
            if (!CheckNumber.IsPhoneNumber(phone))
                throw new DomainException($"手机号码格式不符合规范");

            Id = Guid.NewGuid();
            Password = pwd;
            Phone = phone;
            Name = name;
            Age = age;
        }

        public void SetAvatar(string avatar) => Avatar = avatar;

        public void ChangeUserInfo(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void ChangePhone(string phone)
        {
            if (!CheckNumber.IsPhoneNumber(phone))
                throw new DomainException($"手机号码格式不符合规范");

            Phone = phone;
        }

        public static User Create(string phone,
                                  string pwd,
                                  string name = null,
                                  int age = 0)
        {
            return new User(name, phone, pwd, age);
        }
    }
}
