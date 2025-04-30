namespace FarmToFork.Business.DTOs;
public class IncomeSourceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Code { get; set; }
    public int? OrderNo { get; set; }

}
public class IncomeSourceInsertDtoValidator : AbstractValidator<IncomeSourceInsertDto>
{
    public IncomeSourceInsertDtoValidator()
    {
        _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
        _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
        _ = RuleFor(x => x.Code).NotNull().WithMessage("Code is required");
    }
}
public class IncomeSourceInsertDto
{
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Code { get; set; }
    public int? OrderNo { get; set; }
}
public class IncomeSourceDeleteDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
public class IncomeSourceDeleteDtoValidator : AbstractValidator<IncomeSourceDeleteDto>
{
    public IncomeSourceDeleteDtoValidator()
    {
        _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
        _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
    }
}
public class IncomeSourceUpdateDtoValidator : AbstractValidator<IncomeSourceUpdateDto>
{
    public IncomeSourceUpdateDtoValidator()
    {
        _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
        _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
        _ = RuleFor(x => x.Code).NotNull().WithMessage("Code is required");
        _ = RuleFor(x => x.IsActive).NotNull().WithMessage("Is Active is required");
        _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
    }
}
public class IncomeSourceUpdateDto : IncomeSourceInsertDto
{
    public int Id { get; set; }

}
public class IncomeSourceTableDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public string? Code { get; set; }
    public int? OrderNo { get; set; }
}

