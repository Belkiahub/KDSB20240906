using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDSB.DTOs.ProducDTOs
{
    public class SearchQueryProductDTO
    {
        [Display(Name = "Nombre")]
        public string? NombreKDSB_Like { get; set; }

        [Display(Name = "Descripcion")]
        public string? DescripcionKDSB_Like { get; set; }

        [Display(Name = "Pagina")]
        public int Skip { get; set; }

        [Display(Name = "CantReg X Pagina")]
        public int Take { get; set; }

        /// <summary>
        /// 1 = No se cuenta los resultados de la búsqueda
        /// 2 = Cuenta los resultados de la búsqueda
        /// </summary>
        public byte SendRowCount { get; set; }
    }
}
