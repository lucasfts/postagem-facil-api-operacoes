using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PostagemFacil.Operacoes.API.Data;

var builder = WebApplication.CreateBuilder(args);
var dbConnection = builder.Configuration.GetConnectionString("Default");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OperacoesContext>(opt => opt.UseSqlServer(dbConnection));

var corsPolicy = new CorsPolicyBuilder().AllowAnyHeader().AllowAnyOrigin().Build();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(corsPolicy));

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
