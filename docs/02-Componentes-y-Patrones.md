# Sistema de Gestión de Tareas con Componentes Modulares
## Documento de Componentes, Patrones de Diseño y Reutilización

**Universidad:** Universidad de El Salvador  
**Facultad:** Facultad Multidisciplinaria de Occidente  
**Carrera:** Ingeniería en Desarrollo de Software  
**Asignatura:** Desarrollo y Reutilización de Software  
**Proyecto:** Sistema de Gestión de Tareas con Componentes Modulares  
**Lenguaje de programación:** C#  
**Plataforma:** .NET  
**Tipo de aplicación:** Consola

---

## 1. Introducción

El desarrollo de software reutilizable requiere organizar las aplicaciones mediante componentes que posean responsabilidades definidas y mecanismos de comunicación claros, esta organización permite reducir la duplicación de código, facilitar el mantenimiento y adaptar las funcionalidades a diferentes necesidades sin modificar completamente la estructura de un sistema.

El presente documento describe los componentes y patrones de diseño elegidos para el Sistema de Gestión de Tareas, desarrollado en C# mediante una aplicación de consola, el sistema permitirá registrar, consultar, modificar, eliminar y filtrar tareas, utilizando componentes independientes para gestionar las operaciones, la presentación y el almacenamiento de información.

Para su construcción se utilizarán principios de programación orientada a objetos, interfaces y patrones de diseño que favorezcan la modularidad y la reutilización. Se propone una organización basada en Modelo-Vista-Controlador (MVC), complementada con Factory para la creación de tareas, Strategy para el almacenamiento intercambiable y Observer para la comunicación de cambios mediante eventos.

## 2. Organización modular del sistema

La aplicación se dividirá en módulos que agrupen funcionalidades relacionadas. Cada módulo tendrá una responsabilidad específica y se comunicará con los demás mediante métodos e interfaces definidos.

Esta separación permitirá que las modificaciones realizadas en una funcionalidad tengan el menor impacto posible sobre el resto de la aplicación.

Se propone la siguiente organización:

| Módulo | Responsabilidad |
|---|---|
| Modelos | Representar los datos y el estado de las tareas. |
| Servicios | Gestionar las operaciones y aplicar los filtros. |
| Fábricas | Centralizar la creación de objetos. |
| Almacenamiento | Guardar y recuperar las tareas. |
| Interfaces | Definir contratos para los componentes intercambiables. |
| Controladores | Coordinar las operaciones solicitadas por el usuario. |
| Vistas | Presentar información y recibir entradas mediante consola. |

La estructura busca mantener una alta cohesión dentro de cada módulo y un bajo acoplamiento entre componentes.

## 3. Descripción de los componentes

### 3.1. Componente Tarea

**Clase:** `Tarea`

**Responsabilidad:** Representar una actividad registrada dentro del sistema.

Será la entidad principal de la aplicación y contendrá la información necesaria para identificar y administrar una tarea.

**Atributos propuestos:**

| Atributo | Tipo de dato | Descripción |
|---|---|---|
| Id | Guid | Identificador único de la tarea. |
| Titulo | string | Nombre de la actividad. |
| Descripcion | string | Información adicional sobre la tarea. |
| FechaLimite | DateTime | Fecha establecida para realizar la actividad. |
| Completada | bool | Indica si la tarea se encuentra completada. |

El componente utilizará propiedades y métodos que permitan controlar el estado de la tarea. Se evitará exponer directamente los detalles internos que no deban ser modificados por otros componentes.

**Reutilización:** La clase podrá utilizarse en otras aplicaciones que necesiten representar tareas o actividades con características similares, sin depender de una interfaz de usuario específica.

### 3.2. Componente GestorDeTareas

**Clase:** `GestorDeTareas`

**Responsabilidad:** Coordinar las operaciones principales de administración de tareas.

Será el componente encargado de ejecutar las operaciones solicitadas por el controlador y utilizar los servicios necesarios para manipular la información.

**Métodos implementados:**

| Método | Descripción |
|---|---|
| AgregarTarea() | Registra una nueva tarea. |
| ObtenerTareas() | Recupera las tareas disponibles. |
| ObtenerTareaPorId() | Busca una tarea mediante su identificador. |
| EditarTarea() | Actualiza los datos de una tarea existente. |
| EliminarTarea() | Elimina una tarea registrada. |
| CambiarEstado() | Modifica el estado de completitud de una tarea. |

El gestor utilizará el contrato de almacenamiento definido por el sistema, evitando depender directamente de una implementación específica.

También podrá generar notificaciones cuando se realicen modificaciones, permitiendo que otros componentes respondan a dichos cambios.

**Reutilización:** La lógica de administración podrá utilizarse en aplicaciones de consola, escritorio o web, siempre que se proporcionen las dependencias necesarias.

### 3.3. Componente FiltradoDeTareas

**Clase:** `FiltradoDeTareas`

**Responsabilidad:** Aplicar criterios de filtrado sobre una colección de tareas.

Este componente permitirá separar las operaciones de búsqueda y filtrado de la lógica principal de administración.

**Métodos implementados:**

| Método | Descripción |
|---|---|
| FiltrarPorFecha() | Devuelve las tareas cuya fecha límite coincide con la fecha indicada. |
| FiltrarPorEstado() | Devuelve las tareas pendientes o completadas. |

Los métodos recibirán una colección de tareas y devolverán los elementos que cumplan con el criterio solicitado.

El componente no será responsable de guardar información ni de presentar resultados en consola.

**Reutilización:** Podrá utilizarse desde diferentes controladores o interfaces que necesiten consultar tareas, evitando duplicar las condiciones de filtrado.

### 3.4. Componente FabricaDeTareas

**Clase:** `FabricaDeTareas`

**Responsabilidad:** Centralizar la creación de instancias de la clase `Tarea`.

Este componente aplicará una solución de tipo Factory, evitando distribuir la lógica de construcción de tareas entre diferentes partes de la aplicación.

**Método implementado:**

`CrearTarea(titulo, descripcion, fechaLimite)`

El método recibirá los datos necesarios, generará un identificador único y devolverá una nueva instancia de `Tarea` con estado inicial pendiente.

**Reutilización:** Permitirá utilizar un mecanismo común para construir tareas y facilitará la incorporación de reglas adicionales de creación sin duplicarlas en los componentes consumidores.

### 3.5. Interfaz IAlmacenamientoTareas

**Interfaz:** `IAlmacenamientoTareas`

**Responsabilidad:** Definir las operaciones comunes para guardar y recuperar tareas.

Se propone un contrato que permita trabajar con diferentes mecanismos de almacenamiento.

**Operaciones implementadas:**

- `ObtenerTodas()`: recupera las tareas almacenadas.
- `GuardarTodas(tareas)`: almacena una colección de tareas.

El gestor utilizará esta interfaz sin conocer los detalles de la implementación concreta.

**Reutilización:** El contrato permitirá incorporar otros mecanismos de persistencia, como una base de datos, manteniendo la misma forma de interacción desde la lógica de gestión.

### 3.6. Componente AlmacenamientoMemoria

**Clase:** `AlmacenamientoMemoria`

**Responsabilidad:** Mantener temporalmente las tareas durante la ejecución del programa.

Implementará la interfaz `IAlmacenamientoTareas` y utilizará una colección de objetos para conservar la información.

Los datos permanecerán disponibles mientras la instancia de almacenamiento continúe en uso, pero no se conservarán automáticamente al finalizar la aplicación.

**Reutilización:** Este componente podrá utilizarse en aplicaciones pequeñas, demostraciones o escenarios que no requieran persistencia permanente.

### 3.7. Componente AlmacenamientoArchivo

**Clase:** `AlmacenamientoArchivo`

**Responsabilidad:** Guardar y recuperar tareas mediante un archivo local.

Implementará la interfaz `IAlmacenamientoTareas` y utilizará el formato JSON para representar la información.

Se utilizarán las funcionalidades de serialización y deserialización proporcionadas por .NET.

Este componente encapsulará las operaciones de lectura y escritura, evitando que el gestor de tareas tenga que conocer la ubicación o estructura interna del archivo.

**Reutilización:** Podrá utilizarse en otras aplicaciones que necesiten almacenar colecciones de tareas mediante archivos JSON.

### 3.8. Componente TareasController

**Clase:** `TareasController`

**Responsabilidad:** Coordinar la comunicación entre la vista y los componentes de gestión.

Recibirá las solicitudes del usuario y ejecutará las operaciones correspondientes mediante el gestor y los servicios de filtrado.

El controlador no implementará directamente las reglas de almacenamiento ni la construcción de tareas.

**Reutilización:** La separación del controlador facilitará modificar la forma de presentación sin trasladar las reglas de negocio a la interfaz.

### 3.9. Componente ConsolaView

**Clase:** `ConsolaView`

**Responsabilidad:** Proporcionar la interfaz de interacción con el usuario.

Mostrará el menú principal, solicitará información y presentará los resultados de las operaciones.

La vista se encargará de la entrada y salida de datos mediante consola, evitando contener las reglas de administración o persistencia.

**Reutilización:** Aunque este componente estará vinculado a la consola, podrá modificarse o sustituirse sin necesidad de reescribir los servicios principales.

## 4. Patrones de diseño

### 4.1. Patrón Modelo-Vista-Controlador (MVC)

MVC permite dividir una aplicación en tres responsabilidades principales: modelo, vista y controlador.

En el sistema propuesto, el modelo estará representado por la entidad `Tarea` y los componentes relacionados con la lógica de gestión. La vista estará implementada mediante `ConsolaView`, mientras que `TareasController` coordinará las solicitudes del usuario.

**Aplicación prevista:**

1. El usuario selecciona una operación desde el menú.
2. La vista obtiene los datos necesarios.
3. El controlador recibe la solicitud.
4. El controlador utiliza los componentes correspondientes.
5. La vista presenta el resultado al usuario.

**Beneficios esperados:**

- Separación entre presentación y lógica de negocio.
- Mayor facilidad para modificar la interfaz.
- Mejor organización del código.
- Posibilidad de reutilizar los componentes de gestión.

### 4.2. Patrón Factory

Factory es un patrón creacional que permite centralizar la construcción de objetos.

En el proyecto se utilizará una clase fábrica encargada de crear instancias de `Tarea`, estableciendo un procedimiento común para asignar sus valores iniciales.

**Aplicación prevista:**

Cuando el usuario solicite registrar una tarea, el gestor utilizará `FabricaDeTareas` para construir el objeto correspondiente.

La fábrica asignará los datos proporcionados, generará el identificador y establecerá el estado inicial de la tarea.

**Beneficios esperados:**

- Centralización de la lógica de creación.
- Reducción de código repetido.
- Consistencia en la inicialización de objetos.
- Facilidad para modificar las reglas de construcción.

La solución propuesta corresponde a una fábrica simple. No se considerará automáticamente una implementación del patrón Factory Method de GoF, ya que este último utiliza una estructura específica basada en métodos de creación extensibles mediante subclases.

### 4.3. Patrón Strategy

Strategy permite definir comportamientos alternativos que pueden utilizarse de manera intercambiable mediante un contrato común.

En el sistema se propone aplicar este principio a los mecanismos de almacenamiento.

La interfaz `IAlmacenamientoTareas` definirá las operaciones requeridas, mientras que `AlmacenamientoMemoria` y `AlmacenamientoArchivo` proporcionarán las implementaciones correspondientes.

**Aplicación prevista:**

El gestor recibirá una implementación de `IAlmacenamientoTareas` y utilizará sus métodos sin necesitar conocer si los datos se conservan temporalmente o se guardan en un archivo.

**Beneficios esperados:**

- Sustitución de implementaciones sin modificar la lógica principal.
- Menor dependencia entre componentes.
- Facilidad para agregar nuevos mecanismos de almacenamiento.
- Mayor flexibilidad en la configuración del sistema.

### 4.4. Patrón Observer

Observer permite establecer un mecanismo de notificación entre un objeto que genera cambios y otros componentes interesados en recibir información sobre dichos cambios.

Se propone utilizar eventos de C# para comunicar modificaciones realizadas sobre las tareas.

**Aplicación prevista:**

El componente `GestorDeTareas` emite `TareaCambiada` al registrar, editar, eliminar o establecer el estado de una tarea, después de guardar correctamente. Utiliza el delegado `EventHandler<TareaCambiadaEventArgs>`. El suscriptor `NotificadorDeTareas` informa mediante `ConsolaView` y libera su suscripción con `Dispose`.

**Beneficios esperados:**

- Comunicación desacoplada mediante eventos.
- Posibilidad de incorporar nuevos receptores.
- Separación entre la operación principal y las acciones posteriores.
- Mayor flexibilidad para ampliar las funcionalidades.

## 5. Aplicación de principios de programación orientada a objetos

### 5.1. Abstracción

Se aplicará mediante clases e interfaces que representen las características esenciales de cada componente.

Por ejemplo, `IAlmacenamientoTareas` permitirá utilizar operaciones de almacenamiento sin conocer los detalles internos de las implementaciones.

### 5.2. Encapsulamiento

Los componentes controlarán el acceso a sus datos y operaciones internas mediante propiedades, métodos y modificadores de acceso.

La clase `Tarea` mantendrá la información relacionada con cada actividad, mientras que los componentes especializados administrarán sus respectivas responsabilidades.

### 5.3. Polimorfismo

Se aplicará mediante las implementaciones de `IAlmacenamientoTareas`.

El gestor podrá utilizar diferentes clases de almacenamiento a través del mismo contrato, manteniendo una forma común de interacción.

### 5.4. Composición

La composición permitirá integrar componentes mediante referencias a otros objetos.

Por ejemplo, `GestorDeTareas` utilizará una implementación de almacenamiento y la fábrica de tareas para realizar sus operaciones.

Esta estrategia evitará crear jerarquías de herencia innecesarias entre componentes que poseen responsabilidades diferentes.

## 6. Principios de diseño y modularidad

### 6.1. Responsabilidad única

Cada componente tendrá una responsabilidad principal.

La creación de tareas corresponderá a la fábrica, las operaciones de administración al gestor, los filtros al componente de filtrado y la persistencia a las implementaciones de almacenamiento.

### 6.2. Bajo acoplamiento

Se buscará reducir las dependencias directas entre componentes.

El gestor utilizará una interfaz para acceder al almacenamiento, evitando depender de una clase concreta.

De esta manera, los cambios realizados en un mecanismo de persistencia no deberían requerir modificaciones en las operaciones de administración, siempre que se mantenga el contrato establecido.

### 6.3. Alta cohesión

Las funcionalidades relacionadas permanecerán agrupadas dentro de sus componentes correspondientes.

Por ejemplo, las operaciones de filtrado se concentrarán en `FiltradoDeTareas`, evitando distribuirlas entre el controlador, la vista y el gestor.

### 6.4. Abierto/Cerrado

Se buscará que los componentes puedan extenderse mediante nuevas implementaciones sin modificar innecesariamente el código existente.

Un ejemplo será la posibilidad de incorporar `AlmacenamientoBaseDatos` como una nueva implementación de `IAlmacenamientoTareas`.

### 6.5. Inversión de dependencias

La lógica principal dependerá de abstracciones para las funcionalidades que requieran implementaciones intercambiables.

Esto se reflejará especialmente en la relación entre `GestorDeTareas` y `IAlmacenamientoTareas`.

## 7. Interacción entre los componentes

### 7.1. Registro de una tarea

El proceso de registro seguirá la siguiente secuencia:

1. El usuario selecciona la opción de agregar una tarea.
2. `ConsolaView` solicita los datos correspondientes.
3. `TareasController` recibe la solicitud.
4. `GestorDeTareas` coordina el registro.
5. `FabricaDeTareas` construye la nueva instancia de `Tarea`.
6. El gestor utiliza `IAlmacenamientoTareas` para guardar la colección actualizada.
7. El gestor emite una notificación de cambio después de guardar correctamente.
8. La vista presenta el resultado de la operación.

### 7.2. Consulta y filtrado

El usuario podrá solicitar el listado completo de tareas o aplicar un criterio de filtrado.

El controlador obtendrá las tareas mediante el gestor y utilizará `FiltradoDeTareas` cuando se requiera seleccionar elementos por fecha o estado.

Los resultados se enviarán a la vista para su presentación.

### 7.3. Modificación y eliminación

Para modificar o eliminar una tarea, el controlador recibirá el identificador proporcionado por el usuario y solicitará la operación al gestor.

El gestor comprobará la existencia de la tarea, realizará la modificación correspondiente y actualizará el almacenamiento.

Cuando se complete la operación, podrá emitir una notificación para informar sobre el cambio realizado.

## 8. Estrategia de reutilización del código

La reutilización se realizará principalmente mediante componentes especializados e interfaces que permitan separar la lógica de negocio de los mecanismos de presentación y almacenamiento.

**Reutilización de clases:** La entidad `Tarea` y los componentes de gestión y filtrado podrán aprovecharse en otros proyectos que necesiten administrar actividades.

**Reutilización mediante interfaces:** El contrato `IAlmacenamientoTareas` permitirá utilizar diferentes mecanismos de persistencia sin modificar la forma en que los componentes consumidores acceden a los datos.

**Reutilización mediante patrones:** Factory, MVC, Strategy y Observer proporcionarán soluciones estructurales y de comportamiento que podrán adaptarse a otros sistemas.

**Reutilización mediante composición:** Los componentes podrán combinarse para construir nuevas funcionalidades sin necesidad de duplicar su implementación.

La arquitectura propuesta permitirá que la lógica principal del sistema se mantenga independiente de la interfaz de consola. Esto facilitará utilizar los mismos servicios en una aplicación con otra forma de presentación.

## 9. Extensibilidad del sistema

La organización modular permitirá incorporar nuevas funcionalidades conforme evolucionen las necesidades del proyecto.

Entre las posibles ampliaciones se encuentran:

- Incorporar almacenamiento mediante una base de datos.
- Agregar filtros por prioridad o palabras clave.
- Implementar categorías para organizar las tareas.
- Incorporar una interfaz gráfica o web.
- Agregar componentes que reciban notificaciones sobre cambios en las tareas.

Estas ampliaciones requerirían desarrollar o adaptar componentes específicos, procurando conservar los contratos existentes y evitando modificaciones innecesarias en el resto de la aplicación.

## 10. Consideraciones de implementación

La implementación usa .NET 10 y System.Text.Json. `Tarea` expone `Id` de solo lectura y los métodos `Editar`, `CambiarEstado` y `Copiar`; su constructor JSON conserva identidad y estado al recuperar datos. El filtro por fecha compara el día exacto sin hora. La estrategia de archivo usa `data/tareas.json` en la raíz del repositorio (o junto al ejecutable distribuido), admite archivos inexistentes o vacíos y comunica errores de lectura, validación y escritura. La estrategia en memoria devuelve copias independientes. `Program` compone las dependencias y selecciona el almacenamiento al iniciar. No se utiliza Singleton porque no existe una necesidad técnica real.

Durante el desarrollo se procurará mantener una estructura clara de carpetas y archivos, utilizando nombres descriptivos y las convenciones habituales del lenguaje C#.

Se evitará concentrar las funcionalidades en una única clase y se utilizarán interfaces cuando aporten flexibilidad o reduzcan dependencias.

Los métodos se diseñarán con responsabilidades delimitadas y se incorporarán validaciones para evitar operaciones incorrectas.

Los patrones de diseño se implementarán únicamente cuando proporcionen un beneficio concreto a la estructura del sistema, evitando introducir complejidad innecesaria.

Una vez desarrollado el proyecto, la documentación se revisará para asegurar que los nombres de clases, métodos, relaciones y patrones descritos correspondan con la implementación final.

## 11. Conclusión

El diseño modular del Sistema de Gestión de Tareas permitirá organizar sus funcionalidades mediante componentes independientes, cada uno con responsabilidades específicas y mecanismos de comunicación definidos.

La utilización de MVC contribuirá a separar la presentación de la lógica del sistema, mientras que Factory centralizará la creación de tareas. Strategy permitirá utilizar mecanismos de almacenamiento intercambiables y Observer facilitará la comunicación de cambios mediante eventos.

Asimismo, la aplicación de abstracción, encapsulamiento, polimorfismo, composición y principios de diseño orientados a objetos buscará reducir el acoplamiento y mejorar la cohesión de los componentes.

Esta estructura constituye una base para desarrollar una aplicación mantenible, extensible y reutilizable, cumpliendo con el propósito académico de aplicar patrones de diseño y principios de reutilización de software en un proyecto práctico desarrollado en C#.