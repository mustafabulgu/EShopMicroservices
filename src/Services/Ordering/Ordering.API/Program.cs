using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;
var builder = WebApplication.CreateBuilder(args);

//add services to container

//Infrastructure - EF Core
//Application - Mediatr
//API - Carter, HealthChecks
builder.Services.AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
//configure pipeline
app.UseApiServices();
app.Run();
