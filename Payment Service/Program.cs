using ECommerce.DataService.PaymentData;
using ECommerce.DataService.OrderData;
using ECommerce.DataService.UserData;
using ECommerce.DataService.ProductData;
using ECommerce.DataService.ShippingData;
using Microsoft.EntityFrameworkCore;
using JwtTokenHandlerManager;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbContext_User>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDbConnection")));
builder.Services.AddDbContext<DbContext_Order>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDbConnection")));
builder.Services.AddDbContext<DbContext_Payment>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PaymentDbConnection")));
builder.Services.AddDbContext<DbContext_Product>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDbConnection")));
builder.Services.AddDbContext<DbContext_Shipping>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShippingConnection")));


builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication();
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
app.UseAuthentication();
app.UseRouting(); // Add this line to ensure routing is enabled before authorization.
app.UseAuthorization();

app.MapControllers();

app.Run();
