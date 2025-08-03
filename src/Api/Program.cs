using Api;
using Api.Middlewares;
using Application;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.SeedData;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.AddSqlServerDbContext<AppDbContext>("auth");
builder.AddRabbitMQClient("rabbitmq");

builder.Services.AddApplicationCustomServices();
builder.Services.AddInfrastructureCustomServices();
builder.Services.AddCustomIdentity();
builder.Services.AddCustomAuthenticationJwt(builder.Configuration);
builder.Services.AddCustomCors();
builder.Services.AddCustomMediatR();

// External services
builder.Services.AddCustomSendGrid(builder.Configuration);
builder.Services.AddCustomRabbitMQ();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// OpenAPI or Swagger, you decide which one to use
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // OpenAPI or Swagger, you decide which one to use
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed the database with initial data
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();

    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
