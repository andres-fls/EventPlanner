/* ============================
   CREAR BASE DE DATOS
=========================== */

CREATE DATABASE EventPlannerDB;
GO

USE EventPlannerDB;
GO


/* ============================
   TABLA USUARIO (LOGIN)
=========================== */

CREATE TABLE Usuario(
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    nombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    passwordUsuario VARCHAR(255) NOT NULL,
    rolUsuario VARCHAR(20) NOT NULL
);


/* ============================
   TABLA PROGRAMA
=========================== */

CREATE TABLE Programa(
    idPrograma INT IDENTITY(1,1) PRIMARY KEY,
    codigoPrograma INT NOT NULL UNIQUE,
    nombrePrograma VARCHAR(40) NOT NULL,
    duracionPrograma VARCHAR(20) NOT NULL,
    nivelPrograma VARCHAR(20) NOT NULL
);


/* ============================
   TABLA FICHA
=========================== */

CREATE TABLE Ficha(
    codigoFicha INT PRIMARY KEY,
    fechaInicio DATE NOT NULL,
    fechaFin DATE NOT NULL,
    codigoPrograma INT NOT NULL,

    FOREIGN KEY (codigoPrograma)
        REFERENCES Programa(codigoPrograma)
);


/* ============================
   TABLA APRENDIZ
=========================== */

CREATE TABLE Aprendiz(
    idAprendiz INT IDENTITY(1,1) PRIMARY KEY,
    cedulaAprendiz VARCHAR(20) NOT NULL UNIQUE,
    nombreAprendiz VARCHAR(50) NOT NULL,
    edadAprendiz INT NOT NULL,
    generoAprendiz VARCHAR(15),
    correoAprendiz VARCHAR(60) NOT NULL,
    telefonoAprendiz VARCHAR(15) NOT NULL,

    codigoFicha INT NOT NULL,
    idUsuario INT NOT NULL UNIQUE,

    FOREIGN KEY (codigoFicha)
        REFERENCES Ficha(codigoFicha),

    FOREIGN KEY (idUsuario)
        REFERENCES Usuario(idUsuario)
);


/* ============================
   TABLA GRUPO (OPCIONAL)
=========================== */

CREATE TABLE Grupo(
    idGrupo INT IDENTITY(1,1) PRIMARY KEY,
    nombreGrupo VARCHAR(30) NOT NULL
);
GO


/* ============================
   TABLA EVENTO
=========================== */

CREATE TABLE Evento(
    idEvento INT IDENTITY(1,1) PRIMARY KEY,
    nombreEvento VARCHAR(50) NOT NULL,
    tipoEvento VARCHAR(30) NOT NULL,
    lugarEvento VARCHAR(50) NOT NULL,
    descripcionEvento VARCHAR(MAX),
    
    fechaInicioEvento DATETIME NOT NULL,
    fechaFinEvento DATETIME NOT NULL,
    fechaInicioInscripcion DATETIME NOT NULL,
    fechaFinInscripcion DATETIME NOT NULL,
    
    cupoMaximo INT NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    
    idUsuarioCreador INT NOT NULL,

    FOREIGN KEY (idUsuarioCreador)
        REFERENCES Usuario(idUsuario)
);
GO


/* ============================
   TABLA INSCRIPCION
=========================== */

CREATE TABLE Inscripcion(
    idInscripcion INT IDENTITY(1,1) PRIMARY KEY,
    fechaInscripcion DATETIME NOT NULL DEFAULT GETDATE(),
    tipoInscripcion VARCHAR(30) NOT NULL,
    modalidad VARCHAR(20) NOT NULL,
    estadoInscripcion VARCHAR(20) NOT NULL DEFAULT 'Activo',

    idAprendiz INT NOT NULL,
    idEvento INT NOT NULL,
    idGrupo INT NULL,

    FOREIGN KEY (idAprendiz)
        REFERENCES Aprendiz(idAprendiz),

    FOREIGN KEY (idEvento)
        REFERENCES Evento(idEvento),

-- 7. Tabla de Inscripciones
CREATE TABLE Inscripcion (
    IdInscripcion INT PRIMARY KEY IDENTITY(1,1),
    FechaInscripcion DATETIME DEFAULT GETDATE(),
    TipoInscripcion VARCHAR(50),
    Modalidad VARCHAR(20), -- Individual, Equipo
    EstadoInscripcion VARCHAR(20) DEFAULT 'Activo', -- Activo, Cancelado
    IdAprendiz INT NOT NULL,
    IdEvento INT NOT NULL,
    IdGrupo INT NULL,
    FOREIGN KEY (IdAprendiz) REFERENCES Aprendiz(IdAprendiz),
    FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento),
    FOREIGN KEY (IdGrupo) REFERENCES Grupo(IdGrupo)
);
GO
