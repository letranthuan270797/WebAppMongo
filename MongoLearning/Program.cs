using DataAccess;
using DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
// 1. ??c c?u h¨¬nh t? appsettings.json
builder.Services.Configure<Settings>(
    builder.Configuration.GetSection("MongoDB")
);

//// 2. ??ng k? CustomerContext v¨¤ Repository
//builder.Services.AddSingleton<CustomerContext>();
builder.Services.AddTransient<ICustomerContext, CustomerContext>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
