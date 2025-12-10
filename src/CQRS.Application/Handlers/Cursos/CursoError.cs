using CQRS.Domain.Abstraccions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Handlers.Cursos
{
    public class CursoError
    {
        public static Error NoEncontrado = new(
                    "Curso.NoEncontrado",
                    "El curso no se encuentra");
    }
}
