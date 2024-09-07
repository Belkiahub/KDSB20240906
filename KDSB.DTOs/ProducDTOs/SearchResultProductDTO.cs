using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDSB.DTOs.ProducDTOs
{
    public class SearchResultProductDTO
    {
        public int CountRow { get; set; }
        public List<ProducDTOs> Data { get; set; }
        public class ProducDTOs
        {
            public int Id { get; set; }

            [Display(Name = "Nombre")]
            public string NombreKDSB { get; set; }

            [Display(Name = "Descripcion")]
            public string DescripcionKDSB { get; set; }

            [Display(Name = "Precio")]
            public decimal Precio { get; set; }
        }
    }
}
