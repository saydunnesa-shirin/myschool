using System.ComponentModel.DataAnnotations;

namespace Api.Common
{
    public enum BloodGroup
    {
        [Display(Name = "A+")]
        APositive = 1,
        [Display(Name = "A-")]
        ANegative,
        [Display(Name = "B+")]
        BPositive,
        [Display(Name = "B-")]
        BNegative,
        [Display(Name = "O+")]
        OPositive,
        [Display(Name = "O-")]
        ONegative,
        [Display(Name = "AB+")]
        ABPositive,
        [Display(Name = "AB-")]
        ABNegative

    }
}
