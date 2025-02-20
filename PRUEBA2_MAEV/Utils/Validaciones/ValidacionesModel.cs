using PRUEBA2_MAEV.DataBase.Models;

namespace PRUEBA2_MAEV.Utils.Validaciones
{
    public class ValidacionesModel
    {

        public string mapearEnum(PagoRequest request)
        {

            if (request.moneda == MonedaEnum.USD) return "USD";
            if (request.moneda == MonedaEnum.GBP) return "GBP";
            if (request.moneda == MonedaEnum.EUR) return "EUR";
            return "";

        }


        public void ValidarFechaPago(PagoRequest request)
        {
            DateOnly fechaHoy = DateOnly.FromDateTime(DateTime.Today);

            if (request.fechaPago == fechaHoy)
            {
                throw new ArgumentException("La fecha de pago no puede ser igual a la fecha de hoy.");
            }

            Console.WriteLine("Fecha de pago válida.");
        }

    }
}
