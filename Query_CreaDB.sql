CREATE DATABASE PizzeriaInForno;
GO

USE PizzeriaInForno;
GO

CREATE TABLE Utenti (
    IDUtente INT IDENTITY NOT NULL PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    IsAdmin BIT,
    Nome NVARCHAR(50) NOT NULL,
    Cognome NVARCHAR(50) NOT NULL,
    Email NVARCHAR(MAX),
    Tel NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE Prodotti (
    IDProdotto INT IDENTITY NOT NULL PRIMARY KEY,
    NomeProd NVARCHAR(100) NOT NULL,
    Foto NVARCHAR(MAX),
    Prezzo DECIMAL(10, 2) NOT NULL,
    ConsMin INT NOT NULL,
    Ingredienti NVARCHAR(MAX)
);
GO

CREATE TABLE Ordini (
    IDOrdine INT IDENTITY NOT NULL PRIMARY KEY,
    FK_IDUtente INT,
    DataOrdine DATETIME,
    Indirizzo NVARCHAR(MAX) NOT NULL,
    Note NVARCHAR(MAX),
    Evaso BIT,
    CONSTRAINT FK_Ordini_Utenti FOREIGN KEY (FK_IDUtente)
    REFERENCES Utenti (IDUtente)
);
GO

CREATE TABLE DettagliOrdini (
    IDDettaglioOrd INT IDENTITY NOT NULL PRIMARY KEY,
    FK_IDOrdine INT,
    FK_IDProdotto INT,
    Quantita INT NOT NULL,
    CONSTRAINT FK_DettagliOrdini_Ordini FOREIGN KEY (FK_IDOrdine)
    REFERENCES Ordini (IDOrdine),
    CONSTRAINT FK_DettagliOrdini_Prodotti FOREIGN KEY (FK_IDProdotto)
    REFERENCES Prodotti (IDProdotto)
);
GO
