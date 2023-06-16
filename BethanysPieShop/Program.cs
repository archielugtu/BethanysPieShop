using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*
 * DEPENDENCY INJECTION OF SERVICES
 * To implement dependency injection, we need to configure a DI container with classes that is participating in DI. 
 * DI Container has to decide whether to return a new instance of the service or provide an existing instance. 
 * In startup class, we perform this activity on ConfigureServices method.
 * 
 * The lifetime of the service depends on when the dependency is instantiated and how long it lives. And lifetime depends on how we have registered those services.
 * 
 * 3 methods that allow us to add services to the request pipeline:
 * 
 * .AddScoped() - adds a single instance of that service which will live throughout the duration of the request being handled (i.e,. singleton per request) 
 * .AddTransient() - Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
 * .AddSingleton() - Singleton lifetime services are created the first time they are requested (or when ConfigureServices is run if you specify an instance there) and then every subsequent request will use the same instance.
 * */

// Used Mock repositories before we implemented db functionality (i.e., EF Core) in our app
/*builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<IPieRepository, MockPieRepository>();*/

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();

// Lets our app know about MVC, by default they don't know about code which uses MVC framework
builder.Services.AddControllersWithViews();

// Adds EF Core services using an extension method
builder.Services.AddDbContext<BethanysPieShopDbContext>(options => {
    options.UseSqlServer(builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]); // this is the concatenation of the ConnectionStrings:BethanysPieShopDbContextConnection in appsettings.json
});

var app = builder.Build();

// -- MIDDLEWARES --

// Allows your app to identify requests that contain static files, and attempts to find that static file in the wwwroot folder
// It also shortcircuits the requests meaning the request will be stopped and not go to the next middleware anymore, a response is sent immediately
app.UseStaticFiles();

 // Allows app to show an exception page when app hits an exception in development mode (for DEVs only)
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

// This will set some defaults used in MVC to route the views/pages that we're going to have
app.MapDefaultControllerRoute();

app.Run();
