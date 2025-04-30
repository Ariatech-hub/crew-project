using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.DTOs
{
    public class LabourDivisionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? Code { get; set; }
        public int? OrderId { get; set; }

    }
    public class LabourDivisionInsertDto
    {
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public int? OrderId { get; set; }
        public string? Code { get; set; }
    }

    public class LabourDivisonInsertDtoValidator : AbstractValidator<LabourDivisionInsertDto>
    {
        public LabourDivisonInsertDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
        }
    }

    public class LabourDivisionUpdateDto : LabourDivisionInsertDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class LabourDivisionUpdateDtoValidator : AbstractValidator<LabourDivisionUpdateDto>
    {
        public LabourDivisionUpdateDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
        }
    }

    public class LabourDivisionTableDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public int? OrderId { get; set; }
    }

    public class LabourDivisionDeleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class LabourDivisonDeleteDtoValidator : AbstractValidator<LabourDivisionDeleteDto>
    {
        public LabourDivisonDeleteDtoValidator()
        {
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
        }
    }
}
