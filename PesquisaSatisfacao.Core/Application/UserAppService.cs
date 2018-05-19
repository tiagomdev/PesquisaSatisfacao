using PesquisaSatisfacao.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Core.Application
{
    public class UserAppService
    {
        readonly IUserRepository repository;
        public UserAppService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> CreateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            await repository.Add(user);
            return user;
        }

        public async Task<User> GetBy(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
            return await repository.GetBy(email);
        }

        public async Task<User> Authenticate(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email nao enviado");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Senha nao enviada");

            var user = await repository.GetBy(email) ?? throw new ArgumentException("Usuario nao encotrado");

            var userValid = user.Authenticate(password);

            if(!userValid) throw new ArgumentException("Usuario ou senha incorretos");

            return user;
        }
    }
}
