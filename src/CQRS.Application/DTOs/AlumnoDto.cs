using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.DTOs
{
    public class AlumnoDto
    {
        public Guid Id { get; set; }
        public string NombreAlumno { get; set; }
    }
}
