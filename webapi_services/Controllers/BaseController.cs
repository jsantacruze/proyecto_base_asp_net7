using AutoMapper;
using data_access;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace webapi_services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly DatabaseContext _context;
        protected readonly IMapper _mapper;

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public BaseController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
