using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DataLayer;

namespace BusinessLayer
{
    public class BL_Usuarios
    {
        private DL_Usuarios oDL_Usuarios = new DL_Usuarios();

        public List<Usuario> ListarUsuarios()
        {
            return oDL_Usuarios.listarUsuarios();
        }

    }
}
