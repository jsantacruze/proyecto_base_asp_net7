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
    public class CreateUser
    {
        public class Request : IRequest<LoggedUser>
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            public string Username { get; set; }
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
           
            public Handler(DatabaseContext context, UserManager<SystemUser> userManager, IJwtGenerator jwtGenerador)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }

            public async Task<LoggedUser> Handle(Request request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existe)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con este email" });
                }

                var existeUserName = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();
                if (existeUserName)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario con este username" });
                }


                var usuario = new SystemUser
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    UserName = request.Username
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);
                if (resultado.Succeeded)
                {
                    return new LoggedUser
                    {
                        FullName = usuario.FullName,
                        Token = _jwtGenerador.CreateToken(usuario, null),
                        Username = usuario.UserName,
                        Email = usuario.Email,
                        Roles = null
                    };
                }

                throw new CustomException(HttpStatusCode.BadRequest, "No se pudo crear el usuario");
            }
        }
    }
}
