using Microsoft.AspNetCore.Authentication;
using Server.Interfaces;
using Server.Models;
using Server.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICalculator>(new Calculator());
builder.Services.AddSingleton<IUsersService>(new UsersService());

builder.Services.AddControllers();

builder.Services.AddAuthentication(Constants.BasicAuthentication)
                .AddScheme<AuthenticationSchemeOptions, Authentication>(Constants.BasicAuthentication, null);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
