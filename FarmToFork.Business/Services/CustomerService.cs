using FarmToFork.Business.Enums;
using FarmToFork.Business.Utilities;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> Insert (CustomerInsertDto customerInsertDto, int customerStatusId);
        Task InsertCustomerContactDetails(IEnumerable<CustomerContactDetailInsertDto> customerContactDetailInsertDto);
        Task InsertCustomerGrainCycle(CustomerGrainCycleInsertDto customerGrainCycle, CustomerGrainCycleStatusDto customerGrainCycleStatus);
        Task<IEnumerable<CustomerDto>> GetAllCustomers();
        Task<CustomerDto> GetCustomerById(int id);
        Task<IEnumerable<CustomerGrainCycleDto>> GetCurrentGrainCycleByCustomerId(int id);

        Task<IEnumerable<CustomerGrainCycleCustomerViewDto>> GetById(int customerId);
        Task UpdateCustomerGrainCycleStatus(CustomerGrainCycleStatusUpdateDto customerGrainCycleStatusDto);
        Task<IEnumerable<CustomerGrainCycleReportDto>> GetBuyerReportData();
        Task<BuyerReceiptDto> GetCustomerGrainCycleById(int id, int statusId);
        Task<IEnumerable<CustomerGrainCycleStatusCustomerViewDto>> GetCustomerGrainCyclesByCustomerGrainCycleId(int customerGrainCycleId);
        Task DeleteCustomerGrainCycle(int id);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGeneralUtility _generalUtility;

        public CustomerService(ICustomerRepository customerRepository, IGeneralUtility generalUtility)
        {
            _customerRepository = customerRepository;
            _generalUtility = generalUtility;
        }
        public async Task<CustomerDto> Insert(CustomerInsertDto customerInsertDto, int customerStatusId)
        {
            customerInsertDto.Name = customerInsertDto.Name.Trim();
            customerInsertDto.Email = customerInsertDto.Email.Trim();
            customerInsertDto.PhoneNumber = customerInsertDto.PhoneNumber.Trim();

            Customer customer = ObjectMapper.Mapper.Map<Customer>(customerInsertDto);
            customer.CreatedBy = _generalUtility.GetLoggedInUsername();
            customer.CreatedDate = _generalUtility.GetCurrentNepalTime();
            customer.IsActive = true;

            Customer insertedCustomer = await _customerRepository.InsertCustomer(customer, customerStatusId);
            CustomerDto mappedInsertedCustomer = ObjectMapper.Mapper.Map<CustomerDto>(insertedCustomer);
            return mappedInsertedCustomer;
        }

        public async Task InsertCustomerContactDetails(IEnumerable<CustomerContactDetailInsertDto> customerContactDetailInsertDto)
        {
            foreach (var item in customerContactDetailInsertDto)
            {
                item.Name = item.Name.Trim();
                item.Email = item.Email.Trim();
                item.PhoneNumber = item.PhoneNumber.Trim();
                item.IsActive = true;
            }
            IEnumerable<CustomerContactDetail> customerContactDetail = ObjectMapper.Mapper.Map<IEnumerable<CustomerContactDetail>>(customerContactDetailInsertDto);
           await _customerRepository.InsertCustomerContactDetail(customerContactDetail);
        }

        public async Task InsertCustomerGrainCycle(CustomerGrainCycleInsertDto customerGrainCycle, CustomerGrainCycleStatusDto customerGrainCycleStatus)
        {
            CustomerGrainCycle customerGrainCycleToBeInserted = ObjectMapper.Mapper.Map<CustomerGrainCycle>(customerGrainCycle);
            CustomerGrainCycleStatus customerGrainCycleStatusToBeInserted = ObjectMapper.Mapper.Map<CustomerGrainCycleStatus>(customerGrainCycleStatus);
            customerGrainCycleStatusToBeInserted.StatusId = (int)StatusEnum.InitialConversation;
            customerGrainCycleStatusToBeInserted.CreatedDate = _generalUtility.GetCurrentNepalTime();
            customerGrainCycleStatusToBeInserted.CreatedBy = _generalUtility.GetLoggedInUsername();
            
            await _customerRepository.InsertCustomerGrainCycle(customerGrainCycleToBeInserted, customerGrainCycleStatusToBeInserted);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetAllAsync();
            IEnumerable<CustomerDto> mappedCustomers = ObjectMapper.Mapper.Map<IEnumerable<CustomerDto>>(customers);
            return mappedCustomers;
        }

        public async Task<CustomerDto> GetCustomerById(int id)
        {
            Customer customer = await _customerRepository.GetByIdAsync(id);
            CustomerDto mappedCustomer = ObjectMapper.Mapper.Map<CustomerDto>(customer);
            return mappedCustomer;
        }

        public async Task<IEnumerable<CustomerGrainCycleDto>> GetCurrentGrainCycleByCustomerId(int id)
        {
            IEnumerable<CustomerGrainCycle> customerGrainCycle = await _customerRepository.GetCurrentCustomerGrainCycleByCustomerId(id);
            IEnumerable<CustomerGrainCycleDto> mappedCustomerGrainCycle = ObjectMapper.Mapper.Map<IEnumerable<CustomerGrainCycleDto>>(customerGrainCycle);
            return mappedCustomerGrainCycle;
        }


        public async Task<IEnumerable<CustomerGrainCycleCustomerViewDto>> GetById(int customerId)
        {
            IEnumerable<CustomerGrainCycle> customerGrainCycles =  await  _customerRepository.GetCustomerGrainCycle(customerId);
            IEnumerable<CustomerGrainCycleCustomerViewDto> mappedCustomerGrainCycles = ObjectMapper.Mapper.Map<IEnumerable<CustomerGrainCycleCustomerViewDto>>(customerGrainCycles);
            return mappedCustomerGrainCycles;
        }

        public async Task UpdateCustomerGrainCycleStatus(CustomerGrainCycleStatusUpdateDto customerGrainCycleStatusDto)
        {
            
            CustomerGrainCycleStatus customerGrainCycleStatus = ObjectMapper.Mapper.Map<CustomerGrainCycleStatus>(customerGrainCycleStatusDto);
            customerGrainCycleStatus.CreatedBy = _generalUtility.GetLoggedInUsername();
            customerGrainCycleStatus.CreatedDate = _generalUtility.GetCurrentNepalTime();
            await _customerRepository.UpdateCustomerGrainCycleStatus(customerGrainCycleStatus, customerGrainCycleStatusDto.GrainCycleId);
        }

        public async Task <IEnumerable<CustomerGrainCycleReportDto>> GetBuyerReportData()
        {
            IEnumerable<CustomerGrainCycle> reportData = await _customerRepository.GetBuyerReportData();
            IEnumerable<CustomerGrainCycleReportDto> report = ObjectMapper.Mapper.Map<IEnumerable<CustomerGrainCycleReportDto>>(reportData);
            return report;
        }

        public async Task<BuyerReceiptDto> GetCustomerGrainCycleById(int id, int statusId)
        {
            CustomerGrainCycleStatus customerGrainCycleStatus = await _customerRepository.GetCustomerGrainCycleById(id, statusId);
            BuyerReceiptDto buyerReceipt = ObjectMapper.Mapper.Map<BuyerReceiptDto>(customerGrainCycleStatus);
            return buyerReceipt;
        }

        public async Task<IEnumerable<CustomerGrainCycleStatusCustomerViewDto>> GetCustomerGrainCyclesByCustomerGrainCycleId(int customerGrainCycleId)
        {
            IEnumerable<CustomerGrainCycleStatus> customerGrainCycleStatuses = await _customerRepository.GetCustomerGrainCyclesByCustomerGrainCycleId(customerGrainCycleId);
            IEnumerable<CustomerGrainCycleStatusCustomerViewDto> mappedCustomerGrainCycleStatuses = ObjectMapper.Mapper.Map<IEnumerable<CustomerGrainCycleStatusCustomerViewDto>>(customerGrainCycleStatuses);
            return mappedCustomerGrainCycleStatuses;
        }

        public async Task DeleteCustomerGrainCycle(int id)
        {
            await _customerRepository.DeleteCustomerGrainCycle(id);
        }
    }
}
