using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Core.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task InsertCustomerContactDetail(IEnumerable<CustomerContactDetail> customerContactDetail);
        Task<CustomerGrainCycle> InsertCustomerGrainCycle(CustomerGrainCycle customerGrainCycle, CustomerGrainCycleStatus customerGrainCycleStatus);
        Task<Customer> InsertCustomer(Customer customer, int statusId);
        Task<IEnumerable<CustomerGrainCycle>> GetCurrentCustomerGrainCycleByCustomerId(int id);
        Task<IEnumerable<CustomerGrainCycle>> GetCustomerGrainCycle(int customerId);
        Task<IEnumerable<CustomerGrainCycle>> GetCustomerGrainCycle(int grainCycleId , int customerId);

        Task UpdateCustomerGrainCycleDispatched(int id);
        Task UpdateCustomerGrainCycleStatus(CustomerGrainCycleStatus customerGrainCycleStatus, int grainCycleId);
        Task<IEnumerable<CustomerGrainCycle>> GetBuyerReportData();
        Task<CustomerGrainCycleStatus> GetCustomerGrainCycleById(int id, int statusId);
        Task<IEnumerable<CustomerGrainCycleStatus>> GetCustomerGrainCyclesByCustomerGrainCycleId(int id);
        Task DeleteCustomerGrainCycle(int id);
    }
}

