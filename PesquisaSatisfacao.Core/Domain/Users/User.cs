using System;
using System.Collections.Generic;
using System.Text;
using VidaMais.Protese.Platform.Utility;

namespace PesquisaSatisfacao.Core.Domain.Users
{
    public class User
    {
        public User()
        {

        }

        public User(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Nome nao enviado");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("Email nao enviado");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("Senha nao enviada");
            PasswordSalt = Membership.GenerateSalt();
            Password = Membership.EncodePassword(password, PasswordSalt);
            Name = name;
            Email = email;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public virtual bool Authenticate(string password)
        {
            string hash = Membership.EncodePassword(password, PasswordSalt);

            if (Password == hash)
                return true;
            else
                return false;
        }
    }
}
