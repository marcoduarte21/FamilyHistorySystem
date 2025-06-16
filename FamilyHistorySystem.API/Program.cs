using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using FamilyHistorySystem.Services.interfaces;
using FamilyHistorySystem.Services.services;
using FamilyHistorySystem.Exceptions;
using FamilyHistorySystem.DataAccess;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.SqlServer;
using FamilyHistorySystem.AutoMapperProfiles;
using FamilyHistorySystem.Utils.constants.Response;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FamilyHistorySystem.Services.interfaces.auth;
using FamilyHistorySystem.Services.services.auth;
using FamilyHistorySystem.Utils.constants.messages.ErrorMessage;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.Response.StatusCode = StatusCode.Unauthorized;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(ErrorMessage.ErrorUnauthorized);
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCode.Forbidden;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(ErrorMessage.ErrorForbidden);
        }
    };
}

);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFamilyHistory, FamilyHistoryService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<DBContexto>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionString, sql =>
    {
        sql.MigrationsAssembly("FamilyHistorySystem.API");
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(MapperAuth));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddAuthorization();
var app = builder.Build();


app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        int statusCode = error switch
        {
            CustomException ex => ex.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;

        var response = new ApiResponse<object>(error?.Message, null, false);
        await context.Response.WriteAsJsonAsync(response);
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
