using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using BackCar.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class VehiculosSegurosService : IVehiculosSegurosService
{
    private readonly ApplicationDbContext _context;

    public VehiculosSegurosService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<VehiculosSegurosDTO>> ObtenerVehiculosSegurosAsync()
    {
        var vehiculosSeguros = await _context.VehiculosSeguros
            .Include(vs => vs.Seguro)
            .Include(vs => vs.Vehiculo)
            .ToListAsync();

        return vehiculosSeguros.Select(vs => new VehiculosSegurosDTO
        {
            Seguros_id = vs.Seguros_id,
            Vehiculos_id = vs.Vehiculos_id
        }).ToList();
    }

    public async Task CrearVehiculoSeguroAsync(VehiculosSegurosDTO vehiculosSegurosDTO)
    {
        var vehiculosSeguros = new VehiculoSeguro
        {
            Seguros_id = vehiculosSegurosDTO.Seguros_id,
            Vehiculos_id = vehiculosSegurosDTO.Vehiculos_id
        };
        await _context.VehiculosSeguros.AddAsync(vehiculosSeguros);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ActualizarVehiculoSeguroAsync(int id, VehiculosSegurosDTO vehiculoSeguroActualizado)
    {
        try
        {
            // Buscar el registro en la base de datos
            var vehiculoSeguro = await _context.VehiculosSeguros.FirstOrDefaultAsync(vs => vs.Id_VehiculoSeguro == id);

            // Validar si existe
            if (vehiculoSeguro == null)
            {
                return false; // Retornar falso si no se encuentra el registro
            }

            // Actualizar los campos
            vehiculoSeguro.Seguros_id = vehiculoSeguroActualizado.Seguros_id;
            vehiculoSeguro.Vehiculos_id = vehiculoSeguroActualizado.Vehiculos_id;

            // Guardar los cambios en la base de datos
            _context.VehiculosSeguros.Update(vehiculoSeguro);
            await _context.SaveChangesAsync();

            return true; // Retornar true si la actualización fue exitosa
        }
        catch (Exception)
        {
            return false; // Manejar posibles errores
        }
    }

    public async Task<bool> EliminarVehiculoSeguroAsync(int id)
    {
        var vehiculo = await _context.VehiculosSeguros.FindAsync(id);
        if (vehiculo == null)
            return false;

        _context.VehiculosSeguros.Remove(vehiculo);
        await _context.SaveChangesAsync();
        return true;
    }
}
