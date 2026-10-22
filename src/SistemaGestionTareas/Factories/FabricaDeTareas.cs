using SistemaGestionTareas.Models;

namespace SistemaGestionTareas.Factories;

/// <summary>Fábrica simple; no implementa Factory Method de GoF.</summary>
public sealed class FabricaDeTareas
{
    /// <summary>Crea una tarea válida con identificador nuevo y estado pendiente.</summary>
    public Tarea CrearTarea(string titulo, string descripcion, DateTime fechaLimite) =>
        new(Guid.NewGuid(), titulo, descripcion, fechaLimite, false);
}
