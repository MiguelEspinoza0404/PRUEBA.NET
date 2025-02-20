# Configuración y Ejecución del Proyecto

## Construcción del Proyecto
Para ejecutar el proyecto, primero es necesario realizar un **build** del archivo `Dockerfile`.

## Levantamiento de la Base de Datos
Ejecuta el siguiente comando para desplegar la base de datos SQL Server en un contenedor Docker:

```sh
docker run --name sqlserver -p 1433:1433 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Lem0nCode!' -d mcr.microsoft.com/mssql/server:2022-latest

Importación de la Base de Datos

Una vez que el contenedor esté en ejecución, importa el archivo SQL que se encuentra dentro del proyecto.
Ejecución de Endpoints

Con la base de datos y el proyecto en funcionamiento, ya puedes ejecutar los siguientes endpoints:
Obtener Liquidación de Pagos

GET http://localhost:2323/api/pagos/liquidacion

Obtener Pagos de un Cliente por ID

GET https://localhost:7154/api/pagos/{idcliente}

Registrar un Pago

POST https://localhost:7154/api/pagos 

Body (JSON):

{
  "clienteId": 1,
  "monto": 2,
  "moneda": 0,
  "fechaPago": "2025-02-20"
}

⚠️ Importante: Asegúrate de que el cliente exista en la base de datos antes de realizar una solicitud al endpoint, de lo contrario, la operación no funcionará.
