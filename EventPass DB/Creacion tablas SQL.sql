use EventPassDB

-- 1. Tabla Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL
);

-- 2. Tabla Eventos (Usando patrón TPH para la herencia)
CREATE TABLE Eventos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Fecha DATETIME NOT NULL,
    Ubicacion NVARCHAR(200) NOT NULL,
    IdOrganizador INT NOT NULL,
    
    -- Columnas para el Polimorfismo (Patrón TPH)
    TipoEvento NVARCHAR(50) NOT NULL, -- Guardará 'Gratuito' o 'Pagado'
    PrecioEntrada DECIMAL(18,2) NULL, -- Se llena solo si es Pagado
    RequiereRegistroPrevio BIT NULL,  -- Se llena solo si es Gratuito

    FOREIGN KEY (IdOrganizador) REFERENCES Usuarios(Id)
);

-- 3. Tabla Inscripciones
CREATE TABLE Inscripciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdEvento INT NOT NULL,
    CantidadPersonas INT NOT NULL,
    TotalPagado DECIMAL(18,2) NOT NULL, -- Aquí guardaremos el cálculo del polimorfismo
    
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id),
    FOREIGN KEY (IdEvento) REFERENCES Eventos(Id)
);

-- 4. Tabla Comentarios
CREATE TABLE Comentarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,  --para que los ids se generes automaticamente
    Texto NVARCHAR(500) NOT NULL,
    Autor NVARCHAR(100) NOT NULL,
    IdEvento INT NOT NULL,
    
    FOREIGN KEY (IdEvento) REFERENCES Eventos(Id)
);

Select * from Eventos
