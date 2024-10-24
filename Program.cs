using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{ 
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme,
    securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter The Bearer Authorization : 'Bearer Generated-JWT-Token'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {{
        new OpenApiSecurityScheme
        {
             Reference=new OpenApiReference
             {
                 Type=ReferenceType.SecurityScheme,
                 Id=JwtBearerDefaults.AuthenticationScheme
             }
        },new string[]{}
        }
    });

   } ); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
