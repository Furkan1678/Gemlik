using IssueTrackerPro.Application.Mappings;
using IssueTrackerPro.Application.Features.User.Commands; // CreateUserCommand için
using IssueTrackerPro.Domain.Interfaces.Repositories;
using IssueTrackerPro.Infrastructure.Repositories;
using IssueTrackerPro.Infrastructure.Services.Authentication;
using IssueTrackerPro.Infrastructure.Services.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor(); // Zaten ekli, yerinde duruyor
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IssueTrackerPro API", Version = "v1" });

    // JWT Authentication desteği
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// CORS desteği
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Dependency Injection
builder.Services.AddScoped<IUserRepository>(sp => new UserRepository(connectionString));
builder.Services.AddScoped<IProjectRepository>(sp => new ProjectRepository(connectionString));
builder.Services.AddScoped<IIssueRepository>(sp => new IssueRepository(connectionString));
builder.Services.AddScoped<ICommentRepository>(sp => new CommentRepository(connectionString));
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton(new JwtTokenService(jwtKey, jwtIssuer, jwtAudience));

// MediatR - Doğru assembly’yi tara
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

// AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile), typeof(ProjectProfile), typeof(IssueProfile), typeof(CommentProfile));

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // CORS’u burada etkinleştir
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();