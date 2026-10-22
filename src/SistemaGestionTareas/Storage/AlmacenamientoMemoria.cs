using SistemaGestionTareas.Interfaces;
using SistemaGestionTareas.Models;

namespace SistemaGestionTareas.Storage;

/// <summary>Conserva copias de las tareas durante la vida de esta instancia.</summary>
public sealed class AlmacenamientoMemoria : IAlmacenamientoTareas
{
    private List<Tarea> tareas = [];
    /// <inheritdoc/>
    public List<Tarea> ObtenerTodas() => tareas.Select(t => t.Copiar()).ToList();
    /// <inheritdoc/>
    public void GuardarTodas(IEnumerable<Tarea> tareas) =>
        this.tareas = tareas.Select(t => t.Copiar()).ToList();
}
