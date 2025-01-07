using business_layer.ExceptionsManager;
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

namespace business_layer.Security.Role
{
    public class DeleteRoleToUser
    {
        public class Request : IRequest
        {
            public string UserName { get; set; }
            public string RolName { get; set; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.RolName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly UserManager<SystemUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<SystemUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolName);
                if (role == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, new { mensaje = "No se encontro el rol" });
                }

                var usuarioIden = await _userManager.FindByNameAsync(request.UserName);
                if (usuarioIden == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, new { mensaje = "No se encontro el usuario" });
                }

                var resultado = await _userManager.RemoveFromRoleAsync(usuarioIden, request.RolName);
                if (resultado.Succeeded)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo eliminar el rol");
            }
        }

    }
}
