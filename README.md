<h1 align="center">E.T. Nº12 D.E. 1º "Libertador Gral. José de San Martín"</h1>
<p align="center">
  <img src="https://et12.edu.ar/imgs/computacion/vamoaprogramabanner.png" alt="Banner Computación">
</p>

## Computación 2024

**Asignatura**: Administracion y Gestion de Base de Datos

**Curso**: 5° 8°

# MarketWeight

MarketWeight permite a los usuarios visualizar gráficamente la oferta y demanda de distintas criptomonedas a través de una iterfaz sencilla y gráficos que reflejan las transacciones. Todos estos datos son almacenados en una base de datos relacional escrita en MySQL. El objetivo de la aplicación es facilitar el análisis y la toma de decisiones sobre el comportamiento del mercado.

## Comenzando 🚀

Clonar el repositorio github, desde Github Desktop o ejecutar en la terminal o CMD:
```
git clone https://github.com/carlete-afk/MarketWeight
```

### Pre-requisitos 📋

- [Visual Studio Code](https://code.visualstudio.com/download)
- [.NET 8.0](https://dotnet.microsoft.com/es-es/download/dotnet/8.0).

- [MySQL WorkBench](https://dev.mysql.com/downloads/workbench/)


## Despliegue 📦

- Para poder correr estos scripts ejecuta el siguiente comando dentro de la terminal integrada de la carpeta `scripts.sql`

```shell
mysql -u tuUsuario -p
```

- Una vez loggeado ejecute el siguiente comando para crear la BD.

```shell
source Install.sql
```

- Ahora desde la carpeta `MarketWeight.Ado.Dapper.Test` puede correr cualquier prueba o en la terminal integrada escribir este comando para correrlas al mismo tiempo.

```shell
dotnet test -v d
```


## ⚙️ MinimalAPI

Debés crear un archivo `appSettings.json` dentro de la carpeta `minimalAPI` con el siguiente contenido (reemplazá los datos de conexión con los tuyos):

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MySQL": "Server=localhost;User ID=(TuUsuario);Password=(TuContraseña)!;Database=5to_marketweight;"
  }
}
````

> 🧠 Este archivo es fundamental para que Dapper pueda conectarse a la base de datos correctamente.

---

## 👤 Crear un Usuario

> ⚠️ IMPORTANTE: Al crear un usuario desde Postman, cliente HTTP o frontend, **todos los campos llegan como `null` por defecto**.
> Por eso, debés **escribir los valores tipo string entre comillas**. Si no, se envían como `null` y no se guarda correctamente.

### ✅ Ejemplo correcto (en formato JSON):

```json
{
  "idUsuario": 0,
  "nombre": "Pepe",
  "apellido": "Gomez",
  "email": "pepe@gmail.com",
  "password": "claveSegura123",
  "saldo": 153
}
```

### ❌ Ejemplo incorrecto:

```json
{
  "nombre": Pepe,         // ❌ Faltan comillas
  "apellido": Gomez,
  "email": pepe@gmail.com
}
```

---

## 🪙 Crear una Moneda

> ⚠️ También en el alta de monedas: **el campo `nombre` debe ir entre comillas**.

### ✅ Ejemplo correcto:

```json
{
  "nombre": "PepeCoin",
  "precio": 10.5
}
```

### ❌ Ejemplo incorrecto:

```json
{
  "nombre": PepeCoin,   // ❌ Sin comillas
  "precio": 10.5
}
```

---

## 🔐 ID Manual y Error Controlado (Ultimas modificaciones)

* Si se pasa `idUsuario = 0` o `null`, el sistema lo genera automáticamente con `AUTO_INCREMENT`.
* Si se pasa un `idUsuario` que ya existe, se lanza un error:
  **"El ID ya existe. Elegí otro."**

---

## Construido con 🛠️

- C# 12.0
- MySQL 8.0
- Visual Studio Code.

## Versionado 📌

Usamos [SemVer](http://semver.org/) para el versionado. Para todas las versiones disponibles, mira los [tags en este repositorio](https://github.com/ET12DE1Computacion/simpleTemplateCSharp/tags).

## Autores ✒️

- **Carlos Bello** - [carlete-afk](https://github.com/carlete-afk)
- **Walter Benítez** - [Walter-Cooking](https://github.com/Walter-Cooking)
- **Jorge Casco** - [jorge-link](https://github.com/jorge-link)
- **Guido Gavilán** - [guido-rar](https://github.com/guido-rar)
- **Francisco García** - [SirFrancis2007](https://github.com/SirFrancis2007) 

## Licencia 📄

Este proyecto está bajo la Licencia Creative Commons Attribution 4.0 International - mira el archivo [LICENSE](LICENSE) para detalles.