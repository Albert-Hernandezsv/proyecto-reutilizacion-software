# Sistema de Gestión de Tareas con Componentes Modulares
**Integrantes:** 
- Albert Uziel Hernandez Mendoza - HM20019
- Franklin Giovanny Avila González- AG22046

Proyecto académico de **Desarrollo y Reutilización de Software**, Universidad de El Salvador. Aplicación de consola en C# que demuestra orientación a objetos, composición, reutilización y separación de responsabilidades.

## Objetivos

- Administrar tareas mediante componentes pequeños y reutilizables.
- Separar presentación, coordinación, reglas de dominio y persistencia.
- Aplicar MVC, fábrica simple, Strategy y Observer de forma funcional.

## Funcionalidades

Agregar, listar, consultar por Guid, editar y eliminar tareas con confirmación; marcar como pendientes o completadas; filtrar por día exacto de fecha límite o por estado. Cada tarea conserva su identificador durante la edición. Se validan títulos vacíos, fechas, opciones e identificadores. La descripción puede estar vacía y se permiten fechas pasadas.

## Tecnologías y requisitos

C#, **.NET 10 LTS**, `System.Text.Json`, eventos y delegados de C#. No se requieren paquetes NuGet externos ni bases de datos.

Instale el **SDK de .NET 10** desde [Microsoft](https://dotnet.microsoft.com/download/dotnet/10.0). Compruebe su disponibilidad con `dotnet --list-sdks`. Puede usar la CLI o abrir la solución en una versión de Visual Studio compatible con .NET 10.

## Compilar y ejecutar

Desde la raíz del repositorio:

```sh
dotnet build proyecto-reutilizacion-software.sln
dotnet run --project src/SistemaGestionTareas/SistemaGestionTareas.csproj
```

## Estructura del repositorio

```text
proyecto-reutilizacion-software.sln
README.md
docs/
  01-Analisis-y-Diseno.md
  02-Componentes-y-Patrones.md
src/SistemaGestionTareas/
  SistemaGestionTareas.csproj
  Program.cs
  Models/Tarea.cs
  Controllers/TareasController.cs
  Views/ConsolaView.cs
  Views/NotificadorDeTareas.cs
  Services/GestorDeTareas.cs
  Services/FiltradoDeTareas.cs
  Services/TareaCambiadaEventArgs.cs
  Factories/FabricaDeTareas.cs
  Interfaces/IAlmacenamientoTareas.cs
  Storage/AlmacenamientoMemoria.cs
  Storage/AlmacenamientoArchivo.cs
data/                             # Se crea al guardar en JSON
```

## Componentes

| Componente | Responsabilidad |
|---|---|
| `Tarea` | Encapsula identidad, datos y estado; valida mediante `Editar` y controla la completitud con `CambiarEstado`. |
| `GestorDeTareas` | Centraliza CRUD y estado; recibe almacenamiento y fábrica por constructor. |
| `FiltradoDeTareas` | Filtra colecciones por fecha o completitud sin acceder a consola o archivos. |
| `FabricaDeTareas` | Crea tareas con `Guid.NewGuid()` y estado pendiente. |
| `IAlmacenamientoTareas` | Contrato `ObtenerTodas` y `GuardarTodas`. |
| `AlmacenamientoMemoria` | Guarda copias independientes durante la sesión. |
| `AlmacenamientoArchivo` | Lee y escribe JSON conservando identificadores y estados. |
| `TareasController` | Coordina las opciones y comunica errores a la vista. |
| `ConsolaView` | Menús, entradas y resultados en español. |
| `NotificadorDeTareas` | Suscriptor que informa de cambios guardados. |
| `TareaCambiadaEventArgs` | Identificador y operación del evento. |
| `Program` | Compone dependencias y selecciona la estrategia inicial. |

## Patrones y orientación a objetos

- **MVC adaptado a consola:** modelo y servicios independientes de la consola; vista para entradas/salidas; controlador para coordinar.
- **Fábrica simple:** `FabricaDeTareas.CrearTarea` centraliza la creación. No es Factory Method de GoF.
- **Strategy:** las dos implementaciones de `IAlmacenamientoTareas` son intercambiables; se elige una al iniciar.
- **Observer:** `GestorDeTareas.TareaCambiada` utiliza el delegado `EventHandler<TareaCambiadaEventArgs>`; `NotificadorDeTareas` se suscribe y se desuscribe al finalizar. Se notifica al agregar, editar, eliminar y establecer el estado, después de guardar correctamente.

No se utiliza Singleton porque no existe una necesidad de instancia global. La interfaz de almacenamiento permite abstracción, polimorfismo e inversión de dependencias; la composición y las responsabilidades específicas mantienen bajo acoplamiento y alta cohesión. `Id` solo tiene lectura y los demás datos se modifican mediante métodos validados. Las copias evitan cambiar la memoria persistida sin pasar por el gestor.

## Almacenamiento

Al iniciar seleccione **1. Memoria** o **2. Archivo JSON**. La selección permanece durante la sesión.

En memoria, los datos desaparecen al cerrar. En JSON se utiliza `data/tareas.json` en la raíz del repositorio, localizada desde la carpeta del ejecutable; no depende de la carpeta actual de la terminal. Si se distribuye el ejecutable fuera del repositorio, se utiliza `data/tareas.json` junto al ejecutable. La ruta efectiva se muestra al iniciar.

La carpeta se crea en el primer guardado. Un archivo inexistente, vacío o con espacios representa una colección vacía. Un JSON corrupto o con datos inválidos produce un mensaje de error y no se reemplaza silenciosamente. Corrija o respalde ese archivo antes de continuar. Los errores de acceso se muestran al usuario. El guardado escribe primero un archivo temporal y después reemplaza el destino; un fallo puede dejar un `.tmp` para revisar. Los datos locales no se incluyen en Git. La aplicación está prevista para una sola instancia, sin edición concurrente del archivo.

## Ejemplo de uso

1. Seleccione `2` para trabajar con JSON.
2. Seleccione `1`, escriba `Preparar exposición`, una descripción y `30/11/2026`.
3. Seleccione `2` para listar y copiar el Guid completo.
4. Seleccione `3` para consultar, o `4` para introducir nuevos datos, usando ese Guid.
5. Seleccione `6`, pegue el Guid y elija `2` para marcar completada.
6. Seleccione `7` e ingrese `30/11/2026`, o `8` y el estado deseado.
7. Salga con `0`; vuelva a ejecutar en modo JSON y compruebe que se conserva la tarea.
8. Para eliminarla, use `5`, pegue su Guid y confirme con `s`; `n` cancela.

Las fechas usan `dd/MM/yyyy`; el filtro compara el día sin la hora. Tras cada operación se vuelve al menú. Una entrada de formato inválido vuelve a solicitarse; un error del dominio devuelve al menú. El fin de entrada cierra la sesión sin un bucle infinito.

## Documentación de diseño

- [Análisis y diseño](docs/01-Analisis-y-Diseno.md).
- [Componentes, patrones y reutilización](docs/02-Componentes-y-Patrones.md).
