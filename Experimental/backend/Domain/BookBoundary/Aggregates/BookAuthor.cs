using MiCake.Core;
using MiCake.DDD.Domain;
using System.Collections.Generic;

namespace MiCakeDemoApplication.Domain.BookBoundary.Aggregates
{
    /// <summary>
    /// 作者-值对象
    /// </summary>
    public class BookAuthor : ValueObject
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public BookAuthor(string firstName, string lastName)
        {
            //下一版本将会引入DomainException来替代SoftMiCakeException

            if (string.IsNullOrEmpty(firstName))
                throw new SoftlyMiCakeException("作者信息的姓不能为空");

            if (string.IsNullOrEmpty(lastName))
                throw new SoftlyMiCakeException("作者信息的名不能为空");

            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
