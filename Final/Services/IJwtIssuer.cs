using System.Security.Claims;
using System.Collections.Generic;

namespace Final.Services
{
    public interface IJwtIssuer
    {
        string IssueJwt(IEnumerable<Claim> claims = null);
    }
}
