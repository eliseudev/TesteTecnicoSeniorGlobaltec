using Globaltec.Domain.Auth;
using Globaltec.Domain.Models;
using Globaltec.Domain.Utils;
using Globaltec.Servico.Servicos.Interface;
using System.Net;

namespace Globaltec.Servico.Servicos
{
    public class UsersService : IUsersService
    {
        private static List<Users> Users = new() { new Users(1, "admin", "admin")};
        public ResponseRequest GetUserLoginPass(Login login)
        {
            try
            {
                var user = Users.FirstOrDefault(u => u.Login.Trim().ToUpper().Equals(login.User.Trim().ToUpper()) && u.Pass.Equals(login.Password));
                if (user == null)
                    return new ResponseRequest(HttpStatusCode.NotFound, ReturnMessages.IncorrectUsernamePassword);

                if (user != null)
                    user.Token = GenerateToken.GerarTokenDeAutenticacao(login.User);

                return new ResponseRequest(HttpStatusCode.OK, user!);
            }
            catch (Exception)
            {
                return new ResponseRequest(HttpStatusCode.InternalServerError, ReturnMessages.InternalServerErro);
            }
        }

        public ResponseRequest CreateUser(Login login)
        {
            try
            {
                if (Users.Any(u => u.Login.Trim().ToUpper().Equals(login.User.Trim().ToUpper())))
                    return new ResponseRequest(HttpStatusCode.Conflict, "Esse usuário já está sendo utilizado.");

                var newUser = new Users(Users.Max(c => c.Id) + 1, login.User, login.Password);
                Users.Add(newUser);

                newUser.Token = GenerateToken.GerarTokenDeAutenticacao(newUser.Login);

                return new ResponseRequest(HttpStatusCode.Created, newUser);
            }
            catch (Exception)
            {
                return new ResponseRequest(HttpStatusCode.InternalServerError, ReturnMessages.InternalServerErro);
            }
        }
    }
}
