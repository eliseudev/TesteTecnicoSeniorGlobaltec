using Globaltec.Domain.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globaltec.Domain.Models
{
    public class Login
    {
        [Required(ErrorMessage = ReturnMessages.Request)]
        public string User { get; set; }
        [Required(ErrorMessage = ReturnMessages.Request)]
        public string Password { get; set; }
    }
}
