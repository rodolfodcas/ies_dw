using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.DTO
{
    public class BODTO
    {
        public int Id { get; set; }
        
        [Required]
        [DisplayName("Nombre Sucursal")]
        [MaxLength(32, ErrorMessage = "La logitud maxima es de 32 caracteres")]
        public string Name { get; set; }
        
        [Required]
        [DisplayName("Direccion")]
        [MaxLength(16, ErrorMessage = "La logitud maxima es de 16 caracteres")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Ciudad")]
        [MaxLength(16, ErrorMessage = "La logitud maxima es de 16 caracteres")]
        public string City { get; set; }
        
        [Required]
        [DisplayName("Estado")]
        [MaxLength(16, ErrorMessage = "La logitud maxima es de 16 caracteres")]
        public string State { get; set; }

        [Required]
        [DisplayName("Pais")]
        [MaxLength(16, ErrorMessage = "La logitud maxima es de 16 caracteres")]
        public string Country { get; set; }

        [Required]
        [DisplayName("Codigo Postal")]
        [MaxLength(5, ErrorMessage = "La logitud maxima es de 5 caracteres")]
        public string ZIP { get; set; }

        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
