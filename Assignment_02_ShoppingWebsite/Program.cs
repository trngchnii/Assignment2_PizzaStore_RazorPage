using Assignment_02_ShoppingWebsite.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PizzaStoreContext>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); //xoá sau 30p
    options.Cookie.HttpOnly = true; //bảo vệ khỏi các cuộc tấn công XSS // cho kẻ tấn công chèn mã độc hại vào các ứng dụng website.

    options.Cookie.IsEssential = true; //đảm bảo rằng nó không bị xóa khi người dùng từ chối sử dụng cookie.
});

builder.Services.AddHttpContextAccessor();
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

app.UseAuthorization();

app.MapRazorPages();

app.UseSession();



app.Run();
