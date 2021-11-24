using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAuth.Services.AuthServices
{
    public class AuthOptions
    {
        public const string AUDIENCE = "";
        public const string ISSUER = "";
        private const string KEY = "";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricalKey()
        {
            return new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(KEY));
        }
    }
}
