using ETicaret.Admin.Middlewares;
using ETicaret.Data;
using ETicaret.Data.Entities;
using ETicaret.Services.Concrete;
using ETicaret.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ETicaretDbContext>(option =>
            {
                option.UseSqlServer("Server=.;Database=ECommerce;User Id=sa;Password=Samet.4176;TrustServerCertificate=True");
            });
            builder.Services.AddScoped<IMailRepository, EFMailRepository>();
            builder.Services.AddScoped<IRepository<User>, EFRepository<User>>();
            builder.Services.AddScoped<IRepository<Category>, EFRepository<Category>>();
            builder.Services.AddScoped<IRepository<Product>, EFRepository<Product>>();
            builder.Services.AddScoped<IRepository<Order>, EFRepository<Order>>();
            builder.Services.AddScoped<IRepository<BasketItem>, EFRepository<BasketItem>>();
            builder.Services.AddScoped<IBasketService, EFBasketRepository>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/Account/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(45);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    //app.UseExceptionHandler("/Home/Error");
            //}
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}