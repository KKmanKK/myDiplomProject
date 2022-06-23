using DiplomStore.BLL.DTO.Cart;
using DiplomStore.BLL.Services.Implementations;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.DAL;
using DiplomStore.DAL.Interface;
using DiplomStore.DAL.Repository;
using DiplomStore.Domain.Entity;
using DiplomStore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}).AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequiredUniqueChars = 6;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;        

}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<IRepositoryBase<Titles>, TitlesRepository>();
builder.Services.AddTransient<IRepositoryBase<Categories>, CategoriesRepository>();
builder.Services.AddTransient<ITovarsRepository, TovarsRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITovarService, TovarService>();
builder.Services.AddTransient<ITitleService, TitleService>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddScoped<CartDTO>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
Initialize.Init(builder.Services.BuildServiceProvider());
app.UseEndpoints(end =>
{    

    end.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
