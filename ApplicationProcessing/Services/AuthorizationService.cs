using ApplicationProcessing.Properties;
using Database.Core;
using Database.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApplicationProcessing.Services
{
    internal class AuthorizationService
    {
        private static Core.Models.User m_user;

        public static Core.Models.User User
        {
            get => m_user;
        }

        public async Task<bool> Authorize(string login, string password, bool remember)
        {
            using (var context = new Database.Core.Context())
            {
                var user = await context.Users.Include(item => item.Person).FirstOrDefaultAsync(item => item.Login.Equals(login) && item.Password.Equals(password));

                if (user == null)
                    return false;

                if (user.Person.RoleId != (int)Role.Roles.Operator)
                    return false;

                m_user = new Core.Models.User
                {
                    Id = user.Id,
                    FirstName = user.Person.FirstName,
                    MiddleName = user.Person.MiddleName,
                    LastName = user.Person.LastName,
                    Username = user.Username
                };

                Settings.Default.UserJSON = JsonSerializer.Serialize(m_user);
                Settings.Default.RememberUser = remember;
                Settings.Default.SignedIn = DateTime.UtcNow;
                Settings.Default.Save();
            }

            return true;
        }

        public bool SignOut()
        {
            try
            {
                Settings.Default.UserJSON = "";
                Settings.Default.RememberUser = false;
                Settings.Default.SignedIn = DateTime.MinValue;
                Settings.Default.Save();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool RestoreUser()
        {
            if (!Settings.Default.RememberUser)
                return false;

            if (Settings.Default.SignedIn.AddDays(-3) > Settings.Default.SignedIn)
                return false;

            m_user = JsonSerializer.Deserialize<Core.Models.User>(Settings.Default.UserJSON);

            using (var context = new Context())
                if (context.Users.FirstOrDefault(item => item.Id == m_user.Id) is null)
                    m_user = null;

            return m_user != null;
        }
    }
}