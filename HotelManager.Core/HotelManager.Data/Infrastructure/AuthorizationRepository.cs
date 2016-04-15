using HotelManager.Core.Domain;
using HotelManager.Core.Infranstructure;
using HotelManager.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Infrastructure
{
    public class AuthorizationRepository : IDisposable, IAuthorizationRepository
    {
        private readonly IUserStore<User> _userStore;
        private readonly IDatabaseFactory _databaseFactory;
        private readonly UserManager<User> _userManager;

        private HotelManagerDataContext db;
        protected HotelManagerDataContext Db
        {
            get
            {
                return db ?? (db = _databaseFactory.GetDataContext());
            }
        }

        public AuthorizationRepository(IDatabaseFactory databaseFactory, IUserStore<User> userStore)
        {
            _userStore = userStore;
            _databaseFactory = databaseFactory;
            _userManager = new UserManager<User>(userStore);
        }

        public async Task<IdentityResult> RegisterUser(RegistrationModel model)
        {
            var user = new User
            {
                UserName = model.Username,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<User> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
