using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class PedidoItem
    {
        public long IdPedido { get; set; }

        [Required(ErrorMessage = "El ID del item es obligatorio")]
        public long IdItem { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        public string Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public double Cantidad { get; set; }

    }
}