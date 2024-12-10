using System;


namespace Model
{
    public class PedidoItem
    {
     
        public long IdPedido { get; set; }

       
        public long IdItem { get; set; }

       
        public string Producto { get; set; }

  
        public double Cantidad { get; set; }
    }
}
