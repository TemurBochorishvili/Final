using Final.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Display(Name = "User Email")]
        public string Email { get; set; }

        public int TotalLimit { get; set; }

        public int LimitLeft { get; set; }

        [Display(Name = "System Status")]
        public int SystemStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime PeriodFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime PeriodTill { get; set; }

        public ApplicationUser User { get; set; }
    }
}
