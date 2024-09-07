using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KDSB.DTOs.ProducDTOs
{
    public class EditProductDTO
    {
        public EditProductDTO(GetIdResultProductDTO getIdResultProduct)
        {
            Id = getIdResultProduct.Id;
            NombreKDSB = getIdResultProduct.NombreKDSB;
            DescripcionKDSB = getIdResultProduct.DescripcionKDSB;
            Precio = getIdResultProduct.Precio;
        }

        public EditProductDTO()
        {
            NombreKDSB = string.Empty;
            DescripcionKDSB = string.Empty;
        }

        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        public string NombreKDSB { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Apellido no puede tener más de 50 caracteres.")]
        public string DescripcionKDSB { get; set; }

        [Display(Name = "Precio")]
        public decimal Precio { get; set; }
    }
}
