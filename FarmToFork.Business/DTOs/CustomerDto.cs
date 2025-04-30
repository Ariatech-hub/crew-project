using FarmToFork.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.DTOs
{
    public class CustomerDto
    {
       public int Id { get; set; }
       public string Name { get; set; } = null!;
       public string Email { get; set; } = null!;
       public string PhoneNumber { get; set; } = null!;
       public bool IsActive { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;

    }
    public class CustomerInsertDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool IsActive { get; set; }
        public int? CustomerStatusId { get; set; }
    }


    public class CustomerGrainCycleCustomerViewDto
    {
        public int Id { get; set; }
        public int GrainCycleId { get; set; }
        public decimal EstimatedQuantity { get; set; }
        public string GrainCycleName { get; set; } = string.Empty;
        public string GrainCycleNepaliName { get; set; } = string.Empty;
        public string GrainCycleGrainName { get; set; } = string.Empty;
        public string GrainCycleGrainNepaliName { get; set; } = string.Empty;

        public string StatusName => GetLatestStatus();
        public string StatusCode => CustomerGrainCycleStatuses.MaxBy(x => x.CreatedDate)?.StatusCode ?? string.Empty;
        public decimal Quantity => GetLatestQuantity();
        public decimal Price => GetLatestPrice();
        public int StatusId => CustomerGrainCycleStatuses.MaxBy(x => x.CreatedDate)?.StatusId ?? (int)StatusEnum.InitialConversation;
        public int CustomerGrainCycleId => CustomerGrainCycleStatuses.MaxBy(x => x.CreatedDate)?.CustomerGrainCycleId ?? 0;
        public string Remarks => CustomerGrainCycleStatuses.MaxBy(x => x.CreatedDate)?.Remarks ?? string.Empty;
        public DateTime CreatedDate => CustomerGrainCycleStatuses.MaxBy(x => x.CreatedDate)?.CreatedDate ?? DateTime.MinValue;
 
        public IEnumerable<CustomerGrainCycleStatusCustomerViewDto> CustomerGrainCycleStatuses { get; set; } = Enumerable.Empty<CustomerGrainCycleStatusCustomerViewDto>();

        private string GetLatestStatus()
        {
            return CustomerGrainCycleStatuses.MaxBy(x => x.CreatedDate)?.StatusName ?? string.Empty;
            
        }
        private decimal GetLatestQuantity()
        {
            return CustomerGrainCycleStatuses.MaxBy(x => x.CreatedDate)?.Quantity ?? 0;

        }
        private decimal GetLatestPrice()
        {
            return CustomerGrainCycleStatuses.MaxBy(x => x.CreatedDate)?.Price ?? 0;

        }
    }

    public class CustomerGrainCycleStatusCustomerViewDto
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public string StatusCode { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int CustomerGrainCycleId { get; set; }
        public string CustomerGrainCycleGrainCycleName { get; set; } = string.Empty;
        public string CustomerGrainCycleGrainCycleGrainName { get; set; } = string.Empty;

    }

    public class CustomerContactDetailDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
    public class CustomerContactDetailInsertDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool IsActive { get; set; }
    }

    public class CustomerGrainCycleInsertDto
    {
        public int CustomerId { get; set; }
        public int GrainCycleId { get; set; }
        public decimal EstimatedQuantity { get; set; }
        public decimal EstimatedCost { get; set; }
        public int CustomerGrainCycleStatusId { get; set; }
        public int? StatusId { get; set; }
        public string? Remarks { get; set; }

    }
    public class CustomerGrainCycleDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int GrainCycleId { get; set; }
        public decimal EstimatedQuantity { get; set; }
        public GrainCycleDto GrainCycle { get; set; } = new GrainCycleDto();
        public IEnumerable<CustomerGrainCycleStatusDto> CustomerGrainCycleStatuses { get; set; } = new List<CustomerGrainCycleStatusDto>();
    }
    public class CustomerGrainCycleStatusDto
    {
        public int Id { get; set; }
        public int CustomerGrainCycleId { get; set; }
        public int StatusId { get; set; }
        public string? Remarks { get; set; }
        public StatusDto Status { get; set; } = new StatusDto();
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class CustomerGrainCycleStatusUpdateDto
    {
        public int Id { get; set; }
        public int CustomerGrainCycleId { get; set; }
        public int StatusId { get; set; }
        public string? Remarks { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int GrainCycleId { get; set; }

    }

    public class CustomerGrainCycleReportDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int GrainCycleId { get; set; }
        public CustomerDto Customer { get; set; } = new CustomerDto();
        public GrainCycleDto GrainCycle { get; set; } = new GrainCycleDto();
        public IEnumerable<CustomerGrainCycleStatusDto> CustomerGrainCycleStatuses { get; set; } = new List<CustomerGrainCycleStatusDto>();
    }
    public class CustomerGrainCycleDeleteDto
    {
        public int Id { get; set; }
    }
}
