using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.contracts;
using Microsoft.AspNetCore.Http;

namespace Security.securityToken
{
    public class UserSesion : IUserSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserSesion(IHttpContextAccessor accessor){
            _httpContextAccessor = accessor;
        }
        public string GetUserSesion()
        {
            var userName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userName;
        }
    }
}