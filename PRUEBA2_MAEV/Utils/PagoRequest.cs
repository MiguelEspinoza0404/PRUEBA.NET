using PRUEBA2_MAEV.DataBase.Models;

namespace PRUEBA2_MAEV.Utils
{
    public class PagoRequest
    {
        public int clienteId { get; set; }
        public decimal monto { get; set; }
        public MonedaEnum moneda { get; set; }
        public DateOnly fechaPago { get; set; }
    }
}
