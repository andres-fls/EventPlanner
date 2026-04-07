Markdown
# 🎯 EventPlanner

Sistema de gestión de eventos académicos desarrollado como proyecto formativo del programa **Análisis y Desarrollo de Software – SENA**.

EventPlanner permite administrar eventos, usuarios e inscripciones mediante una aplicación de escritorio basada en **Windows Forms** y arquitectura por capas.

---

## 📌 Objetivo del Proyecto

Desarrollar una aplicación que permita:

- Crear y administrar eventos institucionales.
- Gestionar usuarios y aprendices.
- Realizar inscripciones a eventos.
- Generar reportes administrativos.
- Aplicar buenas prácticas de ingeniería de software.

El proyecto fue construido priorizando **organización, mantenibilidad y separación de responsabilidades**.

---

## 🏗️ Arquitectura del Sistema

El proyecto sigue una arquitectura por capas inspirada en principios de organización y separación de responsabilidades.
UI (Forms) ↓ Services (Reglas de negocio) ↓ DAO (Acceso a datos) ↓ SQL Server (Base de datos)

### Capas del sistema

| Capa | Responsabilidad |
|------|----------------|
| **Forms** | Interfaz gráfica (Windows Forms) |
| **Services** | Validaciones y lógica de negocio |
| **DAO** | Comunicación con la base de datos |
| **Models** | Representación de entidades |
| **Utils** | Configuración y utilidades comunes |


---

## 🛠️ Tecnologías Utilizadas

- C#
- .NET Framework 4.7.2
- Windows Forms
- SQL Server
- ADO.NET
- Git & GitHub

---

## ⚙️ Configuración del Entorno

### 1️⃣ Crear Base de Datos

Ejecutar los scripts ubicados en:
databases/schema.sql databases/seed.sql

usando **SQL Server Management Studio**.

---

### 2️⃣ Configurar conexión

Editar el archivo:
App.config

y ajustar la cadena de conexión según el servidor local.

---

### 3️⃣ Ejecutar aplicación

Abrir la solución:
EventPlanner.sln

en Visual Studio y ejecutar:
Start Debugging (F5)

---

## 🧪 Pruebas

Las pruebas realizadas incluyen:

- Pruebas funcionales manuales.
- Validación de flujo completo del sistema.
- Verificación de operaciones CRUD.
- Validación de reglas de negocio desde la capa Services.
- Pruebas de conexión y persistencia en base de datos.

El plan de pruebas completo se encuentra en la carpeta:
docs/

---

## 📋 Control de Versiones

Se utilizó Git con dos ramas principales:

- `main` → versión estable
- `DEV` → desarrollo activo

Los cambios se integran mediante merge controlado hacia la rama principal.

---

## ✅ Buenas Prácticas Aplicadas

- Separación por capas
- Validaciones centralizadas en Services
- Uso de parámetros SQL (prevención básica de SQL Injection)
- Manejo de errores mediante try/catch
- Documentación técnica del sistema
- Plan SQA y plan de pruebas
- Organización modular del código

---

## 👥 Equipo de Desarrollo

Proyecto desarrollado como trabajo académico dentro del programa ADSO – SENA,

gestionado mediante control de versiones con Git y GitHub.
---

## 🚀 Estado del Proyecto

✅ Funcional  
✅ Arquitectura implementada  
✅ Base de datos integrada  
🔧 Documentación técnica completa  
🔧 Mejoras visuales en progreso

---

## 📖 Documentación

Disponible en la carpeta:
docs/

Incluye:

- Visión del sistema
- Requisitos
- Arquitectura
- Casos de uso
- Plan SQA
- Plan de pruebas

---

## 📄 Licencia

Proyecto académico desarrollado con fines educativos.