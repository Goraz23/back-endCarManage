using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using BackCar.Infrastructure;
using BackCar.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog.Events;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Para la BD:
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();


builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IMantenimientoService, MantenimientoService>();
builder.Services.AddScoped<IContratoRentaService, ContratoRentaService>();


builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IMantenimientoService, MantenimientoService>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ISeguroService, SeguroService>();
builder.Services.AddScoped<IRegistroEstadoVehiculoService, RegistroEstadoVehiculoService>();
builder.Services.AddScoped<IIncidenteService, IncidenteService>();
builder.Services.AddScoped<IVehiculosSegurosService, VehiculosSegurosService>();




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



// Configuración de Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose() // Captura desde el nivel más bajo (Verbose)
    .WriteTo.Console() // Muestra los logs en consola
    .WriteTo.File("logs/all-logs.txt", rollingInterval: RollingInterval.Day) // Almacena todos los niveles en un archivo
    .WriteTo.File("logs/errors.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error) // Solo errores en otro archivo
    .CreateLogger();




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Reemplaza con la URL de tu frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});




// Configurar DbContext con la cadena de conexi�n de MySQL
// Configuraci�n del resto de servicios de la aplicaci�n
builder.Services.AddControllers();  // Registra los controladores (API)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
);  // Configura la conexi�n a la base de datos
    // Aseg�rate de tener la versi�n correcta de MySQL

//M�s BD, LOL y algo de APP:

builder.Host.UseSerilog(); // Asignar Serilog como logger de la aplicación

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// En la configuración de middleware
app.UseAuthentication();

app.UseMiddleware<BackCar.Presentation.Middlewares.ErrorHandlingMiddleware>();


app.UseCors("AllowSpecificOrigin"); // Aplica la política aquí
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


app.Run();
