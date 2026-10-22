using System.Text.Json.Serialization;

namespace SistemaGestionTareas.Models;

/// <summary>Actividad con identidad inmutable y modificaciones validadas.</summary>
public sealed class Tarea
{
    public Guid Id { get; }
    public string Titulo { get; private set; } = "";
    public string Descripcion { get; private set; } = "";
    public DateTime FechaLimite { get; private set; }
    public bool Completada { get; private set; }

    /// <summary>Reconstruye una tarea conservando su identidad y estado.</summary>
    [JsonConstructor]
    public Tarea(Guid id, string titulo, string descripcion, DateTime fechaLimite, bool completada)
    {
        if (id == Guid.Empty) throw new ArgumentException("El identificador no puede estar vacío.");
        Id = id;
        Editar(titulo, descripcion, fechaLimite);
        CambiarEstado(completada);
    }

    /// <summary>Actualiza los datos sin modificar la identidad ni la completitud.</summary>
    public void Editar(string titulo, string descripcion, DateTime fechaLimite)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("El título es obligatorio.");
        ArgumentNullException.ThrowIfNull(descripcion);
        if (fechaLimite == default) throw new ArgumentException("La fecha límite no es válida.");
        Titulo = titulo.Trim();
        Descripcion = descripcion;
        FechaLimite = fechaLimite.Date;
    }

    /// <summary>Establece explícitamente si la tarea está completada.</summary>
    public void CambiarEstado(bool completada) => Completada = completada;

    /// <summary>Crea una copia independiente para proteger los datos almacenados.</summary>
    public Tarea Copiar() => new(Id, Titulo, Descripcion, FechaLimite, Completada);
}
