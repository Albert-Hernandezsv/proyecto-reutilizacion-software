using SistemaGestionTareas.Services;

namespace SistemaGestionTareas.Views;

/// <summary>Suscriptor Observer que adapta las notificaciones a la presentación.</summary>
public sealed class NotificadorDeTareas : IDisposable
{
    private readonly GestorDeTareas gestor;
    private readonly ConsolaView vista;

    /// <summary>Se suscribe a los cambios del gestor.</summary>
    public NotificadorDeTareas(GestorDeTareas gestor, ConsolaView vista)
    {
        this.gestor = gestor;
        this.vista = vista;
        gestor.TareaCambiada += Informar;
    }

    private void Informar(object? emisor, TareaCambiadaEventArgs cambio) =>
        vista.MostrarMensaje($"Éxito: tarea {cambio.Id} {cambio.Operacion}.");

    /// <summary>Libera la suscripción al terminar la sesión.</summary>
    public void Dispose() => gestor.TareaCambiada -= Informar;
}
