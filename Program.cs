using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AIGenerator.Data;
using AIGenerator.Models;
using AIGenerator.Repository;
using AIGenerator.Interface;
using Microsoft.SemanticKernel;
using AIGenerator.Services;

namespace AIGenerator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString,sqlOptions =>
            {
                sqlOptions.CommandTimeout(120); // Increase timeout to 120 seconds
            }));


        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<Person>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddTransient<IResumeRepository, ResumeRepository>();
        builder.Services.AddTransient<IPortfolioRepository, PortfolioRepository>();
        builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
        builder.Services.AddTransient<IServiceRepository, ServiceRepository>();

        var key = builder.Configuration["OpenAI:Key"];

        // Add Semantic Kernel
        builder.Services.AddSingleton<Kernel>(sp =>
        {
            var kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.AddOpenAIChatCompletion("gpt-4", key);
            return kernelBuilder.Build();
        });

        builder.Services.AddSingleton<ICVParser, CvParserService>();
        builder.Services.AddSingleton<IPortfolioParser, PorfolioParserService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
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
        app.MapRazorPages();

        app.Run();
    }
}
