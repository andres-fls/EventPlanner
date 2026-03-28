-- 1. Tabla de Usuarios (Seguridad)
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario VARCHAR(50) UNIQUE NOT NULL,
    PasswordUsuario VARCHAR(255) NOT NULL,
    RolUsuario VARCHAR(20) NOT NULL -- 'Admin', 'Aprendiz'
);

-- 2. Tabla de Programas de Formación
CREATE TABLE Programa (
    IdPrograma INT PRIMARY KEY IDENTITY(1,1),
    CodigoPrograma INT UNIQUE NOT NULL,
    NombrePrograma VARCHAR(100) NOT NULL,
    DuracionPrograma VARCHAR(50),
    NivelPrograma VARCHAR(50)
);

-- 3. Tabla de Fichas (Relación Maestra con Programa)
CREATE TABLE Ficha (
    CodigoFicha INT PRIMARY KEY,
    IdPrograma INT NOT NULL,
    FOREIGN KEY (IdPrograma) REFERENCES Programa(IdPrograma)
);

-- 4. Tabla de Grupos (Para modalidades por equipo)
CREATE TABLE Grupo (
    IdGrupo INT PRIMARY KEY IDENTITY(1,1),
    NombreGrupo VARCHAR(50) NOT NULL
);

-- 5. Tabla de Aprendices
CREATE TABLE Aprendiz (
    IdAprendiz INT PRIMARY KEY IDENTITY(1,1),
    CedulaAprendiz VARCHAR(10) UNIQUE NOT NULL,
    NombreAprendiz VARCHAR(100) NOT NULL,
    EdadAprendiz INT,
    GeneroAprendiz VARCHAR(10),
    CorreoAprendiz VARCHAR(100),
    TelefonoAprendiz VARCHAR(10),
    CodigoFicha INT NOT NULL,
    IdUsuario INT NOT NULL,
    FOREIGN KEY (CodigoFicha) REFERENCES Ficha(CodigoFicha),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- 6. Tabla de Eventos
CREATE TABLE Evento (
    IdEvento INT PRIMARY KEY IDENTITY(1,1),
    NombreEvento VARCHAR(100) NOT NULL,
    TipoEvento VARCHAR(50) NOT NULL, -- Academico, Deportivo, Cultural
    LugarEvento VARCHAR(100),
    DescripcionEvento TEXT,
    FechaInicioEvento DATETIME NOT NULL,
    FechaFinEvento DATETIME NOT NULL,
    FechaInicioInscripcion DATETIME NOT NULL,
    FechaFinInscripcion DATETIME NOT NULL,
    CupoMaximo INT DEFAULT 0,
    Activo BIT DEFAULT 1,
    IdUsuarioCreador INT,
    FOREIGN KEY (IdUsuarioCreador) REFERENCES Usuario(IdUsuario)
);

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
