using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using BackCar.Infrastructure;
using BackCar.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Para la BD:
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
<<<<<<< HEAD

builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IMantenimientoService, MantenimientoService>();
builder.Services.AddScoped<IContratoRentaService, ContratoRentaService>();

=======
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IMantenimientoService, MantenimientoService>();
builder.Services.AddScoped<IContratoRentaService, ContratoRentaService>();
>>>>>>> 08004e7f1f59ce83fc862f281e380c15474ca09c
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ISeguroService, SeguroService>();
builder.Services.AddScoped<IRegistroEstadoVehiculoService, RegistroEstadoVehiculoService>();
builder.Services.AddScoped<IIncidenteService, IncidenteService>();
builder.Services.AddScoped<IVehiculosSegurosService, VehiculosSegurosService>();

<<<<<<< HEAD


// Añadir servicios
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Configuración de Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "BackCarApp",
            ValidAudience = "BackCarClients",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

// Añadir autorización
builder.Services.AddAuthorization();






=======
>>>>>>> 08004e7f1f59ce83fc862f281e380c15474ca09c
// Configurar DbContext con la cadena de conexi�n de MySQL
// Configuraci�n del resto de servicios de la aplicaci�n
builder.Services.AddControllers();  // Registra los controladores (API)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
);  // Configura la conexi�n a la base de datos
    // Aseg�rate de tener la versi�n correcta de MySQL

//M�s BD, LOL y algo de APP:



builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// En la configuración de middleware
app.UseAuthentication();

//Prueba para BD:
// Configurar la tuber�a de solicitud y respuesta
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();  // Mapea los controladores a las rutas


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}