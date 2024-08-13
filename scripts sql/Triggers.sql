USE 5to_MarketWeight $$
DELIMITER $$
/*USUARIO*/

    /*Cifrado de contraseña*/

    
    DROP TRIGGER IF EXISTS `aftAltaPass` $$
    CREATE DEFINER=`5to_agbd`@`localhost` TRIGGER `aftAltaPass` AFTER INSERT ON `Usuario` 
    FOR EACH ROW 
    BEGIN 
        UPDATE Usuario 
        SET new.pass = SHA2(NEW.pass, 256)
        WHERE idUsuario = NEW.idUsuario;
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
    DROP TRIGGER IF EXISTS `BefComprarMoneda`$$
    CREATE DEFINER=`5to_agbd`@`localhost` TRIGGER `BefComprarMoneda` BEFORE UPDATE ON `UsuarioMoneda` 
    FOR EACH ROW 
    BEGIN
            SELECT saldo INTO @xsaldo
            FROM `UsuarioMoneda`
            WHERE `idUsuario` = NEW.`idUsuario`;
            
            IF (@xsaldo >= PrecioCompra(NEW.cantidad, NEW.idMoneda))
            THEN 
                UPDATE Usuario
                SET saldo =- PrecioCompra(NEW.cantidad, NEW.idMoneda)
                WHERE idUsuario = xidusuario;
            
                INSERT INTO Historial (idMoneda, cantidad, fechaHora, accion, idUsuario)
                    VALUES (NEW.idMoneda, NEW.cantidad, NOW(), TRUE, NEW.idUsuario);
            ELSE
                SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = "Saldo Insuficiente";
            END IF;
    END $$



    /*Crea tabla UsuarioMoneda(si no existe)*/
    DROP TRIGGER IF EXISTS `BefComprarMoneda2`$$
    CREATE DEFINER=`5to_agbd`@`localhost` TRIGGER `BefComprarMoneda2` BEFORE UPDATE ON `UsuarioMoneda` 
    FOR EACH ROW FOLLOWS BefComprarMoneda
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


    /*Venta*/

    /*Verifica si el usuario tiene las monedas a vender*/
    DROP TRIGGER IF EXISTS `BefVenderMoneda`$$
    CREATE DEFINER=`5to_agbd`@`localhost` TRIGGER `BefVenderMoneda` BEFORE UPDATE ON `UsuarioMoneda` 
    FOR EACH ROW
    BEGIN
            IF (OLD.cantidad > NEW.cantidad)
            THEN 
                SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = "Cantidad Insuficiente!";
            ELSE
                UPDATE Usuario
                SET saldo =+ PrecioCompra(NEW.cantidad, NEW.idMoneda)
                WHERE idUsuario = new.idUsuario;

                INSERT INTO Historial (idMoneda, cantidad, fechaHora, accion, idUsuario)
                    VALUES (NEW.idMoneda, NEW.cantidad, NOW(), FALSE, NEW.idUsuario);
            END IF;
    END $$



/*MONEDA*/

    DROP TRIGGER IF EXISTS BefAltaMoneda $$
    CREATE DEFINER=`5to_agbd`@`localhost` TRIGGER `BefAltaMoneda` BEFORE INSERT ON `Moneda` 
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