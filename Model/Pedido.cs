using System;


namespace Model
{
    public class Pedido
    {
       
        public long IdPedido { get; set; }

      
        public DateTime FechaPedido { get; set; }

     
        public DateTime FechaEntrega { get; set; }


        public string ContactName { get; set; }

 
        public long IdCliente { get; set; }
    }
}
