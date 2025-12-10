using CQRS.Domain.Abstraccions;
using CQRS.Domain.Entities.Matriculas;
using System.ComponentModel.DataAnnotations;

namespace CQRS.Domain.Entities.Cursos
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

        public string? Titulo { get; private set; }

        public string? Descripcion { get; private set; }
  
        public DateTime? FechaPublicacion { get; private set; }

        public DateTime? FechaCreacion { get; private set; }

        public decimal Precio { get; private  set; }

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();


    }
}




