using AutoMapper;
using business_layer.Persons.DTO;
using business_layer.Persons.Helpers;
using business_layer.Persons.Request;
using data_access;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi_services.Controllers
{
    public class PersonaController : BaseController
    {
        public PersonaController(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<PersonaDTO>>> getList(string filtro)
        {
            return await new ListPerson(_context, _mapper).getList(filtro);
        }

        [HttpGet("list-by-genero")]
        public async Task<ActionResult<List<PersonaDTO>>> getListByGeneroID(int genero_id)
        { 
            return await new ListPerson(_context, _mapper).getListByGeneroID(genero_id);
        }

        [HttpGet("find")]
        public async Task<ActionResult<PersonaDTO>> findPersona(long persona_id)
        {
            return await new FindPerson(_context, _mapper).find(persona_id);
        }

        [HttpPost("create")]
        public async Task<PersonaDTO> create_persona(CreatePersonRequest request)
        {
            return await new CreatePerson(_context, _mapper).create_person(request);
        }

        [HttpPut("edit")]
        public async Task<PersonaDTO> edit_persona(EditPersonRequest request)
        {
            return await new EditPerson(_context, _mapper).edit_person(request);
        }

    }
}
