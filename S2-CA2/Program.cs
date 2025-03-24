using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using S2_CA2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Configure MySQL with Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Could not get 'DefaultConnection' connection string");
builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseMySQL(connectionString); });

// Configure Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => { options.SignIn.RequireConfirmedAccount = false; })
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Cookie Policy (IMPORTANT for Google OAuth state)
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax; // Prevents OAuth errors
});

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]
                           ?? throw new InvalidOperationException("Could not find Google Client ID");
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]
                               ?? throw new InvalidOperationException("Could not find Google Client Secret");
    })
    .AddFacebook(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Facebook:AppId"]
                           ?? throw new InvalidOperationException("Could not find Facebook App ID");
        options.ClientSecret = builder.Configuration["Authentication:Facebook:AppSecret"]
                               ?? throw new InvalidOperationException("Could not find Facebook App Secret");
    })
    .AddTwitter(options =>
    {
        options.ConsumerKey = builder.Configuration["Authentication:Twitter:ConsumerAPIKey"]
                              ?? throw new InvalidOperationException("Could not find Twitter Consumer Key");
        options.ConsumerSecret = builder.Configuration["Authentication:Twitter:ConsumerSecret"]
                              ?? throw new InvalidOperationException("Could not find Twitter Consumer Secret");
    });

var mvcBuilder = builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy(); // Add BEFORE auth middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();