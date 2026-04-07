# Requisitos del Sistema — EventPlanner

## 1. Introducción

Este documento define los requisitos funcionales y no funcionales del sistema EventPlanner.  
Los requisitos describen el comportamiento esperado del sistema, las restricciones operativas y las características necesarias para cumplir los objetivos planteados en la visión del proyecto.

---

## 2. Alcance del Sistema

EventPlanner permite gestionar eventos institucionales dentro del SENA, incluyendo:

- Administración de usuarios.
- Creación y control de eventos.
- Inscripción de aprendices.
- Gestión académica básica (programas, fichas y grupos).
- Generación de reportes administrativos.

---

## 3. Actores del Sistema

### Administrador

Usuario encargado de configurar y gestionar toda la información del sistema.

Funciones principales:

- Crear y administrar eventos.
- Gestionar usuarios y aprendices.
- Consultar reportes.
- Controlar inscripciones.

### Aprendiz

Usuario participante en eventos institucionales.

Funciones principales:

- Consultar eventos disponibles.
- Inscribirse en eventos.
- Visualizar información relacionada.

---

## 4. Requisitos Funcionales

### RF01 — Autenticación de Usuario

El sistema debe permitir a los usuarios iniciar sesión mediante credenciales válidas.

### RF02 — Gestión de Usuarios

El administrador podrá crear, consultar y administrar usuarios del sistema.

### RF03 — Gestión de Programas

El sistema permitirá registrar y consultar programas de formación.

### RF04 — Gestión de Fichas

El administrador podrá crear y gestionar fichas asociadas a programas.

### RF05 — Gestión de Grupos

El sistema permitirá organizar aprendices en grupos.

### RF06 — Registro de Aprendices

El sistema permitirá registrar aprendices asociados a fichas de formación.

### RF07 — Creación de Eventos

El administrador podrá crear eventos indicando:

- Nombre
- Tipo
- Lugar
- Descripción
- Fechas del evento
- Fechas de inscripción
- Cupo máximo

### RF08 — Modificación de Eventos

El administrador podrá actualizar información de eventos existentes.

### RF09 — Desactivación de Eventos

El sistema permitirá desactivar eventos sin eliminarlos físicamente (soft delete).

### RF10 — Consulta de Eventos

Los usuarios podrán visualizar los eventos disponibles.

### RF11 — Inscripción Individual

El aprendiz podrá inscribirse individualmente a un evento.

### RF12 — Inscripción por Grupo

El sistema permitirá registrar inscripciones grupales cuando aplique.

### RF13 — Validación de Cupos

El sistema deberá verificar que el evento no supere el cupo máximo permitido.

### RF14 — Validación de Fechas

El sistema deberá validar coherencia entre fechas de evento e inscripción.

### RF15 — Control de Estado de Inscripción

Las inscripciones podrán cambiar entre estados activos o cancelados.

### RF16 — Asociación Evento–Usuario

Cada evento deberá registrar el usuario creador.

### RF17 — Generación de Reportes

El sistema permitirá generar reportes relacionados con eventos e inscripciones.

### RF18 — Consulta de Participación

El administrador podrá consultar participantes por evento.

### RF19 — Persistencia de Datos

Toda la información deberá almacenarse en base de datos SQL Server.

### RF20 — Control de Acceso por Rol

El sistema restringirá funcionalidades según el rol del usuario.

### RF21 — Mensajes del Sistema

El sistema deberá mostrar mensajes informativos y de error ante acciones realizadas.

---

## 5. Requisitos No Funcionales

### RNF01 — Plataforma

El sistema debe ejecutarse en sistemas operativos Windows 10 y Windows 11.

### RNF02 — Base de Datos

Se utilizará Microsoft SQL Server como gestor de base de datos.

### RNF03 — Rendimiento

El sistema debe responder a operaciones comunes en menos de 3 segundos bajo condiciones normales.

### RNF04 — Usabilidad

La interfaz debe permitir navegación sencilla mediante formularios Windows Forms.

### RNF05 — Seguridad Básica

El acceso al sistema estará protegido mediante autenticación de usuario.

### RNF06 — Mantenibilidad

El sistema deberá seguir una estructura modular basada en:

- Models
- DAO
- Services
- Forms

### RNF07 — Integridad de Datos

Las operaciones deberán respetar relaciones y restricciones definidas en la base de datos.

### RNF08 — Manejo de Errores

El sistema deberá capturar errores mediante bloques try-catch mostrando mensajes controlados al usuario.

---

## 6. Reglas de Negocio

- Un evento debe tener fechas válidas (inicio < fin).
- El cupo máximo no puede ser negativo.
- No se permiten inscripciones fuera del periodo habilitado.
- Cada evento debe tener un usuario creador.
- Las eliminaciones se realizan mediante desactivación lógica.

---

## 7. Suposiciones y Restricciones

- El sistema está orientado inicialmente al entorno SENA.
- Se utiliza arquitectura de aplicación de escritorio.
- Las pruebas se realizan manualmente en esta fase del proyecto.

---
