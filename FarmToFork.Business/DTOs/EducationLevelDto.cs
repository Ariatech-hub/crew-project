using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FarmToFork.Business.DTOs
{
    public class EducationLevelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NepaliName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? Code { get; set; } = string.Empty;
        public int? OrderNo { get; set; } = null;

    }

    public class EducationLevelInsertDto
    {
        public string Name { get; set; } = string.Empty;
        public string NepaliName { get; set; } = string.Empty;
        public string? Code { get; set; } = null;
        public int? OrderNo { get; set; }
    }

    public class EducationLevelInsertDtoValidator : AbstractValidator<EducationLevelInsertDto>
    {
        public EducationLevelInsertDtoValidator()
        {
            _ = RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name is Required");
            _ = RuleFor(v => v.NepaliName).NotNull().NotEmpty().WithMessage("Nepali Name is Required");
        }
    }

    public class EducationLevelUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string NepaliName { get; set; } = string.Empty;
        public string? Code { get; set; } = null;
        public int? OrderNo { get; set; } = null;
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }

    public class EducationLevelUpdateDtoValidator : AbstractValidator<EducationLevelUpdateDto>
    {
        public EducationLevelUpdateDtoValidator()
        {
            _ = RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name is Required");
            _ = RuleFor(v => v.NepaliName).NotNull().NotEmpty().WithMessage("Nepali Name is Required");
            _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Invalid Education Level");
        }
    }


    public class EducationLevelDeleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class EducationLevelDeleteDtoValidator : AbstractValidator<EducationLevelDeleteDto>
    {
        public EducationLevelDeleteDtoValidator()
        {
            _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Invalid Education Level");
        }
    }


}
