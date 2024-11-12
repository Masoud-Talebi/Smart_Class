using Smart_Class.Web.Application.Contracts;
using Smart_Class.Web.Infrastructure.Persistence;
using Smart_Class.Web.Infrastructure;
using Smart_Class.Web.Application.Services;
using Smart_Class.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Smart_Class.Web.Core.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Smart_Class.Web.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
#region Register Service
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var settings = builder.Configuration.Get<AppSettings>();
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddDbContextPool<IApplicationDbContext, ApplicationDbContext>((options) =>
{
    if (settings is null) throw new Exception("Settings is not fixed");

    options.UseSqlServer(settings.Connections.ApiDb);

}, poolSize: 16);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
#endregion

#region Idp Registration

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddIdentity<Teacher, IdentityRole<Guid>>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders().AddErrorDescriber<PersianIdentityErrorDescriber>();
builder.Services.Configure<SecurityStampValidatorOptions>(option =>
{
    option.ValidationInterval = TimeSpan.FromSeconds(5);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Error";
    options.Cookie.Name = "IncodityCookie";
    options.LoginPath = "/signin";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});
#endregion
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
