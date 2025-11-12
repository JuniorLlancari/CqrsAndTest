using CQRS.Domain.Abstraccions;
using CQRS.Domain.Matriculas;
using System.ComponentModel.DataAnnotations;

namespace CQRS.Domain.Cursos
{
    public class Curso : Entity
    {
        public Curso()
        {
                
        }
        private Curso(
            Guid cursoId,
            string? titulo,
            string? descripcion,
            DateTime? fechaPublicacion,
            DateTime? fechaCreacion,
            decimal precio
            ) : base(cursoId)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            FechaCreacion = fechaCreacion;
            FechaPublicacion = fechaPublicacion;
            Precio = precio;
        }

        public static Curso Create(
            string? titulo,
            string? descripcion,
            DateTime? fechaPublicacion,
            decimal precio)
        {
            var usuario = new Curso(
                Guid.NewGuid(),
                titulo,
                descripcion,
                fechaPublicacion,
                DateTime.Now,
                precio
                );

            return usuario;
        }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }
  
        public DateTime? FechaPublicacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public decimal Precio { get; set; }

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();


    }
}




