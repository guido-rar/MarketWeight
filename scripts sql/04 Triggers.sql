USE 5to_MarketWeight
DELIMITER $$
/*USUARIO*/

    /*Cifrado de contraseña*/

    
    DROP TRIGGER IF EXISTS `aftAltaPass` $$
    CREATE DEFINER=`root`@`localhost` TRIGGER `aftAltaPass` BEFORE INSERT ON `Usuario` 
    FOR EACH ROW 
    BEGIN   
        SET new.pass = SHA2(NEW.pass, 256);
    END $$
    

    /*Verifica si no existe otro usuario registrado con el mismo email*/
    DROP TRIGGER IF EXISTS `Usuario_BEFORE_INSERT`$$
    CREATE  TRIGGER `Usuario_BEFORE_INSERT` BEFORE INSERT ON `Usuario` 
    FOR EACH ROW 
    BEGIN
        IF(EXISTS(
            SELECT *
            FROM Usuario
            WHERE email = NEW.email
        ))THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = "Email ya registrado.";
        END IF;
    END $$



/*USUARIO MONEDA*/
    /*Compra*/
    
    /*Verifica y añade saldo a Usuario*/
    DROP TRIGGER IF EXISTS `aftInsertHistorial`$$
    CREATE DEFINER=`root`@`localhost` TRIGGER `aftInsertHistorial` AFTER INSERT ON `Historial` 
    FOR EACH ROW
    BEGIN
        IF(NEW.compra = TRUE)
        THEN 
            UPDATE Usuario
            SET saldo = saldo - PrecioCompra(NEW.cantidad, NEW.idMoneda)
            WHERE idUsuario = NEW.idUsuario;

            UPDATE `Moneda`
            SET cantidad = cantidad - NEW.cantidad
            WHERE `idMoneda` = NEW.`idMoneda`;
        ELSE
            UPDATE Usuario
            SET saldo = saldo + PrecioCompra(NEW.cantidad, NEW.idMoneda)
            WHERE idUsuario = NEW.idUsuario;

            UPDATE `Moneda`
            SET cantidad = cantidad + NEW.cantidad
            WHERE `idMoneda` = NEW.`idMoneda`;
        END IF;
    END $$



    /*Crea tabla UsuarioMoneda(si no existe)
    DROP TRIGGER IF EXISTS `BefComprarMoneda2_CreaUsuarioMoneda`$$
    CREATE DEFINER=`root`@`localhost` TRIGGER `BefComprarMoneda2_CreaUsuarioMoneda` BEFORE UPDATE ON `UsuarioMoneda` 
    FOR EACH ROW FOLLOWS `BefComprarMoneda_VerificaSaldo`
    BEGIN
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
        */

    /*Venta*/

    /*Verifica si el usuario tiene las monedas a vender*/
    -- DROP TRIGGER IF EXISTS `BefVenderMoneda`$$
    -- CREATE DEFINER=`root`@`localhost` TRIGGER `BefVenderMoneda` BEFORE UPDATE ON `UsuarioMoneda` 
    -- FOR EACH ROW
    -- BEGIN
    --         IF (OLD.cantidad > NEW.cantidad)
    --         THEN 
    --             SIGNAL SQLSTATE '45000'
    --             SET MESSAGE_TEXT = "Cantidad Insuficiente!";
    --         ELSE
    --             UPDATE Usuario
    --             SET saldo = saldo + PrecioCompra(NEW.cantidad, NEW.idMoneda)
    --             WHERE idUsuario = new.idUsuario;

    --             INSERT INTO Historial (idMoneda, cantidad, fechaHora, compra, idUsuario)
    --                 VALUES (NEW.idMoneda, NEW.cantidad, NOW(), FALSE, NEW.idUsuario);
    --         END IF;
    -- END $$



/*MONEDA*/

    DROP TRIGGER IF EXISTS BefAltaMoneda $$
    CREATE DEFINER=`root`@`localhost` TRIGGER `BefAltaMoneda` BEFORE INSERT ON `Moneda` 
    FOR EACH ROW   
    BEGIN
        IF(EXISTS(
            SELECT *
            FROM Moneda
            WHERE nombre = new.nombre
        ))THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = "Moneda ya registrada :v";
        END IF;
    END $$