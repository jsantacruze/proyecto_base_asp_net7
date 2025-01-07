using AutoMapper;
using business_layer.Security.Users;
using data_access;
using domain_layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace webapi_services.Controllers
{
    public class UserController : BaseController
    {
        public UserController(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<ActionResult<List<SystemUser>>> getList()
        {
            return await Mediator.Send(new ListUsers.Request());
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoggedUser>> Login(LoginUser.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("create")]
        public async Task<ActionResult<LoggedUser>> Create(CreateUser.Request request)
        {
            return await Mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpGet("current")]
        public async Task<ActionResult<LoggedUser>> getCurrentUser()
        {
            return await Mediator.Send(new CurrentUser.Request());
        }

        [HttpPut("edit")]
        public async Task<ActionResult<LoggedUser>> Actualizar(EditUser.Request request)
        {
            return await Mediator.Send(request);
        }
    }
}
