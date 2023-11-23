using Globaltec.Domain.Models;
using Globaltec.Servico.Servicos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Globaltec.API.Controllers
{
    public class UserController : Controller
    {
      private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("LoginUsuario")]
        [ProducesResponseType(typeof(Login), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult AutentiqueUsuario([FromBody] Login login)
        {
            var responseRequest = _usersService.GetUserLoginPass(login);
            return StatusCode(responseRequest.CodHTTP, responseRequest.Result);
        }

        [Authorize]
        [HttpPost("CriarUsuario")]
        [ProducesResponseType(typeof(Users), 201)]
        [ProducesResponseType(typeof(string), 409)]
        public ActionResult CriarUsuario([FromBody] Login login)
        {
            var responseRequest = _usersService.CreateUser(login);
            return StatusCode(responseRequest.CodHTTP, responseRequest.Result);
        }
    }
}
