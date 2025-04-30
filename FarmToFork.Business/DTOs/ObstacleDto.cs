using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.Mapper;
using FarmToFork.Core.Repositories;
using FluentValidation;

namespace FarmToFork.Business.DTOs
{
    public class ObstacleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public bool? IsSale { get; set; }
        public int OrderId { get; set; }
        public bool? IsProduction { get; set; }
    }

    public class ObstacleInsertDto
    {
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public bool IsSale { get; set; }
        public int? OrderId { get; set; }
        public string? Code { get; set; }
        public bool IsProduction { get; set; }
    }

    public class ObstacleInsertDtoValidator : AbstractValidator<ObstacleInsertDto>
    {
        public ObstacleInsertDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
            _ = RuleFor(x => x.IsSale).NotNull().WithMessage("Is Sale is required");
            _ = RuleFor(x => x.IsProduction).NotNull().WithMessage("Is Production is required");
        }
    }

    public class ObstacleUpdateDto : ObstacleInsertDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class ObstacleUpdateDtoValidator : AbstractValidator<ObstacleUpdateDto>
    {
        public ObstacleUpdateDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
            _ = RuleFor(x => x.IsSale).NotNull().WithMessage("Is Sale is required");
            _ = RuleFor(x => x.IsProduction).NotNull().WithMessage("Is Production is required");
            _ = RuleFor(x => x.IsActive).NotNull().WithMessage("Is Active is required");
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
        }
    }

    public class ObstacleTableDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public bool IsSale { get; set; }
        public string? Code { get; set; }
        public int OrderId { get; set; }
        public bool IsProduction { get; set; }
    }

    public class ObstacleDeleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class ObstacleDeleteDtoValidator : AbstractValidator<ObstacleDeleteDto>
    {
        public ObstacleDeleteDtoValidator()
        {
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
        }
    }
}
