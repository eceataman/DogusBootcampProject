using DogusBootcampProject.Data;
using DogusBootcampProject.Models;
using DogusBootcampProject.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<BlogDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

// ? SignalR servisini ekle
builder.Services.AddSignalR();

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ? Admin kullanýcý ve rol oluþturma
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var context = services.GetRequiredService<BlogDbContext>();

    // ?? Migration'ý otomatik uygula
    context.Database.Migrate();

    // ?? Admin rolü oluþtur
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole<int>("Admin"));
    }

    // ?? Admin kullanýcý oluþtur
    var adminUser = await userManager.FindByNameAsync("admin");
    if (adminUser == null)
    {
        var admin = new User
        {
            UserName = "admin",
            Email = "admin@bootcamp.com"
        };

        var result = await userManager.CreateAsync(admin, "Admin123!");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }

    // ? Seed data çaðrýsý (Kategorileri vs. otomatik ekle)
    SeedData.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ? SignalR Hub Route

app.Run();
