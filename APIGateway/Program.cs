using Ocelot.DependencyInjection;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        // User service Swagger endpoint
        c.SwaggerEndpoint("/user/swagger/v1/swagger.json", "User Service API V1");

        // Account service Swagger endpoint
        c.SwaggerEndpoint("/account/swagger/v1/swagger.json", "Account Service API V1");

        // Shipping service Swagger endpoint
        c.SwaggerEndpoint("/shipping/swagger/v1/swagger.json", "Shipping Service API V1");

        // Product service Swagger endpoint
        c.SwaggerEndpoint("/product/swagger/v1/swagger.json", "Product Service API V1");

        // Payment service Swagger endpoint
        c.SwaggerEndpoint("/payment/swagger/v1/swagger.json", "Payment Service API V1");

        // Orders service Swagger endpoint
        c.SwaggerEndpoint("/orders/swagger/v1/swagger.json", "Orders Service API V1");
        c.RoutePrefix = string.Empty;

    });


}
app.UseCors("CORSPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.UseOcelot();
app.Run();