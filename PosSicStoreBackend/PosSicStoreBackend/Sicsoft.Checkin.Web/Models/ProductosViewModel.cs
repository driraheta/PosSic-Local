using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class ProductosViewModel
    {
        [Key]
        [StringLength(10)]
        public string CodPro { get; set; }

        [StringLength(2)]
        public string CodCorrida { get; set; }

        [StringLength(5)]
        public string NumTalla { get; set; }

        [StringLength(30)]
        public string NomPro { get; set; }

        [StringLength(50)]
        public string CodBarras { get; set; }

        [StringLength(15)]
        public string CodBarrasProv { get; set; }

        [StringLength(3)]
        public string CodMarca { get; set; }

        [StringLength(3)]
        public string CodDepto { get; set; }

        [StringLength(3)]
        public string CodLinea { get; set; }

        [StringLength(15)]
        public string CodInterno { get; set; }

        [StringLength(3)]
        public string CodProveedor { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoPro { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrecioVenta { get; set; }

        [StringLength(15)]
        public string UniMed { get; set; }

        public byte? PorDescuento { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoIni { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoAnt { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoAct { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoFob { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrecioMayor { get; set; }

        public short? InvInicial { get; set; }

        public long? Existencia { get; set; }

        public int? UnidPedidas { get; set; }

        public int? UnidCompras { get; set; }

        public int? UnidVentas { get; set; }

        public int? UnidEntrada { get; set; }

        public int? UnidSalida { get; set; }

        public DateTime? FecUltCompra { get; set; }

        public DateTime? FecUltVenta { get; set; }

        public DateTime? FecUltPedido { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoCompras { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoVtas { get; set; }

        public float? ConteoFisico { get; set; }

        [StringLength(50)]
        public string NombreSeccion { get; set; }

        public bool Exento { get; set; }

        public bool PrecioVariable { get; set; }

        public double? PorcUtilidad { get; set; }

        public double? Maximos { get; set; }

        public double? Minimos { get; set; }

        public bool ImprimeEtiquetas { get; set; }

        [StringLength(150)]
        public string Imagen { get; set; }

        public int? Busquedas { get; set; }

        public decimal? ImpuestoTarifa { get; set; }

        [StringLength(2)]
        public string CodImpuestoTarifa { get; set; }

        [StringLength(20)]
        public string CodProHacienda { get; set; }

        [StringLength(500)]
        public string CabysDescripcion { get; set; }

        public bool ModificaPrecio { get; set; }
        public string ImagenFullPath { get; set; }
        public string ImagenBase64 { get; set; }
        [Display(Name = "Imagen")]
        public IFormFile Upload { get; set; }

    }
}
