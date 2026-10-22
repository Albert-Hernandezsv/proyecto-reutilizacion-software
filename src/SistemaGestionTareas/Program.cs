using System.Text;
using SistemaGestionTareas.Controllers;
using SistemaGestionTareas.Factories;
using SistemaGestionTareas.Interfaces;
using SistemaGestionTareas.Services;
using SistemaGestionTareas.Storage;
using SistemaGestionTareas.Views;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
var vista = new ConsolaView();
try
{
    int seleccion = vista.SeleccionarAlmacenamiento();
    // Parte de la ubicación del ejecutable, no del directorio de la terminal.
    var directorio = new DirectoryInfo(AppContext.BaseDirectory);
    while (directorio.Parent is not null && !Directory.Exists(Path.Combine(directorio.FullName, "src", "SistemaGestionTareas")))
        directorio = directorio.Parent;
    var raiz = Directory.Exists(Path.Combine(directorio.FullName, "src", "SistemaGestionTareas"))
        ? directorio.FullName : AppContext.BaseDirectory;
    var ruta = Path.Combine(raiz, "data", "tareas.json");
    IAlmacenamientoTareas almacenamiento = seleccion == 1
        ? new AlmacenamientoMemoria() : new AlmacenamientoArchivo(ruta);
    vista.MostrarMensaje(seleccion == 1 ? "Modo memoria: los datos se perderán al salir." : $"Archivo de datos: {ruta}");
    var gestor = new GestorDeTareas(almacenamiento, new FabricaDeTareas());
    using var notificador = new NotificadorDeTareas(gestor, vista);
    new TareasController(gestor, new FiltradoDeTareas(), vista).Ejecutar();
}
catch (EndOfStreamException) { }
vista.MostrarMensaje("Hasta pronto.");
