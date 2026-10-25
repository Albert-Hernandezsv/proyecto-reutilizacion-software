# Sistema de Gestión de Tareas con Componentes Modulares
## Documento de Análisis y Diseño

**Universidad:** Universidad de El Salvador  
**Facultad:** Facultad Multidisciplinaria de Occidente  
**Carrera:** Ingeniería en Desarrollo de Software  
**Asignatura:** Desarrollo y Reutilización de Software  
**Proyecto:** Sistema de Gestión de Tareas con Componentes Modulares  
**Lenguaje de programación:** C#  
**Plataforma:** .NET  
**Tipo de aplicación:** Consola  
**Miembros del equipo:**  
* Albert Uziel Hernandez Mendoza - HM20019  
* Cristhoper Alexander Mendez Montes - MM22116

---

## 1. Introducción

La reutilización de software es un principio importante en el desarrollo de aplicaciones ya que permite aprovechar componentes existentes, reducir la duplicación de código y facilitar el mantenimiento de los sistemas.

El presente proyecto consiste en el diseño y desarrollo de un sistema de gestión de tareas mediante una aplicación de consola en C# y su propósito es permitir al usuario administrar tareas a través de operaciones de creación, consulta, modificación y eliminación, así como realizar búsquedas y filtrados según su fecha o estado de completitud.

El sistema se desarrollará utilizando programación orientada a objetos, componentes modulares y patrones de diseño que faciliten la reutilización del código, se utilizará el patrón Modelo-Vista-Controlador (MVC) para organizar la aplicación, el patrón Factory para centralizar la creación de tareas y mecanismos de abstracción que permitan utilizar diferentes alternativas de almacenamiento.

Con este enfoque se busca construir una aplicación funcional, organizada y adaptable, en la que los componentes puedan modificarse o reutilizarse sin afectar innecesariamente al resto del sistema.

## 2. Planteamiento del problema

La administración de tareas es una actividad común en distintos ámbitos, como el académico, laboral y personal y a medida que aumenta la cantidad de actividades pendientes, resulta necesario contar con herramientas que permitan registrar, consultar y actualizar su información de manera organizada.

Desde el punto de vista del desarrollo de software una solución implementada sin una estructura modular puede presentar dificultades de mantenimiento, por ejemplo si las operaciones de gestión, almacenamiento y visualización se encuentran concentradas en una sola clase cualquier modificación podría afectar diferentes funcionalidades del programa.

Esta situación también limita la reutilización del código ya que los componentes dependen directamente unos de otros y resulta más difícil utilizarlos en nuevas aplicaciones.

Por esta razón se propone desarrollar un sistema de gestión de tareas que además de proporcionar las operaciones necesarias para su administración aplique principios de programación orientada a objetos y patrones de diseño que favorezcan la separación de responsabilidades, la modularidad y la reutilización de componentes.

## 3. Objetivos

### 3.1. Objetivo general

Desarrollar un sistema de gestión de tareas en C#, utilizando componentes modulares y patrones de diseño que permitan administrar tareas de manera organizada, promoviendo la reutilización del código, la mantenibilidad y la escalabilidad de la aplicación.

### 3.2. Objetivos específicos

- Implementar las operaciones necesarias para agregar, consultar, editar y eliminar tareas mediante componentes independientes.
- Desarrollar mecanismos de filtrado que permitan consultar tareas según su fecha y estado de completitud.
- Aplicar el patrón Factory para centralizar la creación de objetos de tipo `Tarea`.
- Organizar la aplicación mediante el patrón MVC, separando la representación de los datos, la interacción con el usuario y la coordinación de las operaciones.
- Aplicar principios de programación orientada a objetos para reducir el acoplamiento y facilitar la reutilización de los componentes.

## 4. Alcance del proyecto

El sistema estará orientado a la administración básica de tareas mediante una interfaz de consola. Permitirá registrar actividades, consultar su información, modificar sus propiedades y eliminarlas cuando sea necesario.

Cada tarea contará con un identificador único, un título, una descripción, una fecha límite y un estado que indicará si se encuentra pendiente o completada.

El usuario podrá consultar todas las tareas registradas, filtrar los resultados según su fecha límite o estado y seleccionar el mecanismo de almacenamiento utilizado por la aplicación.

El proyecto contempla dos alternativas de almacenamiento:

- **Almacenamiento en memoria:** permitirá administrar las tareas durante la ejecución del programa, sin conservar los datos después de finalizarlo.
- **Almacenamiento en archivo:** permitirá guardar y recuperar las tareas mediante un archivo local en formato JSON, conservando su información entre ejecuciones.

La aplicación será de uso local y funcionará mediante un menú de opciones en consola. No se contempla la implementación de autenticación de usuarios, bases de datos externas, sincronización en línea ni interfaces gráficas, debido a que el propósito principal del proyecto es demostrar la aplicación de componentes reutilizables y patrones de diseño.

## 5. Requisitos del sistema

### 5.1. Requisitos funcionales

| Código | Requisito | Descripción |
|---|---|---|
| RF-01 | Registrar tareas | El sistema permitirá crear tareas proporcionando título, descripción y fecha límite. |
| RF-02 | Consultar tareas | El usuario podrá visualizar todas las tareas registradas. |
| RF-03 | Editar tareas | El sistema permitirá modificar los datos de una tarea existente. |
| RF-04 | Eliminar tareas | El usuario podrá eliminar una tarea mediante su identificador. |
| RF-05 | Actualizar estado | El sistema permitirá marcar una tarea como completada o pendiente. |
| RF-06 | Filtrar por fecha | El usuario podrá consultar tareas correspondientes a una fecha límite determinada. |
| RF-07 | Filtrar por estado | El sistema permitirá mostrar tareas pendientes o completadas. |
| RF-08 | Almacenar en memoria | El sistema permitirá trabajar con una colección de tareas almacenadas temporalmente. |
| RF-09 | Almacenar en archivo | El sistema permitirá guardar y recuperar tareas desde un archivo JSON. |
| RF-10 | Interacción por consola | El sistema ofrecerá un menú para acceder a las operaciones disponibles. |

### 5.2. Requisitos no funcionales

| Código | Requisito | Descripción |
|---|---|---|
| RNF-01 | Modularidad | Las funcionalidades estarán distribuidas en componentes con responsabilidades definidas. |
| RNF-02 | Reutilización | Los componentes de gestión, filtrado y almacenamiento podrán utilizarse en otros proyectos compatibles. |
| RNF-03 | Mantenibilidad | La organización del código facilitará modificaciones y correcciones. |
| RNF-04 | Extensibilidad | La arquitectura permitirá incorporar nuevas funcionalidades sin modificar innecesariamente los componentes existentes. |
| RNF-05 | Legibilidad | El código utilizará nombres descriptivos y convenciones de programación de C#. |
| RNF-06 | Encapsulamiento | Los componentes protegerán su estado interno y expondrán operaciones mediante interfaces y métodos definidos. |
| RNF-07 | Validación | El sistema verificará los datos ingresados para evitar operaciones inválidas. |
| RNF-08 | Portabilidad | La aplicación utilizará tecnologías compatibles con el entorno .NET seleccionado. |

## 6. Enfoque de desarrollo

El proyecto utilizará el paradigma de programación orientada a objetos (POO), organizando las funcionalidades mediante clases que representen entidades y responsabilidades específicas.

Se aplicarán los siguientes principios:

**Abstracción:** permitirá representar las características esenciales de una tarea y definir contratos para los mecanismos de almacenamiento, sin exponer detalles innecesarios de implementación.

**Encapsulamiento:** se utilizará para controlar el acceso y modificación de los datos internos de las clases, evitando que otros componentes alteren directamente su funcionamiento.

**Polimorfismo:** permitirá utilizar diferentes implementaciones de almacenamiento mediante una interfaz común, sin que los componentes que consumen dichos servicios dependan de una implementación específica.

**Composición:** se utilizará para integrar componentes independientes, como el gestor de tareas y el mecanismo de almacenamiento, evitando establecer relaciones de herencia cuando no sean necesarias.

## 7. Arquitectura del sistema

Se propone utilizar una arquitectura organizada mediante el patrón Modelo-Vista-Controlador (MVC), complementada con componentes especializados para las operaciones del sistema.

### 7.1. Modelo

Representará los datos y las reglas del dominio de la aplicación.

Incluirá la clase `Tarea`, que contendrá la información de cada actividad, así como los componentes responsables de gestionar, filtrar y almacenar las tareas.

La lógica de gestión será independiente de la interfaz de consola, permitiendo su reutilización en aplicaciones con diferentes mecanismos de presentación.

### 7.2. Vista

Será responsable de mostrar la información al usuario y recibir los datos necesarios para realizar las operaciones.

Se implementará mediante una interfaz de consola que presentará un menú principal, formularios de entrada y mensajes con los resultados de las operaciones.

La vista no implementará directamente las reglas de gestión ni el almacenamiento de las tareas.

### 7.3. Controlador

Actuará como intermediario entre la vista y los componentes del modelo.

Recibirá las acciones solicitadas por el usuario, coordinará la ejecución de las operaciones correspondientes y proporcionará los resultados necesarios para su presentación.

Esta separación permitirá modificar la interfaz sin alterar la lógica principal del sistema.

### 7.4. Componentes especializados

Además de la estructura MVC, se utilizarán componentes con responsabilidades específicas:

| Componente | Responsabilidad |
|---|---|
| `Tarea` | Representar la información y el estado de una tarea. |
| `GestorDeTareas` | Coordinar las operaciones de creación, consulta, edición y eliminación. |
| `FiltradoDeTareas` | Aplicar criterios de filtrado por fecha y completitud. |
| `FabricaDeTareas` | Centralizar la construcción de objetos de tipo `Tarea`. |
| `IAlmacenamientoTareas` | Definir el contrato para guardar y recuperar tareas. |
| `AlmacenamientoMemoria` | Gestionar las tareas mediante una colección en memoria. |
| `AlmacenamientoArchivo` | Guardar y recuperar las tareas desde un archivo JSON. |
| `TareasController` | Coordinar las solicitudes del usuario y los componentes del sistema. |
| `ConsolaView` | Presentar menús, solicitar información y mostrar resultados. |

## 8. Patrones de diseño propuestos

### 8.1. Modelo-Vista-Controlador (MVC)

Se utilizará para separar la presentación, la coordinación de operaciones y los datos del sistema.

Esta organización permitirá que la lógica de gestión de tareas no dependa directamente de la consola, facilitando el mantenimiento y la posible incorporación de otras interfaces.

### 8.2. Factory

Se utilizará para centralizar la creación de objetos de tipo `Tarea`.

La clase `FabricaDeTareas` será responsable de construir las instancias a partir de los datos necesarios, estableciendo un mecanismo común de creación que podrá reutilizarse desde diferentes partes de la aplicación.

La implementación utiliza una fábrica simple, no Factory Method de GoF. No se incorpora Singleton porque no existe una necesidad de instancia global.

### 8.3. Strategy

Se propone utilizar este patrón para permitir diferentes mecanismos de almacenamiento.

La interfaz `IAlmacenamientoTareas` establecerá las operaciones comunes, mientras que `AlmacenamientoMemoria` y `AlmacenamientoArchivo` proporcionarán implementaciones alternativas.

De esta manera, el gestor podrá trabajar con cualquiera de las dos estrategias sin depender de los detalles específicos de almacenamiento.

### 8.4. Observer

Se utiliza el patrón Observer para comunicar cambios en las tareas mediante eventos de C#.

Por ejemplo, cuando se agregue, modifique, elimine o cambie el estado de una tarea, el gestor emite una notificación después de guardar correctamente que otros componentes interesados podrán recibir.

Su implementación se mantendrá independiente de las operaciones principales, evitando que la lógica de gestión dependa directamente de los componentes que reciben las notificaciones.

## 9. Decisiones de diseño

### 9.1. Selección de C# y .NET

Se utilizará C# debido a su compatibilidad con la programación orientada a objetos y su soporte para clases, interfaces, propiedades, eventos y colecciones.

La plataforma .NET proporcionará las bibliotecas necesarias para desarrollar la aplicación, manipular colecciones y realizar operaciones de lectura y escritura de archivos.

### 9.2. Aplicación de consola

Se seleccionó una aplicación de consola debido a que permite implementar las funcionalidades solicitadas sin incorporar la complejidad adicional de una interfaz gráfica.

Esta decisión permitirá concentrar el desarrollo en los componentes reutilizables, los patrones de diseño y la separación de responsabilidades.

### 9.3. Almacenamiento mediante JSON

Se utilizará el formato JSON para el almacenamiento persistente de las tareas, debido a que permite representar los datos mediante una estructura sencilla y legible.

La serialización y deserialización se realizarán mediante las herramientas disponibles en .NET, evitando la necesidad de incorporar una base de datos para las operaciones requeridas.

### 9.4. Uso de interfaces

Se utilizarán interfaces en los componentes que requieran implementaciones intercambiables, especialmente en el almacenamiento.

Esto permitirá que la lógica de gestión dependa de contratos y no directamente de clases concretas, facilitando la sustitución de implementaciones.

### 9.5. Separación de responsabilidades

Las operaciones de gestión, filtrado, creación, presentación y almacenamiento se distribuirán entre componentes especializados.

Esta decisión busca evitar la concentración de funcionalidades en una única clase y facilitar el mantenimiento y la reutilización del código.

## 10. Interacción entre componentes

El funcionamiento general del sistema seguirá un flujo de comunicación organizado.

Cuando el usuario solicite registrar una tarea, la vista recopilará los datos y el controlador coordinará la operación. El gestor utilizará la fábrica para construir el objeto correspondiente y posteriormente empleará el componente de almacenamiento para conservarlo.

Para consultar tareas, el controlador solicitará la información al gestor y enviará los resultados a la vista para su presentación.

Cuando se requiera aplicar un filtro, el componente `FiltradoDeTareas` procesará la colección correspondiente y devolverá los elementos que cumplan con el criterio solicitado.

Las operaciones de modificación y eliminación utilizarán el gestor de tareas, manteniendo centralizadas las reglas relacionadas con su administración.

El mecanismo de almacenamiento será intercambiable mediante una interfaz común, por lo que los componentes de gestión no necesitarán conocer los detalles específicos de persistencia.

## 11. Estrategia de reutilización

La reutilización del software se abordará principalmente mediante la separación de responsabilidades, la composición de componentes y la definición de interfaces.

La clase `Tarea` podrá utilizarse en otros sistemas que necesiten representar actividades con características similares.

El componente `FiltradoDeTareas` concentrará las operaciones de filtrado, evitando duplicar esta lógica en la vista o el controlador.

La fábrica permitirá reutilizar un mecanismo común de construcción de tareas, mientras que la interfaz de almacenamiento facilitará la incorporación de nuevas implementaciones sin modificar las operaciones principales.

Asimismo, la separación entre la interfaz de usuario y la lógica del sistema permitirá aprovechar los componentes de gestión en otros tipos de aplicaciones, como una aplicación de escritorio o una interfaz web.

Con este enfoque se busca que las funcionalidades puedan evolucionar de manera independiente, reduciendo el impacto de los cambios y favoreciendo el mantenimiento del sistema.

## 12. Conclusión

El diseño propuesto para el sistema de gestión de tareas establece una estructura modular basada en programación orientada a objetos, componentes reutilizables y patrones de diseño.

La separación de responsabilidades mediante MVC, la centralización de la creación de objetos mediante Factory y la abstracción de los mecanismos de almacenamiento permitirán organizar el código y reducir las dependencias entre sus componentes.

Asimismo, el uso de interfaces, encapsulamiento y composición facilitará la adaptación del sistema a nuevas necesidades sin requerir modificaciones extensas en su estructura.

Este documento constituye la base para la implementación del proyecto. Durante el desarrollo se verificará que los componentes y patrones propuestos respondan a los requisitos establecidos, realizando los ajustes necesarios para mantener una solución funcional, comprensible y reutilizable.