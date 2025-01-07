using business_layer.Contracts;
using data_access;
using domain_layer;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Users
{
    public class CurrentUser
    {
        public class Request : IRequest<LoggedUser> { }

        public class Handler : IRequestHandler<Request, LoggedUser>
        {
            private readonly UserManager<SystemUser> _userManager;
            private readonly IJwtGenerator _jwtGenerador;
            private readonly IUserSession _usuarioSesion;

            private readonly DatabaseContext _context;
            public Handler(UserManager<SystemUser> userManager, IJwtGenerator jwtGenerador, IUserSession usuarioSesion, DatabaseContext context)
            {
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
                _context = context;
            }
            public async Task<LoggedUser> Handle(Request request, CancellationToken cancellationToken)
            {
                var userNameSession = _usuarioSesion.getUserSession().ToUpper().Normalize(); 
                var usuario = await _userManager.FindByNameAsync(userNameSession);
                var resultadoRoles = await _userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultadoRoles);

                //var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new System.Guid(usuario.Id)).FirstOrDefaultAsync();
                //if (imagenPerfil != null)
                //{
                    //var imagenCliente = new ImagenGeneral
                    //{
                    //    Data = Convert.ToBase64String(imagenPerfil.Contenido),
                    //    Extension = imagenPerfil.Extension,
                    //    Nombre = imagenPerfil.Nombre
                    //};

                    //return new UsuarioData
                    //{
                    //    NombreCompleto = usuario.NombreCompleto,
                    //    Username = usuario.UserName,
                    //    Token = _jwtGenerador.CrearToken(usuario, listaRoles),
                    //    Email = usuario.Email,
                    //    ImagenPerfil = imagenCliente
                    //};
                //}
                //else
                //{
                    return new LoggedUser
                    {
                        FullName = usuario.FullName,
                        Username = usuario.UserName,
                        Token = _jwtGenerador.CreateToken(usuario, listaRoles),
                        Email = usuario.Email,
                        Roles = listaRoles
                    };
                //}
            }
        }

    }
}
