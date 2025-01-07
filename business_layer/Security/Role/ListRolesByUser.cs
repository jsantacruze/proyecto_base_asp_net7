using business_layer.ExceptionsManager;
using domain_layer;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Role
{
    public class ListRolesByUser
    {
        public class Request : IRequest<List<string>>
        {
            public string Username { get; set; }
        }

        public class Manejador : IRequestHandler<Request, List<string>>
        {
            private readonly UserManager<SystemUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Manejador(UserManager<SystemUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
                _userManager = userManager;
            }

            public async Task<List<string>> Handle(Request request, CancellationToken cancellationToken)
            {
                var usuarioIden = await _userManager.FindByNameAsync(request.Username);
                if (usuarioIden == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, new { mensaje = "No existe el usuario" });
                }

                var resultados = await _userManager.GetRolesAsync(usuarioIden);
                return new List<string>(resultados);
            }
        }
    }
}
