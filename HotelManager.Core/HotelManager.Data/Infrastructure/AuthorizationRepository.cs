﻿using HotelManager.Core.Domain;
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
    public class AuthorizationRepository : IAuthorizationRepository, IDisposable
    {
        private readonly IUserStore<User, string> _userStore;
        private readonly IDatabaseFactory _databaseFactory;
        private readonly UserManager<User, string> _userManager;

        private HotelManagerDataContext db;
        protected HotelManagerDataContext Db => db ?? (db = _databaseFactory.GetDataContext());

        public AuthorizationRepository(IDatabaseFactory databaseFactory, IUserStore<User, string> userStore)
        {
            _userStore = userStore;
            _databaseFactory = databaseFactory;
            _userManager = new UserManager<User, string>(userStore);
        }

        public async Task<IdentityResult> RegisterUser(RegistrationModel model)
        {
            var user = new User
            {
                UserName = model.Username

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
