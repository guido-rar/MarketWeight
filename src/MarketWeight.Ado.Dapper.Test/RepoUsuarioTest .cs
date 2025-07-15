// using System.Formats.Asn1;
// using MarketWeight.Core;
// using MarketWeight.Core.Persistencia;
// using MySqlConnector;

// namespace MarketWeight.Ado.Dapper.Test;

// public class RepoUsuarioTest : TestBase
// {
//     IRepoUsuario _repo;
//     public RepoUsuarioTest() : base()
//         => _repo = new RepoUsuario(Conexion);
    
//     [Fact]

//     public void TraerOK()
//     {
//         var usuarios = _repo.Obtener();
        
//         Assert.NotEmpty(usuarios);
//         Assert.Contains(usuarios,
//             m => m.Nombre == "Ana");
//     }

//     [Fact]

//     public void IngresarDineroOK()
//     {
//         _repo.Ingreso(1, 7707m);

//         _repo.Ingreso(2, 420m);

//         _repo.Ingreso(3, 5000m);

//         _repo.Ingreso(4, 6666m);

//     }

//     [Fact]
//     public void AltaUsuarioOK()
//     {
//         Usuario usuarioWalter = new()
//         {
//             Nombre = "Walte",
//             Apellido = "Beníte",
//             Email = "waltercoocker@gmail.com",
//             Password = "314159265358979"
//         };

//         Usuario usuarioJorge = new()
//         {
//             Nombre = "Jorge",
//             Apellido = "Casco",
//             Email = "JorgeCasco@gmail.com",
//             Password = "jorge123"
//         };

//         Usuario usuarioGuido = new()
//         {
//             Nombre = "Guido",
//             Apellido = "Gavilán",
//             Email = "guidopepin@gmail.com",
//             Password = "guidopepin123"
//         };

//         Usuario usuarioCarlos = new()
//         {
//             Nombre = "Carlos",
//             Apellido = "Bello",
//             Email = "carloselbello@gmail.com",
//             Password = "carlos123"
//         };


//         _repo.Alta(usuarioWalter);
//         _repo.Alta(usuarioJorge);
//         _repo.Alta(usuarioGuido);
//         _repo.Alta(usuarioCarlos);
//     }

//     [Fact]
//     public void ComprarMonedaOK()
//     {
//         _repo.Compra(3, 0.5m, 2);

//         _repo.Compra(2, 1m, 3);

//         _repo.Compra(2, 0.5m, 1);
//     }

//     [Fact]
//     public void ComprarMonedaFail()
//     {
//         var error =  Assert.ThrowsAny<Exception> (()=>_repo.Compra(6, 0.5m, 1));
//         Assert.Contains("Insuficiente", error.Message);
//     }

//     [Fact]
//     public void VenderMonedaOK()
//     {
//         _repo.Vender(2, 0.1m, 1);
//     }

//     [Fact]
//     public void VenderMonedaFail()
//     {
//         var error =  Assert.ThrowsAny<Exception> (()=>_repo.Vender(5, 0.5m, 2));
//         Assert.Contains("Insuficiente", error.Message);

//         error =  Assert.ThrowsAny<Exception> (()=>_repo.Vender(6, 0.5m, 5));
//         Assert.Contains("Insuficiente", error.Message);

//     }

//     [Fact]
//     public void ObtenerPorCondicionOK()
//     {
//         var usuarios = _repo.ObtenerPorCondicion("saldo >= 1000");
//         Assert.NotEmpty(usuarios);
//     }
//     [Fact]

//     public void TransferenciaOK()
//     {
//         var usuariosMoneda1 = _repo.ObtenerPorCondicionUsuarioMoneda(2, 0.5m);/*string? userid, decimal cantidad*/
//         _repo.Compra(2, 2.5m, 1);

//         Assert.NotEmpty(usuariosMoneda1);

//         _repo.Transferencia(2, 0.5m, 2, 6);

//         var usuariosMoneda2 = _repo.ObtenerPorCondicionUsuarioMoneda(6, 0.5m);
//         Assert.NotEmpty(usuariosMoneda2);
//     }

//         [Fact]
//         public void TransferenciaFAIL()
//     {

//         var error =  Assert.ThrowsAny<Exception> (()=>_repo.Transferencia(2, 0.5m, 8, 6));
//         Assert.Equal("Cantidad Insuficiente!", error.Message);

//     }

//      [Fact]
//     public void DetalleCompletoOK()
//     {
//         var usuario =_repo.DetalleCompleto(1);
//         Assert.NotNull(usuario);

//     }

//     [Fact]
//     public void DetalleCompletoBilleteraOK()
//     {
//         var usuario =_repo.DetalleCompleto(2);
//         Assert.NotNull(usuario);
//         Assert.NotNull(usuario.Billetera);
//         Assert.NotNull(usuario.Transacciones);
//         Assert.NotEmpty(usuario.Billetera);
//         Assert.NotEmpty(usuario.Transacciones);

//     }

//     /*Asyncc*/
//      [Fact]

//     public async Task TraerOKAsync()
//     {
//         var usuarios = await _repo.ObtenerAsync();
        
//         Assert.NotEmpty(usuarios);
//         Assert.Contains(usuarios,
//             m => m.Nombre == "Ana");
//     }

//     [Fact]

//     public async Task IngresarDineroOKAsync()
//     {
//         await _repo.IngresoAsync(1, 7707m);

//         await _repo.IngresoAsync(2, 420m);

//         await _repo.IngresoAsync(3, 5000m);

//         await _repo.IngresoAsync(4, 6666m);

//     }

//     [Fact]
//     public async Task AltaUsuarioOKAsync()
//     {
//         Usuario usuarioWalterAsync = new()
//         {
//             Nombre = "WalteAsync",
//             Apellido = "BeníteAsync",
//             Email = "waltercoockerAsync@gmail.com",
//             Password = "314159265358979"
//         };

//         Usuario usuarioJorgeAsync = new() 
//         {
//             Nombre = "JorgeAsync",
//             Apellido = "CascoAsync",
//             Email = "JorgeCascoAsync@gmail.com",
//             Password = "jorge123"
//         };

//         Usuario usuarioGuidoAsync = new()
//         {
//             Nombre = "GuidoAsync",
//             Apellido = "GavilánAsync",
//             Email = "guidopepinAsync@gmail.com",
//             Password = "guidopepin123"
//         };

//         Usuario usuarioCarlosAsync = new()
//         {
//             Nombre = "CarlosAsync",
//             Apellido = "BelloAsync",
//             Email = "carloselbelloAsync@gmail.com",
//             Password = "carlos123"
//         };


//         await _repo.AltaAsync(usuarioWalterAsync);
//         await _repo.AltaAsync(usuarioJorgeAsync);
//         await _repo.AltaAsync(usuarioGuidoAsync);
//         await _repo.AltaAsync(usuarioCarlosAsync);
//     }

//     [Fact]
//     public async Task ComprarMonedaOKAsync()
//     {
//         await _repo.CompraAsync(3, 0.5m, 2);

//         await _repo.CompraAsync(2, 1m, 3);

//         await _repo.CompraAsync(2, 0.5m, 1);
//     }

//     [Fact]
//     public async Task ComprarMonedaFailAsync()
//     {
//         var error =  await Assert.ThrowsAnyAsync<Exception> (async ()=>await _repo.CompraAsync(6, 0.5m, 1));
//         Assert.Contains("Insuficiente", error.Message);
//     }

//     [Fact]
//     public async Task VenderMonedaOKAsync()
//     {
//         await _repo.VenderAsync(2, 0.1m, 1);
//     }

//     [Fact]
//     public async Task VenderMonedaFailAsync()
//     {
//         var error = await Assert.ThrowsAnyAsync<Exception> (async ()=>await _repo.VenderAsync(5, 0.5m, 2));
//         Assert.Contains("Insuficiente", error.Message);

//         error = await Assert.ThrowsAnyAsync<Exception> (async ()=>await _repo.VenderAsync(6, 0.5m, 5));
//         Assert.Contains("Insuficiente", error.Message);

//     }

//     [Fact]
//     public async Task ObtenerPorCondicionOKAsync()
//     {
//         var usuarios = await _repo.ObtenerPorCondicionAsync("saldo >= 1000");
//         Assert.NotEmpty(usuarios);
//     }
//     [Fact]

//     public async Task TransferenciaOKAsync()
//     {
//         var usuariosMoneda1 = await _repo.ObtenerPorCondicionUsuarioMonedaAsync(2, 0.5m);/*string? userid, decimal cantidad*/
//         await _repo.CompraAsync(2, 2.5m, 1);

//         Assert.NotEmpty(usuariosMoneda1);

//         await _repo.TransferenciaAsync(2, 0.5m, 2, 6);

//         var usuariosMoneda2 = await _repo.ObtenerPorCondicionUsuarioMonedaAsync(6, 0.5m);
//         Assert.NotEmpty(usuariosMoneda2);
//     }

//         [Fact]
//         public async Task TransferenciaFAILAsync()
//     {

//         var error = await Assert.ThrowsAnyAsync<Exception> (async()=>await _repo.TransferenciaAsync(2, 0.5m, 8, 6));
//         Assert.Equal("Cantidad Insuficiente!", error.Message);

//     }

//      [Fact]
//     public async Task DetalleCompletoOKAsync()
//     {
//         var usuario =await _repo.DetalleCompletoAsync(1);
//         Assert.NotNull(usuario);

//     }

//     [Fact]
//     public async Task DetalleCompletoBilleteraOKAsync()
//     {
//         var usuario =await _repo.DetalleCompletoAsync(2);
//         Assert.NotNull(usuario);
//         Assert.NotNull(usuario.Billetera);
//         Assert.NotNull(usuario.Transacciones);
//         Assert.NotEmpty(usuario.Billetera);
//         Assert.NotEmpty(usuario.Transacciones);
//     }

// }