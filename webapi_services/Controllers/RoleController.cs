using AutoMapper;
using business_layer.Security.Role;
using data_access;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace webapi_services.Controllers
{
    public class RoleController : BaseController
    {
        public RoleController(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<IdentityRole>>> List()
        {
            return await Mediator.Send(new ListRoles.Request());
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Unit>> Create(CreateRole.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<Unit>> Delete(DeleteRole.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost]
        [Route("add-rol-to-user")]
        public async Task<ActionResult<Unit>> AddRoleToUser(AddRoleToUser.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost]
        [Route("del-rol-to-user")]
        public async Task<ActionResult<Unit>> DeleteRoleToUser(DeleteRoleToUser.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("get-roles/{username}")]
        public async Task<ActionResult<List<string>>> GetRolesByUser(string username)
        {
            return await Mediator.Send(new ListRolesByUser.Request { Username = username });
        }
    }
}
