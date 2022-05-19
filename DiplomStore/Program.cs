using DiplomStore.BLL.DTO.Cart;
using DiplomStore.BLL.Services.Implementations;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.DAL;
using DiplomStore.DAL.Interface;
using DiplomStore.DAL.Repository;
using DiplomStore.Domain.Entity;
using DiplomStore.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IRepositoryBase<Titles>, TitlesRepository>();
builder.Services.AddTransient<IRepositoryBase<Categories>, CategoriesRepository>();
builder.Services.AddTransient<ITovarsRepository, TovarsRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITovarService, TovarService>();
builder.Services.AddTransient<ITitleService, TitleService>();
builder.Services.AddScoped<CartDTO>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseRouting();
app.UseSession();

app.UseEndpoints(end =>
{    
    end.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
