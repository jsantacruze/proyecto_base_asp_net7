using data_access;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Role
{
    public class ListRoles
    {
        public class Request : IRequest<List<IdentityRole>>
        {
        }

        public class Handler : IRequestHandler<Request, List<IdentityRole>>
        {

            private readonly DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
                _context = context;
            }
            public async Task<List<IdentityRole>> Handle(Request request, CancellationToken cancellationToken)
            {
                var roles = await _context.Roles.ToListAsync();
                return roles;
            }
        }

    }
}
