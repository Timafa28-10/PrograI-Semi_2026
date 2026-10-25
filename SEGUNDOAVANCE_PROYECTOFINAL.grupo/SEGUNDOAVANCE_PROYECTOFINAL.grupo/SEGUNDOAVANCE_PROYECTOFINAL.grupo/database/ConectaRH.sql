-- =====================================================================
-- ConectaRH - Base de datos MySQL (8 tablas)
-- Según el documento "Tablas ConectaRH (mismo formato, corregidas)"
-- La aplicación también crea estas tablas automáticamente al iniciar
-- (EnsureCreated); este script es útil para crearlas manualmente.
-- =====================================================================
CREATE DATABASE IF NOT EXISTS conectarh CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE conectarh;

CREATE TABLE rol (
    id_rol               INT AUTO_INCREMENT PRIMARY KEY,
    nombre_rol           VARCHAR(30)  NOT NULL,
    descripcion_permisos VARCHAR(500) NOT NULL,
    CONSTRAINT uq_rol_nombre UNIQUE (nombre_rol)
);

CREATE TABLE usuario (
    id_usuario      INT AUTO_INCREMENT PRIMARY KEY,
    id_rol          INT          NOT NULL,
    nombre_usuario  VARCHAR(100) NOT NULL,
    correo          VARCHAR(150) NOT NULL,
    contrasena_hash VARCHAR(255) NOT NULL,
    fecha_registro  DATETIME     NOT NULL,
    estado          VARCHAR(20)  NOT NULL,           -- activo | inactivo
    CONSTRAINT uq_usuario_correo UNIQUE (correo),
    INDEX ix_usuario_rol (id_rol),
    CONSTRAINT fk_usuario_rol FOREIGN KEY (id_rol) REFERENCES rol (id_rol)
);

CREATE TABLE candidato (
    id_candidato        INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario          INT          NOT NULL,
    nombre_completo     VARCHAR(150) NOT NULL,
    fecha_nacimiento    DATE         NULL,
    telefono            VARCHAR(20)  NULL,
    direccion           VARCHAR(200) NULL,
    resumen_profesional TEXT         NULL,
    CONSTRAINT uq_candidato_usuario UNIQUE (id_usuario),
    CONSTRAINT fk_candidato_usuario FOREIGN KEY (id_usuario) REFERENCES usuario (id_usuario) ON DELETE CASCADE
);

CREATE TABLE empresa (
    id_empresa      INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario      INT          NOT NULL,
    nombre_empresa  VARCHAR(150) NOT NULL,
    sector          VARCHAR(100) NULL,
    direccion       VARCHAR(200) NULL,
    telefono        VARCHAR(20)  NULL,
    descripcion     TEXT         NULL,
    CONSTRAINT uq_empresa_usuario UNIQUE (id_usuario),
    CONSTRAINT fk_empresa_usuario FOREIGN KEY (id_usuario) REFERENCES usuario (id_usuario) ON DELETE CASCADE
);

CREATE TABLE formacion_academica (
    id_formacion    INT AUTO_INCREMENT PRIMARY KEY,
    id_candidato    INT          NOT NULL,
    nivel_academico VARCHAR(100) NOT NULL,
    institucion     VARCHAR(150) NOT NULL,
    fecha_inicio    DATE         NULL,
    fecha_fin       DATE         NULL,
    INDEX ix_formacion_candidato (id_candidato),
    CONSTRAINT fk_formacion_candidato FOREIGN KEY (id_candidato) REFERENCES candidato (id_candidato) ON DELETE CASCADE
);

CREATE TABLE experiencia_laboral (
    id_experiencia   INT AUTO_INCREMENT PRIMARY KEY,
    id_candidato     INT          NOT NULL,
    puesto           VARCHAR(120) NOT NULL,
    empresa_anterior VARCHAR(150) NOT NULL,
    fecha_inicio     DATE         NULL,
    fecha_fin        DATE         NULL,
    descripcion      TEXT         NULL,
    INDEX ix_experiencia_candidato (id_candidato),
    CONSTRAINT fk_experiencia_candidato FOREIGN KEY (id_candidato) REFERENCES candidato (id_candidato) ON DELETE CASCADE
);

CREATE TABLE oferta_empleo (
    id_oferta         INT AUTO_INCREMENT PRIMARY KEY,
    id_empresa        INT          NOT NULL,
    titulo            VARCHAR(150) NOT NULL,
    descripcion       TEXT         NOT NULL,
    requisitos        TEXT         NOT NULL,
    ubicacion         VARCHAR(150) NULL,
    fecha_publicacion DATETIME     NOT NULL,
    fecha_cierre      DATE         NULL,
    estado            VARCHAR(20)  NOT NULL,         -- publicada | pausada | cerrada
    INDEX ix_oferta_empresa (id_empresa),
    INDEX ix_oferta_titulo (titulo),
    INDEX ix_oferta_fecha_publicacion (fecha_publicacion),
    INDEX ix_oferta_estado (estado),
    CONSTRAINT fk_oferta_empresa FOREIGN KEY (id_empresa) REFERENCES empresa (id_empresa) ON DELETE CASCADE
);

CREATE TABLE postulacion (
    id_postulacion    INT AUTO_INCREMENT PRIMARY KEY,
    id_candidato      INT          NOT NULL,
    id_oferta         INT          NOT NULL,
    fecha_postulacion DATETIME     NOT NULL,
    estado            VARCHAR(20)  NOT NULL,         -- pendiente | en revisión | aceptada | rechazada
    observacion       VARCHAR(500) NULL,
    INDEX ix_postulacion_candidato (id_candidato),
    INDEX ix_postulacion_oferta (id_oferta),
    CONSTRAINT uq_postulacion_candidato_oferta UNIQUE (id_candidato, id_oferta),
    CONSTRAINT fk_postulacion_candidato FOREIGN KEY (id_candidato) REFERENCES candidato (id_candidato) ON DELETE CASCADE,
    CONSTRAINT fk_postulacion_oferta FOREIGN KEY (id_oferta) REFERENCES oferta_empleo (id_oferta) ON DELETE CASCADE
);

-- Roles base (la aplicación también los crea al iniciar si no existen)
INSERT INTO rol (nombre_rol, descripcion_permisos) VALUES
('candidato',     'Crear y editar su perfil, formación académica y experiencia laboral; consultar ofertas, postularse y consultar el estado de sus postulaciones.'),
('empresa',       'Registrar su empresa, publicar y administrar ofertas de empleo, y revisar los perfiles de las personas postuladas.'),
('administrador', 'Acceso completo: gestionar usuarios, empresas, ofertas de empleo y roles de la plataforma.');
