using SistemaGestionTareas.Models;

namespace SistemaGestionTareas.Services;

/// <summary>Filtros reutilizables sin dependencia de consola o almacenamiento.</summary>
public sealed class FiltradoDeTareas
{
    /// <summary>Compara el día de la fecha límite, sin considerar la hora.</summary>
    public List<Tarea> FiltrarPorFecha(IEnumerable<Tarea> tareas, DateTime fecha) =>
        tareas.Where(t => t.FechaLimite.Date == fecha.Date).ToList();
    /// <summary>Selecciona tareas con el estado solicitado.</summary>
    public List<Tarea> FiltrarPorEstado(IEnumerable<Tarea> tareas, bool completada) =>
        tareas.Where(t => t.Completada == completada).ToList();
}
