DELIMITER $$
DROP FUNCTION PrecioCompra;
CREATE DEFINER=`5to_agbd`@`localhost` FUNCTION `PrecioCompra`(xcantidad INT, xidmoneda INT UNSIGNED) RETURNS float
    READS SQL DATA
BEGIN
    SELECT precio INTO @xprecio
    FROM `Moneda`
    WHERE `idMoneda` = xidmoneda;
    IF(idMoneda = xidmoneda)
    THEN
        SET @preciototal = @xprecio * xcantidad;
    END IF;
    RETURN @preciototal;
END $$