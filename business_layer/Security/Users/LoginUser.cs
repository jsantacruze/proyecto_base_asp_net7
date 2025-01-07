using business_layer.Contracts;
using business_layer.ExceptionsManager;
using data_access;
using domain_layer;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Users
{
    public class LoginUser
    {
        public class Request : IRequest<LoggedUser>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Request, LoggedUser>
        {

            private readonly UserManager<SystemUser> _userManager;
            private readonly SignInManager<SystemUser> _signInManager;
            private readonly IJwtGenerator _jwtGenerador;

            private readonly DatabaseContext _context;
            public Handler(UserManager<SystemUser> userManager, SignInManager<SystemUser> signInManager, IJwtGenerator jwtGenerador, DatabaseContext context)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerador = jwtGenerador;
                _context = context;
            }
            public async Task<LoggedUser> Handle(Request request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                {
                    throw new CustomException(HttpStatusCode.Unauthorized);
                }

                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                var resultadoRoles = await _userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultadoRoles);



                //var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(usuario.Id)).FirstOrDefaultAsync();

                if (resultado.Succeeded)
                {
                    //if (imagenPerfil != null)
                    //{
                    //    var imagenCliente = new ImagenGeneral
                    //    {
                    //        Data = Convert.ToBase64String(imagenPerfil.Contenido),
                    //        Extension = imagenPerfil.Extension,
                    //        Nombre = imagenPerfil.Nombre
                    //    };
                        return new LoggedUser
                        {
                            FullName = usuario.FullName,
                            Token = _jwtGenerador.CreateToken(usuario, listaRoles),
                            Username = usuario.UserName,
                            Email = usuario.Email,
                            Roles = listaRoles
                            //,
                            //ImagenPerfil = imagenCliente
                        };
                    //}
                    //else
                    //{
                    //    return new LoggedUser
                    //    {
                    //        FullName = usuario.FullName,
                    //        Token = _jwtGenerador.CreateToken(usuario, listaRoles),
                    //        Username = usuario.UserName,
                    //        Email = usuario.Email,
                    //        //Imagen = null
                    //    };
                    //}
                }
                throw new CustomException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
