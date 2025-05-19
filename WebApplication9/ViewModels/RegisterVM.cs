using System.ComponentModel.DataAnnotations;

namespace WebApplication9.ViewModels;

public class RegisterVM
{
    [Required,MinLength(3)]
    public string Name { get; set; }

    [Required, MinLength(3)]
    public string Surname { get; set; }

    [Required, MinLength(3)]
    public string Username { get; set; }
    
    
    [DataType(DataType.EmailAddress)]    
    public string Email { get; set; }

    [DataType(DataType.Password),Compare("ConfirmPassword")]
    public string Password { get; set; }
    
    
    
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }


}
