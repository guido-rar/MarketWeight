USE 5to_MarketWeight;

/*USERS*/
CREATE USER 'papuSupremo'@'localhost' IDENTIFIED BY 'passPapuSupremo';
CREATE USER 'usuario'@'%' IDENTIFIED BY 'passUsuario';

/*GRANTS*/
GRANT ALL ON MarkeWeight.* TO 'papuSupremo'@'localhost';
GRANT SELECT, UPDATE(cantidad) ON MarketWeight.Moneda TO 'usuario'@'%';
GRANT SELECT, UPDATE, INSERT ON MarketWeight.UsuarioMoneda TO 'usuario'@'%';
GRANT SELECT, INSERT ON MarketWeight.Historial TO 'usuario'@'%';