
using Microsoft.EntityFrameworkCore;
using ProductRegistration_Group_8.Models;

namespace ProductRegistration_Group_8;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        builder.Services.AddControllersWithViews();


        builder.Services.AddDbContext<ProductContext>(options =>
            options.UseSqlite("Data Source=Product.db"));

        builder.Services.AddHttpClient("api", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5000"); // optional, for MVC HttpClient
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();



        // enables Swagger in development
        if (app.Environment.IsDevelopment())
        {
          
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else   // Configure the HTTP request pipeline.
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

            app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllers();

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}