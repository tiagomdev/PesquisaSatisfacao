using PesquisaSatisfacao.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Core.Infrastructure.Database.Repository
{
    public class UserRepository : BaseDB, IUserRepository
    {
        public async Task Add(User user)
        {
            var sql = @"INSERT INTO [User](Email, Name, Password, PasswordSalt)
                                        VALUES(@Email, @Name, @Password, @PasswordSalt)";
            await ExecuteAsync(sql, user);
        }

        public async Task<User> GetBy(string email)
        {
            var result = await QueryAsync<User>(@"SELECT * FROM [User] WHERE Email = @email", new { email });
            return result.FirstOrDefault();
        }
    }
}
