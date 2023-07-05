using cryptotracker.dataaccess.Abstract;
using cryptotracker.dataaccess.Concrete.EF;
using cryptotracker.web.Identity;
using cryptotracker.web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllersWithViews().AddViewOptions(options =>
{
    options.HtmlHelperOptions.ClientValidationEnabled = true;
});
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("Data Source = DESKTOP-UQQU5HJ;Initial Catalog=cryptoTrackerDb;Integrated Security=SSPI;TrustServerCertificate=True"));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i=>
   new SmtpEmailSender(
      configuration["EmailSender:Host"],
      configuration.GetValue<int>("EmailSender:Port"),
      configuration.GetValue<bool>("EmailSender:EnableSSL"),
      configuration["EmailSender:Username"],
      configuration["EmailSender:Password"])
      );
builder.Services.Configure<IdentityOptions>(options =>
   {
       options.Password.RequireDigit = true;
       options.Password.RequireLowercase = true;
       options.Password.RequireUppercase = true;
       options.Password.RequiredLength = 6;
       options.Lockout.MaxFailedAccessAttempts = 3;
       options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
       options.Lockout.AllowedForNewUsers = true;
       options.User.RequireUniqueEmail = true;
       options.SignIn.RequireConfirmedEmail = true;
   }
);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LoginPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(2);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".CryptoTracker.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});
var app = builder.Build();
app.UseAuthentication();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
         name: "default",
         pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
