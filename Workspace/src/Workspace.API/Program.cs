using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;
using Workspace.API;
using Workspace.Application;
using Workspace.Domain;
using Workspace.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();

// Add configuration

builder.Services.AddConfigureMediatR();
builder.Services.ConfigurePostgreSQLRetryOptions(builder.Configuration.GetSection(nameof(PostgreSQLRetryOptions)));
builder.Services.AddSqlConfiguration();

builder.Services.AddConfigureAutoMapper();
builder.Services.AddRepositoryBaseConfiguration();

builder.Services.AddJWTConfiguration(builder.Configuration);

// Add API

builder.Services.AddControllers().AddApplicationPart(Workspace.Presentation.AssemblyReference.Assembly);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services
        .AddSwaggerGenNewtonsoftSupport()
        .AddFluentValidationRulesToSwagger()
        .AddEndpointsApiExplorer()
        .AddSwagger();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapIdentityApi<User>();

// Configure the HTTP request pipeline.
//app.UseSwagger();
//app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
    app.ConfigureSwagger();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

app.Run();
