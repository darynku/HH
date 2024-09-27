using FluentResults;
using HH.Domain.Common;

namespace HH.Domain.Entitties
{
    public class User : BaseEntity
    {
        private User() { }
        public string UserName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PasswordHash { get; init; } = string.Empty;

        public Role Role { get; init; }

        private readonly List<Vacancy> _vacancy = [];
        public IReadOnlyList<Vacancy> Vacancies => _vacancy;

        public string Avatar { get; init; } = string.Empty;

        private User(Guid id, Role role, string avatar, string userName, string email, string passwordHash) : base(id)
        {
            Role = role;
            Avatar = avatar;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
        }
        public static Result<User> CreateUser(Guid id, Role role, string avatar, string userName, string email, string passwordHash)
        {
            if (string.IsNullOrEmpty(userName))
                return Result.Fail("пустое имя ");

            if (string.IsNullOrEmpty(email))
                return Result.Fail("пустая ПОЧТА");

            if (string.IsNullOrEmpty(passwordHash))
                return Result.Fail("пустой пароль");


            return Result.Ok(new User(Guid.NewGuid(), Role.Rab, avatar, userName, email, passwordHash)); 
        }

        public static Result<User> CreateBoss(Guid id, Role role, string avatar, string userName, string email, string passwordHash)
        {
            if (string.IsNullOrEmpty(userName))
                return Result.Fail("пустое имя ");

            if (string.IsNullOrEmpty(email))
                return Result.Fail("пустая ПОЧТА");

            if (string.IsNullOrEmpty(passwordHash))
                return Result.Fail("пустой пароль");


            return Result.Ok(new User(Guid.NewGuid(), Role.Boss, avatar, userName, email, passwordHash));
        }

       
    }
}
