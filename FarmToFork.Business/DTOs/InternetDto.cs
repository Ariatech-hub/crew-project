using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FarmToFork.Business.DTOs;

public class InternetDto
{
}

public class InternetTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
}

public class InternetTypeInsertDto
{

    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public string? Code { get; set; }

    public int? OrderNumber { get; set; }
}

public class InternetTypeInsertDtoValidator : AbstractValidator<InternetTypeInsertDto>
{
    public InternetTypeInsertDtoValidator()
    {
        _ = RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name is Required");
        _ = RuleFor(v => v.NepaliName).NotNull().NotEmpty().WithMessage("Nepali  is Required");
    }
}

public class InternetTypeUpdateDto 
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public string? Code { get; set; }
    public int? OrderNumber { get; set; }

}


public class InternetTypeUpdateDtoValidator : AbstractValidator<InternetTypeUpdateDto>
{
    public InternetTypeUpdateDtoValidator()
    {
        _ = RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name is Required");
        _ = RuleFor(v => v.NepaliName).NotNull().NotEmpty().WithMessage("Nepali  is Required");
        _ = RuleFor(v => v.Id).NotEqual(0).WithMessage("Id is required");
    }


}

public class InternetTypeDeleteDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

}


public class InternetTypeTableDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public string? Code { get; set; }
    public bool IsActive { get; set; }
    public int? OrderNumber { get; set; }
}




public class InternetUseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
}

public class InternetUseInsertDto
{

    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public string? Code { get; set; }
    public int? OrderNumber { get; set; }
}


public class InternetUseInsertDtoValidator : AbstractValidator<InternetUseInsertDto>
{
    public InternetUseInsertDtoValidator()
    {
        _ = RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name is Required");
        _ = RuleFor(v => v.NepaliName).NotNull().NotEmpty().WithMessage(" Nepali Name is Required");

    }
}

public class InternetUseTableDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = null!;
    public string NepaliName { get; set; } = null!;
    public int? OrderNumber { get; set; }
    public string? Code { get; set; }
    

}




public class InternetUseUpdateDto : InternetUseInsertDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }

}

public class InternetUseDeleteDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
public class InternetUseDeleteDtoValidator : AbstractValidator<InternetUseDeleteDto>
{
    public InternetUseDeleteDtoValidator()
    {
        _ = RuleFor(v => v.Id).NotEqual(0).WithMessage("Id is required");
    }
}


