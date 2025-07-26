using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Dynamically combine config files
builder.Configuration
    .AddJsonFile("ocelot-auth.json", optional: false, reloadOnChange: true)
    .AddJsonFile("ocelot-product.json", optional: true, reloadOnChange: true)
    .AddJsonFile("ocelot-masterdata.json", optional: true, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
await app.UseOcelot();
app.Run();
