namespace FarmToFork.Business.DTOs;

public class YearDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public DateOnly StartDateAd { get; set; }
    public DateOnly EndDateAd { get; set; }
    public int? OrderNo { get; set; }
    public bool IsActive { get; set; }

}
public class YearInsertDtoValidator : AbstractValidator<YearInsertDto>
{
    public YearInsertDtoValidator()
    {
        _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
        _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
        _ = RuleFor(x => x.StartDate).NotEmpty().NotNull().WithMessage("Start Date is required");
        _ = RuleFor(x => x.EndDate).NotEmpty().NotNull().WithMessage("End Date is required");
    }
}
public class YearInsertDto
{
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public DateOnly? StartDateAd { get; set; } = null;
    public DateOnly? EndDateAd { get; set; } = null;
    public int? OrderNo { get; set; } = null;

}
public class YearDeleteDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
public class YearDeleteDtoValidator : AbstractValidator<YearDeleteDto>
{
    public YearDeleteDtoValidator()
    {
        _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
        _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
    }
}
public class YearUpdateDtoValidator : AbstractValidator<YearUpdateDto>
{
    public YearUpdateDtoValidator()
    {
        _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
        _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
        _ = RuleFor(x => x.IsActive).NotNull().WithMessage("Is Active is required");
        _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
    }
}
public class YearUpdateDto
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
   public DateOnly? StartDateAd { get; set; } = null;
    public DateOnly? EndDateAd { get; set; } = null;
    public bool IsActive { get; set; }
    public int? OrderNo { get; set; }

}
public class YearTableDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public DateOnly StartDateAd { get; set; }
    public DateOnly EndDateAd { get; set; }
    public int? OrderNo { get; set; }
}
