using BackCar.Application.Interfaces;
using BackCar.Infrastructure;
using BackCar.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ISeguroService, SeguroService>();
builder.Services.AddScoped<IRegistroEstadoVehiculoService, RegistroEstadoVehiculoService>();
builder.Services.AddScoped<IIncidenteService, IncidenteService>();


>>>>>>> d5b22415334c534204caa9f30661d87bba01c92a

// Configurar DbContext con la cadena de conexión de MySQL
// Configuración del resto de servicios de la aplicación
builder.Services.AddControllers();  // Registra los controladores (API)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
);  // Configura la conexión a la base de datos
    // Asegúrate de tener la versión correcta de MySQL

//Más BD, LOL y algo de APP:



builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//Prueba para BD:
// Configurar la tubería de solicitud y respuesta
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