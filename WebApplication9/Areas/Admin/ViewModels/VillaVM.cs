using System.ComponentModel.DataAnnotations;

namespace WebApplication9.Areas.Admin.ViewModels;

public class VillaVM
{

    public int Id { get; set; }

    [Required, MinLength(3)]
    public string Name { get; set; }

    [Required, MinLength(3)]
    public string Description { get; set; }

    [Required, MinLength(3)]
    public string Category { get; set; }


    public string? ImgUrl { get; set; }

    public IFormFile? formFile { get; set; }
}
