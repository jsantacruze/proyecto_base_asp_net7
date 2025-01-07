using business_layer.ExceptionsManager;
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
    public class DeleteRole
    {
        public class Request : IRequest
        {
            public string RolName { get; set; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.RolName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolName);
                if (role == null)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, new { mensaje = "No existe el rol" });
                }

                var resultado = await _roleManager.DeleteAsync(role);
                if (resultado.Succeeded)
                {
                    return Unit.Value;
                }

                throw new System.Exception("No se pudo eliminar el rol");
            }
        }

    }
}
