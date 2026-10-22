using SistemaGestionTareas.Models;

namespace SistemaGestionTareas.Interfaces;

/// <summary>Contrato de las estrategias de almacenamiento de una colección completa.</summary>
public interface IAlmacenamientoTareas
{
    /// <summary>Obtiene una colección independiente de tareas.</summary>
    List<Tarea> ObtenerTodas();
    /// <summary>Reemplaza la colección almacenada; comunica los errores al consumidor.</summary>
    void GuardarTodas(IEnumerable<Tarea> tareas);
}
