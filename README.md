# EventPass - Sistema de Gestión de Eventos (Examen Final)

**Materia:** Programación II - UCB
**Tecnologías:** Angular 17, .NET 8 (C# API REST), Entity Framework Core, SQL Server.

---

## 📌 Consideraciones Previas (Entorno macOS / Windows)
El proyecto ha sido preparado cumpliendo la rúbrica de empaquetado (sin carpetas auto-generadas como `node_modules`, `bin` u `obj`). Siga los pasos a continuación para compilar y ejecutar el proyecto funcionalmente en su entorno local.

---

## ⚙️ Paso 1: Configuración de la Base de Datos
Se adjunta un script SQL con la estructura completa y datos de prueba poblados.

1. Navegue a la carpeta `EventPass DB`.
2. Ejecute el script `01_Estructura_Y_Datos.sql` en su gestor de SQL Server (Azure Data Studio, DBeaver o SSMS).
   * *Nota:* El script es multiplataforma. Crea automáticamente la base de datos `EventPassDB` y puebla las tablas de Usuarios, Eventos, Inscripciones y Comentarios.

---

## 🚀 Paso 2: Ejecución del Backend (.NET)
El proyecto utiliza Entity Framework Core. Las dependencias se restaurarán automáticamente al abrir el proyecto.

1. Abra la carpeta `EventPass UCB-backend` en su IDE (Rider o Visual Studio).
2. Abra el archivo `appsettings.json` y verifique que el `DefaultConnection` coincida con las credenciales de su instancia de SQL Server local o Docker.
3. El IDE detectará el archivo `.csproj` y descargará los paquetes NuGet automáticamente. No es necesario ejecutar migraciones, ya que el script SQL estructuró la base de datos.
4. Ejecute el proyecto. El servidor backend se levantará en: `http://localhost:5056`

---

## 💻 Paso 3: Ejecución del Frontend (Angular)
Al haberse eliminado la carpeta `node_modules` por requerimientos de peso, es necesario restaurar las dependencias de Node.

1. Abra una terminal y navegue hacia la carpeta `EventPass UCB-Frontend`.
2. Ejecute el siguiente comando para descargar las dependencias:
   ```bash
   npm install
Una vez finalizada la instalación, levante el servidor de desarrollo de Angular con:

Bash
ng serve -o
El navegador se abrirá automáticamente en http://localhost:4200.

🔑 Credenciales de Prueba (Datos Poblados)
El sistema ya cuenta con datos pre-cargados para probar las Historias de Usuario inmediatamente.

Perfil de Usuario (Participante):

Correo: javi@ucb.edu.bo

Contraseña: 12345

(Este usuario ya cuenta con inscripciones activas y tickets comprados en su perfil).
