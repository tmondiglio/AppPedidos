using System;


namespace Model
{
    public class Cliente
    {
        public long IdCliente { get; set; }

        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
    }
}
