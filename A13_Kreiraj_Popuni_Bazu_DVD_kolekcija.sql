CREATE DATABASE DVD_Kolekcija;
GO

USE DVD_Kolekcija;

CREATE TABLE Tip_Uloge (
	TipUlogeID INT PRIMARY KEY,
	Tip VARCHAR(50)
);

CREATE TABLE Glumac 
(
	GlumacID INT PRIMARY KEY,
	Ime VARCHAR(255),
	Prezime VARCHAR(255),
	DatumRodjenja DATE,
	MestoRodjenja VARCHAR(255)
);

CREATE TABLE Producent
(
	ProducentID INT PRIMARY KEY,
	Ime VARCHAR(255),
	Email VARCHAR(25),
	WebSite VARCHAR(255)
);

CREATE TABLE Zanr
(
	ZanrID INT PRIMARY KEY,
	NazivZanra VARCHAR(250)
);
GO

CREATE TABLE Film
(
	FilmID INT PRIMARY KEY,
	Naziv VARCHAR(50),
	DatumIzlaska DATE,
	Trajanje TIME,
	ZanrID INT,
	OpisRadnje VARCHAR(255),
    CONSTRAINT fk_film_zanr FOREIGN KEY (ZanrID) REFERENCES Zanr(ZanrID)
);
GO

CREATE TABLE Producirao
(
	FilmID INT,
	ProducentID INT,
	CONSTRAINT pk_producirao PRIMARY KEY (FilmID,ProducentID),
    CONSTRAINT fk_Producirao_Film FOREIGN KEY (FilmID) REFERENCES Film(FilmID),
    CONSTRAINT fk_Producirao_Producant FOREIGN KEY (ProducentID) REFERENCES Producent(ProducentID) 
);
CREATE TABLE Uloga
(
	FilmID INT,
	GlumacID INT,
	TipUlogeID INT,
	ImeLika VARCHAR(25),
	OpisLika VARCHAR(255),
	CONSTRAINT pk_uloga PRIMARY KEY (FilmID,GlumacID,ImeLika),
    CONSTRAINT fk_uloga_film FOREIGN KEY (FilmID) REFERENCES Film(FilmID),
    CONSTRAINT fk_uloga_glumac FOREIGN KEY (GlumacID) REFERENCES Glumac(GlumacID),
    CONSTRAINT fk_uloga_tipUl FOREIGN KEY (TipUlogeID) REFERENCES Tip_Uloge(TipUlogeID)
);
CREATE TABLE Nagrada
(
	NagradaID INT PRIMARY KEY,
	Naziv VARCHAR(50),
	GodinaPocetka DATE
);
GO

CREATE TABLE Film_Nagrada
(
	FilmID INT,
	NagradaID INT,
	GodinaDodele DATE,
	CONSTRAINT pk_film_nagrada PRIMARY KEY (FilmID,NagradaID),
	CONSTRAINT fk_FN_Film FOREIGN KEY (FilmID) REFERENCES Film(FilmID),
	CONSTRAINT fk_FN_Nagrada FOREIGN KEY (NagradaID) REFERENCES Nagrada(NagradaID)
);
GO


INSERT INTO tip_uloge(TipUlogeID, Tip)
 VALUES (1, 'Glavna'), (2, 'Sporedna'), (3, 'Dodatak');

INSERT INTO Glumac(GlumacID, Ime, Prezime, DatumRodjenja, MestoRodjenja)
 VALUES (1, 'Đorđe', 'Timotijev', null, null),
     	(2, 'Vladimir', 'Vladimirovič', '1966-10-12', 'SSSR'),
        (3, 'Johny', 'Deep', '1963-06-09', 'US'),
        (4, 'Vigo', 'Mortensen', '1958-10-20', 'New York'),  
        (5, 'Jovan', 'Jovanović', '1956-02-11', 'US'),
        (6, 'Natalie', 'Dormer', '1976-05-22', 'UK'),
        (7, 'Robert', 'De Niro', '1966-03-12', 'New York'),
        (8, 'Tom', 'Hanks', '1971-07-21', 'Francuska'),
        (9, 'Velimir', 'Živojinović', '1933-06-05', 'Kraljevina Jugoslavija'),
        (10, 'Jovan', 'Jovanović', '1936-12-02', 'Leskovac'),
        (111, 'John', 'Miler', '1984-02-08', 'New York'),
        (12, 'Sarah', 'Johnson', '1988-04-20', 'London, UK');


INSERT INTO producent(ProducentID, Ime, Email)
 VALUES (7, '20th Century Fox', 'filmjtk@outlook.net'), 
		(17, 'American International Pictures', 'kmcsnk21@hotmail.com'),
		(8, 'BBC Films', 'adresa@gmail.com'),
		(18, 'Capitol Films', 'ndcjds34@gmail.com'), 
		(19, 'Cinemation Industries', 'cdscejn@outlook.com'), 
		(20, 'Columbia TriStar Motion Picture Group', 'vbhj12@hotmail.com'),
		(9, 'DreamWorks', 'dschs@outlook.com'), 
		(6, 'Four Star Television','ummi7@mail.org'), 
		(5, 'ITC Entertainment Productions', 'itc@acme.com'),
		(4, 'MGM-British Studios', 'mgm@acme.com'),
		(3, 'Miramax Films', 'katarina@acme.com'),
		(2, 'MTV Productions', 'milan@acme.com'),
		(11, 'National Film Board of Canada', 'ntfb@can.com'),
		(1, 'Paramount Pictures', 'jovan@acme.com'),
 	    (12, 'Pinewood Studios', 'pine@wood.com'),
		(13, 'Screen Media Films', 'screen@us.com'),
		(14, 'Sony Pictures Classics', 'sony@us.com'),
		(15, 'Spyglass Entertainment', 'spy@studio.com'),
		(16, 'Triglav Film', 'triglav@ljubljana.com'),
		(10, 'Warner Bross', 'warner@bross.com');
 	

INSERT INTO zanr(ZanrID, NazivZanra)
	VALUES 	( 1, 'Drama'),
    		( 2, 'Krimi'),
            ( 3, 'Avantura'),
            ( 4, 'Akcija'),
            ( 5, 'Komedija'),
            ( 6, 'Fantazija');

INSERT INTO film (FilmID, Naziv, DatumIzlaska, Trajanje, ZanrID, OpisRadnje) 

VALUES ('3', 'Kum', '1972-04-15', '02:33:29', '1', 'Priča filma pokriva period od 10 godina (1945-1955) i prati italijansko-američku porodicu Korleone. '),
       ('4', 'Gospodar prstenova', '2006-12-17', '03:02:46', '6', 'filmski serijal od tri epska avanturistička filma u režiji Pitera Džeksona, zasnovana na istoimenom romanu Dž. R. R. Tolkina. '),
	   ('5', 'Valter brani Sarajevo', '1972-11-24', '02:22:32', '2', NULL),
	   ('6', 'Clash of Titans', '2012-04-14', '01:05:23', '4', NULL),
	   ('7', 'Pirates of the Carebbean', '2012-04-14', '2:17:03', '3', NULL),
	   ('61', 'The Rifleman', '1958-04-14', '0:30:03', 4, 'western, family'), 
		('62', 'The Big Valley ', '1965-04-14', '1:00:03', 4, 'western, family'),
		 ('68', 'Wanted: Dead or Alive  ', '1958-04-14', '0:30:03', 4, 'western, family'),
		 ('69', 'Stagecoach West ', '1960-04-14', '1:00:03', 4, 'western'),
		 ('60', 'Off to See the Wizard ', '1967-04-14', '1:00:03', 6, 'Animation, Family, Fantasy '),
		 ('71', 'Titan A.E.', '2000-06-16', '1:34:03', 6, ' Fantasy '),
		 ('72', 'X-Men', '2000-04-14', '1:00:03', 6, 'Fantasy '),
		 ('73', 'Kiss of the Dragon', '1967-04-14', '1:00:03', 4, 'Action'),
		 ('74', 'Ice Age', '2001-07-14', '1:00:03', 3, 'Animation, Family, Fantasy '),
		 ('75', 'Minority Report', '2002-04-14', '1:00:03', 1, 'Triler'),
		 ('76', 'The Transporter', '2002-04-14', '1:00:03', 2, 'Action, Family,'),
		 ('77', 'Solaris', '2002-11-11', '1:00:03', 6, 'Fantasy '),
		 ('81', 'Coriolanus', '2012-04-14', '1:00:03', 1, 'History '),
		 ('80', 'Dancer', '2017-04-14', '1:00:03', '3', 'Balet'),
		 ('82', 'Great Expectations', '2012-11-14', '1:00:03', 2, 'Dickens Classics'),
		 ('90', 'Antz', '1998-04-14', '1:00:03', 3, 'Animation, Family'),
		 ('91', 'The Prince of Egypt', '1998-04-14', '1:00:03', 3, 'Animation, Family'),
		 ('92', 'The Road to El Dorado', '1998-04-14', '1:00:03', 3, 'Animation, Family'),
		 ('93', 'Chicken Run', '1998-04-14', '1:00:03', 3, 'Animation, Family'),
		 ('94', 'Shrek', '1998-04-14', '1:00:03', 3, 'Animation, Family'),
		 ('95', 'Shark Tale', '1998-04-14', '1:00:03', 3, 'Animation, Family'),
		 ('96', 'Madagascar', '1998-04-14', '1:00:03', 5, 'Animation, Family'),
		 ('97', 'Bee Movie', '1998-04-14', '1:00:03', 5, 'Animation, Family'),
		 ('98', 'Kung Fu Panda', '1998-04-14', '1:00:03', 5, 'Animation, Family'),
		 ('99', 'Puss in Boots', '1998-04-14', '1:00:03', 5, 'Animation, Family'),
		 ('900', 'Rise of the Guardians', '1998-04-14', '1:00:03', 4, 'Animation, Family'),
		 ('906', 'Abominable', '1998-04-14', '1:00:03', 1, 'Animation, Family'),
		 ('916', 'Spirit Untamed', '1998-04-14', '1:00:03', 2, 'Animation, Family'),
		 ('170', 'Abby', '1974-04-14', '1:00:03', 3, 'Animation, Family, Fantasy '),
		 ('171', 'Apache Woman', '1955-04-14', '1:00:03', 4, 'Animation, Family, Fantasy '),
		 ('172', 'The Bat People', '1974-04-14', '1:00:03', 5, 'Blacula'),
		 ('173', 'Blacula', '1972-04-14', '1:00:03', 6, 'Blacula'),
		 ('174', 'Killer Force', '1976-04-14', '1:00:03', 1, 'Animation, Family, Fantasy '),
		 ('175', 'Erik the Conqueror', '1961-04-14', '1:00:03', 2, 'Animation, Family, Fantasy '),
		 ('176', 'Operation Bikini', '1963-04-14', '1:00:03', 3, 'Animation, Family, Fantasy '),
		 ('177', 'Our Man in Marrakesh', '1966-04-14', '1:00:03', 4, 'Animation, Family, Fantasy '),
		 ('18', 'Dok đavo ne sazna da si mrtav', '2007-04-14', '1:57:03', 5, 'Animation, Family, Fantasy '),
		 ('180', 'Alpha Dog', '2006-04-14', '2:00:03', 1, 'Biography, Crime, Drama '),
		 ('181', 'Jeepers Creepers ', '2001-04-14', '1:30:03', 1, 'Horror, Mystery '),
		 ('182', 'Gosford Park ', '2001-04-14', '2:17:03', 5, 'Comedy, Drama, Mystery '),
		 ('19', 'Fritz the Cat', '1972-04-14', '1:17:03', 5, 'Animation, Comedy, Drama'),
		 ('191', 'Turkish Delight ', '1971-04-14', '2:17:03', 3, 'Comedy, Drama, Mystery '),
		 ('192', 'Baby Doll', '1971-04-14', '2:17:03', 2, 'Comedy, Drama, Mystery '),
		 ('194', 'Devil Times Five', '1971-04-14', '2:17:03', 1, 'Comedy, Drama, Mystery '),
		 ('195', 'Top Sensation', '1971-04-14', '2:17:03', 2, 'Comedy, Drama, Mystery '),
		 ('20', 'Renegades', '1946-06-06', '2:17:03', 3, 'Comedy, Drama, Mystery '),
		 ('200', 'The Unknown', '1946-06-06', '2:17:03', 3, 'Comedy, Drama, Mystery '),
		 ('201', 'The Jolson Story', '1946-06-06', '2:17:03', 3, 'Comedy, Drama, Mystery '),
		 ('202', 'The Secret of the Whistler', '1946-06-06', '2:17:03', 4, 'Comedy, Drama, Mystery '),
		 ('203', 'The Fighting Frontiersman', '1946-06-06', '2:17:03', 4, 'Comedy, Drama, Mystery '),
		 ('204', 'Alias Mr. Twilight', '1946-06-06', '2:17:03', 4, 'Comedy, Drama, Mystery '),
		 ('205', 'Dead Reckoning', '1947-06-06', '2:17:03', 4, 'Comedy, Drama, Mystery '),
		 ('206', 'The Thirteenth Hour', '1947-06-06', '2:17:03', 4, 'Comedy, Drama, Mystery '),
		 ('207', 'Gunfighters', '1947-06-06', '2:17:03', 4, 'Comedy, Drama, Mystery '),
		 ('208', 'Down to Earth', '1947-06-06', '2:17:03', 5, 'Comedy, Drama, Mystery '),
		 ('209', 'The Swordsman', '1946-06-06', '2:17:03', 5, 'Comedy, Drama, Mystery '),
		 ('210', 'Adventures in Silverado', '1946-06-06', '2:17:03', 5, 'Comedy, Drama, Mystery '),
		 ('219', 'Best Man Wins', '1946-06-06', '2:17:03', 6, 'Comedy, Drama, Mystery '),
		 ('229', 'The Lady from Shanghai', '1946-06-06', '2:17:03', 6, 'Comedy, Drama, Mystery ');	   

INSERT INTO uloga (FilmID, GlumacID, TipUlogeID, ImeLika, OpisLika) 
	VALUES ('3', '5', '1', 'Christopher', 'Magia Boss'),
	('4', '4', '1', 'Aragorn', 'Dunadain Ranger'),
	('4', '111', '2', 'Hurin', NULL),
	('5', '9', '1', 'Gladius', NULL),
	('6', '111', '1', 'Zeus', NULL);



INSERT INTO Producirao( ProducentID,FilmID)
VALUES	(7,71),(7,72),(7,73),(17,74),(17,75),(8,76),(8,77),(8,69),(8,68),(18,80),(18,81),(18,82),(18,90),(18,91),(18,92),
		(19,93),(19,94),(19,95),(20,96),(20,97),(20,98),(9,99),(9,900),(9,906),(9,916),
		(6,60),(6,61),(6,62),(5,170),(5,171),(5,172),(5,173),(5,174),(5,175),
		(4,18),(4,180),(4,181),(4,182),(3,5),(2,4),(11,176),(11,177),(1,3),
		(10,6),(10,208),(10,209),(10,210),(10,219),(10,229),(12,19),(13,191),(13,192),
		(14,7),(14,194),(14,195),(15,20),(15,200),(15,201),(16,202),(16,203),(16,204);