using AutoMapper;
using business_layer.Persons.DTO;
using business_layer.Persons.Request;
using data_access;
using domain_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Persons.Helpers
{
    public class CreatePerson : BaseHelper
    {
        public CreatePerson(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<PersonaDTO> create_person(CreatePersonRequest request)
        {
            Persona persona = new Persona()
            {
                persona_apellidos = request.persona_apellidos,
                persona_nombre = request.persona_nombre,   
                persona_fecha_nacimiento = request.persona_fecha_nacimiento,
                persona_email = request.persona_email ?? "",  
                persona_observaciones = request.persona_observaciones ?? "",
                persona_telefono = request.persona_telefono ?? "",
                persona_movil = request.persona_movil ?? "",
                genero_id = request.genero_id
            };
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return await new FindPerson(_context, _mapper).find(persona.persona_id);
        }
    }
}
