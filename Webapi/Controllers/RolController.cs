using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    public class RolController: MyControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<Unit>> Create(NewRole.Execute parameters){
            return await Mediator.Send(parameters);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Unit>> DeleteRole(DeleteRole.Execute parameters){
            return await Mediator.Send(parameters);
        }

        [HttpGet("List")]
        public async Task<ActionResult<List<IdentityRole>>> ListRoles(){
            return await Mediator.Send(new ListRole.Execute());
        }

        [HttpPost("AddRoleUser")]
        public async Task<ActionResult<Unit>> AddRoleUser(UserAssignRole.Execute parameters){
            return await Mediator.Send(parameters);
        }
        [HttpPost("UserDeleteRole")]
        public async Task<ActionResult<Unit>> UserDeleteRole(UserDeleteRole.Execute parameters){
            return await Mediator.Send(parameters);
        }

        }
    }
