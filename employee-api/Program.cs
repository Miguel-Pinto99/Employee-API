using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using employee_api;
using employee_api.Application.ApplicationUsers.Commands.CreateApplicationUser;
using employee_api.JsonConverter;
using employee_api.Data;
using employee_api.Exceptions;
using employee_api.Infrastructure;
using employee_api.Persistance;
using employee_api.Timers;
using employee_api.Workers;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

void setJsonOptions(JsonSerializerOptions serializerOptions)
{
    serializerOptions.Converters.Add(new NullableTimeSpanJsonConverter());
    serializerOptions.Converters.Add(new TimeSpanJsonConverter());
}

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlite(@"Data Source=DB.db"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        setJsonOptions(options.JsonSerializerOptions);
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(CreateApplicationUserCommand));
builder.Services.AddValidatorsFromAssemblies(new Assembly[] { typeof(CreateApplicationUserCommandValidator).Assembly });

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

builder.Services.AddTransient<IApplicationUsersRepository, ApplicationUserRepository>();
builder.Services.AddSingleton<IUnsService, UnsService>();
builder.Services.AddSingleton<IMqttService, MqttService>();
builder.Services.AddSingleton<ITimerService, TimerService>();
builder.Services.AddTransient<IWorkPatternRepository, WorkPatternRepository>();
builder.Services.AddTransient<IAbsentRepository, AbsentRepository>();
builder.Services.AddHostedService<UnsWorker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ApiExceptionsMiddleware>();

app.Run();
