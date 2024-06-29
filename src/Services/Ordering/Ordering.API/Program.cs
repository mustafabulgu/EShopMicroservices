using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//add services to container

//Infrastructure - EF Core
//Application - Mediatr
//API - Carter, HealthChecks
builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//app.
//configure pipeline
app.Run();
