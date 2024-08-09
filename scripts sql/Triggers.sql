DELIMITER $$

DROP TRIGGER `aftAltaPass`;
CREATE DEFINER=`5to_agbd`@`localhost` TRIGGER `aftAltaPass` AFTER INSERT ON `Usuario` FOR EACH ROW BEGIN 
    UPDATE Usuario 
    SET NEW.pass = SHA2 (NEW.pass, 256)
    WHERE idUsuario = NEW.idUsuario;
END $$

DROP TRIGGER `BefAltaUsuario`;
CREATE DEFINER=`5to_agbd`@`localhost` TRIGGER `BefAltaUsuario` BEFORE INSERT ON `Usuario` FOR EACH ROW BEGIN
        INSERT INTO Billetera (saldo)
        VALUES (0);
END $$

DROP TRIGGER `BefComprarMoneda`;
CREATE DEFINER=`5to_agbd`@`localhost` TRIGGER `BefComprarMoneda` BEFORE UPDATE ON `UsuarioMoneda` FOR EACH ROW BEGIN
        IF NOT (EXISTS (
            SELECT *
            FROM `UsuarioMoneda`
            WHERE `idMoneda` = NEW.`idMoneda` AND `idUsuario` = NEW.`idUsuario`
        ))
        THEN 
            INSERT INTO `UsuarioMoneda` (`idUsuario`, `idMoneda`, cantidad)
                    Values(NEW.idUsuario, NEW.idMoneda, 0);
        END IF;
END $$

DROP TRIGGER `BefVenderMoneda`;
CREATE DEFINER=`5to_agbd`@`localhost` TRIGGER `BefVenderMoneda` BEFORE UPDATE ON `UsuarioMoneda` FOR EACH ROW BEGIN
        SELECT cantidad INTO @xcantidad
        FROM `UsuarioMoneda`
        WHERE idUsuario = NEW.idUsuario;
        IF (@xcantidas > NEW.cantidad)
        THEN 
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = "Cantidad Insuficiente!";
        END IF;
END $$

DROP TRIGGER `Usuario_BEFORE_INSERT`;
CREATE  TRIGGER `Usuario_BEFORE_INSERT` BEFORE INSERT ON `Usuario` FOR EACH ROW BEGIN
    IF(EXISTS(
        SELECT *
        FROM Usuario
        WHERE email = NEW.email
    ))THEN
    SIGNAL SQLSTATE '45000'
    SET MESSAGE_TEXT = "Email ya registrado.";
END