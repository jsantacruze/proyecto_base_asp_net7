using AutoMapper;
using business_layer.Persons.DTO;
using business_layer.Persons.Helpers;
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

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<ActionResult<List<PersonaDTO>>> getList(string filtro)
        {
            return await new ListPerson(_context, _mapper).getList(filtro);
        }

        [AllowAnonymous]
        [HttpGet("list-by-genero")]
        public async Task<ActionResult<List<PersonaDTO>>> getListByGeneroID(int genero_id)
        { 
            return await new ListPerson(_context, _mapper).getListByGeneroID(genero_id);
        }
    }
}
