using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.comentaries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    public class ComentaryController: MyControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateComentary(NewComentary.Execute data){
            return await Mediator.Send(data);
            
        }
    }
}