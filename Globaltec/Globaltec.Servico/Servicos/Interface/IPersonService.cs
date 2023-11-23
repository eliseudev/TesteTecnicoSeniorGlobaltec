using Globaltec.Domain.Models;
using Globaltec.Domain.Utils;

namespace Globaltec.Servico.Servicos.Interface
{
    public interface IPersonService
    {
        ResponseRequest ConsultePessoaPorCodigo(int codPerson);
        ResponseRequest ConsultePessoasPorUF(string uf);
        ResponseRequest ConsulteTodasAsPessoas();
        ResponseRequest GravePessoa(Person person);
        ResponseRequest AtualizarPessoa(Person person);
        ResponseRequest ExcluirPessoa(int codPerson);
    }
}
