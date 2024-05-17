CREATE DATABASE Golf;
GO

USE Golf;

CREATE TABLE Grad (
      GradID INT PRIMARY KEY, 
      Grad VARCHAR(50),
      PozivniBroj VARCHAR(3),
      PostanskiBroj INT, 
      BrojStanovnika INT
);
GO

CREATE TABLE Igrac (
      IgracID INT PRIMARY KEY IDENTITY,
      Ime VARCHAR(50),
      Prezime VARCHAR(50),
      Adresa VARCHAR(50),
      GradID INT,
      Email VARCHAR(50),
      Telefon VARCHAR(50),
      CONSTRAINT FK_Igrac_Grad FOREIGN KEY (GradID) REFERENCES Grad(GradID)
	 
);

CREATE TABLE Teren (
     TerenID INT PRIMARY KEY, 
     Teren VARCHAR(50),
     Adresa VARCHAR(50),
     GradID INT,
     KontaktTelefon VARCHAR(50),
     CONSTRAINT FK_Teren_Grad FOREIGN KEY (GradID) REFERENCES Grad(GradID)
	  
);

GO

CREATE TABLE Partija (
       PartijaID INT PRIMARY KEY,
       TerenID INT,
       Datum DATE DEFAULT NULL,
       VremePocetka TIME,
       VremeZavrsetka TIME,
       
       CONSTRAINT FK_Partija_Teren FOREIGN KEY (TerenID) REFERENCES Teren(TerenID)
	   ON DELETE CASCADE
);
GO




CREATE TABLE Igraju(
	PartijaID INT,
	IgracID INT,
	CONSTRAINT PK_Igraju PRIMARY KEY (PartijaID,IgracID),
	CONSTRAINT FK_Partija_Igraju FOREIGN KEY (PartijaID) REFERENCES Partija(PartijaID)
	ON DELETE CASCADE,
	CONSTRAINT FK_Igrac_Igraju FOREIGN KEY (IgracID) REFERENCES Igrac(IgracID) 
);

CREATE TABLE Rupa(
	RupaID INT PRIMARY KEY,
	RupaRB INT,
	TerenID INT,
	Opis VARCHAR(255),
	UdaljenostRupe INT,
	UdaracaPoRupi INT,
	CONSTRAINT FK_Rupa_Teren FOREIGN KEY (TerenID) REFERENCES Teren(TerenID)
	ON DELETE CASCADE
);
GO

CREATE TABLE Rezultat(
	RezultatID INT PRIMARY KEY,
	IgracID INT,
	PartijaID INT,
	RupaID INT,
	CONSTRAINT FK_Partija_Rezultat FOREIGN KEY (PartijaID) REFERENCES Partija(PartijaID)
	ON DELETE CASCADE,
	CONSTRAINT FK_Igrac_Rezultat FOREIGN KEY (IgracID) REFERENCES Igrac(IgracID) ON DELETE NO ACTION,
	CONSTRAINT FK_Igrac_Rupa FOREIGN KEY (RupaID) REFERENCES Rupa(RupaID)
	ON DELETE NO ACTION
);
GO

INSERT INTO Grad (GradID, Grad,PozivniBroj,PostanskiBroj,BrojStanovnika)
VALUES (1, 'Fremont','096',123456,123545),
		(2, 'Boston', '097', 97777, 977899),
		(3, 'Jersey', '099', 99123, 1234567),
		(4, 'Lubbock', '066', 69123, 634567),
		(5, 'Chicago', '098', 98123, 9834567),
		(6, 'Newark', '078', 78123, 734568),
		(7, 'Columbus', '076', 76126, 734566),
		(8, 'Jacksonville', '088', 88123, 834568),
		(9, 'Nashville', '079', 79127, 734507),
		(10, 'Dallas', '056', 56123, 563455);
		
INSERT INTO Grad (GradID, Grad,PozivniBroj,PostanskiBroj,BrojStanovnika)
VALUES	(14, 'Liverpool','011',423454,435445),
		(16, 'Preston','023',523456,523545),
		(20, 'Watford','045',623456,623546),
		(23, 'Gloucester','065',723456,723547),
		(24, 'Oxford','086',823486,1823548),
		(27, 'Nottingham','056',923456,923549),
		(28, 'Sheffield','089',103450,1023540),
		(35, 'Poole','022',623456,1623546),
		(36, 'Colchester','033',723456,1723547),
		(39, 'St Helens','044',823488,1823548),
		(41, 'Northempton','055',923496,92349),
		(43, 'Blackburn','066',103406,103500),
		(48, 'Wolverhampton','077',100056,1023505),
		(50, 'Bristol','013',104956,1023545);
		
	

INSERT INTO Teren (TerenID,Teren,Adresa,GradID,KontaktTelefon)
     	VALUES 
     	(1,'Hanover Golf Club','Tampa 741',1,'096-925-5372'),
     	(2,'Abilene Country Club','Mesa 84',2,'097-271-7108'),
     	(3,'Strawberry Valley Golf Club','Arlington 779',3,'099-748-5132'),
     	(4,'Cedar River Country Club','New York 451',4,'066-276-3907'),
     	(5,'Boulder Creek Golf Club','Dallas 772',5,'098-702-7351'),
     	(6,'The Wetlands Golf Club','Philadelphia 18',4,'086-487-6289'),
     	(7,'Grays Harbor Golf Club','Jacksonville 84',6,'078-504-7929'),
     	(8,'The Bridges Golf Club','Raleigh 721',7,'076-994-6027'),
     	(9,'Ruggles Golf Club','Lubbock 77',8,'088-201-9604'),
     	(10,'Silver Lake Country Club','El Paso 83',9,'079-823-2141');
     	
     INSERT INTO Teren (TerenID,Teren,Adresa,GradID,KontaktTelefon)
     	VALUES 
     	(15, 'Hearth of the Valey Golf Club', 'Tampa 521', 10,'096-945-5356'),
     	(16, 'Hazard Creek Golf Club', 'Abilene 56', 16,'076-745-7357'),
     	(17, 'Brookstone Country Club', 'Cedar 23', 2,'086-848-8386'),
     	(18, 'Forest Park Country Club', 'Silver Lake 456', 24,'093-345-3336'),
     	(19, 'Governors Towne Club' , 'Bude 78 ', 41,'097-747-7357'),
     	(25, 'High Meadows Golf Club', 'Abilene 123', 28,'036-343-3353'),
     	(26, 'Maxwell Muncipal Golf Course', 'El Paso 566', 23,'099-999-5959'),
     	(28, 'Adrian Golf Course', 'Lubbock 11 ', 14,'097-747-7357'),
     	(32, 'Diamondback Golf Club', 'Denbigh 6 ', 14,'016-141-1310');
     	
     		
     	
INSERT INTO Igrac (Ime,Prezime,Adresa,GradID,Email,Telefon)
      VALUES
      ('Nicole', 'Bartlett', 'Plymoth, Bude 78', 41, 'taeuuy@yahoo.com', '068-264-4016' ),
      ('Christine', 'Rubio', 'Liverpool, Banff 1', 28, 'vavgr.tohxn@mail.com', '079-855-6508' ),
      ('Pablo', 'Werner', 'St Helens, Ulapool 678', 28, 'guubm.jkqo@yahoo.com', '067-064-4824' ),
      ('Darnell', 'Calderon', 'Preston, Lochgoilhead 5', 23, 'wslzl@outlook.org', '078-284-9875' ),
      ('Desiree', 'Farmer', 'St Helens, Denbigh 6', 14, 'mldg.qlerx@mail.com', '076-134-9872' ),
      ('Holly', 'Fernandez', 'Watford, Forest Hill 912', 24, 'tgwx.hhyle@gmail.com', '086-631-0505' ),
      ('Chadwick', 'Trevino', 'Bradford, Eyemouth 3', 24, 'bpov@gmail.net', '099-128-2816' ),
      ('Julie', 'Woreno', 'Peterborough, Craven Arms 732', 2, 'ynxad.jbog@gmail.net', NULL ),
      ('Daphne', 'Dudley', 'Bristol, Waltham Abbey 754', 50, 'zebd@yahoo.com', '088-304-5567' ),
      ('Jean BBB', 'Trevino', 'Wolverhampton, Rhosneigr 59', 16, 'lbmc0@hotmail.net', '086-785-2504' ),
      ('Sonya', 'Ryan', 'Peterborough, Mablethorpe 7', 43, 'xkjt2@gmail.org', '086-999-4421' ),
      ('Lindsay', 'Conner', 'Rotherham, Huddersfield 84', 2, 'itivp@mail.net', '098-169-1104' ),
      ('Katina', 'Archer', 'Gloucester, Bridport 586', 27, 'gjvlyq@outlook.org', '076-383-4177' ),
      ('Wendi', 'Hanson', 'Colchester, Witney 5', 35, 'asln@yahoo.com', '087-533-3087' ),
      ('Kari', 'Hunter', 'Oxford, North End 541', 5, 'uxiam@hotmail.net', '086-672-5666' ),
      ('Chadwick', 'Velazquez', 'Nottingham, Bicester 423', 23, 'uhua@hotmail.com', '089-037-2714' ),
      ('Seth', 'Gordon', 'Northampton, Littlehampton 579', 50, 'noyvq.icvx@yahoo.com', '079-871-4208' ),
      ('Warren', 'West', 'Poole, Tresco 7', 39, 'zfszu.nfxejo@outlook.com', '097-951-0768' ),
      ('Bernard', 'Rowe', 'Blackburn, Sydenham 99', 20, 'ucgw@mail.org', '097-325-3632' ),
      ('Shelby', 'Bradford', 'Oxford, Swaffham 4', 48, 'mcdlc@mail.org', '077-561-6172' ),
      ('Devon', 'Henry', 'Nottingham, Stansted 934', 36, 'uuwy@gmail.com', '068-594-6941' ),
      ('Tonia', 'Houston', 'Sheffield, Menai Bridge 6', 14, 'kqsdlj@outlook.org', '068-744-4084' );
      
      
INSERT INTO Partija(PartijaID, TerenID, Datum, VremePocetka, VremeZavrsetka)
VALUES	(487, 4, '2014-11-21', '03:00:00', '18:43:00' ), 
		(8, 2, '2016-02-20', '05:00:00', '20:20:00'),
		(307, 16, '2015-09-17', '06:20:00', '21:37:00'),
		(212, 17, '2016-04-26', '04:10:00', '19:20:00');
		
		
INSERT INTO Partija(PartijaID, TerenID, Datum, VremePocetka, VremeZavrsetka)
VALUES	(1, 1, '2018-11-21', '03:00:00', '11:43:00' ), 
		(2, 3, '2018-06-06', '04:00:00', '10:43:00' ), 
		(3, 10, '2019-04-04', '05:00:00', '10:23:00' ), 
		(4, 15, '2018-08-08', '06:00:00', '11:33:00' ), 
		(5, 18, '2019-05-05', '07:10:00', '12:13:00' ),
		(6, 19, '2018-11-11', '04:00:00', '14:44:00' ), 
		(7, 25, '2018-10-20', '03:00:00', '13:03:00' ),  
		(9, 25, '2018-10-11', '03:00:00', '13:13:00' ),
		(10, 26, '2018-09-09', '03:00:00', '13:23:00' ),  
		(11, 28, '2018-09-07', '03:00:00', '13:33:00' ),
		(12, 32, '2018-07-21', '03:00:00', '13:53:00' );		