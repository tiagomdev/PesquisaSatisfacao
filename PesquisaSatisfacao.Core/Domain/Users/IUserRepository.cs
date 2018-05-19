using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Core.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetBy(string email);
        Task<int> Add(User user);
    }
}
