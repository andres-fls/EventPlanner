# Arquitectura del Sistema — EventPlanner

---

## 1. Introducción

Este documento describe la arquitectura utilizada en el sistema EventPlanner, explicando la organización del código, la separación de responsabilidades y la interacción entre los componentes principales.

La arquitectura fue diseñada para mantener el sistema organizado, mantenible y escalable, incluso siendo un proyecto académico.

---

## 2. Enfoque Arquitectónico

EventPlanner implementa una arquitectura por capas inspirada en principios de:

- Separación de responsabilidades (Separation of Concerns)
- Clean Architecture (conceptualmente)
- Patrón DAO (Data Access Object)
- Patrón Service Layer

El objetivo principal es evitar la mezcla de:

- Interfaz gráfica
- Lógica de negocio
- Acceso a base de datos

---

## 3. Estructura General del Proyecto

src/EventPlanner/
│ ├── Forms/ → Interfaz gráfica (UI) ├── Models/ → Entidades del sistema ├── Data/ │ └── DAO/ → Acceso a base de datos ├── Services/ → Lógica de negocio ├── Utils/ → Utilidades comunes └── Program.cs → Punto de entrada

---

## 4. Capas del Sistema

---

### 4.1 Capa de Presentación (Forms)

Responsable de la interacción con el usuario.

Ubicación:
Forms/

Funciones principales:

- Mostrar información.
- Capturar datos del usuario.
- Invocar servicios del sistema.
- Mostrar mensajes de éxito o error.

Importante:

Los formularios NO acceden directamente a la base de datos.

Ejemplo:

- LoginForm
- EventosForm
- CrearEventoForm
- ReportesForm

---

### 4.2 Capa de Modelos (Models)

Representa las entidades del dominio del sistema.

Ubicación:
Models/

Cada clase corresponde a una tabla de la base de datos.

Ejemplos:

- Usuario
- Evento
- Aprendiz
- Programa
- Inscripcion

Responsabilidad:

- Contener datos.
- Representar estructuras del sistema.
- No contener lógica de negocio.

---

### 4.3 Capa de Acceso a Datos (DAO)

Implementa el patrón DAO para aislar las operaciones SQL.

Ubicación:
Data/DAO/

Responsabilidades:

- Ejecutar consultas SQL.
- Insertar, actualizar y consultar datos.
- Mapear resultados hacia modelos.

Ejemplo:
EventoDAO → tabla Evento UsuarioDAO → tabla Usuario

Ventaja:

Si cambia la base de datos, solo esta capa necesita modificación.

---

### 4.4 Capa de Servicios (Services)

Contiene la lógica de negocio del sistema.

Ubicación:
Services/

Responsabilidades:

- Validaciones.
- Reglas de negocio.
- Coordinación entre Forms y DAO.

Ejemplo:
EventoService UsuarioService InscripcionService

Ejemplo de regla:

- Validar fechas de eventos.
- Verificar cupos disponibles.

---

### 4.5 Capa de Utilidades (Utils)

Componentes reutilizables del sistema.

Ubicación:
Utils/

Incluye:

- Session → manejo de usuario activo.
- Validator → validaciones generales.
- AppConfig → configuraciones del sistema.

---

## 5. Flujo de Funcionamiento

El flujo estándar del sistema es:
Usuario ↓ Formulario (Forms) ↓ Servicio (Services) ↓ DAO (Data Access) ↓ Base de Datos SQL Server

Este flujo evita dependencias directas entre la interfaz y la base de datos.

---

## 6. Ventajas de la Arquitectura

- Código organizado y modular.
- Fácil mantenimiento.
- Separación clara de responsabilidades.
- Mayor facilidad para pruebas futuras.
- Escalabilidad del sistema.

---

## 7. Decisiones Técnicas

| Elemento             | Tecnología           |
| -------------------- | -------------------- |
| Lenguaje             | C#                   |
| Framework            | .NET Framework 4.7.2 |
| Interfaz             | Windows Forms        |
| Base de datos        | SQL Server           |
| Control de versiones | Git + GitHub         |

---

## 8. Escalabilidad Futura

La arquitectura permite:

- Migrar la interfaz a otra tecnología.
- Implementar APIs.
- Agregar nuevos roles de usuario.
- Incorporar pruebas automatizadas.

---

## 9. Conclusión

La arquitectura adoptada busca equilibrar simplicidad académica con buenas prácticas profesionales, permitiendo que el sistema crezca sin necesidad de reorganizar completamente el código.
