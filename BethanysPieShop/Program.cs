using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// #####################################################################################
// ############################### DEPENDENCY INJECTION ################################
// #####################################################################################

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
//builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
//builder.Services.AddScoped<IPieRepository, MockPieRepository>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();

// Adds the shopping cart functionality here through the use of Sessions and HttpContextAccessor
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor(); // this is added to be able to use IHttpContextAccessor in the GetCart() method 

// Lets our app know about MVC, by default they don't know about code which uses MVC framework
builder.Services.AddControllersWithViews();

// Adds Entity Framework Core services using an extension method
// This connects the BethanysPieShopDbContext class to your code by using the connection string you have specified in the appsettings.json
builder.Services.AddDbContext<BethanysPieShopDbContext>(options => {
    options.UseSqlServer(builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]); // this is the concatenation of the ConnectionStrings:BethanysPieShopDbContextConnection in appsettings.json
});

var app = builder.Build();


// #####################################################################################
// #################################### MIDDLEWARES ####################################
// #####################################################################################

// Allows your app to identify requests that contain static files, and attempts to find that static file in the wwwroot folder
// It also shortcircuits the requests meaning the request will be stopped and not go to the next middleware anymore, a response is sent immediately
app.UseStaticFiles();
app.UseSession(); // brings in support for sessions. Sessions require middleware to use it, so add this method call.

 // Allows app to show an exception page on the browser page when app hits an exception in development mode (for DEVs only).
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

/* This is an endpoint middleware which will add support to routing to controller and controller actions/methods
 * MapDefaultControllerRoute() middleware is the same as if you manually add this:
 * app.MapControllerRoute(
 *     name: "default",
 *     pattern: "{controller=Home}/{action=Index}/{id?}");
 * 
 * This middleware uses the following default controller route: "{controller=Home}/{action=Index}/{id?}"
 * So if a controller or action is not defined then the default values are Home and Index respectively.
*/
app.MapDefaultControllerRoute(); 


// populate database with seed data, if there are no data in the tables
DbInitializer.Seed(app);
app.Run();
