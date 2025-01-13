using AutoMapper;
using business_layer.ExceptionsManager;
using business_layer.Persons.DTO;
using data_access;
using domain_layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static business_layer.Persons.Helpers.FindPerson;

namespace business_layer.Persons.Helpers
{
    public class ListPerson : BaseHelper
    {
        public ListPerson(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<PersonaDTO>> getListByGeneroID(int genero_id)
        {
            var personas = await _context.Personas
                .Include(p => p.Genero)
                .Where(p => p.genero_id == genero_id)
                .ToListAsync();

            if (!personas.Any())
            {
                throw new CustomException(HttpStatusCode.NotFound, 
                    new { mensaje = "No se encontro ninguna coincidencia para el id de género: " 
                    + genero_id.ToString() });
            }
            var result = _mapper.Map<List<Persona>, List<PersonaDTO>>(personas);
            return result;
        }

        public async Task<List<PersonaDTO>> getList(string filtro)
        {
            var personas = await _context.Personas
            .Include(p => p.Genero)
            .Where(p => p.persona_nombre.Contains(filtro) || p.persona_apellidos.Contains(filtro))
            .ToListAsync();

            if (!personas.Any())
            {
                throw new CustomException(HttpStatusCode.NotFound, new { mensaje = "No se encontro ninguna coincidencia con el criterio: " + filtro });
            }

            var result = _mapper.Map<List<Persona>, List<PersonaDTO>>(personas);
            return result;
        }
    }
}
