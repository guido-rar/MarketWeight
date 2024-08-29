USE 5to_MarketWeight;

DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCriptoMoneda $$
CREATE PROCEDURE `AltaCriptoMoneda`(xprecio DECIMAL(20,10), xcantidad DECIMAL(20,10), xnombre VARCHAR(45))
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
CREATE PROCEDURE `ComprarMoneda`(xidusuario INT UNSIGNED, xcantidad DECIMAL(20,10), xidmoneda INT UNSIGNED)
BEGIN
       IF (PuedeComprar(xidUsuario, xcantidad, xidmoneda))
       THEN 
              IF (NOT (EXISTS (
                     SELECT *
                     FROM `UsuarioMoneda`
                     WHERE `idMoneda` = xidmoneda AND `idUsuario` = xidusuario
                     )))
                     THEN 
                            INSERT INTO `UsuarioMoneda` (`idUsuario`, `idMoneda`, cantidad)
                            VALUES(xidusuario, xidmoneda, 0);
              END IF;

              UPDATE UsuarioMoneda
              SET cantidad = cantidad + xcantidad
              WHERE idMoneda = xidmoneda
              AND idUsuario = xidusuario;

              INSERT INTO Historial (idMoneda, cantidad, fechaHora, compra, idUsuario)
              VALUES (xidmoneda, xcantidad, NOW(), TRUE, xidusuario);
       ELSE
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = "Saldo Insuficiente!";
       END IF;
END $$

DROP PROCEDURE IF EXISTS IngresarDinero $$
CREATE PROCEDURE `IngresarDinero`(xidUsuario INT UNSIGNED, xsaldo DECIMAL(20,10))
BEGIN 
    UPDATE Usuario 
    SET saldo = saldo + xsaldo
    WHERE `idUsuario` = xidUsuario;
END $$

DROP PROCEDURE IF EXISTS VenderMoneda $$
CREATE PROCEDURE `VenderMoneda`(xidusuario INT UNSIGNED, xcantidad DECIMAL(20,10), xidmoneda INT UNSIGNED)
BEGIN
       SELECT cantidad INTO @xcantidad
       FROM `UsuarioMoneda`
       WHERE `idMoneda` = xidmoneda AND `idUsuario` = xidusuario;


       IF(@xcantidad >= xcantidad)
       THEN
              UPDATE UsuarioMoneda
              SET cantidad = cantidad - xcantidad
              WHERE idMoneda = xidmoneda
              AND idUsuario = xidusuario;

              INSERT INTO Historial (idMoneda, cantidad, fechaHora, compra, idUsuario)
              VALUES (xidmoneda, xcantidad, NOW(), FALSE, xidusuario);
       ELSE
              SIGNAL SQLSTATE '45000'
              SET MESSAGE_TEXT = "Cantidad Insuficiente!";
       END IF;
END $$



DROP PROCEDURE IF EXISTS AltaHistorial $$
CREATE PROCEDURE `AltaHistorial`(xidMoneda INT UNSIGNED, xcantidad DECIMAL(20,10) UNSIGNED, xcompra TINYINT UNSIGNED, xidUsuario INT UNSIGNED)
BEGIN
       INSERT INTO `Historial` (idMoneda, cantidad, fechaHora, compra, idUsuario)
           VALUES(xidMoneda, xcantidad, NOW(), xcompra, xidUsuario);
END $$


