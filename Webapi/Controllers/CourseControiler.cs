using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dominion;
using Application.courses;
using Microsoft.AspNetCore.Authorization;
using Persistence.DapperConnection.pagination;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    public class CourseController: MyControllerBase
    {

        [HttpGet]
        // [Authorize] vamos a crear una autorizacion global
        public async Task<ActionResult<List<CourseDTO>>> Get(){
                var courses = await Mediator.Send(new CourseQuery.CoursesList());

                return courses;
        }

        [HttpGet("{id}")]
        public async Task<CourseDTO> Detail(Guid id){
            return await Mediator.Send(new CourseIdQuery.CourseById(id));
        }

        [HttpPost("create")]
        public async Task<ActionResult<Unit>> Create(NewCourse.Execute data){
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update([FromRoute]Guid id, [FromBody]UpdateCourse.Execute data){
            data.CourseId = id;
        return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id){
            return await Mediator.Send(new DeleteCourse.Execute { Id = id });
            
        }

        [HttpPost("Report")]
        public async Task<ActionResult<PageModel>> Report(CoursePage.Execute data){
            return await Mediator.Send(data);
        }

    }
}