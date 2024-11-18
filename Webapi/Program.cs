using System.Text;
using Application.contracts;
using AutoMapper;
using Application.courses;
using Dominion;
using FluentValidation.AspNetCore;
using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.middleware;
using Security.securityToken;
using Persistence.DapperConnection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Persistence.DapperConnection.Teacher;
using Persistence.DapperConnection.pagination;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers( opt =>{
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        opt.Filters.Add(new AuthorizeFilter(policy));
    }
);

// Configurar el DbContext y la cadena de conexión
builder.Services.AddDbContext<CoursesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// FluentValidation
builder.Services.AddControllers().AddFluentValidation(cfg => {
    cfg.RegisterValidatorsFromAssemblyContaining<NewCourse>();
    cfg.RegisterValidatorsFromAssemblyContaining<UpdateCourse>();
});

//user identity service
var idBuilder = builder.Services.AddIdentityCore<User>();
var identityBuilder = new IdentityBuilder(idBuilder.UserType, idBuilder.Services);
identityBuilder.AddRoles<IdentityRole>();
identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();


identityBuilder.AddEntityFrameworkStores<CoursesContext>();
identityBuilder.AddSignInManager<SignInManager<User>>();
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);


// MediatR
builder.Services.AddMediatR(typeof(CourseQuery.Handler));
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
        Title = "Courses API", 
        Version = "v1" 
    });
    
    
    c.CustomSchemaIds(type => type.FullName);
});
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IUserSesion,UserSesion>();
builder.Services.AddAutoMapper(typeof(CourseQuery.Handler));
//para agregar dapper
builder.Services.AddOptions();
builder.Services.Configure<ConnectionCFG>(builder.Configuration.GetSection("ConnectionStrings"));

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>{
    opt.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience=false, // desde que ip se puede generar
        ValidateIssuer=false // a que ips se puede enviar
    };
});

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", 
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});



builder.Services.AddTransient<IFactioryConnection, FactoryConnection>();
builder.Services.AddScoped<ITeacher, TeacherRepo>();
builder.Services.AddScoped<IPagination,PageRepository>();

var app = builder.Build();

app.UseCors("corsApp");
app.Urls.Add("http://localhost:5000");
//app.Urls.Add("https://localhost:7001");

app.UseAuthentication();

// Migrar la base de datos automáticamente al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var context = services.GetRequiredService<CoursesContext>();
        //context.Database.Migrate();  // Aplica las migraciones
        TestData.InsertData(context, userManager).GetAwaiter().GetResult();

    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred during migration");
    }
}

// Middlewares personalizados
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
