using APIProspecto.DTO;
using APIProspecto.DTO.Interfaces;
using APIProspecto.Service;
using APIProspecto.Service.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Configuracion para documentacion
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();//con esto se vuelven visibles las anotaciones 
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Test Prospecto",
        Description = "Api creada para evalucaion con tematica de prospecto"
    });
});

//Independency inyection
builder.Services.AddScoped<IContextDB, ContextDB>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IGenericRespon, GenericRespon>();
builder.Services.AddScoped<IProspectoService, ProspectoService>();
builder.Services.AddScoped<IDocumentsService, DocumentsService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options =>
options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
