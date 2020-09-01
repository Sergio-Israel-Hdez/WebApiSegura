using System;
using System.Collections.Generic;

namespace WebApiSegura.Models.BD
{
    public partial class Usuario
    {
        public Usuario()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int Idusuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Rol { get; set; }
        public int? Estado { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
