using System.ComponentModel.DataAnnotations;

namespace PRUEBA2_MAEV.DataBase.Models
{
    public class PagoModel
    {
        [Key]
        public int pagoId { get; set; }
        public int clienteId { get; set; }
        public decimal monto { get; set; }
        public string modena { get; set; }
        public string nombre { get; set; }
        public DateOnly fechaPago { get; set; }

        public PagoModel() { }

        public PagoModel(decimal monto, string modena, DateOnly fechaPago)
        {
            this.monto = monto;
            this.modena = modena;
            this.fechaPago = fechaPago;
        }
    }
}
