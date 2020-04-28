using System;
using System.Collections.Generic;
using System.Text;

namespace Cofim.Common.Model.Response
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string Rol { get; set; }
        public DateTime Valid { get; set; }
        public DateTime Expiration { get; set; }
        public bool TokenExpired { get; set; }
    }
}
