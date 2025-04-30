using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FarmToFork.Business.DTOs
{
    public class PalikaDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public bool? IsActive { get; set; }
        public int? DistrictId { get; set; }
    }

    public class PalikaUpdateActiveStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public bool IsActive { get; set; }
        public string DistrictName { get; set; } = null!;
    }

    public class PalikaUpdateDto
    {
        public int Id { get; set; }
    }


    public class PalikaUpdateDtoValidator : AbstractValidator<PalikaUpdateDto>
    {
        public PalikaUpdateDtoValidator()
        {
            _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Invalid Palika Id ");
        }
    }





}
