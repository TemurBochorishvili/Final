using Final.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    //შეიძლება გაკეთდეს Enum ები : სისტემებისთვის 
    /*
     * public enum SystemType
     * {
     *  1,
     *  2,
     *  3
     * }
     * */
    public class Profile
    {
        public int Id { get; set; }

        [Display(Name = "User Email")]
        public string Email { get; set; }

        public int TotalLimit { get; set; }

        public int LimitLeft { get; set; }

        [Display(Name = "System Status")]
        public int SystemStatus { get; set; }

        public DateTime PeriodFrom { get; set; }

        public DateTime PeriodTill { get; set; }

        public ApplicationUser User { get; set; }
    }
}
