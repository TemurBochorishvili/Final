using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

    }
}
