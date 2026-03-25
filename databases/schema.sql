/* ============================
   CREAR BASE DE DATOS
============================ */

CREATE DATABASE EventPlannerDB;
GO

USE EventPlannerDB;
GO


/* ============================
   TABLA USUARIO (LOGIN)
============================ */

CREATE TABLE Usuario(
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    nombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    passwordUsuario VARCHAR(50) NOT NULL,
    rolUsuario VARCHAR(20) NOT NULL
);
GO


/* ============================
   TABLA PROGRAMA
============================ */

CREATE TABLE Programa(
    codigoPrograma INT PRIMARY KEY,
    nombrePrograma VARCHAR(40) NOT NULL,
    duracionPrograma VARCHAR(20) NOT NULL,
    nivelPrograma VARCHAR(20) NOT NULL
);
GO


/* ============================
   TABLA FICHA
============================ */

CREATE TABLE Ficha(
    codigoFicha INT PRIMARY KEY,
    fechaInicio DATE NOT NULL,
    fechaFin DATE NOT NULL,
    codigoPrograma INT NOT NULL,

    FOREIGN KEY (codigoPrograma)
        REFERENCES Programa(codigoPrograma)
);
GO


/* ============================
   TABLA APRENDIZ
============================ */

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
GO


/* ============================
   TABLA EVENTO
============================ */

CREATE TABLE Evento(
    idEvento INT IDENTITY(1,1) PRIMARY KEY,
    nombreEvento VARCHAR(50) NOT NULL,
    tipoEvento VARCHAR(30) NOT NULL,
    lugarEvento VARCHAR(50) NOT NULL,
    fechaEvento DATE NOT NULL,
    duracionEvento TIME NOT NULL,
    estadoEvento VARCHAR(20) NOT NULL,

    idUsuarioCreador INT NOT NULL,

    FOREIGN KEY (idUsuarioCreador)
        REFERENCES Usuario(idUsuario)
);
GO


/* ============================
   TABLA GRUPO (OPCIONAL)
============================ */

CREATE TABLE Grupo(
    idGrupo INT IDENTITY(1,1) PRIMARY KEY,
    nombreGrupo VARCHAR(30) NOT NULL
);
GO


/* ============================
   TABLA INSCRIPCION
============================ */

CREATE TABLE Inscripcion(
    idInscripcion INT IDENTITY(1,1) PRIMARY KEY,

    fechaInscripcion DATE NOT NULL,
    modalidad VARCHAR(20) NOT NULL,
    estadoInscripcion VARCHAR(20) NOT NULL DEFAULT 'Activo',

    idAprendiz INT NOT NULL,
    idEvento INT NOT NULL,
    idGrupo INT NULL,

    FOREIGN KEY (idAprendiz)
        REFERENCES Aprendiz(idAprendiz),

    FOREIGN KEY (idEvento)
        REFERENCES Evento(idEvento),

    FOREIGN KEY (idGrupo)
        REFERENCES Grupo(idGrupo)
);
GO