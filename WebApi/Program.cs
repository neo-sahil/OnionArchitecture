using Application;
using Application.MiddleWare;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Context;
using Persistence.Data;
using Services;
using Services.JWT;
using Services.MiddelWares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "OnionArchitecture",
    });
    c.CustomSchemaIds(type => type.FullName);
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Onion Architecture",
        Description = "Security authorization bearer token...",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

    //this is to add authorization tab to every api 
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });
    //c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
var jwtSetting = new JWTSettings();
builder.Configuration.GetSection("JWTSettings").Bind(jwtSetting);
builder.Services.AddServices(builder.Configuration, jwtSetting);




var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x.AllowAnyMethod().AllowAnyOrigin().WithOrigins("https://localhost:4200").AllowAnyHeader());

//authencation middleWare
app.UseAuthentication();

app.UseMiddleware<JWTMIddleWare>();

app.UseAuthorization();

app.MapControllers();

app.Run();
