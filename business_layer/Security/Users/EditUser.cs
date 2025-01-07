using business_layer.Contracts;
using business_layer.ExceptionsManager;
using data_access;
using domain_layer;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Users
{
    public class EditUser
    {
        public class Request : IRequest<LoggedUser>
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
            //public ImagenGeneral ImagenPerfil { get; set; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.FullName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Request, LoggedUser>
        {
            private readonly DatabaseContext _context;
            private readonly UserManager<SystemUser> _userManager;
            private readonly IJwtGenerator _jwtGenerador;
            private readonly IPasswordHasher<SystemUser> _passwordHasher;

            public Handler(DatabaseContext context, UserManager<SystemUser> userManager, IJwtGenerator jwtGenerador, IPasswordHasher<SystemUser> passwordHasher)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _passwordHasher = passwordHasher;
            }

            public async Task<LoggedUser> Handle(Request request, CancellationToken cancellationToken)
            {
                var usuarioIden = await _userManager.FindByNameAsync(request.Username);
                if (usuarioIden == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con este username" });
                }

                var resultado = await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.Username).AnyAsync();
                if (resultado)
                {
                    throw new CustomException(HttpStatusCode.InternalServerError, new { mensaje = "Este email pertenece a otro usuario" });
                }

                //if (request.ImagenPerfil != null)
                //{

                //    var resultadoImagen = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(usuarioIden.Id)).FirstOrDefaultAsync();
                //    if (resultadoImagen == null)
                //    {
                //        var imagen = new Documento
                //        {
                //            Contenido = System.Convert.FromBase64String(request.ImagenPerfil.Data),
                //            Nombre = request.ImagenPerfil.Nombre,
                //            Extension = request.ImagenPerfil.Extension,
                //            ObjetoReferencia = new Guid(usuarioIden.Id),
                //            DocumentoId = Guid.NewGuid(),
                //            FechaCreacion = DateTime.UtcNow
                //        };
                //        _context.Documento.Add(imagen);
                //    }
                //    else
                //    {
                //        resultadoImagen.Contenido = System.Convert.FromBase64String(request.ImagenPerfil.Data);
                //        resultadoImagen.Nombre = request.ImagenPerfil.Nombre;
                //        resultadoImagen.Extension = request.ImagenPerfil.Extension;
                //    }


                //}



                usuarioIden.FullName = request.FullName;
                usuarioIden.PasswordHash = _passwordHasher.HashPassword(usuarioIden, request.Password);
                usuarioIden.Email = request.Email;

                var resultadoUpdate = await _userManager.UpdateAsync(usuarioIden);

                var resultadoRoles = await _userManager.GetRolesAsync(usuarioIden);
                var listRoles = new List<string>(resultadoRoles);

                //var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(usuarioIden.Id)).FirstAsync();
                //ImagenGeneral imagenGeneral = null;
                //if (imagenPerfil != null)
                //{
                //    imagenGeneral = new ImagenGeneral
                //    {
                //        Data = Convert.ToBase64String(imagenPerfil.Contenido),
                //        Nombre = imagenPerfil.Nombre,
                //        Extension = imagenPerfil.Extension
                //    };
                //}


                if (resultadoUpdate.Succeeded)
                {
                    return new LoggedUser
                    {
                        FullName = usuarioIden.FullName,
                        Username = usuarioIden.UserName,
                        Email = usuarioIden.Email,
                        Token = _jwtGenerador.CreateToken(usuarioIden, listRoles)
                        //,
                        //ImagenPerfil = imagenGeneral
                    };
                }

                throw new System.Exception("No se pudo actualizar el usuario");

            }
        }
    }
}
