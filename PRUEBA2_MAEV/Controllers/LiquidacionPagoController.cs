using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRUEBA2_MAEV.DataBase;
using PRUEBA2_MAEV.DataBase.Models;
using PRUEBA2_MAEV.Utils;
using PRUEBA2_MAEV.Utils.Validaciones;

namespace PRUEBA2_MAEV.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LiquidacionPagoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ValidacionesModel _validar;

        public LiquidacionPagoController(ValidacionesModel validar, ApplicationDbContext context)
        {
            _context = context;
            _validar = validar;
        }

        [HttpPost("pagos")]
        public async Task<ActionResult<GenericResponse<PagoModel>>> Operar([FromBody] PagoRequest request)
        {
            try
            {

                var response = new GenericResponse<PagoModel>(true, "Operación realizada con éxito", null);
                var moneda = _validar.mapearEnum(request);
                _validar.ValidarFechaPago(request);
                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@monto", request.monto ),
                    new SqlParameter("@moneda", moneda),
                    new SqlParameter("@fechaPago", request.fechaPago),
                    new SqlParameter("@clienteId", request.clienteId),
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC SP_REGISTRAR_PAGO @monto, @moneda, @fechaPago,@clienteId", parametros);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new GenericResponse<PagoModel>(false, "Error en la operación: " + ex.Message, null);
                return StatusCode(500, errorResponse);
            }
        }
        [HttpGet("pagos/{clienteId}")]
        public async Task<ActionResult<GenericResponse<object>>> Historial(int clienteId)
        {
            try
            {
                var historicoOperaciones = await _context.pagoModel
                    .FromSqlRaw("EXEC SP_GET_PAGO @clienteId = {0}", clienteId)
                    .ToListAsync();

                decimal totalPagos = historicoOperaciones.Sum(p => p.monto);

                var clienteNombre = historicoOperaciones.FirstOrDefault()?.nombre;

                if (clienteNombre == null)
                {
                    var errorResponse = new GenericResponse<object>(false, "Cliente no encontrado.", null);
                    return NotFound(errorResponse);
                }

                var responseData = new
                {
                    nombre = clienteNombre,     
                    totalPagos = totalPagos,     
                    pagos = historicoOperaciones 
                };

                var genericResponse = new GenericResponse<object>(
                    true,
                    "Historial obtenido con éxito",
                    responseData           
                );

                return Ok(genericResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new GenericResponse<object>(
                    false,
                    "Error en la operación: " + ex.Message,
                    null
                );
                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet("pagos/liquidacion")]
        public async Task<ActionResult<GenericResponse<object>>> liquidacion()
        {
            try
            {
                var historicoOperaciones = await _context.liquidacionModel
                    .FromSqlRaw("EXEC SP_GET_LIQUIDACION")
                    .ToListAsync();

              

                var genericResponse = new GenericResponse<object>(
                    true,
                    "Historial obtenido con éxito",
                    historicoOperaciones
                );

                return Ok(genericResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new GenericResponse<object>(
                    false,
                    "Error en la operación: " + ex.Message,
                    null
                );
                return StatusCode(500, errorResponse);
            }
        }


    }
}
