using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM_CORE.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get;private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        public bool IsActive { get; private set; }

        public User(string name, string email, string passwordHash) 
        { 
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException( nameof(passwordHash));
            IsActive = true;
        }

        //setters

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Nowe imie nie może być puste");

            Name = name;
        }

        public void ChangeEmail(string email) 
        {
            if(string.IsNullOrWhiteSpace(email))
               throw new ArgumentNullException("Nowy email nie może być pusty");

            Email = email;
        }

        public void ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Nowy hash hasła nie może być pusty");

            PasswordHash = newPassword;
        }

        public void Deactivate()
        {
            IsActive = false;
        }



    }
}
