using System.ComponentModel.DataAnnotations;

namespace Api.Common
{
    public enum StudentStatus
    {
        [Display(Name = "New_Registered")]
        New_Registered = 1,
        [Display(Name = "Registered")]
        Registered,
        [Display(Name = "Enrolled")]
        Enrolled
    }
}
