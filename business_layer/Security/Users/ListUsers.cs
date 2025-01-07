using data_access;
using domain_layer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace business_layer.Security.Users
{
    public class ListUsers
    {
        public class Request : IRequest<List<SystemUser>>
        {
        }

        public class Handler : IRequestHandler<Request, List<SystemUser>>
        {

            private readonly DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
                _context = context;
            }
            public async Task<List<SystemUser>> Handle(Request request, CancellationToken cancellationToken)
            {
                var roles = await _context.Users.ToListAsync();
                return roles;
            }
        }

    }
}
