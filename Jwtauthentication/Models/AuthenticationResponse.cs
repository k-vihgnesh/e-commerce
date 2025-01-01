using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwtauthentication.Models
{
    public class AuthenticationResponse
    {
        public string? JwtToken { get; set; }
        public string? UserName { get; set; }
        public string? ExpiresTime { get; set; }
    }
}
