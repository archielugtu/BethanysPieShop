var builder = WebApplication.CreateBuilder(args);

// Lets our app know about MVC, by default they don't know about code which uses MVC framework
builder.Services.AddControllersWithViews();

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
