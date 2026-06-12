var builder = WebApplication.CreateBuilder(args);

// MVC (контроллеры + представления)
builder.Services.AddControllersWithViews();

// Session — здесь храним состояние игры (аналог Session у преподавателя)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();        // включаем Session до маршрутизации запросов
app.UseAuthorization();

// Маршрут как в классическом MVC: по умолчанию Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
