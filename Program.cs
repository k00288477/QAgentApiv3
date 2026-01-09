using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QAgentApi.Data;
using QAgentApi.Repository;
using QAgentApi.Repository.Interfaces;
using QAgentApi.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Database Provider
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Detect database provider
if (connectionString.Contains("Server=localhost") || connectionString.Contains("SQLExpress"))
{
    // SQL Server (local development)
    builder.Services.AddDbContext<AppDBContext>(options =>
        options.UseSqlServer(connectionString)
    );
}
else
{
    // MySQL (production on Railway)
    builder.Services.AddDbContext<AppDBContext>(options =>
        options.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        )
    );
}

//builder.Services.AddDbContext<AppDBContext>(options =>
//   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//);

// Allow Requests from Local Vue app - Must Update for Production
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:5173",
                "https://qagent.netlify.app/" // Demo Frontend URL
                )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


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
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)
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

app.Run();
