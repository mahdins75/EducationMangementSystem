using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mstech.Entity.Etity;
using DataBase.Repository;
using Mstech.ADV.Config;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DNTCaptcha.Core;
using Common.Models;
using Mstech.Accounting.Data;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var seedData = new SeedData();
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
var configuration = builder.Configuration;
var appConfig = builder.Configuration.Get<AppConfig>();

builder.Services.AddDNTCaptcha(options =>
{
    // options.UseSessionStorageProvider(); // -> It doesn't rely on the server or client's times. Also it's the safest one.
    // options.UseMemoryCacheStorageProvider(); // -> It relies on the server's times. It's safer than the CookieStorageProvider.
    options.UseCookieStorageProvider(
            /* If you are using CORS, set it to `None` */) // -> It relies on the server and client's times. It's ideal for scalability, because it doesn't save anything in the server's memory.
                                                           // .UseDistributedCacheStorageProvider(); // --> It's ideal for scalability using `services.AddStackExchangeRedisCache()` for instance.
                                                           // .UseDistributedSerializationProvider();

        // Don't set this line (remove it) to use the installed system's fonts (FontName = "Tahoma").
        // Or if you want to use a custom font, make sure that font is present in the wwwroot/fonts folder and also use a good and complete font!
        //.UseCustomFont(Path.Combine(env.WebRootPath, path2: "fonts", path3: "tahoma.ttf"))
        .AbsoluteExpiration(minutes: 7)
        .RateLimiterPermitLimit(
            permitLimit:
            10) // for .NET 7x, Also you need to call app.UseRateLimiter() after calling app.UseRouting().
        .ShowExceptionsInResponse(env.IsDevelopment())
        .ShowThousandsSeparators(show: false)
        .WithNoise(baseFrequencyX: 0.015f, baseFrequencyY: 0.015f, numOctaves: 1, seed: 0.0f)
        .WithEncryptionKey(key: "This is my secure key!")
        .WithNonceKey(key: "NETESCAPADES_NONCE")
        .WithCaptchaImageControllerNameTemplate(template: "my-custom-captcha")
        .WithCaptchaImageControllerRouteTemplate(template: "my-custom-captcha/[action]")
        .InputNames(new DNTCaptchaComponent
        {
            CaptchaHiddenInputName = "DNTCaptchaText",
            CaptchaHiddenTokenName = "DNTCaptchaToken",
            CaptchaInputName = "DNTCaptchaInputText"
        })
        .Identifier(className: "dntCaptcha");
});


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
}).AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Configure JWT Authentication

string jwtSecret = "bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b"; // At least 16 characters
var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

builder.Services.AddAuthentication((options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}))
.AddJwtBearer(options =>
{
    options.Authority = "https://localhost:7081";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7081", // Replace with your issuer
        ValidAudience = "api", // Replace with your audience
        IssuerSigningKey = jwtKey,


    };

});

builder.Services.AddApplicationServices();
builder.Services.Configure<SmtpOption>(builder.Configuration.GetSection("SmtpOption"));
builder.Services.AddDNTCaptcha(options =>
{
    options
        .UseCookieStorageProvider( /* If you are using CORS, set it to `None` */) // -> It relies on the server and client's times. It's ideal for scalability, because it doesn't save anything in the server's memory.
                                                                                  // .UseDistributedCacheStorageProvider() // --> It's ideal for scalability using `services.AddStackExchangeRedisCache()` for instance.
                                                                                  // .UseDistributedSerializationProvider()

        // Don't set this line (remove it) to use the installed system's fonts (FontName = "Tahoma").
        // Or if you want to use a custom font, make sure that font is present in the wwwroot/fonts folder and also use a good and complete font!
        .UseCustomFont(Path.Combine(env.WebRootPath, "fonts", "IRANSans(FaNum)_Bold.ttf"))
        .ShowExceptionsInResponse(env.IsDevelopment())
        .AbsoluteExpiration(7)
        .RateLimiterPermitLimit(10) // for .NET 7x, Also you need to call app.UseRateLimiter() after calling app.UseRouting().
        .ShowThousandsSeparators(false)
        .WithNoise(0.015f, 0.015f, 1, 0.0f)
        .WithEncryptionKey("This is my secure key!")
        .WithNonceKey("NETESCAPADES_NONCE")
        .InputNames( // This is optional. Change it if you don't like the default names.
                    new DNTCaptchaComponent
                    {
                        CaptchaHiddenInputName = "DNT_CaptchaText",
                        CaptchaHiddenTokenName = "DNT_CaptchaToken",
                        CaptchaInputName = "DNT_CaptchaInputText",
                    })
        .Identifier("dnt_Captcha") // This is optional. Change it if you don't like its default name.
        ;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                      cp => cp
                      .WithOrigins("https://my.trabase.ir", "http://localhost:5262", "https://localhost:7124", "https://test102.mitalearn.com")
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod()

                            );
});

builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddControllers(); // this is necessary for the captcha's image provider
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<AppConfig>(configuration.GetSection("AppConfig"));


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    seedData.SeedUsersAsync(userManager).Wait();
}
app.UseDeveloperExceptionPage();
var fileService = app.Services.GetService<IServiceScopeFactory>();
var map = new MapsterConfig(fileService);
map.Register();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); // 🔄 Add this
app.UseCors("CorsPolicy"); // CORS after routing
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();


app.Run();
