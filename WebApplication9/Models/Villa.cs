using WebApplication9.Models.Abstracts;

namespace WebApplication9.Models;

public class Villa : BaseEntity
{

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public string? ImgUrl { get; set; }
}
