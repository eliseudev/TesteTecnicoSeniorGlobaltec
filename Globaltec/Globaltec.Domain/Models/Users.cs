using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globaltec.Domain.Models
{
    public class Users
    {
        public Users() { }

        public Users(int id, string login, string pass)
        {
            Id = id;
            Login = login;
            Pass = pass;
        }

        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Login { get; set; }
        [Required, MaxLength(100)]
        public string Pass { get; set; }
        public string Token { get; set; }
    }
}
