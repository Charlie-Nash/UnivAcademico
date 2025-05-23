using UnivAcademico.Api.Helpers;
using UnivAcademico.Application.UseCases;
using UnivAcademico.Domain.Interfaces;
using UnivAcademico.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();
builder.Services.AddScoped<MatriculaService>();
builder.Services.AddScoped<AppAuth>();
builder.Services.AddSingleton<RabbitMqPublisher>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "UnivAcademico: Activo");
app.MapControllers();

app.Run();