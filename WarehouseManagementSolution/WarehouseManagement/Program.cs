using System.Text;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApplication1.Context;
using WebApplication1.JWTProvider;
using WebApplication1.Logging;
using WebApplication1.Service.Implementations;
using WebApplication1.Service.Interfaces;
using WebApplication1.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

#region Database

builder.Services.AddDbContext<DatabaseContext>( option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

#endregion

#region Services

builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IOperatorService, OperatorService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUserContextService, UserContextService>();

#endregion

#region JWT

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
    ValidAudience = builder.Configuration["JwtSettings:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
};

builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x=>x.TokenValidationParameters = tokenValidationParameters);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.AdminPolicyName, policy =>
    {
        policy.RequireClaim(IdentityData.ClaimType, IdentityData.AdminClaimValue);
    });

    options.AddPolicy(IdentityData.UserPolicyName, policy =>
    {
        policy.RequireClaim(IdentityData.ClaimType, IdentityData.UserClaimValue);
    });
    
    options.AddPolicy(IdentityData.OperatorPolicyName, policy =>
    {
        policy.RequireClaim(IdentityData.ClaimType, IdentityData.OperatorClaimValue);
    });
    
    options.AddPolicy(IdentityData.BackOfficePolicyName, policy =>
    {
        policy.RequireClaim(IdentityData.ClaimType, IdentityData.BackOfficeClaimValue);
    });
}); 

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();


#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Warehouse Management", Version = "v1" });

    // Define the Bearer token security scheme
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    // Add the Bearer token as a requirement
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse Management V1");

        // Add an authorization input for Bearer Token in Swagger UI
        c.DefaultModelsExpandDepth(-1);
        c.ConfigObject.AdditionalItems["bearerAuth"] = new
        {
            type = "apiKey",
            name = "Authorization",
            description = "Copy 'Bearer token' here",
            @in = "header"
        };
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();