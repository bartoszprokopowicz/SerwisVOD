-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema projektdb2
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema projektdb2
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `projektdb2` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `projektdb2` ;

-- -----------------------------------------------------
-- Table `projektdb2`.`__efmigrationshistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`__efmigrationshistory` (
  `MigrationId` VARCHAR(95) NOT NULL,
  `ProductVersion` VARCHAR(32) NOT NULL,
  PRIMARY KEY (`MigrationId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`daneosobowe`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`daneosobowe` (
  `DaneosoboweID` INT(11) NOT NULL AUTO_INCREMENT,
  `Imie` VARCHAR(30) CHARACTER SET 'latin1' NOT NULL,
  `Nazwisko` VARCHAR(60) CHARACTER SET 'latin1' NOT NULL,
  `Data_Urodzin` DATE NOT NULL,
  PRIMARY KEY (`DaneosoboweID`),
  INDEX `Nazwisko_imie` (`Nazwisko` ASC, `Imie` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`aktorzy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`aktorzy` (
  `AktorID` INT(11) NOT NULL AUTO_INCREMENT,
  `DaneosoboweID` INT(11) NOT NULL,
  PRIMARY KEY (`AktorID`),
  INDEX `fk_Aktorzy_Daneosobowe_idx` (`DaneosoboweID` ASC) VISIBLE,
  CONSTRAINT `fk_Aktorzy_Daneosobowe`
    FOREIGN KEY (`DaneosoboweID`)
    REFERENCES `projektdb2`.`daneosobowe` (`DaneosoboweID`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`gatunki`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`gatunki` (
  `GatunekID` INT(11) NOT NULL AUTO_INCREMENT,
  `Gatunek` VARCHAR(20) CHARACTER SET 'latin1' NOT NULL,
  PRIMARY KEY (`GatunekID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`uzytkownicy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`uzytkownicy` (
  `UzytkownikID` INT(11) NOT NULL AUTO_INCREMENT,
  `Login` VARCHAR(40) NULL DEFAULT NULL,
  `ZnormalizowanyLogin` VARCHAR(40) NULL DEFAULT NULL,
  `Email` VARCHAR(60) NULL DEFAULT NULL,
  `ZnormalizowanyEmail` VARCHAR(60) NULL DEFAULT NULL,
  `PotwierdzenieEmail` BIT(1) NOT NULL,
  `PasswordHash` LONGTEXT NULL DEFAULT NULL,
  `SecurityStamp` LONGTEXT NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT NULL DEFAULT NULL,
  `NumerTelefonu` LONGTEXT NULL DEFAULT NULL,
  `PotwierdzenieNumeru` BIT(1) NOT NULL,
  `TwoFactorEnabled` BIT(1) NOT NULL,
  `KoniecBlokady` DATETIME NULL DEFAULT NULL,
  `LockoutEnabled` BIT(1) NOT NULL,
  `AccessFailedCount` INT(11) NOT NULL,
  `DataUtworzenia` DATETIME NOT NULL,
  `DaneosoboweId` INT(11) NOT NULL,
  PRIMARY KEY (`UzytkownikID`),
  UNIQUE INDEX `UserNameIndex` (`ZnormalizowanyLogin` ASC) VISIBLE,
  INDEX `IX_Uzytkownicy_DaneosoboweId` (`DaneosoboweId` ASC) VISIBLE,
  INDEX `EmailIndex` (`ZnormalizowanyEmail` ASC) VISIBLE,
  INDEX `Email_login` (`Email` ASC, `Login` ASC) VISIBLE,
  CONSTRAINT `FK_Uzytkownicy_daneosobowe_DaneosoboweId`
    FOREIGN KEY (`DaneosoboweId`)
    REFERENCES `projektdb2`.`daneosobowe` (`DaneosoboweID`)
    ON DELETE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`filmy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`filmy` (
  `FilmID` INT(11) NOT NULL AUTO_INCREMENT,
  `Tytul_Org` VARCHAR(50) NOT NULL,
  `Tytul_Pol` VARCHAR(50) NULL DEFAULT NULL,
  `Opis` TEXT NOT NULL,
  `Plakat` VARCHAR(200) NOT NULL,
  `Cena` DECIMAL(5,2) NOT NULL,
  `Data_Premiery` DATE NOT NULL,
  `GatunekID` INT(11) NOT NULL,
  `PracownikID` INT(11) NOT NULL,
  PRIMARY KEY (`FilmID`),
  INDEX `FK_Filmy_GatunkiID` (`GatunekID` ASC) VISIBLE,
  INDEX `FK_Filmy_Pracownicy_PracownicyID` (`PracownikID` ASC) VISIBLE,
  CONSTRAINT `FK_Filmy_GatunkiID`
    FOREIGN KEY (`GatunekID`)
    REFERENCES `projektdb2`.`gatunki` (`GatunekID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `FK_Filmy_Pracownicy_PracownicyID`
    FOREIGN KEY (`PracownikID`)
    REFERENCES `projektdb2`.`uzytkownicy` (`UzytkownikID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`aktorzy_filmy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`aktorzy_filmy` (
  `FilmID` INT(11) NOT NULL,
  `AktorID` INT(11) NOT NULL,
  PRIMARY KEY (`FilmID`, `AktorID`),
  INDEX `fk_Aktorzy_Filmy_Aktorzy_idx` (`AktorID` ASC) VISIBLE,
  INDEX `fk_Aktorzy_Filmy_Filmy_idx` (`FilmID` ASC) VISIBLE,
  CONSTRAINT `fk_Aktorzy_Filmy_Aktorzy`
    FOREIGN KEY (`AktorID`)
    REFERENCES `projektdb2`.`aktorzy` (`AktorID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Aktorzy_Filmy_Filmy`
    FOREIGN KEY (`FilmID`)
    REFERENCES `projektdb2`.`filmy` (`FilmID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`kraje`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`kraje` (
  `KrajID` INT(11) NOT NULL AUTO_INCREMENT,
  `Kraj_kod` VARCHAR(2) CHARACTER SET 'latin1' NOT NULL,
  `Kraj_nazwa` VARCHAR(100) CHARACTER SET 'latin1' NOT NULL,
  PRIMARY KEY (`KrajID`),
  INDEX `kk` (`Kraj_nazwa` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`kraje_filmy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`kraje_filmy` (
  `FilmID` INT(11) NOT NULL,
  `KrajID` INT(11) NOT NULL,
  PRIMARY KEY (`FilmID`, `KrajID`),
  INDEX `fk_Kraje_Filmy_Kraje_idx` (`KrajID` ASC) VISIBLE,
  INDEX `fk_Kraje_Filmy_Filmy_idx` (`FilmID` ASC) VISIBLE,
  CONSTRAINT `fk_Kraje_Filmy_Filmy`
    FOREIGN KEY (`FilmID`)
    REFERENCES `projektdb2`.`filmy` (`FilmID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Kraje_Filmy_Kraje`
    FOREIGN KEY (`KrajID`)
    REFERENCES `projektdb2`.`kraje` (`KrajID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`ocena_film`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`ocena_film` (
  `Ocena` DECIMAL(2,1) NULL DEFAULT NULL,
  `FilmID` INT(11) NOT NULL,
  PRIMARY KEY (`FilmID`),
  CONSTRAINT `fk_Ocena_Film_film`
    FOREIGN KEY (`FilmID`)
    REFERENCES `projektdb2`.`filmy` (`FilmID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`rezyserzy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`rezyserzy` (
  `RezyserID` INT(11) NOT NULL AUTO_INCREMENT,
  `DaneosoboweID` INT(11) NOT NULL,
  PRIMARY KEY (`RezyserID`),
  INDEX `fk_Rezyserzy_Daneosobowe_idx` (`DaneosoboweID` ASC) VISIBLE,
  CONSTRAINT `fk_Rezyserzy_Daneosobowe`
    FOREIGN KEY (`DaneosoboweID`)
    REFERENCES `projektdb2`.`daneosobowe` (`DaneosoboweID`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`rezyserzy_filmy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`rezyserzy_filmy` (
  `FilmID` INT(11) NOT NULL,
  `RezyserID` INT(11) NOT NULL,
  PRIMARY KEY (`FilmID`, `RezyserID`),
  INDEX `fk_Rezyserzy_Filmy_Rezyserzy_idx` (`RezyserID` ASC) VISIBLE,
  INDEX `fk_Rezyserzy_Filmy_Filmy_idx` (`FilmID` ASC) VISIBLE,
  CONSTRAINT `fk_Rezyserzy_Filmy_Filmy`
    FOREIGN KEY (`FilmID`)
    REFERENCES `projektdb2`.`filmy` (`FilmID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Rezyserzy_Filmy_Rezyserzy`
    FOREIGN KEY (`RezyserID`)
    REFERENCES `projektdb2`.`rezyserzy` (`RezyserID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`role`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`role` (
  `RolaID` INT(11) NOT NULL AUTO_INCREMENT,
  `Nazwa` VARCHAR(256) NULL DEFAULT NULL,
  `ZnormalizowanaNazwa` VARCHAR(256) NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`RolaID`),
  UNIQUE INDEX `RoleNameIndex` (`ZnormalizowanaNazwa` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`roleclaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`roleclaims` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `RoleId` INT(11) NOT NULL,
  `ClaimType` LONGTEXT NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_RoleClaims_RoleId` (`RoleId` ASC) VISIBLE,
  CONSTRAINT `FK_RoleClaims_Role_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES `projektdb2`.`role` (`RolaID`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`userclaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`userclaims` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `UserId` INT(11) NOT NULL,
  `ClaimType` LONGTEXT NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_UserClaims_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_UserClaims_Uzytkownicy_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `projektdb2`.`uzytkownicy` (`UzytkownikID`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`userlogins`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`userlogins` (
  `LoginProvider` VARCHAR(255) NOT NULL,
  `ProviderKey` VARCHAR(255) NOT NULL,
  `ProviderDisplayName` LONGTEXT NULL DEFAULT NULL,
  `UserId` INT(11) NOT NULL,
  PRIMARY KEY (`ProviderKey`, `LoginProvider`),
  UNIQUE INDEX `AK_UserLogins_LoginProvider_ProviderKey` (`LoginProvider` ASC, `ProviderKey` ASC) VISIBLE,
  INDEX `IX_UserLogins_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_UserLogins_Uzytkownicy_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `projektdb2`.`uzytkownicy` (`UzytkownikID`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`usertokens`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`usertokens` (
  `UserId` INT(11) NOT NULL,
  `LoginProvider` VARCHAR(255) NOT NULL,
  `Name` VARCHAR(255) NOT NULL,
  `Value` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
  CONSTRAINT `FK_UserTokens_Uzytkownicy_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `projektdb2`.`uzytkownicy` (`UzytkownikID`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`uzytkownicy_filmy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`uzytkownicy_filmy` (
  `UzytkownikID` INT(11) NOT NULL,
  `FilmID` INT(11) NOT NULL,
  `Ocena` DECIMAL(2,1) NULL DEFAULT NULL,
  PRIMARY KEY (`UzytkownikID`, `FilmID`),
  INDEX `fk_Uzytkownicy_Filmy_Filmy_idx` (`FilmID` ASC) VISIBLE,
  INDEX `fk_Uzytkownicy_Filmy_Uzytkownicy_idx` (`UzytkownikID` ASC) VISIBLE,
  CONSTRAINT `fk_Uzytkownicy_Filmy_Filmy`
    FOREIGN KEY (`FilmID`)
    REFERENCES `projektdb2`.`filmy` (`FilmID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Uzytkownicy_Filmy_Uzytkownicy`
    FOREIGN KEY (`UzytkownikID`)
    REFERENCES `projektdb2`.`uzytkownicy` (`UzytkownikID`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projektdb2`.`uzytkownicy_role`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projektdb2`.`uzytkownicy_role` (
  `UzytkownikID` INT(11) NOT NULL,
  `RolaID` INT(11) NOT NULL,
  PRIMARY KEY (`UzytkownikID`, `RolaID`),
  INDEX `IX_Uzytkownicy_Role_RolaID` (`RolaID` ASC) VISIBLE,
  CONSTRAINT `FK_Uzytkownicy_Role_Role_RolaID`
    FOREIGN KEY (`RolaID`)
    REFERENCES `projektdb2`.`role` (`RolaID`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_Uzytkownicy_Role_Uzytkownicy_UzytkownikID`
    FOREIGN KEY (`UzytkownikID`)
    REFERENCES `projektdb2`.`uzytkownicy` (`UzytkownikID`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

DELIMITER $$

USE `projektdb2`$$
CREATE
DEFINER=`root`@`localhost`
TRIGGER `projektdb2`.`TytulPol_Null`
BEFORE INSERT ON `projektdb2`.`filmy`
FOR EACH ROW
BEGIN
    IF NEW.Tytul_Pol IS NULL THEN
      SET NEW.Tytul_Pol = NEW.Tytul_Org;
    end if;
END$$

USE `projektdb2`$$
CREATE
DEFINER=`root`@`localhost`
TRIGGER `projektdb2`.`auto_ocena`
AFTER INSERT ON `projektdb2`.`filmy`
FOR EACH ROW
BEGIN
INSERT `projektdb2`.`Ocena_Film` VALUES(null,NEW.FilmID);
END$$

USE `projektdb2`$$
CREATE
DEFINER=`root`@`localhost`
TRIGGER `projektdb2`.`Avg_rating`
AFTER UPDATE ON `projektdb2`.`uzytkownicy_filmy`
FOR EACH ROW
BEGIN
UPDATE `projektdb2`.`Ocena_Film`
    SET Ocena = (SELECT AVG(Ocena) FROM Uzytkownicy_Filmy WHERE Uzytkownicy_Filmy.FilmID = Ocena_Film.FilmID);
END$$


DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;


CREATE VIEW vMovieInfo AS
SELECT f.Tytul_Pol AS TytulPolski, f.Tytul_Org AS TytulOryginalny, f.Data_Premiery AS DataPremiery, g.gatunek AS Gatunek, dr.imie AS ImieRezysera, dr.nazwisko AS NazwiskoRezysera, da.imie AS ImieAktora, da.nazwisko AS NazwiskoAktora, k.kraj_nazwa AS Kraj, o.ocena AS Ocena
FROM Filmy f
INNER JOIN gatunki g ON f.gatunekID = g.gatunekID
INNER JOIN rezyserzy_filmy rf ON rf.filmID = f.filmID
INNER JOIN rezyserzy r ON r.rezyserID = rf.rezyserID
INNER JOIN daneosobowe dr ON r.daneosoboweID = dr.daneosoboweID
INNER JOIN aktorzy_filmy af ON af.filmID = f.filmID
INNER JOIN aktorzy a ON a.aktorID = af.aktorID
INNER JOIN daneosobowe da ON a.daneosoboweID = da.daneosoboweID
INNER JOIN kraje_filmy kf ON kf.filmid = f.filmID
INNER JOIN kraje k ON k.KrajID = kf.KrajID
INNER JOIN ocena_film o ON o.filmID = f.filmID;

CREATE VIEW vDirectorInfo AS
SELECT d.Imie, d.Nazwisko, d.Data_Urodzin AS DataUrodzin, f.Tytul_Org AS TytulOryginalny, g.Gatunek AS Gatunek, f.Data_Premiery AS DataPremiery
FROM Daneosobowe d
INNER JOIN Rezyserzy r ON r.DaneosoboweID = d.DaneosoboweID
INNER JOIN Rezyserzy_Filmy rf ON RF.RezyserID = R.RezyserID
INNER JOIN Filmy f ON f.FilmID = rf.FilmID
INNER JOIN Gatunki g ON g.GatunekID = f.GatunekID;

CREATE VIEW vActorInfo AS
SELECT d.Imie, d.Nazwisko, d.Data_Urodzin AS DataUrodzin, f.Tytul_Org AS TytulOryginalny, g.Gatunek AS Gatunek, f.Data_Premiery AS DataPremiery
FROM Aktorzy a
INNER JOIN Aktorzy_Filmy af
INNER JOIN Daneosobowe d ON d.DaneosoboweID = a.DaneosoboweID
INNER JOIN Filmy f ON f.FilmID = af.FilmID AND a.AktorID = af.AktorID
INNER JOIN Gatunki g ON g.GatunekID = f.GatunekID;

CREATE VIEW vCountryMovies AS
SELECT f.Tytul_Org AS TytulOryginalny, f.Data_Premiery AS DataPremiery, k.Kraj_nazwa AS Kraj
FROM filmy f
INNER JOIN Kraje_Filmy kr ON kr.FilmID = f.FilmID
INNER JOIN Kraje k ON k.KrajID = kr.KrajID;

CREATE VIEW vUserMovies AS
SELECT u.login, u.email, d.Imie, d.Nazwisko, d.Data_Urodzin AS DataUrodzin, 
f.Tytul_Org AS TytulOryginalny, f.Data_Premiery AS DataPremiery, g.Gatunek AS Gatunek
FROM Uzytkownicy u
INNER JOIN uzytkownicy_filmy uf
INNER JOIN Daneosobowe d ON d.DaneosoboweID = u.DaneosoboweID
INNER JOIN Filmy f ON f.FilmID = uf.FilmID AND u.UzytkownikID = uf.UzytkownikID
INNER JOIN Gatunki g ON g.GatunekID = f.GatunekID;

/*Dodanie indexów*/
CREATE INDEX Email_numer ON uzytkownicy(Email, NumerTelefonu);
CREATE INDEX Email_login ON Uzytkownicy(Email, Login);
CREATE INDEX Tytul_data ON Filmy(Tytul_Org, Data_Premiery);
CREATE INDEX Nazwisko_imie ON Daneosobowe(Nazwisko, Imie);

ALTER TABLE Uzytkownicy ADD CONSTRAINT  CHECK (LEN(NumerTelefonu) > 9);
ALTER TABLE Uzytkownicy ADD CONSTRAINT CHECK (NumerTelefonu like '[0-9]*9');
ALTER TABLE Uzytkownicy ADD CONSTRAINT  CHECK (LEN(Login) >= 8);
ALTER TABLE Filmy ADD CONSTRAINT  CHECK (LEN(Tytul_Org) >= 2);
ALTER TABLE Daneosobowe ADD CONSTRAINT  CHECK (LEN(Imie) >= 2);
ALTER TABLE Daneosobowe ADD CONSTRAINT  CHECK (LEN(Nazwisko) >= 2);
ALTER TABLE Uzytkownicy ADD CONSTRAINT CHECK (Email like '%_@__%.__%');

INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Bartosz', 'Prokopowicz', '1997-04-05');
INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Sebastian', 'Stanclik', '1997-01-05');
INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Kamil', 'Nowak', '1997-07-05');

INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('James', 'Mangold', '1956-07-05');
INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Giuseppe', 'Tornatore', '1942-07-05');

INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Tim','Robbins', '1967-07-05');
INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Patrick','McGoohan', '1964-07-05');
INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Edward','Burns', '1961-07-05');
INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Andrew','Garfield', '1946-07-05');
INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Matt','Damon', '1924-07-05');
INSERT INTO Daneosobowe (Imie, Nazwisko, Data_Urodzin) VALUES ('Ben','Affleck', '1995-07-05');

INSERT INTO Rezyserzy (DaneosoboweID) VALUES (4);
INSERT INTO Rezyserzy (DaneosoboweID) VALUES (5);
/*Dodanie aktorow*/
INSERT INTO Aktorzy (DaneosoboweID) VALUES(6);
INSERT INTO Aktorzy (DaneosoboweID) VALUES(7);
INSERT INTO Aktorzy (DaneosoboweID) VALUES(8);
INSERT INTO Aktorzy (DaneosoboweID) VALUES(9);
INSERT INTO Aktorzy (DaneosoboweID) VALUES(10);
INSERT INTO Aktorzy (DaneosoboweID) VALUES(11);
/*Dodanie gatunkow*/
INSERT INTO Gatunki (Gatunek) VALUES ('Akcja');
INSERT INTO Gatunki (Gatunek) VALUES ('Komedia');
INSERT INTO Gatunki (Gatunek) VALUES ('Dramat');
INSERT INTO Gatunki (Gatunek) VALUES ('Fantasy');
INSERT INTO Gatunki (Gatunek) VALUES ('Horror');
INSERT INTO Gatunki (Gatunek) VALUES ('Katastroficzny');
INSERT INTO Gatunki (Gatunek) VALUES ('Przygodowy');
INSERT INTO Gatunki (Gatunek) VALUES ('Science-Fiction');
INSERT INTO Gatunki (Gatunek) VALUES ('Thriller');
INSERT INTO Gatunki (Gatunek) VALUES ('Western');
INSERT INTO Gatunki (Gatunek) VALUES ('Wojenny');
/*Dodanie krajow*/
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AF', 'Afghanistan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AL', 'Albania');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('DZ', 'Algeria');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('DS', 'American Samoa');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AD', 'Andorra');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AO', 'Angola');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AI', 'Anguilla');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AQ', 'Antarctica');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AG', 'Antigua and Barbuda');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AR', 'Argentina');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AM', 'Armenia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AW', 'Aruba');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AU', 'Australia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AT', 'Austria');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AZ', 'Azerbaijan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BS', 'Bahamas');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BH', 'Bahrain');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BD', 'Bangladesh');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BB', 'Barbados');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BY', 'Belarus');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BE', 'Belgium');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BZ', 'Belize');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BJ', 'Benin');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BM', 'Bermuda');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BT', 'Bhutan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BO', 'Bolivia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BA', 'Bosnia and Herzegovina');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BW', 'Botswana');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BV', 'Bouvet Island');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BR', 'Brazil');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('IO', 'British Indian Ocean Territory');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BN', 'Brunei Darussalam');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BG', 'Bulgaria');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BF', 'Burkina Faso');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('BI', 'Burundi');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KH', 'Cambodia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CM', 'CameroON');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CA', 'Canada');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CV', 'Cape Verde');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KY', 'Cayman Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CF', 'Central African Republic');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TD', 'Chad');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CL', 'Chile');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CN', 'China');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CX', 'Christmas Island');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CC', 'Cocos (Keeling) Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CO', 'Colombia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KM', 'Comoros');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CG', 'CONgo');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CK', 'Cook Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CR', 'Costa Rica');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('HR', 'Croatia (Hrvatska)');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CU', 'Cuba');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CY', 'Cyprus');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CZ', 'Czech Republic');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('DK', 'Denmark');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('DJ', 'Djibouti');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('DM', 'Dominica');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('DO', 'Dominican Republic');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TP', 'East Timor');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('EC', 'Ecuador');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('EG', 'Egypt');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SV', 'El Salvador');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GQ', 'Equatorial Guinea');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ER', 'Eritrea');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('EE', 'EstONia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ET', 'Ethiopia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('FK', 'Falkland Islands (Malvinas)');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('FO', 'Faroe Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('FJ', 'Fiji');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('FI', 'Finland');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('FR', 'France');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('FX', 'France, Metropolitan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GF', 'French Guiana');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PF', 'French Polynesia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TF', 'French Southern Territories');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GA', 'GabON');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GM', 'Gambia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GE', 'Georgia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('DE', 'Germany');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GH', 'Ghana');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GI', 'Gibraltar');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GK', 'Guernsey');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GR', 'Greece');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GL', 'Greenland');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GD', 'Grenada');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GP', 'Guadeloupe');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GU', 'Guam');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GT', 'Guatemala');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GN', 'Guinea');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GW', 'Guinea-Bissau');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GY', 'Guyana');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('HT', 'Haiti');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('HM', 'Heard and Mc DONald Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('HN', 'HONduras');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('HK', 'HONg KONg');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('HU', 'Hungary');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('IS', 'Iceland');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('IN', 'India');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('IM', 'Isle of Man');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ID', 'IndONesia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('IR', 'Iran (Islamic Republic of)');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('IQ', 'Iraq');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('IE', 'Ireland');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('IL', 'Israel');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('IT', 'Italy');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CI', 'Ivory Coast');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('JE', 'Jersey');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('JM', 'Jamaica');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('JP', 'Japan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('JO', 'Jordan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KZ', 'Kazakhstan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KE', 'Kenya');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KI', 'Kiribati');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KP', 'Korea, Democratic People''s Republic of');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KR', 'Korea, Republic of');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('XK', 'Kosovo');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KW', 'Kuwait');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KG', 'Kyrgyzstan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LA', 'Lao People''s Democratic Republic');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LV', 'Latvia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LB', 'LebanON');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LS', 'Lesotho');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LR', 'Liberia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LY', 'Libyan Arab Jamahiriya');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LI', 'Liechtenstein');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LT', 'Lithuania');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LU', 'Luxembourg');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MO', 'Macau');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MK', 'MacedONia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MG', 'Madagascar');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MW', 'Malawi');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MY', 'Malaysia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MV', 'Maldives');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ML', 'Mali');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MT', 'Malta');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MH', 'Marshall Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MQ', 'Martinique');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MR', 'Mauritania');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MU', 'Mauritius');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TY', 'Mayotte');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MX', 'Mexico');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('FM', 'MicrONesia, Federated States of');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MD', 'Moldova, Republic of');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MC', 'MONaco');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MN', 'MONgolia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ME', 'MONtenegro');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MS', 'MONtserrat');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MA', 'Morocco');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MZ', 'Mozambique');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MM', 'Myanmar');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NA', 'Namibia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NR', 'Nauru');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NP', 'Nepal');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NL', 'Netherlands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AN', 'Netherlands Antilles');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NC', 'New CaledONia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NZ', 'New Zealand');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NI', 'Nicaragua');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NE', 'Niger');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NG', 'Nigeria');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NU', 'Niue');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NF', 'Norfolk Island');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('MP', 'Northern Mariana Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('NO', 'Norway');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('OM', 'Oman');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PK', 'Pakistan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PW', 'Palau');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PS', 'Palestine');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PA', 'Panama');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PG', 'Papua New Guinea');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PY', 'Paraguay');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PE', 'Peru');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PH', 'Philippines');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PN', 'Pitcairn');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PL', 'Poland');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PT', 'Portugal');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PR', 'Puerto Rico');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('QA', 'Qatar');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('RE', 'ReuniON');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('RO', 'Romania');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('RU', 'Russian FederatiON');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('RW', 'Rwanda');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('KN', 'Saint Kitts and Nevis');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LC', 'Saint Lucia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('VC', 'Saint Vincent and the Grenadines');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('WS', 'Samoa');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SM', 'San Marino');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ST', 'Sao Tome and Principe');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SA', 'Saudi Arabia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SN', 'Senegal');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('RS', 'Serbia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SC', 'Seychelles');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SL', 'Sierra LeONe');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SG', 'Singapore');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SK', 'Slovakia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SI', 'Slovenia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SB', 'SolomON Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SO', 'Somalia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ZA', 'South Africa');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GS', 'South Georgia South Sandwich Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SS', 'South Sudan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ES', 'Spain');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('LK', 'Sri Lanka');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SH', 'St. Helena');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('PM', 'St. Pierre and MiquelON');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SD', 'Sudan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SR', 'Suriname');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SJ', 'Svalbard and Jan Mayen Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SZ', 'Swaziland');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SE', 'Sweden');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('CH', 'Switzerland');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('SY', 'Syrian Arab Republic');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TW', 'Taiwan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TJ', 'Tajikistan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TZ', 'Tanzania, United Republic of');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TH', 'Thailand');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TG', 'Togo');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TK', 'Tokelau');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TO', 'TONga');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TT', 'Trinidad and Tobago');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TN', 'Tunisia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TR', 'Turkey');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TM', 'Turkmenistan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TC', 'Turks and Caicos Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('TV', 'Tuvalu');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('UG', 'Uganda');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('UA', 'Ukraine');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('AE', 'United Arab Emirates');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('GB', 'United Kingdom');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('US', 'United States');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('UM', 'United States minor outlying islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('UY', 'Uruguay');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('UZ', 'Uzbekistan');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('VU', 'Vanuatu');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('VA', 'Vatican City State');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('VE', 'Venezuela');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('VN', 'Vietnam');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('VG', 'Virgin Islands (British)');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('VI', 'Virgin Islands (U.S.)');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('WF', 'Wallis and Futuna Islands');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('EH', 'Western Sahara');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('YE', 'Yemen');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ZR', 'Zaire');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ZM', 'Zambia');
INSERT INTO Kraje (Kraj_kod, Kraj_nazwa) VALUES ('ZW', 'Zimbabwe');

INSERT INTO Filmy (Tytul_Org, Tytul_Pol, Opis, Plakat, Cena, Data_Premiery, GatunekID, PracownikID) VALUES ('Avengers', null, 'SKIBIDI', 'https://mir-s3-cdn-cf.behance.net/project_modules/disp/69779e21891003.563092b69edaf.jpg', 99.99,'2014-12-06', 1, 1);
INSERT INTO Filmy (Tytul_Org, Tytul_Pol, Opis, Plakat, Cena, Data_Premiery, GatunekID, PracownikID) VALUES ('Green Mile', 'Zielona Mila', 'SKIBIDI', 'https://mir-s3-cdn-cf.behance.net/project_modules/disp/69779e21891003.563092b69edaf.jpg', 99.99, '2011-12-06', 2, 1);
INSERT INTO Filmy (Tytul_Org, Tytul_Pol, Opis, Plakat, Cena, Data_Premiery, GatunekID, PracownikID) VALUES ('Sausage Party', null, 'SKIBIDI', 'https://mir-s3-cdn-cf.behance.net/project_modules/disp/69779e21891003.563092b69edaf.jpg', 99.99, '2016-12-06', 3, 1);

INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (1,1);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (1,2);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (1,3);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (1,4);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (2,6);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (2,4);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (2,1);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (2,3);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (3,3);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (3,5);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (3,1);
INSERT INTO Aktorzy_Filmy (FilmID, AktorID) VALUES (3,2);
/*Dodanie relacji Filmy uzytkownik */
INSERT INTO Uzytkownicy_Filmy (FilmID, UzytkownikID, Ocena) VALUES (1,1,null);
INSERT INTO Uzytkownicy_Filmy (FilmID, UzytkownikID, Ocena) VALUES (1,2,null);
INSERT INTO Uzytkownicy_Filmy (FilmID, UzytkownikID, Ocena) VALUES (2,1,null);
INSERT INTO Uzytkownicy_Filmy (FilmID, UzytkownikID, Ocena) VALUES (3,2,null);
INSERT INTO Uzytkownicy_Filmy (FilmID, UzytkownikID, Ocena) VALUES (3,1,null);
/*Dodanie relacji Filmy rezyser*/
INSERT INTO Rezyserzy_Filmy (FilmID, RezyserID) VALUES (1,2);
INSERT INTO Rezyserzy_Filmy (FilmID, RezyserID) VALUES (2,2);
INSERT INTO Rezyserzy_Filmy (FilmID, RezyserID) VALUES (3,1);
INSERT INTO Rezyserzy_Filmy (FilmID, RezyserID) VALUES (2,1);
INSERT INTO Rezyserzy_Filmy (FilmID, RezyserID) VALUES (3,2);
/*Dodanie relacji Filmy Kraje*/
INSERT INTO Kraje_Filmy (FilmID, KrajID) VALUES (1,5);
INSERT INTO Kraje_Filmy (FilmID, KrajID) VALUES (2,5);
INSERT INTO Kraje_Filmy (FilmID, KrajID) VALUES (3,10);
INSERT INTO Kraje_Filmy (FilmID, KrajID) VALUES (2,11);
INSERT INTO Kraje_Filmy (FilmID, KrajID) VALUES (3,212);
INSERT INTO Kraje_Filmy (FilmID, KrajID) VALUES (1,24);
