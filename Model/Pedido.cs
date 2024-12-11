using System;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class Pedido
    {
       
        public long IdPedido { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es obligatoria")]
        public DateTime FechaPedido { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es obligatoria")]
        public DateTime FechaEntrega { get; set; }

        [Required(ErrorMessage = "El nombre de contacto es obligatorio")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public long IdCliente { get; set; }

        public List<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();
    }
}
