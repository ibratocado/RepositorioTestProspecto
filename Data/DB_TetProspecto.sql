IF EXISTS (SELECT * FROM sysdatabase WHERE (name = 'DB_TestProspecto')) 
BEGIN
	drop database DB_TestProspecto;
END
ELSE
	create database DB_TestProspecto;

GO
USE DB_TestProspectos;
GO	

CREATE TABLE prospecto (
	Id VARCHAR(50) PRIMARY KEY,
	Nombre VARCHAR(20),
	PrimerApellido VARCHAR(20),
	SegundoApellido VARCHAR(20),
	Calle VARCHAR(20),
	Numero INT,
	Colonia VARCHAR(20),
	CodigoPostal CHAR(7),
	Teléfono VARCHAR(12),
	RFC CHAR(19)
	);

CREATE TABLE documents (
	Id VARCHAR(50) PRIMARY KEY,
	IdProspecto VARCHAR(50),
	NameDocument VARCHAR(20),
	Link VARCHAR(50),
	FOREIGN KEY (IdProspecto) REFERENCES prospecto(Id) ON UPDATE CASCADE ON DELETE CASCADE
	);

CREATE TABLE statusSolicitud(
	Id INT PRIMARY KEY IDENTITY(1,1),
	NameStatus VARCHAR(12)
	);

CREATE TABLE solicitudProspecto(
	Id VARCHAR(50) PRIMARY KEY,
	IdProspecto VARCHAR(50),
	StatusSolicitud INT, 
	FOREIGN KEY (IdProspecto) REFERENCES prospecto(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (StatusSolicitud) REFERENCES statusSolicitud(Id) ON UPDATE CASCADE ON DELETE CASCADE
	);

INSERT INTO statusSolicitud (NameStatus) VALUES
	('Autorizado'),
	('Rechazado'),
	('Enviado');

GO
CREATE OR ALTER PROCEDURE InsertProspecto(
	@Id VARCHAR(50),
	@Nombre VARCHAR(20),
	@PrimerApellido VARCHAR(20),
	@SegundoApellido VARCHAR(20),
	@Calle VARCHAR(20),
	@Numero INT,
	@Colonia VARCHAR(20),
	@CodigoPostal CHAR(7),
	@Telefono VARCHAR(12),
	@RFC CHAR(19),
	@IdSolicitud VARCHAR(50))
AS
	BEGIN
	INSERT INTO prospecto (Id,Nombre,PrimerApellido,SegundoApellido,Calle,Numero,Colonia,CodigoPostal,Teléfono,RFC) VALUES
	(@Id,@Nombre,@PrimerApellido,@SegundoApellido,@Calle,@Numero,@Colonia,@CodigoPostal,@Telefono,@RFC);
	INSERT INTO solicitudProspecto (Id,IdProspecto,StatusSolicitud) VALUES
	(@IdSolicitud,@Id,3);
	END
GO
CREATE OR ALTER PROCEDURE UpdateStateProspecto(@Id Varchar(50),@State INT)
AS
	UPDATE solicitudProspecto SET 
		StatusSolicitud = @State
		WHERE IdProspecto = @Id;

GO
CREATE PROCEDURE GetFullStatusSolicitud
AS
	SELECT * FROM statusSolicitud;
	RETURN;
GO
CREATE OR ALTER PROCEDURE GetExistRFC(@RFC CHAR(19))
AS
	SELECT RFC FROM prospecto
		WHERE RFC = @RFC;
	RETURN;
GO
CREATE OR ALTER PROCEDURE GetExistProspecto(@Id VARCHAR(50))
AS
	SELECT Id FROM prospecto
		WHERE Id = @Id;
	RETURN;
GO
ALTER PROCEDURE GetProspectosForState(@Status INT)
AS
	SELECT prospecto.Id,
			prospecto.Nombre,
			prospecto.PrimerApellido,
			prospecto.SegundoApellido,
			prospecto.Calle,
			prospecto.Numero,
			prospecto.Colonia,
			prospecto.CodigoPostal,
			prospecto.Teléfono,
			prospecto.RFC,
			statusSolicitud.Id,
			statusSolicitud.NameStatus
			FROM prospecto 
	join solicitudProspecto 
	on
	prospecto.Id = solicitudProspecto.IdProspecto
	join statusSolicitud
	on solicitudProspecto.StatusSolicitud = statusSolicitud.Id
	where solicitudProspecto.StatusSolicitud = @Status;
	RETURN;
GO
ALTER PROCEDURE GetProspectoForId(@Id Varchar(50))
AS
	SELECT prospecto.Id,
			prospecto.Nombre,
			prospecto.PrimerApellido,
			prospecto.SegundoApellido,
			prospecto.Calle,
			prospecto.Numero,
			prospecto.Colonia,
			prospecto.CodigoPostal,
			prospecto.Teléfono,
			prospecto.RFC,
			solicitudProspecto.StatusSolicitud,
			statusSolicitud.NameStatus
	FROM prospecto 
	join solicitudProspecto 
	on
	prospecto.Id = solicitudProspecto.IdProspecto
	join statusSolicitud
	on solicitudProspecto.StatusSolicitud = statusSolicitud.Id
	where prospecto.Id = @Id
	RETURN;
GO
CREATE OR ALTER PROCEDURE GetDocumentsByProspecto(@IdProspecto VARCHAR(50))
AS
	SELECT
		documents.Id,
		documents.IdProspecto,
		documents.NameDocument,
		documents.Link
		FROM documents 
		JOIN prospecto
		ON prospecto.Id = documents.IdProspecto
		WHERE prospecto.Id = @IdProspecto;
		RETURN;
GO
CREATE OR ALTER PROCEDURE InsertDocumentsByProspecto(
@Id VARCHAR(50),
@IdProspecto VARCHAR(50),
@Name VARCHAR(20),
@Link VARCHAR(50))
AS
	INSERT INTO documents (Id,IdProspecto,NameDocument,Link) VALUES
		(@Id,@IdProspecto,@Name,@Link);


exec GetProspectosForState 3;

--INSERT INTO prospecto (Nombre,PrimerApellido,SegundoApellido,Calle,Numero,Colonia,CodigoPostal,Teléfono,RFC) VALUES
--	('Juan','Perez','Rodriguez','Manzana',123,'Pedregal','56615','555988179534','PRRJ100219973J8'),
--	('Maria','Rosales','Maracana','Pera',134,'Pedregal','56619','555988909534','RSMM100219973J8');


ALTER LOGIN sa ENABLE ;  
GO  
ALTER LOGIN sa WITH PASSWORD = '12345sa' ;  
GO  