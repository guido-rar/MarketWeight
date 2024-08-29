-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema 5to_MarketWeight
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `5to_MarketWeight` ;

-- -----------------------------------------------------
-- Schema 5to_MarketWeight
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `5to_MarketWeight` ;
USE `5to_MarketWeight` ;

-- -----------------------------------------------------
-- Table `Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Usuario` (
  `idUsuario` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `apellido` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `pass` CHAR(64) NOT NULL,
  `saldo` DECIMAL(20,10) NOT NULL,
  PRIMARY KEY (`idUsuario`),
  UNIQUE INDEX `idUsuario_UNIQUE` (`idUsuario` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Moneda`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Moneda` (
  `idMoneda` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `precio` DECIMAL(20,10) NOT NULL,
  `cantidad` DECIMAL(20,10) NOT NULL,
  `nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idMoneda`),
  UNIQUE INDEX `idMoneda_UNIQUE` (`idMoneda` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `UsuarioMoneda`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `UsuarioMoneda` (
  `idUsuario` INT UNSIGNED NOT NULL,
  `idMoneda` INT UNSIGNED NOT NULL,
  `cantidad` DECIMAL(20,10) NOT NULL,
  PRIMARY KEY (`idUsuario`, `idMoneda`),
  INDEX `fk_BilleteraMoneda_Moneda1_idx` (`idMoneda` ASC) VISIBLE,
  INDEX `fk_BilleteraMoneda_Usuario1_idx` (`idUsuario` ASC) VISIBLE,
  CONSTRAINT `fk_BilleteraMoneda_Moneda1`
    FOREIGN KEY (`idMoneda`)
    REFERENCES `Moneda` (`idMoneda`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_BilleteraMoneda_Usuario1`
    FOREIGN KEY (`idUsuario`)
    REFERENCES `Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Historial`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Historial` (
  `idHistorial` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `idUsuario` INT UNSIGNED NOT NULL,
  `idMoneda` INT UNSIGNED NOT NULL,
  `cantidad` DECIMAL(20,10) UNSIGNED NULL,
  `fechaHora` DATETIME NOT NULL,
  `compra` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`idHistorial`, `idMoneda`, `idUsuario`),
  INDEX `fk_Historial_Moneda1_idx` (`idMoneda` ASC) VISIBLE,
  INDEX `idMoneda_UNIQUE` (`idMoneda` ASC) VISIBLE,
  INDEX `fk_Historial_Usuario1_idx` (`idUsuario` ASC) VISIBLE,
  CONSTRAINT `fk_Historial_Moneda1`
    FOREIGN KEY (`idMoneda`)
    REFERENCES `Moneda` (`idMoneda`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Historial_Usuario1`
    FOREIGN KEY (`idUsuario`)
    REFERENCES `Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `5to_MarketWeight`;

DELIMITER $$
USE `5to_MarketWeight`$$
CREATE DEFINER = CURRENT_USER TRIGGER `5to_MarketWeight`.`Usuario_BEFORE_INSERT` BEFORE INSERT ON `Usuario` FOR EACH ROW
BEGIN

END
$$


DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

