using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Teachers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Persistence.DapperConnection.Teacher;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    public class TeacherController: MyControllerBase
    {
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<List<TeacherModel>>> GetTeachers()
    {
        var result = await Mediator.Send(new TeacherQuery.TeacherList());
        return Ok(result.ToList());
    }

    [HttpPost]
    public async Task<ActionResult<Unit>> AddTeacher(NewTeacher.Execute data){
        var result = await Mediator.Send(data);
        return Ok(result);
    } 

    [HttpPut]
    public async Task<ActionResult<Unit>> UpdateTeacher(Guid teacherId, UpdateTeacher.Execute data){
        data.TeacherId = teacherId;
        return await Mediator.Send(data);

    }


    [HttpDelete]
    public async Task<ActionResult<Unit>> DeleteTeacher(Guid id){
        return await Mediator.Send(new DeleteTeacher.Execute{teacherId = id});
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherModel>> GetTeacherId(Guid id){
        return await Mediator.Send(new GetTeacherById.Execute{teacherId = id});
    }
    }
}