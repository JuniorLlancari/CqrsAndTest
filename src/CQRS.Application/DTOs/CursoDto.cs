using CQRS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.DTOs
{
    public class CursoDto
    {
        public Guid Id { get; set; }
        public required  string Titulo { get; set; }
        public  string Descripcion { get; set; } = string.Empty;
        public DateTime? FechaPublicacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public Decimal Precio { get; set; }
    }
}
