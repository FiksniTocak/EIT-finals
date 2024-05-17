
USE BibliotekaA02;

ALTER TABLE Knjiga add Format_knjige varchar(100)

alter table Citalac add DatumPlacanja date
 constraint ck_datum_placanja_clanarine_citaoca check (DatumPlacanja>=GETDATE())