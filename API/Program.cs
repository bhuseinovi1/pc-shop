using System.Net;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

// postavlja konfiguracije aplikacije kao što su root folder i slično

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(x=> x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* builder.Services.AddHttpsRedirection(options => {
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;options.HttpsPort = 7025;
}); */

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync(); // kreira migracije i bazu
        await StoreContextSeed.SeedAsync(context,loggerFactory);
    }
    catch (System.Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e,"An error occured during migrations");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
