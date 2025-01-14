using AutoMapper;
using business_layer.ExceptionsManager;
using business_layer.Persons.DTO;
using business_layer.Persons.Request;
using data_access;
using domain_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Persons.Helpers
{
    public class EditPerson : BaseHelper
    {
        public EditPerson(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<PersonaDTO> edit_person(EditPersonRequest request)
        {
            var persona = await _context.Personas.FindAsync(request.persona_id);
            if (persona == null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    new
                    {
                        mensaje = "No se encontró ninguna persona co nel id: "
                    + request.persona_id.ToString()
                    });
            }
            persona.persona_apellidos = request.persona_apellidos;
            persona.persona_nombre = request.persona_nombre;
            persona.persona_email = request.persona_email;
            persona.persona_observaciones = request.persona_observaciones;
            persona.persona_fecha_nacimiento = request.persona_fecha_nacimiento;
            persona.persona_telefono = request.persona_telefono;    
            persona.persona_movil = request.persona_movil;
            persona.genero_id = request.genero_id;

            int result = await _context.SaveChangesAsync();

            if (result <= 0)
            {
                throw new CustomException(HttpStatusCode.InternalServerError,
                    new
                    {
                        mensaje = "No se pudo editar la persona con el id: "
                    + request.persona_id.ToString()
                    });
            }

            return _mapper.Map<Persona, PersonaDTO>(persona);

        }
    }
}
