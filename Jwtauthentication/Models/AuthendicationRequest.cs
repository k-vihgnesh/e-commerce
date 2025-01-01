using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwtauthentication.Models
{
    public class AuthendicationRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
