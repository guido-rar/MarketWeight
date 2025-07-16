USE 5to_MarketWeight

DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCriptoMoneda $$
CREATE PROCEDURE `AltaCriptoMoneda`(xprecio DECIMAL(20,10), xcantidad DECIMAL(20,10), xnombre VARCHAR(45))
BEGIN
       INSERT INTO `Moneda` (precio, cantidad, nombre)
           VALUES(xprecio, xcantidad, xnombre);
       SELECT LAST_INSERT_ID() AS idMoneda;
END $$

DROP PROCEDURE IF EXISTS AltaUsuario $$
CREATE PROCEDURE `AltaUsuario`(
    IN xidUsuario INT UNSIGNED,
    IN xnombre VARCHAR(45),
    IN xapellido VARCHAR(45),
    IN xemail VARCHAR(45),
    IN xpass CHAR(64)
)
BEGIN
    IF xidUsuario IS NULL OR xidUsuario = 0 THEN
        INSERT INTO `Usuario` (nombre, apellido, email, pass, saldo)
        VALUES (xnombre, xapellido, xemail, xpass, 0.0);
        SELECT LAST_INSERT_ID() AS idUsuario;
    ELSE
        INSERT INTO `Usuario` (idUsuario, nombre, apellido, email, pass, saldo)
        VALUES (xidUsuario, xnombre, xapellido, xemail, xpass, 0.0);
        SELECT xidUsuario AS idUsuario;
    END IF;
END $$


DROP PROCEDURE IF EXISTS ComprarMoneda $$
CREATE PROCEDURE `ComprarMoneda`(xidusuario INT UNSIGNED, xcantidad DECIMAL(20,10), xidmoneda INT UNSIGNED)
BEGIN
       START TRANSACTION;
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
       COMMIT;
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
       IF(PuedeVender(xidusuario, xcantidad, xidmoneda))
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
       
       SELECT LAST_INSERT_ID() AS idHistorial;
END $$

DROP PROCEDURE IF EXISTS Transferencia $$
CREATE PROCEDURE `Transferencia`(xidMoneda INT UNSIGNED, xCantidad DECIMAL(20,10) UNSIGNED, xidUsuarioTransfiere INT UNSIGNED, xidUsuarioTransferido INT UNSIGNED)
BEGIN
       START TRANSACTION;
       IF (PuedeVender(xidUsuarioTransfiere, xCantidad, xidMoneda))
       THEN
              UPDATE UsuarioMoneda
              SET cantidad = cantidad - xCantidad
              WHERE idMoneda = xidMoneda
              AND idUsuario = xidUsuarioTransfiere;

              INSERT INTO Historial (idMoneda, cantidad, fechaHora, compra, idUsuario)
              VALUES (xidMoneda, xCantidad, NOW(), NULL, xidUsuarioTransfiere);
       ELSE
              SIGNAL SQLSTATE '45000'
              SET MESSAGE_TEXT = "Cantidad Insuficiente!";
       END IF;

       IF (NOT (EXISTS (
                     SELECT *
                     FROM `UsuarioMoneda`
                     WHERE `idMoneda` = xidMoneda AND `idUsuario` = xidUsuarioTransferido
                     )))
                     THEN 
                            INSERT INTO `UsuarioMoneda` (`idUsuario`, `idMoneda`, cantidad)
                            VALUES(xidUsuarioTransferido, xidMoneda, 0);
       END IF;


              UPDATE UsuarioMoneda
              SET cantidad = cantidad + xCantidad
              WHERE idMoneda = xidMoneda
              AND idUsuario = xidUsuarioTransferido;

              INSERT INTO Historial (idMoneda, cantidad, fechaHora, compra, idUsuario)
              VALUES (xidMoneda, (xCantidad * -1), NOW(), NULL, xidUsuarioTransferido);
       COMMIT;
END $$