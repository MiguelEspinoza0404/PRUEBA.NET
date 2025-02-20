Para ejecutar el proyecto hacer un BUILD del docker file.
Para levantar la base realizar este comando
docker run --name sqlserver -p 1433:1433 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Lem0nCode!' -d mcr.microsoft.com/mssql/server:2022-latest
despues importar el sql que se encuentra en el proyecto

una vez levantado la base y el proyecto ya puede ejecutar los endPoints
GET http://localhost:2323/api/pagos/liquidacion
GET https://localhost:7154/api/pagos/{idcliente}
POST https://localhost:7154/api/pagos 
en el body enviar lo siguiente
{
  "clienteId": 1,
  "monto": 2,
  "moneda": 0,
  "fechaPago": "2025-02-20"
}
cambiar valor de parametro en base a lo que necesita. ( SI NO EXISTE EL CLIENTE EN LA BASE NO FUNCIONA EL ENDPOINT)
