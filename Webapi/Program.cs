using Application.courses;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.middleware;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Configurar el DbContext y la cadena de conexión
builder.Services.AddDbContext<CoursesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddFluentValidation(cfg => {
    cfg.RegisterValidatorsFromAssemblyContaining<NewCourse>();
    cfg.RegisterValidatorsFromAssemblyContaining<UpdateCourse>();
}); // D E P R E C A T E D
//builder.Services.AddControllers();
// Si estás usando Swagger para documentación, puedes habilitarlo así:
builder.Services.AddMediatR(typeof(CourseQuery.Handler));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorManagerMiddleWare>();
// Configurar la tubería de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
