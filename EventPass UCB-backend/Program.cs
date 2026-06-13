//1. LOS USING SON PARA EF CORE BASE DE DATOS
using Microsoft.EntityFrameworkCore;
using tiendaweb_backend.Datos;
var builder = WebApplication.CreateBuilder(args);
//2. Esta es la segunda linea EF CORE BASE DE DATOS
builder.Services.AddDbContext<EventPassDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//3. Registramos nuestro gestor para que los controladores puedan usarlo
// La aplicacion necesita saber que los gestores existen
builder.Services.AddScoped<tiendaweb_backend.Negocio.GestionUsuarios>();
builder.Services.AddScoped<tiendaweb_backend.Negocio.GestionEventos>();
builder.Services.AddScoped<tiendaweb_backend.Negocio.GestionComentarios>();
builder.Services.AddScoped<tiendaweb_backend.Negocio.GestionInscripciones>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200") // Cambiar si el puerto es diferente
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//CORS
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();