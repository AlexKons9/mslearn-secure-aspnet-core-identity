using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RazorPagesPizza.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using RazorPagesPizza.Services;
using QRCoder;

var builder = WebApplication.CreateBuilder(args);
// The EF Core database context class, named RazorPagesPizzaAuth, is configured with the connection string.
var connectionString = builder.Configuration.GetConnectionString("RazorPagesPizzaAuthConnection");
builder.Services.AddDbContext<RazorPagesPizzaAuth>(options => options.UseSqlServer(connectionString));
// AddDefaultIdentity<IdentityUser> tells the Identity services to use the default user model.
// The lambda expression options => options.SignIn.RequireConfirmedAccount = true 
// specifies that users must confirm their email accounts.
// .AddEntityFrameworkStores<RazorPagesPizzaAuth>() specifies that Identity uses the default 
// Entity Framework Core store for its database. The RazorPagesPizzaAuth DbContext class is used.
builder.Services.AddDefaultIdentity<RazorPagesPizzaUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<RazorPagesPizzaAuth>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSingleton(new QRCodeService(new QRCodeGenerator()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapRazorPages();

app.Run();
