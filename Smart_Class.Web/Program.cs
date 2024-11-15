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
using Smart_Class.Web.Core.Domain.Ipd;
using Smart_Class.Web.Application.Initializer;

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

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IDbinitializer, DbInitializer>();
builder.Services.AddScoped<IPresenceService, PresenceService>();
#endregion

#region Idp Registration

builder.Services.AddIdentity<Teacher, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders().AddErrorDescriber<PersianIdentityErrorDescriber>();
builder.Services.Configure<SecurityStampValidatorOptions>(option =>
{
    option.ValidationInterval = TimeSpan.FromSeconds(5);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/AccessDenie";
    options.Cookie.Name = "Smart_Class";
    options.LoginPath = "/Login";
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
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbinitializer>();
    await dbInitializer.Initialize();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
