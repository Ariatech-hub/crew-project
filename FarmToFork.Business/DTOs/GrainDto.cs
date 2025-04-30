using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Core.Entities;
using FluentValidation;

namespace FarmToFork.Business.DTOs
{
    public class GrainDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
    }
    public class GrainCycleDto
    {
        public int Id { get; set; }
        public int FromMonthId { get; set; }
        public int ToMonthId { get; set; }
        public int OrderNo { get; set; }
        public int YearId { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public bool IsActive { get; set; }
        public int GrainId { get; set; }
        public GrainDto Grain { get; set; } = new GrainDto();

    }

    public class GrainInsertDto
    {
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public int? OrderNumber { get; set; }
    }

    public class GrainInsetDtoValidator : AbstractValidator<GrainInsertDto>
    {
        public GrainInsetDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
           
        }
    }

    public class GrainUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public int? OrderNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class GrainUpdateDtoValidator : AbstractValidator<GrainUpdateDto>
    {
        public GrainUpdateDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required");

        }
    }

    public class GrainTableDto : GrainUpdateDto
    {

    }

    public class GrainDeleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }


    public class GrainCycleInsertDto
    {
       
        public int FromMonthId { get; set; }
        public int ToMonthId { get; set; }
        public int? OrderNo { get; set; }
        public int YearId { get; set; }
        public int GrainId { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
      
    }
    public class GrainCycleUpdateStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int GrainId { get; set; }
        public int YearId { get; set; }
        public int FromMonthId { get; set; }
        public int ToMonthId { get; set; }
    }
    public class GrainCycleInsertDtoValidator : AbstractValidator<GrainCycleInsertDto>
    {
        public GrainCycleInsertDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
            _ = RuleFor(x => x.YearId).NotEqual(0).WithMessage("Year is required");
            _ = RuleFor(x => x.FromMonthId).NotEqual(0).WithMessage("From Month  is required");
            _ = RuleFor(x => x.ToMonthId).NotEqual(0).WithMessage("To Month  is required");
        }
    }
    public class GrainCycleUpdateDto : GrainCycleInsertDto

    {
         public int Id { get; set; }
          public bool IsActive { get; set; }

    }
    public class GrainCycleUpdateDtoValidator : AbstractValidator<GrainCycleUpdateDto>
    {
        public GrainCycleUpdateDtoValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.NepaliName).NotEmpty().NotNull().WithMessage("Nepali Name is required");
            _ = RuleFor(x => x.YearId).NotEqual(0).WithMessage("Year is required");
            _ = RuleFor(x => x.FromMonthId).NotEqual(0).WithMessage("From Month  is required");
            _ = RuleFor(x => x.ToMonthId).NotEqual(0).WithMessage("To Month  is required");
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id  is required");
            _ = RuleFor(x => x.IsActive).NotNull().WithMessage("Is Active is required"); 
        }
    }
    public class GrainCycleDeleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }


    public class GrainCycleDeleteValidator : AbstractValidator<GrainCycleDeleteDto>
    {
        public GrainCycleDeleteValidator()
        {
            _ = RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            _ = RuleFor(x => x.Id).NotEqual(0).WithMessage("Id  is required");
        }
       
    }
    public class GrainCycleTableDto
    { 
        public int Id { get; set; }
        public string FromMonthName { get; set; } = null!;
        public string ToMonthName { get; set; } = null!;
        public int OrderNo { get; set; }
        public string YearName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string GrainName { get; set; } = null!;
        public bool IsActive { get; set; }
        public int YearId { get; set; }
        public int GrainId { get; set; }
        public int FromMonthId { get; set; }
        public int ToMonthId { get; set; }
    }
    public class GrainCycleGetByIdDto
    {
        public int Id { get; set; }
        public int FromMonthId { get; set; }
        public int ToMonthId { get; set; }
        public int OrderNo { get; set; }
        public int YearId { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public bool IsActive { get; set; }
        public int GrainId { get; set; }

    }
}
