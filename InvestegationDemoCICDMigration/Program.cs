using Microsoft.EntityFrameworkCore;
using DevOpsWebApp.Data;
using DevOpsWebApp.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using DevOpsWebApp.Models;
using DevOpsWebApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.WithCorrelationIdHeader());
    
builder.Services
    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
    .AddScoped<RequestInfo, BackEndRequestInfo>()
    .AddScoped<ICustomLogger, Logger>()
    .AddScoped<ErrorHandlingMiddleware>()
    .AddScoped<RequestLoggingMiddleware>()
    .AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseExceptionHandler(
          new ExceptionHandlerOptions()
          {
              AllowStatusCode404Response = true, // important!
              ExceptionHandlingPath = "/error"
          }
      );
app.Run();
