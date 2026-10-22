using SistemaGestionTareas.Factories;
using SistemaGestionTareas.Interfaces;
using SistemaGestionTareas.Models;

namespace SistemaGestionTareas.Services;

/// <summary>Coordina CRUD y emite eventos únicamente después de guardar los cambios.</summary>
public sealed class GestorDeTareas(IAlmacenamientoTareas almacenamiento, FabricaDeTareas fabrica)
{
    /// <summary>Notifica altas, ediciones, eliminaciones y cambios de completitud.</summary>
    public event EventHandler<TareaCambiadaEventArgs>? TareaCambiada;

    /// <summary>Registra una nueva tarea utilizando la fábrica simple.</summary>
    public Tarea AgregarTarea(string titulo, string descripcion, DateTime fechaLimite)
    {
        var tarea = fabrica.CrearTarea(titulo, descripcion, fechaLimite);
        var tareas = ObtenerTareas();
        tareas.Add(tarea);
        GuardarYNotificar(tareas, tarea.Id, "agregada");
        return tarea;
    }

    /// <summary>Recupera todas las tareas.</summary>
    public List<Tarea> ObtenerTareas() => almacenamiento.ObtenerTodas();

    /// <summary>Consulta una tarea o comunica que no existe.</summary>
    public Tarea ObtenerTareaPorId(Guid id) => Buscar(ObtenerTareas(), id);

    /// <summary>Edita los datos conservando el identificador y el estado.</summary>
    public void EditarTarea(Guid id, string titulo, string descripcion, DateTime fechaLimite) =>
        Modificar(id, t => t.Editar(titulo, descripcion, fechaLimite), "editada");

    /// <summary>Establece el estado solicitado.</summary>
    public void CambiarEstado(Guid id, bool completada) =>
        Modificar(id, t => t.CambiarEstado(completada), completada ? "marcada como completada" : "marcada como pendiente");

    /// <summary>Elimina una tarea existente.</summary>
    public void EliminarTarea(Guid id)
    {
        var tareas = ObtenerTareas();
        tareas.Remove(Buscar(tareas, id));
        GuardarYNotificar(tareas, id, "eliminada");
    }

    private static Tarea Buscar(List<Tarea> tareas, Guid id) =>
        tareas.FirstOrDefault(t => t.Id == id) ?? throw new KeyNotFoundException("No existe una tarea con ese identificador.");

    private void Modificar(Guid id, Action<Tarea> cambio, string operacion)
    {
        var tareas = ObtenerTareas();
        cambio(Buscar(tareas, id));
        GuardarYNotificar(tareas, id, operacion);
    }

    private void GuardarYNotificar(List<Tarea> tareas, Guid id, string operacion)
    {
        almacenamiento.GuardarTodas(tareas);
        TareaCambiada?.Invoke(this, new TareaCambiadaEventArgs(id, operacion));
    }
}
