
USE 5to_MarketWeight
/*USERS*/

CREATE USER 'papuSupremo'@'127.0.0.1' IDENTIFIED BY 'passPapuSupremo1#';
CREATE USER 'usuario'@'%' IDENTIFIED BY 'passUsuario1#';

/*GRANTS*/
GRANT SELECT, UPDATE, INSERT ON 5to_MarketWeight.* TO 'papuSupremo'@'localhost';
GRANT SELECT, UPDATE(cantidad) ON 5to_MarketWeight.Moneda TO 'usuario'@'%';
GRANT SELECT, UPDATE, INSERT ON 5to_MarketWeight.UsuarioMoneda TO 'usuario'@'%';
GRANT SELECT, INSERT ON 5to_MarketWeight.Historial TO 'usuario'@'%';