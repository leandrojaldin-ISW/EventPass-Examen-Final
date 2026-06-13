Create Database EventPassDB;
Go

USE EventPassDB;
GO

CREATE TABLE Eventos (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- IDENTITY hace que el ID sea autoincrementable
    Nombre NVARCHAR(100),
    Ubicacion NVARCHAR(100),
    PrecioEntrada FLOAT
);
GO


-- Insertamos un par de datos de prueba para que no esté vacía
INSERT INTO Eventos (Nombre, Ubicacion, PrecioEntrada) 
VALUES ('Concierto Rock', 'Campus UCB', 150.0),
       ('Feria de Tecnología', 'Pabellón A', 50.0);
GO