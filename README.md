# Todo API with .NET 8
# **📌 TodoAPI - Web API con .NET 8 + PostgreSQL + JWT**  

Un proyecto completo para gestionar tareas (**Todo**) con autenticación JWT, diseñado con:  
✅ **.NET 8**  
✅ **PostgreSQL** (EF Core)  
✅ **JWT Authentication**  
✅ **Repository Pattern** + **DTOs**  
✅ **Swagger** (OpenAPI)  
✅ **Docker-ready**  

---

## **🚀 Instalación y Configuración**  

### **Requisitos**  
- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- [PostgreSQL](https://www.postgresql.org/download/) (o Docker)  
- [Git](https://git-scm.com/)  

### **Pasos iniciales**  
1. Clona el repositorio:  
   ```bash
   git clone https://github.com/tuusuario/TodoApi.git
   cd TodoApi
   ```

2. Configura la base de datos:  
   - Crea una DB en PostgreSQL:  
     ```sql
     CREATE DATABASE todoapi;
     CREATE USER todo_user WITH PASSWORD 'P@ssw0rd';
     GRANT ALL PRIVILEGES ON DATABASE todoapi TO todo_user;
     ```  
   - Actualiza la cadena de conexión en `appsettings.json`:  
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=todoapi;Username=todo_user;Password=P@ssw0rd"
     }
     ```  

3. Ejecuta migraciones:  
   ```bash
   dotnet ef database update
   ```

---

## **🔐 Autenticación JWT**  
El sistema usa tokens JWT para seguridad. Configuración en `appsettings.json`:  
```json
"Jwt": {
  "Key": "ClaveSecretaDe64Caracteres1234567890abcdefghijklmnopqrstuvwxyz",
  "Issuer": "https://localhost:5001",
  "Audience": "https://localhost:5001"
}
```  

### **Endpoints**  
| Método | Ruta                | Descripción                     |  
|--------|---------------------|---------------------------------|  
| POST   | `/api/auth/register`| Registro de usuario             |  
| POST   | `/api/auth/login`   | Inicio de sesión (obtener JWT)  |  

**Ejemplo de login**:  
```bash
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"user@example.com","password":"P@ssw0rd"}'
```

---

## **📚 Endpoints de Tareas (Todo)**  
Requieren autenticación JWT (header `Authorization: Bearer <token>`).  

| Método | Ruta          | Descripción               |  
|--------|---------------|---------------------------|  
| GET    | `/api/todos`  | Obtener todas las tareas  |  
| POST   | `/api/todos`  | Crear nueva tarea         |  
| PUT    | `/api/todos/{id}` | Actualizar tarea      |  
| DELETE | `/api/todos/{id}` | Eliminar tarea      |  

**Ejemplo**:  
```bash
curl -X GET https://localhost:5001/api/todos \
  -H "Authorization: Bearer eyJhbGciOiJIUz..."
```

---

## **🐳 Docker Support**  
Para ejecutar con PostgreSQL en Docker:  

1. Crea un archivo `docker-compose.yml`:  
   ```yaml
   version: '3.8'
   services:
     db:
       image: postgres:15
       environment:
         POSTGRES_PASSWORD: P@ssw0rd
         POSTGRES_USER: todo_user
         POSTGRES_DB: todoapi
       ports:
         - "5432:5432"
     api:
       build: .
       ports:
         - "5000:80"
       depends_on:
         - db
   ```  

2. Ejecuta:  
   ```bash
   docker-compose up
   ```

---

## **🛠️ Estructura del Proyecto**  
```  
TodoApi/  
├── Controllers/       # Controladores (API endpoints)  
├── Data/              # DbContext y config DB  
├── DTOs/              # Objetos de transferencia  
├── Models/            # Entidades (Todo, User)  
├── Repositories/      # Patrón Repository  
├── Services/          # Lógica de negocio (JWT)  
├── appsettings.json   # Configuración  
└── Program.cs         # Configuración principal  
```  

---

## **💡 Tecnologías Clave**  
- **.NET 8**: Framework principal.  
- **Entity Framework Core**: ORM para PostgreSQL.  
- **JWT Bearer**: Autenticación segura.  
- **Swagger**: Documentación interactiva.  
- **Docker**: Contenerización.  

---

## **📄 Licencia**  
Este proyecto está bajo la licencia MIT. Ver [github.com/jesusignaciob/TodoApi/LICENSE](MIT) para más detalles.

--- 

¡Contribuciones son bienvenidas! 🎉  
**¿Problemas?** Abre un *issue* en GitHub.  

--- 

🔗 **Enlace al repositorio**: [github.com/jesusignaciob/TodoApi](https://github.com/jesusignaciob/TodoApi)  

---