using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FarmToFork.Business.DTOs
{
    public class DistrictDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? NepaliName { get; set; }
        public int ProvinceId { get; set; }

    }

    public class DistrictTableDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? NepaliName { get; set; }
        public string ProvinceName { get; set; } = null!;

        public bool IsActive { get; set; }

    }

    

    public class DistrictActiveStatusDto
    {
        public int Id { get; set; }
    }

    public class DistrictActiveStatusDtoValidator : AbstractValidator<DistrictActiveStatusDto>
    {
        public DistrictActiveStatusDtoValidator()
        {
            _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Invalid District Id ");
        }
    }
}
