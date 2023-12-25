using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Core.Api.Configurations;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using SchoolManagementSystem.Application;
using SchoolManagementSystem.Application.Middlewares;
using SchoolManagementSystem.Application.Extensions;
using Workers;
using Hangfire;
using Hangfire.PostgreSql;
using ConnectLive.Core.Api.Filters;
using HangfireBasicAuthenticationFilter;
using SchoolManagementSystem.Proxy;
using SchoolManagementSystem.Core.Api.Extensions;
using SchoolManagementSystem.Core.Controllers;

//var builder = WebApplication.CreateBuilder(args);
var builder = HostExtensions.CreateWebHostBuilder<StudentsController>(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("SchoolManagementSystemContext"));
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailWorker, EmailWorker>();
builder.Services.AddScoped<IProxy, Proxy>();

//builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));



builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly, typeof(ApplicationDbContext).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MappingProfile).Assembly));

builder.Services.AddMemoryCache();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(WatchBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheBehavior<,>));

builder.Services.AddHangfire(x => x.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(builder.Configuration.GetConnectionString("SchoolManagementSystemContext"))));
//builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCustomException();


app.MapControllers();
app.UseHangfireDashboard("/workers", new DashboardOptions
{
    //Authorization = new[] { new AuthorizationFilter() },
    Authorization = new[] {
        new HangfireCustomBasicAuthenticationFilter {
              User = "admin",
              Pass = "dougeulO!"
        }
     },
    DashboardTitle = "Hangfire Job for SchoolManagementSystem",
    DarkModeEnabled = false,
    DisplayStorageConnectionString = false,

});

    app.Run();
