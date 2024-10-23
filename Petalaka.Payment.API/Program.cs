using System.Text.Json;
using Petalaka.Payment.API;
using Petalaka.Payment.API.CustomMiddleware;
using Petalaka.Payment.API.CustomModelConvention;
using Petalaka.Payment.Repository;
using Petalaka.Payment.Service;
using Petalaka.Payment.Service.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseControllerModelConvention());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfigureServiceRepository(builder.Configuration);
builder.Services.AddConfigureServiceService(builder.Configuration);
builder.Services.AddConfigureServiceAPI(builder.Configuration);

var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
await app.UseInitializeDatabaseAsync();
app.UseCors("AllowAll");
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service API v1");
});

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<PaymentService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(JsonSerializer.Serialize(new
            { Message = "Welcome to Petalaka.Payment.API" }));
    });
});
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service API v1");
});
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseMiddleware<ValidateJwtTokenMiddleware>();

app.MapControllers();

app.Run();
