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
        public async Task<int> Add(User user)
        {
            var sql = @"INSERT INTO [User](Email, Name, Password, PasswordSalt)
                                        VALUES(@Email, @Name, @Password, @PasswordSalt)
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await QueryAsync<int>(sql, user);
            return id.First();
        }

        public async Task<User> GetBy(string email)
        {
            var result = await QueryAsync<User>(@"SELECT * FROM [User] WHERE Email = @email", new { email });
            return result.FirstOrDefault();
        }
    }
}
