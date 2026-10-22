using System.Globalization;
using SistemaGestionTareas.Models;

namespace SistemaGestionTareas.Views;

/// <summary>Presentación y lectura segura de entradas en español.</summary>
public sealed class ConsolaView
{
    /// <summary>Muestra un mensaje al usuario.</summary>
    public void MostrarMensaje(string mensaje) => Console.WriteLine(mensaje);

    /// <summary>Lee una línea; el fin de entrada termina la sesión de forma controlada.</summary>
    public string LeerTexto(string etiqueta)
    {
        Console.Write(etiqueta + ": ");
        return Console.ReadLine() ?? throw new EndOfStreamException();
    }

    /// <summary>Solicita una opción dentro de un intervalo.</summary>
    public int LeerOpcion(int minimo, int maximo)
    {
        while (true)
        {
            if (int.TryParse(LeerTexto("Seleccione una opción"), out int opcion)
                && opcion >= minimo && opcion <= maximo) return opcion;
            MostrarMensaje($"Ingrese un número entre {minimo} y {maximo}.");
        }
    }

    /// <summary>Presenta la selección inicial de estrategia.</summary>
    public int SeleccionarAlmacenamiento()
    {
        MostrarMensaje("SELECCIONE EL TIPO DE ALMACENAMIENTO\n1. Memoria (temporal)\n2. Archivo JSON (persistente)");
        return LeerOpcion(1, 2);
    }

    /// <summary>Presenta el menú principal.</summary>
    public int MostrarMenu()
    {
        MostrarMensaje("\n========================================\n       SISTEMA DE GESTIÓN DE TAREAS\n========================================\n1. Agregar tarea\n2. Listar tareas\n3. Consultar tarea\n4. Editar tarea\n5. Eliminar tarea\n6. Cambiar estado de tarea\n7. Filtrar tareas por fecha\n8. Filtrar tareas por estado\n0. Salir");
        return LeerOpcion(0, 8);
    }

    /// <summary>Lee una fecha exacta en formato día/mes/año.</summary>
    public DateTime LeerFecha()
    {
        while (true)
        {
            if (DateTime.TryParseExact(LeerTexto("Fecha límite (dd/MM/yyyy)"), "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha)) return fecha;
            MostrarMensaje("Fecha inválida. Ejemplo: 25/12/2026.");
        }
    }

    /// <summary>Lee un identificador completo mostrado en los listados.</summary>
    public Guid LeerId()
    {
        while (true)
        {
            if (Guid.TryParse(LeerTexto("Identificador de la tarea"), out var id)) return id;
            MostrarMensaje("Identificador inválido. Copie el identificador completo del listado.");
        }
    }

    /// <summary>Solicita una respuesta explícita de sí o no.</summary>
    public bool Confirmar(string pregunta)
    {
        while (true)
        {
            var respuesta = LeerTexto(pregunta + " (s/n)").Trim().ToLowerInvariant();
            if (respuesta == "s") return true;
            if (respuesta == "n") return false;
            MostrarMensaje("Responda s o n.");
        }
    }

    /// <summary>Permite elegir uno de los dos estados.</summary>
    public bool LeerEstado()
    {
        MostrarMensaje("1. Pendiente\n2. Completada");
        return LeerOpcion(1, 2) == 2;
    }

    /// <summary>Presenta cada tarea con un identificador completo que puede copiarse.</summary>
    public void MostrarTareas(IEnumerable<Tarea> tareas)
    {
        bool hayTareas = false;
        foreach (var tarea in tareas)
        {
            hayTareas = true;
            MostrarMensaje($"\nId: {tarea.Id}\nTítulo: {tarea.Titulo}\nDescripción: {tarea.Descripcion}\nFecha límite: {tarea.FechaLimite:dd/MM/yyyy}\nEstado: {(tarea.Completada ? "Completada" : "Pendiente")}\n----------------------------------------");
        }
        if (!hayTareas) MostrarMensaje("No hay tareas para mostrar.");
    }
}
