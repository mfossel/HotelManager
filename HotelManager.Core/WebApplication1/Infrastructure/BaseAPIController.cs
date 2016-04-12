using HotelManager.Core.Domain;
using HotelManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Infrastructure
{
    public class BaseApiController : ApiController
    {
        protected readonly IUserRepository _UserRepository;

        public BaseApiController(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        protected User CurrentUser
        {
            get
            {
                return _UserRepository.GetFirstOrDefault(u => u.UserName == User.Identity.Name);
            }
        }
    }
}