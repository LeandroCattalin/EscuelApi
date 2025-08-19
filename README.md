# 🏫 EscuelAPI

Este es un proyecto de API web desarrollado con ASP.NET Core que simula la gestión de una escuela, incluyendo la administración de alumnos, cursos y profesores. 🚀 El objetivo principal es aplicar las mejores prácticas de desarrollo y explorar las funcionalidades más relevantes de ASP.NET Core.

---

### ✨ Características Principales

* API RESTful Completa: Métodos GET, POST, PUT y DELETE para gestionar los recursos de la escuela.
* Conexión a Base de Datos: Persistencia de datos mediante Entity Framework Core conectado a un servidor de SQL Server en Linux. 🐧
* Patrones de Diseño: Implementación de DTOs (Data Transfer Objects) para una comunicación clara y segura.
* Inyección de Dependencias: Gestión de servicios y dependencias de forma organizada y eficiente.
* Validación de Datos: Reglas de validación para garantizar la integridad de los datos de entrada.
* Manejo de Errores: Respuestas HTTP claras y descriptivas para diferentes escenarios de error.

---

### 🛠️ Tecnologías Utilizadas

* ASP.NET Core: El framework principal para el desarrollo de la API.
* Entity Framework Core: ORM (Object-Relational Mapper) para interactuar con la base de datos.
* SQL Server: Sistema de gestión de bases de datos relacionales, corriendo en un entorno Linux.
* Docker: Posiblemente utilizado para la contenedorización del servidor de la base de datos, facilitando su despliegue y configuración.
* Swagger/OpenAPI: Documentación automática de la API para facilitar las pruebas y el consumo. 📝

---

### 🚀 ¿Cómo Empezar?

1. Clonar el repositorio:
git clone https://github.com/LeandroCattalin/EscuelApi.git
cd EscuelaApi
2. Configurar la conexión a la base de datos:
* Asegúrate de tener SQL Server en Linux instalado y funcionando.
* Actualiza la cadena de conexión en el archivo appsettings.json con los datos de tu servidor.
3. Ejecutar la aplicación:
* Desde la terminal, ejecuta el siguiente comando para levantar la API:
dotnet run
4. Explorar la API:
* Una vez que la aplicación esté en marcha, puedes acceder a la documentación de Swagger en https://localhost:puerto/swagger.

¡Saludos! 💻
