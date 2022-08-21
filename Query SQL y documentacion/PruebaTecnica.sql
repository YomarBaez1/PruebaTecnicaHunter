
-- Crear Base de Datos
create database PruebaTecnica

-- Crear tabla Actor con estos campos

CREATE TABLE Actor  
   (
   ActorID int IDENTITY (1,1) PRIMARY KEY NOT NULL,  
   NombreCompleto varchar(255) NOT NULL UNIQUE,  
   FechaNacimiento datetime,  
   Sexo varchar(255),
   PeliculaID int
   )

GO  



-- Crear tabla Pelicula con los siguientes datos
CREATE TABLE Pelicula  
   (PeliculaID int IDENTITY (1,1) PRIMARY KEY NOT NULL,  
   Titulo varchar(255) NOT NULL UNIQUE,  
   Genero varchar(255),
   FechaEstreno datetime )

   -- Crear tabla Pelicula Actor, esto se hace para juntar los datos de Actor y Pelicula y mostrarlos en detalles avanzados
   CREATE TABLE PeliculaActor (
   PeliculaActorID int IDENTITY (1,1) PRIMARY KEY NOT NULL,
   PeliculaID int not null,
   ActorID int Not null
   )





