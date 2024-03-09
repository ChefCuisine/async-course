using AsyncCourse.AccountingApi.Configuration;
using AsyncCourse.AccountingApi.Extensions;
using AsyncCourse.Core.Db.Configuration;
using AsyncCourse.Core.Service.Domain.Startup;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add settings
builder.Services.AddAsyncCourseProperties();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Access/Login";
        option.ExpireTimeSpan = TimeSpan.FromDays(30);
    });

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter()));

// Add DB settings
builder.Services.AddAsyncCourseDbSettings<AccountingApiApplicationSettings>();
builder.Services.AddAsyncCourseDomain();
builder.Services.AddAsyncCourseDbContext();
builder.Services.AddKafkaBus();
builder.Services.AddRepositories();
builder.Services.AddCommands();
builder.Services.AddExternalClients();


// app section
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();