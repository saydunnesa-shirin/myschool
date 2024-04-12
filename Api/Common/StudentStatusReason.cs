using System.ComponentModel.DataAnnotations;

namespace Api.Common
{
    public enum StudentStatusReason
    {
        [Display(Name = "New_Registered_Done")]
        New_Registered_Done = 1,
        [Display(Name = "Registered_Done")]
        Registered_Done,
        [Display(Name = "Enrollment_Done")]
        Enrollment_Done
    }
}
