DELIMITER $$
DROP PROCEDURE AltaCriptoMoneda;
CREATE PROCEDURE `AltaCriptoMoneda`(xprecio FLOAT, xcantidad DECIMAL(8.8), xnombre VARCHAR(45))
BEGIN
   INSERT INTO `Moneda` (precio, cantidad, nombre)
           VALUES(xprecio, xcantidad, xnombre);
END $$

DROP PROCEDURE AltaUsuario;
CREATE PROCEDURE `AltaUsuario`(xnombre VARCHAR(45), xapellido  VARCHAR(45), xemail  VARCHAR(45), xpass CHAR(64) )
BEGIN
       INSERT INTO `Usuario` (nombre, apellido, email, pass, Saldo)
           VALUES(xnombre, xapellido, xemail, xpass, 0.0);
END $$

DROP PROCEDURE ComprarMoneda;
CREATE PROCEDURE `ComprarMoneda`(xidusuario INT UNSIGNED,xcantidad INT, xidmoneda INT UNSIGNED)
BEGIN
       UPDATE UsuarioMoneda
       SET cantidad =+ xcantidad
       WHERE idMoneda = xidmoneda
       AND idUsuario = xidusuario;
END $$

DROP PROCEDURE IngresarDinero;
CREATE PROCEDURE `IngresarDinero`(xidUsuario INT UNSIGNED, xsaldo FLOAT)
BEGIN 
    UPDATE Usuario 
    SET Saldo =+ xsaldo
    WHERE `idUsuario` = xidUsuario;
END $$

DROP PROCEDURE VenderMoneda;
CREATE PROCEDURE `VenderMoneda`(xidusuario INT UNSIGNED, xidmoneda INT UNSIGNED, xcantidad FLOAT)
BEGIN
       UPDATE UsuarioMoneda
       SET cantidad =- xcantidad
       WHERE idMoneda = xidmoneda
       AND idUsuario = xidusuario;
END $$