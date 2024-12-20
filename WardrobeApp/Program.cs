using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WardrobeApp.Data;
using WardrobeApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = "742837832107-mfda79lbibpioj7h15uasujdc797nnc3.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("AdminClaim", policy => policy.RequireClaim("SuperUser", "1"));
});

builder.Services.AddRazorPages(options =>
    options.Conventions.AuthorizeFolder("/Areas/Administration/Pages", "Admin")
);

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();
