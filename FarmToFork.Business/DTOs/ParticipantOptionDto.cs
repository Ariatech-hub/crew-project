using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.DTOs
{
    public class ParticipantOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; } = null!;
        public int? OrderNo { get; set; }
    }

    public class ParticipantOptionInsertDtoValidator : AbstractValidator<ParticipantOptionInsertDto>
    {
        public ParticipantOptionInsertDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
        }
    }
    public class ParticipantOptionInsertDto
    {
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public int? OrderNo { get; set; }
    }
    public class ParticipantOptionDeleteDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class ParticipantOptionDeleteDtoValidator : AbstractValidator<ParticipantOptionDeleteDto>
    {
        public ParticipantOptionDeleteDtoValidator()
        {
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
        }
    }
    public class ParticipantOptionUpdateDtoValidator : AbstractValidator<ParticipantOptionUpdateDto>
    {
        public ParticipantOptionUpdateDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
            _ = RuleFor(x => x.IsActive).NotNull().WithMessage("Is Active is required");
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
        }
    }
    public class ParticipantOptionUpdateDto : ParticipantOptionInsertDto
    {
        public int Id { get; set; }
        public bool? IsActive { get; set; }

    }

    public class ParticipantOptionTableDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int? OrderNo { get; set; }
    }
}
