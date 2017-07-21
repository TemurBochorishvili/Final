using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace Final.Services
{
    public class JwtIssuer : IJwtIssuer
    {
        protected static readonly JwtSecurityTokenHandler JwtSecurityHandler = new JwtSecurityTokenHandler();

        protected readonly JwtIssuerOptions _options;

        public JwtIssuer(IOptionsSnapshot<JwtIssuerOptions> optionsSnapshot)
        {
            _options = optionsSnapshot.Value;
        }

        public virtual string IssueJwt(IEnumerable<Claim> claims = null) => EncodeJwtSecurityToken(IssueJwtSecurityToken(claims));

        protected virtual JwtSecurityToken IssueJwtSecurityToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_options.Lifetime);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Key)), SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(_options.Issuer, _options.Audience, claims, now, expires, signingCredentials);
        }

        protected virtual string EncodeJwtSecurityToken(JwtSecurityToken jwtSecurityToken) => JwtSecurityHandler.WriteToken(jwtSecurityToken);
    }
}
