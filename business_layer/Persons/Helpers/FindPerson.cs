using AutoMapper;
using business_layer.ExceptionsManager;
using business_layer.Persons.DTO;
using data_access;
using domain_layer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Persons.Helpers
{
    public class FindPerson
    {
        public class FindPersonRequest : IRequest<PersonaDTO>
        {
            public long? Id { get; set; }
        }

        public class FindPersonRequestHandler : IRequestHandler<FindPersonRequest, PersonaDTO>
        {
            private readonly DatabaseContext _context;
            private readonly IMapper _mapper;
            public FindPersonRequestHandler(DatabaseContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PersonaDTO> Handle(FindPersonRequest request, CancellationToken cancellationToken)
            {
                var persona = await _context.Personas
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(p => p.persona_id == request.Id);

                if (persona == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, new { mensaje = "No se encontro la persona con el ID: " + request.Id.ToString() });
                }

                var result = _mapper.Map<Persona, PersonaDTO>(persona);
                return result;
            }
        }

    }
}
