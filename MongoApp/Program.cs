using DataAccess.Repositories;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

// 1. Đọc cấu hình từ appsettings.json
builder.Services.Configure<Settings>(builder.Configuration.GetSection("MongoDB"));

//// 2. Đăng ký CustomerContext và Repository
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
