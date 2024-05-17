
CREATE DATABASE IzlozbaPasa3;
GO

USE IzlozbaPasa3;
GO

CREATE TABLE Kategorija (
    KategorijaID INT PRIMARY KEY,
    Naziv VARCHAR(50)
    );

CREATE TABLE Izlozba (
    IzlozbaID CHAR(9) PRIMARY KEY,
    Mesto VARCHAR(25),
    Datum DATE
    );

CREATE TABLE Duzina_Dlake (
    DDID INT PRIMARY KEY,
    Duzina VARCHAR(25)
    );

CREATE TABLE Boja (
    BojaID INT PRIMARY KEY,
    Boja VARCHAR(25)
    );

CREATE TABLE Rasa (
    RasaID INT PRIMARY KEY,
    NazivRase VARCHAR(25)
    );
CREATE TABLE Pol (
	PolID INT PRIMARY KEY,
	Pol VARCHAR(25)
	);

CREATE TABLE Vlasnik (
	VlasnikID INT PRIMARY KEY,
	Ime VARCHAR(25),
	Prezime VARCHAR(25),
	Adresa VARCHAR(255),
	JMBG CHAR(13)
	);
GO

CREATE TABLE Pas (
    PasID INT PRIMARY KEY,
    Ime VARCHAR(25),
    Tezina decimal(5,2),
    DDID INT,
    BojaID INT,
    RasaID INT,
    Ostenjen VARCHAR(25),
    VlasnikID INT,
    PolID INT,
    CONSTRAINT fk_Pas_Dlaka FOREIGN KEY (DDID) REFERENCES duzina_dlake(DDID),
    CONSTRAINT fk_Pas_Boja FOREIGN KEY (BojaID) REFERENCES boja(BojaID),
    CONSTRAINT fk_Pas_Rasa FOREIGN KEY (RasaID) REFERENCES rasa(RasaID),
    CONSTRAINT fk_Pas_Vlasnik FOREIGN KEY (VlasnikID) REFERENCES Vlasnik(VlasnikID),
    CONSTRAINT fk_Pas_Pol FOREIGN KEY (PolID) REFERENCES Pol(PolID)
    );
GO

CREATE TABLE Rezultat (
    IzlozbaID CHAR(9), 
    KategorijaID INT,
    PasID INT,
    Rezultat INT, 
    Napomena VARCHAR(255),
    CONSTRAINT pk_Rezultat PRIMARY KEY (IzlozbaID, KategorijaID, PasID), 
    CONSTRAINT fk_Rezultat_Kategorija FOREIGN KEY (KategorijaID) REFERENCES kategorija(KategorijaID),
    CONSTRAINT fk_Rezultat_Pas FOREIGN KEY (PasID) REFERENCES Pas(PasID),
    CONSTRAINT FK_REZULTAT_IZLOZBA FOREIGN KEY (IzlozbaID) REFERENCES izlozba(IzlozbaID)
    );
    

GO

CREATE TABLE Prijava (
    PrijavaID INT  IDENTITY(1,1),
    PasID INT NOT NULL,
    IzlozbaID CHAR(9) NOT NULL,
    KategorijaID INT NOT NULL,
    CONSTRAINT fk_Prijava_Pas FOREIGN KEY (PasID) REFERENCES Pas(PasID),
    CONSTRAINT fk_Prijava_Izlozba FOREIGN KEY (IzlozbaID) REFERENCES Izlozba(IzlozbaID),
    CONSTRAINT fk_Prijava_Kategorija FOREIGN KEY (KategorijaID) REFERENCES Kategorija(KategorijaID)
     
);

GO

INSERT INTO boja (BojaID, Boja) 
	VALUES (1, 'Bela'), (2, 'crna'),(3, 'braon'),(4, 'siva'), (5, 'zuta'),
	(6, 'crno-bela'), (7, 'braon-bela');

INSERT INTO duzina_dlake (DDID, Duzina) VALUES 
	(1, 'kratka'), (2, 'srednja'),(3, 'duga');

INSERT INTO rasa (RasaID, NazivRase) 
	VALUES (1, 'Dalmatinac'), (2, 'Pudlica'),(3, 'Staford'), 
		(4, 'Pit Bul'),(5, 'Doberman'), (6, 'mesanac');

INSERT INTO Pol (PolID, Pol) 
	VALUES (1, 'muzjak'), (2, 'zenka');

GO

INSERT INTO izlozba (IzlozbaID, Mesto, Datum) 
         VALUES ('BEO042021', 'Beograd', '2021-04-14'),
         ('BEO032021', 'Beograd', '2021-03-09'), 
         ('NS032021', 'Novi Sad', '2021-03-20'),
         ('JAG022020', 'Jagodina', '2020-02-02'), 
         ('JAG032021', 'Jagodina', '2021-03-03'),
         ('ZRE012021', 'Zrenjanin', '2021-01-10'), 
         ('ZRE122020', 'Zrenjanin', '2020-12-12');
INSERT INTO izlozba (IzlozbaID, Mesto, Datum) 
         VALUES ('NIS082015', 'Nis', '2022-12-03');
INSERT INTO izlozba (IzlozbaID, Mesto, Datum) 
         VALUES ('BEO102015', 'Beograd', '2018-02-05');
				
INSERT INTO izlozba (IzlozbaID, Mesto, Datum) 
         VALUES ('BEO102023', 'Beograd', '2023-02-05'),
				('NS082022', 'Novi Sad', '2022-08-08'),
				('NS102022', 'Novi Sad', '2022-10-10'),
				('JAG082028', 'Jagodina', '2022-08-08'),
				('ZRE092022', 'Zrenjanin', '2021-09-09'),
				 ('BEO102022', 'Beograd', '2022-10-10');
				 
INSERT INTO kategorija (KategorijaID, Naziv) VALUES (1, 'Mladi'),
	(3, 'Srednji'), (4, 'Stariji'),(2, 'CACIB'), (5, 'Sampion mladih');

GO

INSERT INTO Vlasnik(VlasnikID, Ime, Prezime, Adresa, JMBG) VALUES
	(1, 'Pera', 'Markovic', 'Vladike X', '0123654789654'),
	(2, 'Ana', 'Panic', 'Simina 3', '1236546546546'),
	(3, 'Gavrilo', 'Matic', 'Titova 5', '0101123123123'),
	(4, 'Sanja', 'Delkovic', 'Praska ss', '1231231231231'),
	(5, 'Tito', 'Prvi', 'Vrbas bb', '0101123123123'),
	(6, 'Petar', 'Pan', 'Nedodjija', '0202002123123'),
	(7, 'Vendi', 'Darling', 'Nepoznata adresa', '0505055123123'),
	(8, 'Sima', 'Matavulj', 'Kraljevo bb', '0303321321321'),
	(9, 'Ilija', 'Panini', 'Crvenka 123', '0606156156156'),
	(10, 'Marija', 'Danini', 'Kucura bb', '1212123123123');
GO

INSERT INTO pas (PasID, Ime, Tezina, DDID, BojaID, RasaID, Ostenjen,VlasnikID,PolID) 
   VALUES 
   (1, 'Alice', '2.23', '3', '1', '2', NULL,1,1),
   (2, 'Betty', '23.35', '1', '6', '1', NULL,2,1),
   (3, 'Ginna', '45.45', '2', '2', '3', NULL,3,2),
   (4, 'Teuta', '15.15', '3', '3', '5', NULL,4,2),
   (5, 'Zimi', '50.05', '2', '7', '6', NULL,5,1),
   (6, 'Reks', '40.2', '1', '1', '4', NULL,6,2),
   (7, 'Maza', '7.7', '3', '1', '2', NULL,7,1),
   (8, 'Lunja', '25.2', '2', '3', '6', NULL,8,2);

INSERT INTO pas (PasID, Ime, Tezina, DDID, BojaID, RasaID, Ostenjen,VlasnikID,PolID) 
   VALUES 
   (9, 'Miki', '3.23', '3', '1', '2', NULL,10,1),   
   (10, 'Paja', '3.23', '3', '1', '2', NULL,10,1),
   (11, 'Buca', '4.23', '3', '1', '2', NULL,9,1),
   (12, 'Bibi', '4.35', '1', '6', '1', NULL,8,1),
   (13, 'Gaga', '5.45', '2', '2', '3', NULL,7,2),
   (14, 'Tiki', '6.15', '3', '3', '5', NULL,4,2),
   (15, 'Silvester', '7.05', '2', '7', '6', NULL,5,1),
   (16, 'Reks', '8.2', '1', '1', '4', NULL,6,2),
   (17, 'Munja', '9.7', '3', '1', '2', NULL,7,1),
   (18, 'Liki', '15.2', '2', '3', '6', NULL,8,2),
   (19, 'Alili', '12.23', '3', '1', '2', NULL,10,1);
GO

INSERT INTO pas (PasID, Ime, Tezina, DDID, BojaID, RasaID, Ostenjen,VlasnikID,PolID) 
   VALUES 
   (20, 'Pipi', '3.23', '3', '1', '2', NULL,10,1),
   (21, 'Bubi', '4.23', '3', '1', '2', NULL,9,1),
   (22, 'Bici', '4.35', '1', '6', '1', NULL,8,1),
   (23, 'Gasa', '5.45', '2', '2', '3', NULL,7,2),
   (24, 'Tibi', '6.15', '3', '3', '5', NULL,4,2),
   (25, 'Siki', '7.05', '2', '7', '6', NULL,5,1),
   (26, 'Rege', '8.2', '1', '1', '4', NULL,6,2),
   (27, 'Mumu', '9.7', '3', '1', '2', NULL,7,1),
   (28, 'Lulu', '15.2', '2', '3', '6', NULL,8,2),
   (29, 'Aula', '12.23', '3', '1', '2', NULL,10,1);
GO
INSERT INTO pas (PasID, Ime, Tezina, DDID, BojaID, RasaID, Ostenjen,VlasnikID,PolID) 
   VALUES 
   (30, 'Pip', '3.23', '3', '1', '2', NULL,10,1),
   (31, 'Buc', '4.23', '3', '1', '2', NULL,9,1),
   (32, 'Bib', '4.35', '1', '6', '1', NULL,8,1),
   (33, 'Gak', '5.45', '2', '2', '3', NULL,7,2),
   (34, 'Tik', '6.15', '3', '3', '5', NULL,4,2),
   (35, 'Sic', '7.05', '2', '7', '6', NULL,5,1),
   (36, 'Reg', '8.2', '1', '1', '4', NULL,6,2),
   (37, 'Mun', '9.7', '3', '1', '2', NULL,7,1),
   (38, 'Luk', '15.2', '2', '3', '6', NULL,8,2),
   (39, 'Auk', '12.23', '3', '1', '2', NULL,10,1);
GO


INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('BEO032021', '2', '1', '1');
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('BEO042021', '1', '1', '2');
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('JAG022020', '2', '1', '3');
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('JAG032021', '2', '1', '4');
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('JAG032021', '5', '2', '1');
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('BEO032021', '2', '3', '2');
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('BEO032021', '1', '2', '3');
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('NS032021', '4', '5', '1');
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('NS032021', '4', '6', '5');
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES ('BEO032021', '2', '8', '2');

GO

INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES 
	('BEO102015', '1', '1', '2'),('BEO102015', '1', '6', '2'),
	('BEO102015', '1', '11', '2'),
	('BEO102015', '1', '2', '2'),('BEO102015', '1', '7', '2'),
	('BEO102015', '1', '3', '2'),('BEO102015', '1', '8', '2'),
	('BEO102015', '1', '4', '2'),('BEO102015', '1', '9', '2'),
	('BEO102015', '1', '5', '2'),('BEO102015', '1', '10', '2');
	
	INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES 
	('BEO102015', '2', '27', '2'),('BEO102015', '2', '6', '2'),
	('BEO102015', '2', '11', '2'),('BEO102015', '2', '16', '2'),
	('BEO102015', '2', '12', '2'),('BEO102015', '2', '17', '2'),
	('BEO102015', '2', '13', '2'),('BEO102015', '2', '18', '2'),
	('BEO102015', '2', '14', '2'),('BEO102015', '2', '19', '2'),
	('BEO102015', '2', '15', '2'),('BEO102015', '2', '20', '2'),
	('BEO102015', '2', '26', '2');
	
	INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES 
	('BEO102015', '3', '37', '2'),('BEO102015', '3', '16', '2'),
	('BEO102015', '3', '11', '2'),('BEO102015', '3', '26', '2'),
	('BEO102015', '3', '12', '2'),('BEO102015', '3', '27', '2'),
	('BEO102015', '3', '13', '2'),('BEO102015', '3', '28', '2'),
	('BEO102015', '3', '14', '2'),('BEO102015', '3', '29', '2'),
	('BEO102015', '3', '15', '2'),('BEO102015', '3', '20', '2'),
	('BEO102015', '3', '21', '2'),('BEO102015', '3', '30', '2');

	INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES 
	('BEO102015', '4', '7', '2'),('BEO102015', '4', '13', '2'),
	('BEO102015', '4', '8', '2'),('BEO102015', '4', '14', '2'),
	('BEO102015', '4', '9', '2'),('BEO102015', '4', '15', '2'),
	('BEO102015', '4', '10', '2'),('BEO102015', '4', '16', '2'),
	('BEO102015', '4', '11', '2'),('BEO102015', '4', '17', '2'),
	('BEO102015', '4', '12', '2'),('BEO102015', '4', '18', '2');
	
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID, Rezultat) VALUES 
	('BEO102015', '5', '17', '2'),('BEO102015', '5', '23', '2'),
	('BEO102015', '5', '18', '2'),('BEO102015', '5', '24', '2'),
	('BEO102015', '5', '19', '2'),('BEO102015', '5', '25', '2'),
	('BEO102015', '5', '11', '2'),('BEO102015', '5', '26', '2'),
	('BEO102015', '5', '12', '2'),('BEO102015', '5', '27', '2'),
	('BEO102015', '5', '13', '2');
	
INSERT INTO rezultat (IzlozbaID, KategorijaID, PasID) VALUES 
	('BEO102015', '1', '30'),('BEO102015', '2', '30'),
	('BEO102015', '3', '1'),('BEO102015', '4', '2'),
	('BEO102015', '5', '1'),('BEO102015', '5', '2');	