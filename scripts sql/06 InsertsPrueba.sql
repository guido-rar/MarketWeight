Use 5to_MarketWeight

CALL AltaCriptoMoneda(100.50, 100, 'Bitcoin');
CALL AltaCriptoMoneda(50.25, 100, 'Ethereum');
CALL AltaCriptoMoneda(75.00, 100, 'Ripple');
CALL AltaCriptoMoneda(20.75, 100, 'Litecoin');
CALL AltaCriptoMoneda(30.10, 100, 'Cardano');
CALL AltaCriptoMoneda(60.80, 100, 'Polkadot');
CALL AltaCriptoMoneda(90.00, 100, 'Chainlink');
CALL AltaCriptoMoneda(110.15, 100, 'Stellar');
CALL AltaCriptoMoneda(40.60, 100, 'Dogecoin');
CALL AltaCriptoMoneda(70.85, 100, 'Tron');
CALL AltaUsuario(NULL, 'Ana', 'García', 'ana.garcia@example.com', 'pass1234');
CALL AltaUsuario(NULL, 'Luis', 'Martínez', 'luis.martinez@example.com', '1234abcd');
CALL AltaUsuario(NULL, 'Marta', 'Fernández', 'marta.fernandez@example.com', 'abcd1234');
CALL AltaUsuario(NULL, 'Carlos', 'Gómez', 'carlos.gomez@example.com', 'qwerty12');
CALL AltaUsuario(NULL, 'Laura', 'Rodríguez', 'laura.rodriguez@example.com', 'password');
CALL AltaUsuario(NULL, 'Pedro', 'López', 'pedro.lopez@example.com', 'abcd1234');
CALL AltaUsuario(NULL, 'Sofía', 'Hernández', 'sofia.hernandez@example.com', '12345678');
CALL AltaUsuario(NULL, 'Daniel', 'Pérez', 'daniel.perez@example.com', '1q2w3e4r');
CALL AltaUsuario(NULL, 'María', 'Torres', 'maria.torres@example.com', 'letmein1');
CALL AltaUsuario(NULL, 'Javier', 'Ramírez', 'javier.ramirez@example.com', 'welcome1');
CALL IngresarDinero(2, 0);
CALL IngresarDinero(3, 10000);
CALL IngresarDinero(2, 10000);

CALL ComprarMoneda (2, 3, 2);
CALL Transferencia (2, 0.5, 2, 3);