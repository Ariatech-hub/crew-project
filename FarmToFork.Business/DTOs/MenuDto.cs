using System.Runtime.InteropServices.ComTypes;
using FluentValidation;

namespace FarmToFork.Business.DTOs;

public class MenuDto
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } =  null! ;
    public bool IsActive { get; set; }
    public IEnumerable<MenuDto> Children { get; set; } = new List<MenuDto>();
    public string Icon { get; set; } = null!;
    public int OrderId { get; set; }
    public bool IsSelected { get; set; }
    public bool? IsLink { get; set; }
}

public class UpdateMenusForAccessLevel
{
    public int Id { get; set; }
    public int[]? MenuIds { get; set; }  
}

public class UpdateMenusForUser
{
    public string UserId { get; set; } = string.Empty;
    public int[]? MenuIds { get; set; }
}

public class UpdateMenuForUserValidation : AbstractValidator<UpdateMenusForUser>
{
    public UpdateMenuForUserValidation()
    {
        _ = RuleFor(v => v.UserId).NotNull().NotEmpty().WithMessage("Invalid User");
    }
}

