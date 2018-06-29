using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace Model
{
    public class TokenGenerateOption
    {
        public string Path { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}
