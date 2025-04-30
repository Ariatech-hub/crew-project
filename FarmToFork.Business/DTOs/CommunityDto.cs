

namespace FarmToFork.Business.DTOs;

public class CommunityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NepaliName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? Code { get; set; } = null;
    public int? OrderNo { get; set; } = null;
}


public class CommunityInsertDto
{
    public string Name { get; set; } = string.Empty;
    public string NepaliName { get; set; } = string.Empty;
    public string? Code { get; set; } = null;
    public int? OrderNo { get; set; }
}

public class CommunityInsertDtoValidator : AbstractValidator<CommunityInsertDto>
{
    public CommunityInsertDtoValidator()
    {
        _ = RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name is Required");
        _ = RuleFor(v => v.NepaliName).NotNull().NotEmpty().WithMessage("Nepali Name is Required");
    }
}


public class CommunityUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NepaliName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int? OrderNo { get; set; }
    public string? Code { get; set; } = null;

}

public class CommunityUpdateDtoValidator : AbstractValidator<CommunityUpdateDto>
{
    public CommunityUpdateDtoValidator()
    {
        _ = RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name is Required");
        _ = RuleFor(v => v.NepaliName).NotNull().NotEmpty().WithMessage("Nepali Name is Required");
        _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Invalid Id");
    }
}


public class CommunityDeleteDto
{
    public int Id { get; set; }
}

public class CommunityDeleteDtoValidator : AbstractValidator<CommunityDeleteDto>
{
    public CommunityDeleteDtoValidator()
    {
        _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Invalid Id");
    }
}

