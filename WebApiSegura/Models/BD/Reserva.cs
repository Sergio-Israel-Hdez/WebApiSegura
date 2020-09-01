using System;
using System.Collections.Generic;

namespace WebApiSegura.Models.BD
{
    public partial class Reserva
    {
        public int Idreserva { get; set; }
        public int? Idpelicula { get; set; }
        public int? Idusuario { get; set; }
        public int? Estado { get; set; }
        public DateTime? FechaRegis { get; set; }

        public virtual Pelicula IdpeliculaNavigation { get; set; }
        public virtual Usuario IdusuarioNavigation { get; set; }
    }
}
