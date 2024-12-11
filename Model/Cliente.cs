using System;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class Cliente
    {
        
        public long IdCliente { get; set; }

        [Required(ErrorMessage = "El CUIT es obligatorio")]
        public string Cuit { get; set; }

        [Required(ErrorMessage = "La Razón Social es obligatoria")]
        public string RazonSocial { get; set; }
        
        [Required(ErrorMessage = "La Dirección es obligatoria")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La Localidad es obligatoria")]
        public string Localidad { get; set; }
    }
}
