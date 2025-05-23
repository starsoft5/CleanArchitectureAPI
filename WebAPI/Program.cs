using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using GraphQLApi.GraphQL;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Access configuration
var config = builder.Configuration;
var jwtSecret = config["JwtSettings:Secret"];
var appUrl = config["JwtSettings:AppUrl"];

var policyName = "MyCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
        policy =>
        {
            policy.WithOrigins(appUrl!)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // this is REQUIRED
        });
});

builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!))
        };
        options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("jwtToken"))
                {
                    context.Token = context.Request.Cookies["jwtToken"];
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddDbContext<SQLiteDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Add GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

builder.Services.AddScoped<Query>();
builder.Services.AddScoped<Mutation>();

builder.Services.AddAuthorization();
var app = builder.Build();
app.MapControllers();
app.MapGraphQL(); // Maps the GraphQL endpoint (default: /graphql)
app.UseCors(policyName);
app.UseRouting();
app.UseAuthorization();
 
app.Run();
