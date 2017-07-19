using Microsoft.IdentityModel.Tokens;
using System.Text;
 
namespace Final
{
    public class AuthOptions
    {
        public const string ISSUER = "Project"; 
        public const string AUDIENCE = "http://localhost:5000/";
        const string KEY = "mysupersecret_secretkey!123";  
        public const int LIFETIME = 10; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}