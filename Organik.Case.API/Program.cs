using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Organik.Case.Application.Helpers;
using Organik.Case.Application.Interfaces;
using Organik.Case.Application.Interfaces.ExternalServices;
using Organik.Case.Application.Interfaces.Services;
using Organik.Case.Application.Interfaces.Utils;
using Organik.Case.Application.Validations;
using Organik.Case.Infrastructure.Data;
using Organik.Case.Infrastructure.Data.Repositories;
using Organik.Case.Infrastructure.ExternalServices;
using Organik.Case.Infrastructure.Services;
using Organik.Case.Infrastructure.Utils;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IJwtUtilsService, JwtUtilsService>();

builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHangfire(configuration =>
{
    configuration.UseMemoryStorage();
    configuration.UseFilter(
        new AutomaticRetryAttribute
        {
            Attempts = 2,
            OnAttemptsExceeded = AttemptsExceededAction.Delete
        }
    );
});

builder.Services.AddHangfireServer();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHangfireDashboard("/hangfire");

app.UseCors(x => x.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

app.Run();