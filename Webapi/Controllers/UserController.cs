using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.security;
using Dominion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers
{
    [AllowAnonymous]
    public class UserController: MyControllerBase
    {
        //htttp://localhost:5000/Login/User/Login
        [HttpPost("Login")] 
        public async Task<ActionResult<UserData>> Login(Login.Execute data){
            return await Mediator.Send(data);
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<UserData>> Signup(UserRegister.Execute data){
            return await Mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<UserData>> GetUser(){
            return await Mediator.Send(new CurrentUser.Execute());
        }
    }
}