using OjoREGED.BLL;
using OjoREGED.BLL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//menambahkan modul mvc
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//register DI
builder.Services.AddScoped<IemployeeBLL, EmployeeBLL>();
builder.Services.AddScoped<ICustomerBLL, CustomerBLL>();
builder.Services.AddScoped<ISubscriptionBLL, SubscriptionLevelBLL>();


var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error/Error404");
//    app.UseStatusCodePagesWithReExecute("/Error/Error404");
//}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
