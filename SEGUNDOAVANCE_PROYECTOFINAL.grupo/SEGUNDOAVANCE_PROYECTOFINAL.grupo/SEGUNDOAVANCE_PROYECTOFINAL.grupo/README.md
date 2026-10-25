# ConectaRH

Sistema de Gestión de Recursos Humanos para Reclutamiento y Selección de Personal.
Conecta a personas que buscan empleo con empresas que necesitan contratar personal.

## Tecnologías (según el primer avance)

| Capa | Tecnología |
|---|---|
| Presentación | HTML5, CSS3 y JavaScript (vistas Razor de ASP.NET Core MVC) |
| Lógica de negocio | C# (.NET 8) – proyecto `ConectaRH.Business` |
| Datos | MySQL – proyecto `ConectaRH.Data` (Entity Framework Core + Pomelo) |

Arquitectura de tres capas: `Web → Business → Data`.

```
ConectaRH.sln
database/ConectaRH.sql          Script de las 8 tablas (opcional)
src/ConectaRH.Data/             Entidades + DbContext (tablas, claves, UNIQUE e índices)
src/ConectaRH.Business/         Servicios: registro/login, perfiles, ofertas, postulaciones, administración
src/ConectaRH.Web/              Controladores, vistas (Plantillas 1-12), CSS, JS e imágenes
```

## Cómo ejecutarlo

Requisitos: **.NET 8 SDK** y **MySQL 8**.

1. Edita la contraseña de MySQL en `src/ConectaRH.Web/appsettings.json` (`ConnectionStrings:ConectaRH`).
2. Desde la carpeta del proyecto:
   ```
   dotnet restore
   dotnet run --project src/ConectaRH.Web
   ```
3. Al iniciar, la aplicación **crea la base de datos y las 8 tablas**, inserta los 3 roles y una cuenta de administrador.
   (Si prefieres crearlas a mano, usa `database/ConectaRH.sql`.)
4. Abre `https://localhost:7080`.

**Administrador inicial** (cámbialo en `appsettings.json`, sección `AdministradorInicial`):
`admin@conectarh.com` / `Admin12345`

## Base de datos (8 tablas, MySQL)

`rol`, `usuario`, `candidato`, `empresa`, `formacion_academica`, `experiencia_laboral`, `oferta_empleo`, `postulacion`,
con los tipos, claves foráneas, índices y restricciones UNIQUE del documento corregido
(correo único, un perfil por usuario, una postulación por candidato y oferta).
El administrador es un usuario con rol `administrador` (no hay tabla propia). Las contraseñas se guardan con hash BCrypt.

## Lógica de funcionamiento

Inicio → Iniciar sesión / Registro → según el rol:

- **Candidato:** Buscar empleos → Postularse (adjunta CV) → Consultar estado. Edita datos personales, formación y experiencia.
- **Empresa:** Publicar ofertas → Revisar candidatos (cambiar estado, observaciones, descargar CV).
- **Administrador:** Gestionar usuarios (activar/desactivar, crear), clientes, ofertas, roles y reportes.

## Pantallas ↔ plantillas

| Plantilla | Ruta |
|---|---|
| 1 Inicio | `/` |
| 2 Iniciar sesión | `/Cuenta/Login` |
| 3 Crear cuenta | `/Cuenta/Registro` |
| 4 Mi perfil (candidato) | `/Candidato/Perfil` |
| 5 Mi empresa | `/Empresa/MiEmpresa` |
| 6 Gestión de clientes | `/Admin/Clientes` |
| 7 Detalle de oferta | `/Ofertas/Detalle/{id}` |
| 8 Postular a oferta | `/Ofertas/Postular/{id}` |
| 9 Perfil de usuario | `/Admin/PerfilUsuario/{id}` |
| 10 Lista de usuarios | `/Admin/Usuarios` |
| 11 Roles y permisos | `/Admin/Roles` |
| 12 Configuración de la empresa | `/Admin/Configuracion` |

## Decisiones donde la plantilla y la base de datos no coinciden

- Se respetó la estructura de BD del documento, por eso **no** se agregaron columnas. Datos que aparecen en las plantillas pero no existen en la BD
  (tamaño de la empresa, sitio web, tipo de contrato, modalidad, salario) se sustituyeron por datos que sí existen (sector, teléfono, estado, fecha de cierre, publicación).
- El **mensaje adicional** de la postulación se guarda en `postulacion.observacion`; el **CV** se guarda como archivo en `App_Data/cv/{id_postulacion}.ext` (no es público).
- Plantilla 11: los roles son los tres de la BD; "Permisos" muestra la cantidad de usuarios y "Crear rol" está deshabilitado porque los permisos se aplican en código.
- Plantillas 9–12: el círculo con el número de plantilla se reemplazó por un icono.
- Los textos de las plantillas se copiaron tal cual (incluidos "Datod adicionales", "Registrase", "Correro Electrónico", "olvidastes", "3HD"); se pueden corregir en las vistas.
- En Plantilla 3 el botón dice "Crear cuenta" (la plantilla decía "Iniciar sesión") y el enlace inferior lleva a iniciar sesión.
