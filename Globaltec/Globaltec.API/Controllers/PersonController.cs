using Globaltec.Domain.Models;
using Globaltec.Servico.Servicos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Globaltec.API.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("/ConsultePessoaPorCodigo/{codigo}")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsultePessoaPorCodigo([FromRoute] int codigo)
        {
            var respostaDaRequisicao = _personService.ConsultePessoaPorCodigo(codigo);
            return StatusCode(respostaDaRequisicao.CodHTTP, respostaDaRequisicao.Result);
        }

        [HttpGet("/ConsultePessoaPorUF/{uf}")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsultePessoaPorUF([FromRoute] string uf)
        {
            var respostaDaRequisicao = _personService.ConsultePessoasPorUF(uf);
            return StatusCode(respostaDaRequisicao.CodHTTP, respostaDaRequisicao.Result);
        }

        [HttpGet("/ConsulteTodasAsPessoas")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsulteTodasAsPessoas()
        {
            var respostaDaRequisicao = _personService.ConsulteTodasAsPessoas();
            return StatusCode(respostaDaRequisicao.CodHTTP, respostaDaRequisicao.Result);
        }

        [HttpPost("/GravePessoa")]
        [ProducesResponseType(typeof(Person), 201)]
        [ProducesResponseType(typeof(string), 409)]
        public ActionResult GravePessoa([FromBody] Person pessoa)
        {
            var respostaDaRequisicao = _personService.GravePessoa(pessoa);
            return StatusCode(respostaDaRequisicao.CodHTTP, respostaDaRequisicao.Result);
        }

        [HttpPut("/AtualizarPessoa")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult AtualizarPessoa([FromBody] Person pessoa)
        {
            var respostaDaRequisicao = _personService.AtualizarPessoa(pessoa);
            return StatusCode(respostaDaRequisicao.CodHTTP, respostaDaRequisicao.Result);
        }

        [HttpDelete("/RemoverPessoa/{codigo}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult RemoverPessoa([FromRoute] int codigo)
        {
            var respostaDaRequisicao = _personService.ExcluirPessoa(codigo);
            return StatusCode(respostaDaRequisicao.CodHTTP, respostaDaRequisicao.Result);
        }
    }
}
