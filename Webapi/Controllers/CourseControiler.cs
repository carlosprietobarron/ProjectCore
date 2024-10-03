using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dominion;
using Application.courses;

namespace Webapi.Controllers
{
    
    public class CourseControiler: ControllerBase
    {
        public readonly IMediator _mediator;
        public CourseControiler(IMediator mediator){
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Get(){
                var courses = await _mediator.Send(new CourseQuery.CoursesList());

                return courses;
        }

        [HttpGet("{id}")]
        public async Task<Course> Detail(int id){
            return await _mediator.Send(new CourseIdQuery.CourseById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewCourse.Execute data){
            return await _mediator.Send(data);
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> Update(int id, UpdateCourse.Execute data){
            data.CourseId = id;
            return await _mediator.Send(data);
        } 

        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(int id){
            return await _mediator.Send(new DeleteCourse.Execute { Id = id });
            
        }
    }
}