using System.Text.Json;
using SistemaGestionTareas.Services;
using SistemaGestionTareas.Views;

namespace SistemaGestionTareas.Controllers;

/// <summary>Coordina entradas, servicios y resultados sin implementar reglas de persistencia.</summary>
public sealed class TareasController(GestorDeTareas gestor, FiltradoDeTareas filtros, ConsolaView vista)
{
    /// <summary>Procesa operaciones hasta que se seleccione salir o finalice la entrada.</summary>
    public void Ejecutar()
    {
        while (true)
        {
            try
            {
                int opcion = vista.MostrarMenu();
                if (opcion == 0) return;
                EjecutarOperacion(opcion);
            }
            catch (EndOfStreamException) { return; }
            catch (Exception error) when (error is ArgumentException or KeyNotFoundException)
            {
                vista.MostrarMensaje("Error: " + error.Message);
            }
            catch (Exception error) when (error is IOException or UnauthorizedAccessException or JsonException)
            {
                vista.MostrarMensaje("Error de almacenamiento. La operación no pudo completarse: " + error.Message);
            }
        }
    }

    private void EjecutarOperacion(int opcion)
    {
        switch (opcion)
        {
            case 1:
                var datos = LeerDatos();
                gestor.AgregarTarea(datos.Titulo, datos.Descripcion, datos.Fecha);
                break;
            case 2:
                vista.MostrarTareas(gestor.ObtenerTareas());
                break;
            case 3:
                vista.MostrarTareas([gestor.ObtenerTareaPorId(vista.LeerId())]);
                break;
            case 4:
                var tarea = gestor.ObtenerTareaPorId(vista.LeerId());
                vista.MostrarTareas([tarea]);
                vista.MostrarMensaje("Ingrese los nuevos datos de la tarea.");
                var nuevos = LeerDatos();
                gestor.EditarTarea(tarea.Id, nuevos.Titulo, nuevos.Descripcion, nuevos.Fecha);
                break;
            case 5:
                var eliminar = gestor.ObtenerTareaPorId(vista.LeerId());
                vista.MostrarTareas([eliminar]);
                if (vista.Confirmar("¿Desea eliminar esta tarea?")) gestor.EliminarTarea(eliminar.Id);
                else vista.MostrarMensaje("Eliminación cancelada.");
                break;
            case 6:
                var cambiar = gestor.ObtenerTareaPorId(vista.LeerId());
                gestor.CambiarEstado(cambiar.Id, vista.LeerEstado());
                break;
            case 7:
                var fecha = vista.LeerFecha();
                vista.MostrarTareas(filtros.FiltrarPorFecha(gestor.ObtenerTareas(), fecha));
                break;
            case 8:
                var estado = vista.LeerEstado();
                vista.MostrarTareas(filtros.FiltrarPorEstado(gestor.ObtenerTareas(), estado));
                break;
        }
    }

    private (string Titulo, string Descripcion, DateTime Fecha) LeerDatos() =>
        (vista.LeerTexto("Título"), vista.LeerTexto("Descripción"), vista.LeerFecha());
}
