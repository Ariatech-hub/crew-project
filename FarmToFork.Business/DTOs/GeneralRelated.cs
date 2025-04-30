using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.DTOs
{
    internal class GeneralRelated
    {

    }

    public class UserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public IEnumerable<MenuDto> MenuTree { get; set; } = new List<MenuDto>();

        public int[]? MenuIds { get; set; }
    }
    public class UserResponseDTO
    {
        public UserDTO User { get; set; } = new UserDTO();
    }
    #region ------- Location Related-----------
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public int? OrderId { get; set; }
        public bool? IsActive { get; set; }

    }

    public class LocationInsertDto
    {
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public int? OrderId { get; set; }

    }
    public class LocationDeleteDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
    }

    public class LocationUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public string? Code { get; set; }
        public int? OrderId { get; set; }
        public bool? IsActive { get; set; }

    }
    #endregion

    #region ---Province---

    public class ProvinceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
    public class ProvinceTableDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? NepaliName { get; set; }
        public string ProvinceName { get; set; } = null!;

        public bool IsActive { get; set; }

    }
    public class ProvinceUpdateActiveStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
    public class ProvinceActiveStatusDto
    {
        public int Id { get; set; }
        
    }

    public class ProvinceActiveStatusDtoValidator : AbstractValidator<ProvinceActiveStatusDto>
    {
        public ProvinceActiveStatusDtoValidator()
        {
            _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Invalid Province Id ");
        }
    }
    #endregion

    public class AccessLevelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class MaritalStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? NepaliName { get; set; }
        public bool? IsActive { get; set; }
        public int? OrderId { get; set; }

    }

    public class MaritalStatusInsertDto
    {
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
        public int? OrderId { get; set; }

    }
    public class MaritalStatusUpdateDto : MaritalStatusInsertDto
    {
        public int Id{ get; set; }
        public bool? IsActive { get; set; }
    }

    public class MaritalStatusDeleteDto
    {
        public int Id{ get; set; }
        public string? Name { get; set; }
    }



    public class LaborDivisionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
    }

    public class MonthDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NepaliName { get; set; } = null!;
    }

    public class AddressFilterDto
    {
        public IEnumerable<ProvinceDto> Provinces { get; set; } = new List<ProvinceDto>();
        public IEnumerable<DistrictDto> Districts { get; set; } = new List<DistrictDto>();
        public IEnumerable<PalikaDto> Palikas { get; set; } = new List<PalikaDto>();

    }
    public class CardImageDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public bool IsSelected { get; set; }
    }
    public class CardImageInsertDto
    {
        public string Name { get; set; } = null!;
        public string? FileName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
    public class CardImageDeleteDto
    {
        public int Id { get; set; }
    }
}
