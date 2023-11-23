using Globaltec.Domain.Models;
using Globaltec.Domain.Utils;
using Globaltec.Servico.Servicos.Interface;
using System.Net;

namespace Globaltec.Servico.Servicos
{
    public class PersonService : IPersonService
    {
        public static List<Person> Pessoas = new List<Person>()
        {
            new Person(1, "JOÃO BATISTA PEREIRA DE SOUZA", "175.600.670-93", "GO", new DateTime(1989, 08, 31)),
            new Person(2, "FABIO AUGUSTO DOS SANTOS", "278.008.020-51", "TO", new DateTime(1990, 05, 20)),
            new Person(3, "MATHIAS ROCHA BENEDITO", "635.180.120-61", "BA", new DateTime(1991, 08, 22))
        };
        public ResponseRequest AtualizarPessoa(Person person)
        {
            throw new NotImplementedException();
        }

        public ResponseRequest ConsultePessoaPorCodigo(int codPerson)
        {
            try
            {
                var pessoaPersistida = Pessoas.FirstOrDefault(p => p.Id == codPerson);
                if (pessoaPersistida == null)
                    return new ResponseRequest(HttpStatusCode.NotFound, ReturnMessages.RegisternotFound);

                return new ResponseRequest(HttpStatusCode.OK, pessoaPersistida);
            }
            catch (Exception)
            {
                return new ResponseRequest(HttpStatusCode.InternalServerError, ReturnMessages.InternalServerErro);
            }
        }

        public ResponseRequest ConsultePessoasPorUF(string uf)
        {
            try
            {
                var pessoas = Pessoas.Where(c => c.Uf.ToUpper().Equals(uf.Trim().ToUpper()));
                if (pessoas == null || !pessoas.Any())
                    return new ResponseRequest(HttpStatusCode.NotFound, ReturnMessages.RegisternotFound);

                return new ResponseRequest(HttpStatusCode.OK, pessoas);
            }
            catch (Exception)
            {
                return new ResponseRequest(HttpStatusCode.InternalServerError, ReturnMessages.InternalServerErro);
            }
        }

        public ResponseRequest ConsulteTodasAsPessoas()
        {
            try
            {
                if (Pessoas == null || !Pessoas.Any())
                    return new ResponseRequest(HttpStatusCode.NotFound, ReturnMessages.RegisternotFound);

                return new ResponseRequest(HttpStatusCode.OK, Pessoas);
            }
            catch (Exception)
            {
                return new ResponseRequest(HttpStatusCode.InternalServerError, ReturnMessages.InternalServerErro);
            }
        }

        public ResponseRequest ExcluirPessoa(int codPerson)
        {
            try
            {
                var pessoaPersistida = Pessoas.FirstOrDefault(p => p.Id == codPerson);
                if (pessoaPersistida == null)
                    return new ResponseRequest(HttpStatusCode.NotFound, ReturnMessages.RegisternotFound);

                Pessoas.Remove(pessoaPersistida);

                return new ResponseRequest(HttpStatusCode.OK, ReturnMessages.RegistrationRemoved);
            }
            catch (Exception)
            {
                return new ResponseRequest(HttpStatusCode.InternalServerError, ReturnMessages.InternalServerErro);
            }
        }

        public ResponseRequest GravePessoa(Person person)
        {
            try
            {
                if (Pessoas.Any(p => p.Id == person.Id))
                    return new ResponseRequest(HttpStatusCode.Conflict, ReturnMessages.RegistrationAlreadyExistingWhenTryingRegister);

                var erroValid = isValidPerson(person);
                if (erroValid.Any())
                    return new ResponseRequest(HttpStatusCode.BadRequest, erroValid);

                if (person.Id == 0)
                    person.Id = Pessoas.Max(c => c.Id) + 1;

                person.Cpf = formatCpf(person.Cpf);

                Pessoas.Add(person);

                return new ResponseRequest(HttpStatusCode.Created, person);
            }
            catch (Exception)
            {
                return new ResponseRequest(HttpStatusCode.InternalServerError, ReturnMessages.InternalServerErro);
            }
        }

        private Dictionary<string, string[]> isValidPerson(Person person)
        {
            Dictionary<string, string[]> errosDeValidacao = new();

            if (!isValid.Cpf(person.Cpf))
                errosDeValidacao.Add(nameof(person.Cpf), new string[] { "O CPF informado é inválido." });

            if (person.BirthDate?.Date >= DateTime.Now.Date)
                errosDeValidacao.Add(nameof(person.BirthDate), new string[] { "A data de nascimento não pode ser maior que a data atual." });

            return errosDeValidacao;
        }

        private string formatCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return string.Empty;

            return Convert.ToUInt64(cpf.Replace(".", "").Replace("-", "")).ToString(@"000\.000\.000\-00");
        }
    }
}
