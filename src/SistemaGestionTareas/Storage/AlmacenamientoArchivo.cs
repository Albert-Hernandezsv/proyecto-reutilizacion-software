using System.Text.Json;
using SistemaGestionTareas.Interfaces;
using SistemaGestionTareas.Models;

namespace SistemaGestionTareas.Storage;

/// <summary>Persiste tareas en JSON sin ocultar errores ni sobrescribir archivos corruptos al leer.</summary>
public sealed class AlmacenamientoArchivo : IAlmacenamientoTareas
{
    private readonly string ruta;
    private readonly JsonSerializerOptions opciones = new() { WriteIndented = true, RespectRequiredConstructorParameters = true };

    /// <summary>Recibe la ubicación del archivo desde el punto de composición.</summary>
    public AlmacenamientoArchivo(string ruta) => this.ruta = Path.GetFullPath(ruta);

    /// <inheritdoc/>
    public List<Tarea> ObtenerTodas()
    {
        string contenido;
        try { contenido = File.ReadAllText(ruta); }
        catch (FileNotFoundException) { return []; }
        catch (DirectoryNotFoundException) { return []; }
        if (string.IsNullOrWhiteSpace(contenido)) return [];
        try
        {
            var tareas = JsonSerializer.Deserialize<List<Tarea>>(contenido, opciones)
                ?? throw new JsonException("Se esperaba una lista de tareas, no null.");
            if (tareas.Any(t => t is null) || tareas.Select(t => t.Id).Distinct().Count() != tareas.Count)
                throw new JsonException("Hay tareas nulas o identificadores duplicados.");
            return tareas;
        }
        catch (ArgumentException error)
        {
            throw new JsonException("El archivo contiene una tarea con datos inválidos.", error);
        }
    }

    /// <inheritdoc/>
    public void GuardarTodas(IEnumerable<Tarea> tareas)
    {
        var contenido = JsonSerializer.Serialize(tareas, opciones);
        Directory.CreateDirectory(Path.GetDirectoryName(ruta)!);
        var temporal = ruta + "." + Guid.NewGuid().ToString("N") + ".tmp";
        // Se escribe primero un archivo completo; un fallo no trunca el JSON anterior.
        File.WriteAllText(temporal, contenido);
        File.Move(temporal, ruta, overwrite: true);
    }
}
