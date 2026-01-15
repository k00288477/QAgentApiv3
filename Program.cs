using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QAgentApi.Data;
using QAgentApi.Repository;
using QAgentApi.Repository.Interfaces;
using QAgentApi.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("========================================");
Console.WriteLine("APPLICATION BUILD COMPLETE");
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Production: {app.Environment.IsProduction()}");
Console.WriteLine("========================================");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Get the DB Connection string
var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 21))
    )
);

//builder.Services.AddDbContext<AppDBContext>(options =>
//   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:5173",
                "http://localhost:4173",
                "https://qagent.netlify.app"
                )
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                .AllowCredentials();
        });
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowVue",
//        policy =>
//        {
//            policy.AllowAnyOrigin() // Temporary - allows all origins
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});


//// Authentication and Authorization
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
//            ValidAudience = builder.Configuration["AppSettings:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)
//            )
//        };
//    });

// Get JWT settings with fallback
var jwtIssuer = builder.Configuration["AppSettings:Issuer"]
    ?? Environment.GetEnvironmentVariable("AppSettings__Issuer")
    ?? "QAgentIssuer";

var jwtAudience = builder.Configuration["AppSettings:Audience"]
    ?? Environment.GetEnvironmentVariable("AppSettings__Audience")
    ?? "QAgentAudience";

var jwtToken = builder.Configuration["AppSettings:Token"]
    ?? Environment.GetEnvironmentVariable("AppSettings__Token")
    ?? throw new InvalidOperationException("JWT Token not configured");

Console.WriteLine($"JWT Config - Issuer: {jwtIssuer}, Audience: {jwtAudience}, Token Length: {jwtToken?.Length ?? 0}");

// Authentication and Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtToken)
            )
        };
    });

// Service Layer
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrganisationService>();
builder.Services.AddScoped<TestSuiteService>();
builder.Services.AddScoped<TestCaseService>();
builder.Services.AddScoped<AuthService>();

// Repository Layer
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrganisationRepository, OrganisationRepository>();
builder.Services.AddScoped<ITestSuiteRepository, TestSuiteRepository>();
builder.Services.AddScoped<ITestCaseRepository, TestCaseRepository>();
builder.Services.AddScoped<ITestStepRepository, TestStepRepository>();

var app = builder.Build();

// run db migration on startup for production environment
if (app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDBContext>();

            context.Database.Migrate();
            Console.WriteLine("Database migrations applied successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
            throw;
        }
    }
}


Console.WriteLine("CONFIGURING MIDDLEWARE...")
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowVue");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

Console.WriteLine("========================================");
Console.WriteLine("STARTING APPLICATION...");
Console.WriteLine("========================================");

app.Run();
