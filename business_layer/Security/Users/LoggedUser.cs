using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Users
{
    public class LoggedUser
    {
        public string FullName { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<String> Roles { get; set; }
        //public string Imagen { get; set; }

        //public ImagenGeneral ImagenPerfil { get; set; }
    }
}
