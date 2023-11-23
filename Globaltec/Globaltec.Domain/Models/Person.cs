using Globaltec.Domain.Utils;
using System.ComponentModel.DataAnnotations;

namespace Globaltec.Domain.Models
{
    public class Person
    {
        public Person() { }

        public Person(int id, string name, string cpf, string uf, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            Uf = uf;
            BirthDate = birthDate;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = ReturnMessages.Request), MaxLength(200, ErrorMessage = "Máximo de 200 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = ReturnMessages.Request), MaxLength(14, ErrorMessage = "Máximo de 14 caracteres.")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = ReturnMessages.Request), MaxLength(2, ErrorMessage = "Máximo de 2 caracteres.")]
        public string Uf { get; set; }
        [Required(ErrorMessage = ReturnMessages.Request)]
        public DateTime? BirthDate { get; set; }
    }
}
