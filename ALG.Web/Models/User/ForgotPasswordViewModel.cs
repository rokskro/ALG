using System.ComponentModel.DataAnnotations;

namespace ALG.Web.Models.User;
public class ForgotPasswordViewModel
{
    [Required]
    public string Email { get; set; }
    
}
