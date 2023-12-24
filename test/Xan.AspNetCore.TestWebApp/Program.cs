using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Xan.AspNetCore.Http;
using Xan.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Mvc.Filters;
using Xan.AspNetCore.Parameter;
using Xan.AspNetCore.TestWebApp.Controllers;
using Xan.AspNetCore.TestWebApp.Data;
using Xan.AspNetCore.TestWebApp.Models.Crud;
using Xan.AspNetCore.TestWebApp.Rendering;
using Xan.AspNetCore.TestWebApp.Routing;
using Xan.AspNetCore.TestWebApp.Services.Crud;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add<PageSizeFilter>();
    });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services
        .AddDbContext<TestWebAppDbContext>(options =>
        {
            options
                .UseSqlite("DataSource=test.db")
                .EnableSensitiveDataLogging();
        });

builder.Services
    .AddCrudServices<ShipEntity, ListParameter, ShipRouter, ShipCrudService, ShipCrudModelFactory>()
    .AddScoped<TestAppHtmlFactory>()
    ;

builder.Services.AddValidatorsFromAssemblyContaining<ShipEntityValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}",
    defaults: new { controller = MvcHelper.ControllerName<RenderingController>(), action = nameof(RenderingController.DefaultHtmlFactoryTests) }
);

PageSizeCookie.Options.MaxAge = TimeSpan.FromDays(2);

using (IServiceScope scope = app.Services.CreateScope())
{
    TestWebAppDbContext db = scope.ServiceProvider.GetRequiredService<TestWebAppDbContext>();
    db.Database.EnsureCreated();
    
    if (! await db.Ships.AnyAsync())
    {
        db.Ships.Add(new ShipEntity
        {
            Name = "USS Enterpise",
            LengthInMeters = 123,
        });
        db.Ships.Add(new ShipEntity
        {
            Name = "Voyager",
            LengthInMeters = 555
        });
        await db.SaveChangesAsync();
    }
}

app.Run();
