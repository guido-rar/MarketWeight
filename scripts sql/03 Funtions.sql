USE 5to_MarketWeight $$
DELIMITER $$
DROP FUNCTION IF EXISTS PrecioCompra $$
CREATE DEFINER=`5to_agbd`@`localhost` FUNCTION `PrecioCompra`(xcantidad DECIMAL(20,10), xidmoneda INT UNSIGNED) RETURNS DECIMAL(20,10)
    READS SQL DATA
BEGIN
    SELECT precio INTO @xprecio
    FROM `Moneda`
    WHERE `idMoneda` = xidmoneda;
    IF(@xprecio is not null)
    THEN
        RETURN @xprecio * xcantidad;
    END IF;
    RETURN NULL;
END $$