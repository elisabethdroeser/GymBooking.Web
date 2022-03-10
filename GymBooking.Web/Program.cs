using GymBooking.Web.Clients;
using GymBooking.Web.Data;
using GymBooking.Web.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false; //kr�ver att vi har validerat v�r e-mail -true-. S�tter upp en massa defaults, registrera services �t oss (x) cookie authentication. Default Identity UI. User/Provide authentication mm.
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false; //�ver 8 tecken, stora o sm� bokst�ver
        options.Password.RequireUppercase = false; //AAA, 000. Vid testningar. 
    }) 
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews(); //l�gga till filters

//1
builder.Services.AddHttpClient(); //injecta en http client. Även skriva nameclient mm. 

//2
builder.Services.AddHttpClient("GymClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5000");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient("GymClient2", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
    //client.DefaultRequestHeaders.Clear();
    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

//builder.Services.AddHttpClient<BookingClient>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:5001");
//});

builder.Services.AddHttpClient<BookingClient>( );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=GymClasses}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
