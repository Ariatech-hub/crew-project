using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.DTOs
{
    public class OccupationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NepaliName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? Code { get; set; } = null;
        public int? OrderNo { get; set; }
    }
    public class OccupationInsertDto
    {
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public int? OrderNo { get; set; }
    }

    public class OccupationInsertDtoValidator : AbstractValidator<OccupationInsertDto>
    {
        public OccupationInsertDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
        }
    }

    public class OccupationUpdateDto : OccupationInsertDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class OccupationUpdateDtoValidator : AbstractValidator<OccupationUpdateDto>
    {
        public OccupationUpdateDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
            _ = RuleFor(x => x.IsActive).NotNull().WithMessage("Is Active is required");
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
        }
    }

    public class OccupationTableDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public int? OrderNo { get; set; }
    }

    public class OccupationDeleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class OccupationDeleteDtoValidator : AbstractValidator<OccupationDeleteDto>
    {
        public OccupationDeleteDtoValidator()
        {
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
        }
    }

}
