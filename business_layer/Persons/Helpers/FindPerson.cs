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
using Microsoft.AspNetCore.Mvc;


namespace business_layer.Persons.Helpers
{
    public class FindPerson : BaseHelper
    {
        public FindPerson(DatabaseContext context, 
            IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<PersonaDTO> find(long persona_id)
        {
            var persona = await _context.Personas
                .Include(p => p.Genero)
                .Where(p => p.persona_id == persona_id)
                .FirstOrDefaultAsync();
            if (persona == null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    new
                    {
                        mensaje = "No se encontro ninguna persona conel id: "
                    + persona_id.ToString()
                    });
            }

            var result = _mapper.Map<Persona, PersonaDTO>(persona);
            return result;
        }
    }
}
