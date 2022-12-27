using MySqlConnector;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Api.Models;
using System.Net;
using Api.Dependencies;
using Api.DataServices.Interfaces;
using Api.DataServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add origins to cors policy
builder.Services.AddCors(options =>
{
    string[] origins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();
    options.AddPolicy(name: "AllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins(origins)
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
});

// Adding JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => {

    JwtSettings settings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = settings.Issuer,
        ValidAudience = settings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();

// Add the UserDataService to be injected into the controllers.
var socialFactory = new MySqlConnectionFactory(builder.Configuration.GetConnectionString("MomAndPop"));
Console.WriteLine(builder.Configuration.GetConnectionString("MomAndPop"));
builder.Services.AddTransient<IUserDataService>(_ => new UserDataService(socialFactory));
builder.Services.AddTransient<IPostDataService>(_ => new PostDataService(socialFactory));
var authFactory = new MySqlConnectionFactory(builder.Configuration.GetConnectionString("Auth"));
Console.WriteLine(builder.Configuration.GetConnectionString("Auth"));
builder.Services.AddTransient<IAuthDataService>(_ => new AuthDataService(authFactory));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use cors policy
app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
