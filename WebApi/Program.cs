using Application;
using Application.Interfaces;
using DataAccess;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connstr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddMvc().AddControllersAsServices();
builder.Services.AddDbContext<WarehouseContext>(options => options.UseSqlServer(connstr));
builder.Services.AddScoped<DbContext, WarehouseContext>()
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddTransient<IGenericRepository<DeliveryBatch>, GenericRepository<DeliveryBatch>>()
    .AddTransient<IGenericRepository<DeliveryBatchContent>, GenericRepository<DeliveryBatchContent>>()
    .AddTransient<IGenericRepository<Location>, GenericRepository<Location>>()
    .AddTransient<IGenericRepository<LocationType>, GenericRepository<LocationType>>()
    .AddTransient<IGenericRepository<Product>, GenericRepository<Product>>()
    .AddTransient<IGenericRepository<ProductGroup>, GenericRepository<ProductGroup>>()
    .AddTransient<IGenericRepository<ProductGroupContent>, GenericRepository<ProductGroupContent>>()
    .AddTransient<IReplenishProducts, ProductReplenisher>()
    .AddTransient<IMoveProducts, ProductMover>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
