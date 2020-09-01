using System;
using System.Collections.Generic;

namespace WebApiSegura.Models.BD
{
    public partial class Categoria
    {
        public Categoria()
        {
            Pelicula = new HashSet<Pelicula>();
        }

        public int Idcategoria { get; set; }
        public string Nombre { get; set; }
        public int? Estado { get; set; }

        public virtual ICollection<Pelicula> Pelicula { get; set; }
    }
}
