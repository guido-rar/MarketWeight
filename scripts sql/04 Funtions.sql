USE 5to_MarketWeight
DELIMITER $$
DROP FUNCTION IF EXISTS PrecioCompra $$
CREATE FUNCTION `PrecioCompra`(xcantidad DECIMAL(20,10), xidmoneda INT UNSIGNED) RETURNS DECIMAL(20,10)
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

DROP FUNCTION IF EXISTS PuedeComprar $$
CREATE FUNCTION `PuedeComprar`(xidusuario INT, xcantidad DECIMAL(20,10), xidmoneda INT UNSIGNED) RETURNS TINYINT
READS SQL DATA
BEGIN
    SELECT saldo INTO @saldo
    FROM Usuario
    WHERE `idUsuario` = xidusuario;

    SET @precio = PrecioCompra(xcantidad, xidmoneda);

    IF (@saldo >= @precio)
    THEN
        RETURN TRUE;
    ELSE
        RETURN FALSE;
    END IF;
END $$

DROP FUNCTION IF EXISTS PuedeVender $$
CREATE FUNCTION `PuedeVender`(xidusuario INT, xcantidadAVender DECIMAL(20,10), xidmoneda INT UNSIGNED) RETURNS TINYINT
READS SQL DATA
BEGIN
    SELECT cantidad INTO @cantidadMonedasUsuario
    FROM `UsuarioMoneda`
    WHERE `idMoneda` = xidmoneda AND `idUsuario` = xidusuario;

    IF(@cantidadMonedasUsuario >= xcantidadAVender)
    THEN
        RETURN 1;
    ELSE
        RETURN 0;
    END IF;
END $$

