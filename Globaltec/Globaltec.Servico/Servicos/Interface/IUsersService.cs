using Globaltec.Domain.Models;
using Globaltec.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globaltec.Servico.Servicos.Interface
{
    public interface IUsersService
    {
        ResponseRequest CreateUser(Login login);
        ResponseRequest GetUserLoginPass(Login login);
    }
}
