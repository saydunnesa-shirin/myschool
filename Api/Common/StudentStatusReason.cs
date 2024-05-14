using System.ComponentModel.DataAnnotations;

namespace Api.Common
{
    public enum StudentStatusReason
    {
        [Display(Name = "New Registration Done")]
        New_Registered_Done = 1,
        [Display(Name = "Registered")]
        Registered_Done,
        [Display(Name = "Enrollment Done")]
        Enrollment_Done
    }
}
