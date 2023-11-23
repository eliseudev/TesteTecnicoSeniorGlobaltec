using System.Net;

namespace Globaltec.Domain.Utils
{
    public class ResponseRequest
    {
        public ResponseRequest(HttpStatusCode codHTTP, object result)
        {
            CodHTTP = (int)codHTTP;
            Result = result;
        }

        public int CodHTTP { get; set; }
        public object Result { get; set; }
    }
}
