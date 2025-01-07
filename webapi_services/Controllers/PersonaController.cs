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
    }
}
