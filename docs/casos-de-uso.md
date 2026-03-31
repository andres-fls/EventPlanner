# Casos de Uso — EventPlanner

---

## 1. Introducción

Este documento describe los casos de uso del sistema EventPlanner, representando la interacción entre los actores y el sistema para cumplir los objetivos funcionales definidos en los requisitos.

Los casos de uso reflejan el comportamiento implementado en la aplicación Windows Forms.

---

## 2. Actores

### Administrador

Usuario con acceso completo al sistema encargado de gestionar eventos, usuarios y reportes.

### Aprendiz

Usuario participante que puede consultar eventos e inscribirse.

---

## 3. Lista General de Casos de Uso

| Código | Caso de Uso          |
| ------ | -------------------- |
| CU01   | Iniciar sesión       |
| CU02   | Crear evento         |
| CU03   | Consultar eventos    |
| CU04   | Desactivar evento    |
| CU05   | Registrar aprendiz   |
| CU06   | Inscribirse a evento |
| CU07   | Gestionar programas  |
| CU08   | Gestionar fichas     |
| CU09   | Gestionar grupos     |
| CU10   | Generar reportes     |

---

## 4. Descripción de Casos de Uso

---

### CU01 — Iniciar Sesión

**Actor:** Administrador / Aprendiz  
**Descripción:** Permite acceder al sistema mediante credenciales válidas.

**Flujo principal:**

1. El usuario abre la aplicación.
2. Ingresa usuario y contraseña.
3. El sistema valida las credenciales.
4. El sistema identifica el rol del usuario.
5. Se muestra el menú correspondiente.

**Flujos alternos:**

- Credenciales incorrectas → se muestra mensaje de error.

---

### CU02 — Crear Evento

**Actor:** Administrador

**Descripción:** Permite registrar un nuevo evento institucional.

**Flujo principal:**

1. El administrador accede al formulario de creación.
2. Ingresa la información del evento.
3. El sistema valida fechas y cupos.
4. Se guarda el evento en la base de datos.
5. El sistema confirma la creación.

**Reglas aplicadas:**

- Fecha fin debe ser mayor a fecha inicio.
- Cupo máximo ≥ 0.

---

### CU03 — Consultar Eventos

**Actor:** Administrador / Aprendiz

**Descripción:** Permite visualizar eventos disponibles.

**Flujo principal:**

1. El usuario abre la sección de eventos.
2. El sistema consulta la base de datos.
3. Se muestra la lista de eventos activos.

---

### CU04 — Desactivar Evento

**Actor:** Administrador

**Descripción:** Permite desactivar un evento sin eliminarlo físicamente.

**Flujo principal:**

1. El administrador selecciona un evento.
2. Ejecuta la opción desactivar.
3. El sistema cambia el estado del evento a inactivo.

---

### CU05 — Registrar Aprendiz

**Actor:** Administrador

**Descripción:** Permite registrar aprendices en el sistema.

**Flujo principal:**

1. El administrador accede al formulario de registro.
2. Ingresa los datos del aprendiz.
3. El sistema valida la información.
4. Se guarda el registro.

---

### CU06 — Inscribirse a Evento

**Actor:** Aprendiz

**Descripción:** Permite participar en un evento disponible.

**Flujo principal:**

1. El aprendiz visualiza eventos.
2. Selecciona un evento.
3. Solicita inscripción.
4. El sistema valida cupos disponibles.
5. Se registra la inscripción.

**Flujos alternos:**

- Cupo lleno → inscripción rechazada.
- Fuera de fechas → inscripción rechazada.

---

### CU07 — Gestionar Programas

**Actor:** Administrador

**Descripción:** Permite administrar programas de formación.

**Acciones:**

- Crear programa.
- Consultar programas.

---

### CU08 — Gestionar Fichas

**Actor:** Administrador

**Descripción:** Permite administrar fichas asociadas a programas.

---

### CU09 — Gestionar Grupos

**Actor:** Administrador

**Descripción:** Permite organizar aprendices en grupos.

---

### CU10 — Generar Reportes

**Actor:** Administrador

**Descripción:** Permite generar reportes del sistema.

**Flujo principal:**

1. El administrador accede al módulo de reportes.
2. Selecciona el tipo de reporte.
3. El sistema consulta información.
4. Se genera el reporte en formato visual o PDF.

---

## 5. Relación con la Implementación

Cada caso de uso se encuentra representado mediante:

- Formularios (Forms)
- Servicios (Services)
- Acceso a datos (DAO)

Esto asegura trazabilidad entre documentación y código.

---

## 6. Observaciones

Los casos de uso documentados corresponden a la versión actual del sistema y pueden ampliarse en futuras iteraciones.
