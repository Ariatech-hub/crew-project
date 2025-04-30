using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FarmToFork.Business.DTOs
{
    public class EthnicityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NepaliName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? Code { get; set; } = null;
        public int? OrderNo { get; set; }

    }

    public class EthnicityInsertDto
    {
        public string Name { get; set; } = string.Empty;
        public string NepaliName { get; set; } = string.Empty;
        public string? Code { get; set; } = null;
        public int? OrderNo { get; set; }

    }

    public class EthnicityInsertDtoValidator : AbstractValidator<EthnicityInsertDto>
    {
        public EthnicityInsertDtoValidator()
        {
            _ = RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name is Required");
            _ = RuleFor(v => v.NepaliName).NotNull().NotEmpty().WithMessage("Nepali Name is Required");
        }
    }


    public class EthnicityUpdateDto : EthnicityDto
    {

    }

    public class EthnicityUpdateDtoValidator : AbstractValidator<EthnicityUpdateDto>
    {
        public EthnicityUpdateDtoValidator()
        {
            _ = RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name is Required");
            _ = RuleFor(v => v.NepaliName).NotNull().NotEmpty().WithMessage("Nepali Name is Required");
            _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Id is required");
        }
    }

    public class EthnicityDeleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class EthnicityDeleteDtoValidator : AbstractValidator<EthnicityDeleteDto>
    {
        public EthnicityDeleteDtoValidator()
        {
            _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Id is required");
        }
    }


}
