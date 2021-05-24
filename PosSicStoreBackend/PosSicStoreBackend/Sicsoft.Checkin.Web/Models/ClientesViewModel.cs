using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class ClientesViewModel
    {
        [Key]
        [StringLength(15)]
        public string CodCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string NomCliente { get; set; }

        [StringLength(6)]
        public string FecCumpleanos { get; set; }

        [StringLength(10)]
        public string Telefono { get; set; }

        [StringLength(10)]
        public string Celular { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [Column(TypeName = "text")]
        public string Direccion { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoCompra { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoAbono { get; set; }

        public DateTime? FecUltAbono { get; set; }

        public DateTime? FecIng { get; set; }

        [StringLength(50)]
        public string MarcaVehiculo { get; set; }

        [StringLength(50)]
        public string ModeloVehiculo { get; set; }

        [StringLength(20)]
        public string Cedula { get; set; }

        [StringLength(2)]
        public string TipoCedula { get; set; }

        public int? CodPrecio { get; set; }

        public int? DiasCredito { get; set; }

        public int? CondicionVenta { get; set; }

        [StringLength(5)]
        public string CodClienteGaxpar { get; set; }

        public decimal? LimiteCredito { get; set; }
    }
}
