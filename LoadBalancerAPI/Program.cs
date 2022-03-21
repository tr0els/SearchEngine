using LoadBalancerAPI.Controllers;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
    });
});

// Register IHttpClientFactory to configure and create HttpClient instances.
builder.Services.AddHttpClient();

// Setting load balancer as singleton app service with a default strategy
builder.Services.AddSingleton<ILoadBalancerStrategy, RoundRobinStrategy>(); 
builder.Services.AddSingleton(lb => new LoadBalancer(lb.GetRequiredService<ILoadBalancerStrategy>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
