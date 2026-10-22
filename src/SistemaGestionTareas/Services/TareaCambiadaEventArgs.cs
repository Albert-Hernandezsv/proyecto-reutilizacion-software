namespace SistemaGestionTareas.Services;

/// <summary>Datos inmutables de una modificación guardada correctamente.</summary>
public sealed class TareaCambiadaEventArgs(Guid id, string operacion) : EventArgs
{
    public Guid Id { get; } = id;
    public string Operacion { get; } = operacion;
}
