using JsonSubTypes;
using kvaksy_backend.data.DbContexts;
using kvaksy_backend.data.models;
using kvaksy_backend.Helpers;
using kvaksy_backend.Repositories;
using kvaksy_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.EnableAnnotations();
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Quality Sentry API",
            TermsOfService = new Uri("https://example.com/terms"),
            License = new OpenApiLicense
            {
                Name = "License",
                Url = new Uri("https://example.com/license")
            }
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });

    }
);

builder.Services.AddCors(options =>
{
    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
        {
            //policy.WithOrigins("https://quality-sentry--*");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
});


// Add the database contexts

// Get the connection string from the appsettings.json file
var dbConnectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(dbConnectionString));

builder.Services.AddDbContext<ReportDbContext>(options => 
    options.UseSqlServer(dbConnectionString));

// Set up dependency injection for the repositories
builder.Services.AddScoped<UserDbContext>();
builder.Services.AddScoped<ReportDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "JWTAuthenticationServer",
        ValidAudience = "JWTServicePostmanClient",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Security").GetValue<string>("JwtSecret")))
    };
});

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITemperatureRepository, TemperatureRepository>();
builder.Services.AddScoped<IWeightRepository, WeightRepository>();

builder.Services.AddScoped<IReportSessionService, ReportSessionService>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ITemperatureServices, TemperatureServices>();
builder.Services.AddScoped<IWeightServices, WeightServices>();

var app = builder.Build();

app.UseMiddleware<JwtMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
