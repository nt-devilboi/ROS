using ROS;
using ROS.DataBase;
using ROS.Entity;
using ROS.Interface;
using ROS.Service;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.File;
using Vostok.Logging.File.Configuration;
using Vostok.Logging.Microsoft;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var consoleLog = builder.Environment.IsDevelopment()
    ? (ILog)new SynchronousConsoleLog()
    : new ConsoleLog();
var fileLog = new FileLog(new FileLogSettings()
{
    WaitIfQueueIsFull = true,
    RollingStrategy = new RollingStrategyOptions()
    {
        Period = RollingPeriod.Day,
        Type = RollingStrategyType.ByTime,
        MaxFiles = 7
    }
});

var log = new CompositeLog(consoleLog, fileLog);
builder.Logging.ClearProviders();
builder.Logging.AddVostok(log);
builder.Services.AddSingleton<ILog>(log);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository<Cheque>, ChequeRepository>();
builder.Services.AddSingleton<IRepository<Product>, ProductRepository>();
builder.Services.AddSingleton<IShopRepository, ShopRepository>();
builder.Services.AddSingleton<IProduct, Product>();
builder.Services.AddSingleton<IShop, Shop>();
builder.Services.AddScoped<PurchaseContext>();

builder.Services.AddSingleton<ILog>(log);
var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();