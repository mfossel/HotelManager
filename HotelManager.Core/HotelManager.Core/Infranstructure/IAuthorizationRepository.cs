using HotelManager.Core.Domain;
using HotelManager.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Infranstructure
{
    public interface IAuthorizationRepository
    {
        Task<User> FindUser(string username, string password);
        Task<IdentityResult> RegisterUser(RegistrationModel model);
    }
}
