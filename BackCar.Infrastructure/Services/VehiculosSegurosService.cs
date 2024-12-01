using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using BackCar.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

public class VehiculosSegurosService : IVehiculosSegurosService
{
    private readonly ApplicationDbContext _context;

    public VehiculosSegurosService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<VehiculosSegurosDTO>> ObtenerVehiculosSegurosAsync()
    {
        try
        {
            Log.Information("Iniciando solicitud para obtener todos los vehículos seguros.");
            var vehiculosSeguros = await _context.VehiculosSeguros
                .Include(vs => vs.Seguro)
                .Include(vs => vs.Vehiculo)
                .ToListAsync();

            Log.Information("Solicitud completada para obtener todos los vehículos seguros.");
            return vehiculosSeguros.Select(vs => new VehiculosSegurosDTO
            {
                Seguros_id = vs.Seguros_id,
                Vehiculos_id = vs.Vehiculos_id
            }).ToList();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error al obtener los vehículos seguros.");
            throw;
        }
    }

    public async Task CrearVehiculoSeguroAsync(VehiculosSegurosDTO vehiculosSegurosDTO)
    {
        try
        {
            Log.Information("Iniciando solicitud para crear vínculo entre vehículo y seguro. Vehículo ID: {Vehiculos_id}, Seguro ID: {Seguros_id}.", vehiculosSegurosDTO.Vehiculos_id, vehiculosSegurosDTO.Seguros_id);
            var vehiculosSeguros = new VehiculoSeguro
            {
                Seguros_id = vehiculosSegurosDTO.Seguros_id,
                Vehiculos_id = vehiculosSegurosDTO.Vehiculos_id
            };
            await _context.VehiculosSeguros.AddAsync(vehiculosSeguros);
            await _context.SaveChangesAsync();
            Log.Information("Vinculación creada exitosamente entre vehículo ID: {Vehiculos_id} y seguro ID: {Seguros_id}.", vehiculosSegurosDTO.Vehiculos_id, vehiculosSegurosDTO.Seguros_id);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error al crear vínculo entre vehículo y seguro.");
            throw;
        }
    }

    public async Task<bool> ActualizarVehiculoSeguroAsync(int id, VehiculosSegurosDTO vehiculoSeguroActualizado)
    {
        try
        {
            Log.Information("Iniciando solicitud para actualizar vínculo entre vehículo y seguro con ID: {VehiculoSeguroId}.", id);
            var vehiculoSeguro = await _context.VehiculosSeguros.FirstOrDefaultAsync(vs => vs.Id_VehiculoSeguro == id);
            if (vehiculoSeguro == null)
            {
                Log.Warning("Vínculo de vehículo y seguro con ID: {VehiculoSeguroId} no encontrado.", id);
                return false;
            }

            vehiculoSeguro.Seguros_id = vehiculoSeguroActualizado.Seguros_id;
            vehiculoSeguro.Vehiculos_id = vehiculoSeguroActualizado.Vehiculos_id;
            _context.VehiculosSeguros.Update(vehiculoSeguro);
            await _context.SaveChangesAsync();

            Log.Information("Vínculo entre vehículo y seguro con ID: {VehiculoSeguroId} actualizado exitosamente.", id);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error al actualizar vínculo entre vehículo y seguro con ID: {VehiculoSeguroId}.", id);
            return false;
        }
    }

    public async Task<bool> EliminarVehiculoSeguroAsync(int id)
    {
        try
        {
            Log.Information("Iniciando solicitud para eliminar vínculo de vehículo y seguro con ID: {VehiculoSeguroId}.", id);
            var vehiculoSeguro = await _context.VehiculosSeguros.FindAsync(id);
            if (vehiculoSeguro == null)
            {
                Log.Warning("Vínculo de vehículo y seguro con ID: {VehiculoSeguroId} no encontrado para eliminar.", id);
                return false;
            }

            _context.VehiculosSeguros.Remove(vehiculoSeguro);
            await _context.SaveChangesAsync();

            Log.Information("Vínculo de vehículo y seguro con ID: {VehiculoSeguroId} eliminado exitosamente.", id);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error al eliminar vínculo entre vehículo y seguro con ID: {VehiculoSeguroId}.", id);
            return false;
        }
    }
}
