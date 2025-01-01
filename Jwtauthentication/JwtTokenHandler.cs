using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Jwtauthentication.Models;
using Microsoft.IdentityModel.Tokens;


namespace Jwtauthentication
{
    public class JwtTokenHandler
    {
        // Replace with secure key retrieval, e.g., environment variables or config
        public const string JWT_SECURITY_KEY = "jkhgkhjkhsujkghtuiawrioheiothpgairhgiohdohgohjghhol1i2h3h3hlargekey";  // Ideally, use a secret key storage approach
        private const int JWT_TOKEN_VALIDITY_MINUTES = 20;

        private readonly List<Models.UserAccount> _userAccountList;

        public JwtTokenHandler()
        {
            // Sample user data, this should come from a database in a real application
            _userAccountList = new List<Models.UserAccount>
            {
                new Models.UserAccount { UserName = "admin", Password = "admin", Role = "Admin" },
                new Models.UserAccount { UserName = "user", Password = "user", Role = "User" }
            };
        }

        public AuthenticationResponse GenerateJwtToken(AuthendicationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.UserName) || string.IsNullOrEmpty(authenticationRequest.Password))
            {
                throw new ArgumentException("Username or password cannot be null or empty.");
            }

            var userAccount = _userAccountList.SingleOrDefault(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password);
            if (userAccount == null)
            {
                return new AuthenticationResponse { };
            }

            // Token expiration time
            var tokenExpirationTime = DateTime.UtcNow.AddMinutes(JWT_TOKEN_VALIDITY_MINUTES);

            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

            // Creating claims
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, userAccount.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpirationTime,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                JwtToken = token,
                UserName = userAccount.UserName,
                ExpiresTime = tokenExpirationTime.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
    }
}
