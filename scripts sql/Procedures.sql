USE 5to_MarketWeight;

DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCriptoMoneda $$
CREATE PROCEDURE `AltaCriptoMoneda`(xprecio FLOAT, xcantidad DECIMAL(8.8), xnombre VARCHAR(45))
BEGIN
   INSERT INTO `Moneda` (precio, cantidad, nombre)
           VALUES(xprecio, xcantidad, xnombre);
END $$

DROP PROCEDURE IF EXISTS AltaUsuario $$
CREATE PROCEDURE `AltaUsuario`(xnombre VARCHAR(45), xapellido  VARCHAR(45), xemail  VARCHAR(45), xpass CHAR(64) )
BEGIN
       INSERT INTO `Usuario` (nombre, apellido, email, pass, saldo)
           VALUES(xnombre, xapellido, xemail, xpass, 0.0);
END $$

DROP PROCEDURE IF EXISTS ComprarMoneda $$
CREATE PROCEDURE `ComprarMoneda`(xidusuario INT UNSIGNED,xcantidad DECIMAL(8.8), xidmoneda INT UNSIGNED)
BEGIN
       UPDATE UsuarioMoneda
       SET cantidad = cantidad + xcantidad
       WHERE idMoneda = xidmoneda
       AND idUsuario = xidusuario;
END $$

DROP PROCEDURE IF EXISTS IngresarDinero $$
CREATE PROCEDURE `IngresarDinero`(xidUsuario INT UNSIGNED, xsaldo FLOAT)
BEGIN 
    UPDATE Usuario 
    SET Saldo = saldo + xsaldo
    WHERE `idUsuario` = xidUsuario;
END $$

DROP PROCEDURE IF EXISTS VenderMoneda $$
CREATE PROCEDURE `VenderMoneda`(xidusuario INT UNSIGNED, xidmoneda INT UNSIGNED, xcantidad FLOAT)
BEGIN
       UPDATE UsuarioMoneda
       SET cantidad = cantidad - xcantidad
       WHERE idMoneda = xidmoneda
       AND idUsuario = xidusuario;
END $$



DROP PROCEDURE IF EXISTS AltaHistorial $$
CREATE PROCEDURE `AltaHistorial`(xidMoneda INT UNSIGNED, xcantidad DECIMAL(8.8) UNSIGNED, xaccion TINYINT UNSIGNED, xidUsuario INT UNSIGNED)
BEGIN
       INSERT INTO `Historial` (idMoneda, cantidad, fechaHora, accion, idUsuario)
           VALUES(xidMoneda, xcantidad, NOW(), xaccion, xidUsuario);
END $$