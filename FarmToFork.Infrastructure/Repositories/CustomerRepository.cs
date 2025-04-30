using FarmToFork.Business.DTOs;
using FarmToFork.Business.Enums;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Dapper.SqlMapper;

namespace FarmToFork.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneralUtility _generalUtility;

        public CustomerRepository(AppDbContext appDbContext, IGeneralUtility generalUtility)
        {
            _appDbContext = appDbContext;
            _generalUtility = generalUtility;

        }

        public Task<Customer> AddAsync(Customer entity)
        {
            throw new NotImplementedException();       
        }

        public Task<Customer> DeleteAsync(Customer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(bool disableTracking = true)
        {
            return disableTracking
                ? await _appDbContext.Customers
                    .OrderByDescending(x => x.Id).ToListAsync()
                : await _appDbContext.Customers.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
        }

        public Task<IEnumerable<Customer>> GetAsync(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var data = await _appDbContext
                .Customers
                .Select(x=> new Customer()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    IsActive = x.IsActive,
                })
                .FirstAsync(x => x.Id == id);

            return data;
        }  

        public Task UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
        #region CustomerContactDetail
        public async Task InsertCustomerContactDetail(IEnumerable<CustomerContactDetail> customerContactDetail)
        {
            await _appDbContext.CustomerContactDetails.AddRangeAsync(customerContactDetail);
            await _appDbContext.SaveChangesAsync();
        }

        #endregion


        #region CustomerGrainCycle
        public async Task<CustomerGrainCycle> InsertCustomerGrainCycle(CustomerGrainCycle customerGrainCycle, CustomerGrainCycleStatus customerGrainCycleStatus)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                EntityEntry<CustomerGrainCycle> insertedCustomerGrainCycle = await _appDbContext.CustomerGrainCycles.AddAsync(customerGrainCycle);
                await _appDbContext.SaveChangesAsync();
                customerGrainCycleStatus.CustomerGrainCycleId = insertedCustomerGrainCycle.Entity.Id;

                customerGrainCycleStatus.Status = null;
                await _appDbContext.CustomerGrainCycleStatuses.AddAsync(customerGrainCycleStatus);
                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return insertedCustomerGrainCycle.Entity;

            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                throw new GeneralException(e.Message);
            }

        }

        public async Task<Customer> InsertCustomer(Customer customer, int statusId)
        {
                EntityEntry<Customer> insertedCustomer = await _appDbContext.AddAsync(customer);
                await _appDbContext.SaveChangesAsync();

                return insertedCustomer.Entity;
        }

        public async Task<IEnumerable<CustomerGrainCycle>> GetCurrentCustomerGrainCycleByCustomerId(int id)
        {
            var grainCycle = await _appDbContext.CustomerGrainCycles
                .Where(x => x.CustomerId == id)
                .Select(x => new CustomerGrainCycle()
                {
                    Id = x.Id,
                    GrainCycle = x.GrainCycle,
                    CustomerGrainCycleStatuses = x.CustomerGrainCycleStatuses.Select(status => new CustomerGrainCycleStatus()
                    {
                        Id = status.Id,
                        CreatedDate = status.CreatedDate,
                        StatusId = status.StatusId,
                        Status = new Status()
                        {
                            Id = status.Status.Id,
                            Name = status.Status.Name
                        }
                    }).ToList()
                })
                .OrderByDescending(x => x.Id).ToListAsync();
            return grainCycle;
        }

   

        public async Task<IEnumerable<CustomerGrainCycle>> GetCustomerGrainCycle(int customerId)
        {
            IEnumerable<CustomerGrainCycle> customerGrainCycles =
                await _appDbContext.CustomerGrainCycles.Where(x=>x.CustomerId == customerId)
                    .Select(x=> new CustomerGrainCycle()
                    {
                        Id = x.Id,
                        GrainCycleId = x.GrainCycleId,
                        GrainCycle = new GrainCycle()
                        {
                            Id = x.GrainCycle.Id,
                            Name = x.GrainCycle.Name,
                            NepaliName = x.GrainCycle.NepaliName,
                            Grain = new Grain()
                            {
                                Id = x.GrainCycle.Grain.Id,
                                Name = x.GrainCycle.Grain.Name,
                                NepaliName = x.GrainCycle.Grain.NepaliName
                            }
                        },
                        CustomerGrainCycleStatuses = x.CustomerGrainCycleStatuses.Select(grainCycleStatus => new CustomerGrainCycleStatus()
                        {
                            Id = grainCycleStatus.Id,
                            StatusId = grainCycleStatus.StatusId,
                            Remarks = grainCycleStatus.Remarks,
                            CreatedDate = grainCycleStatus.CreatedDate,
                            Quantity = grainCycleStatus.Quantity,
                            Price = grainCycleStatus.Price,
                            CustomerGrainCycleId = grainCycleStatus.CustomerGrainCycleId,
                            Status = new Status()
                            {
                                Id = grainCycleStatus.Status.Id,
                                Name = grainCycleStatus.Status.Name,
                                Code = grainCycleStatus.Status.Code,
                            }
                        }).ToList()
                    })
                     .OrderByDescending(x => x.Id)
                    .ToListAsync();

            return customerGrainCycles;
        }

        public async Task<IEnumerable<CustomerGrainCycle>> GetCustomerGrainCycle(int grainCycleId, int customerId)
        {
            IEnumerable<CustomerGrainCycle> customerGrainCycles =
                await _appDbContext.CustomerGrainCycles
                    .Where(x=> ((grainCycleId == 0) || x.GrainCycleId == grainCycleId) && (customerId == 0 || x.CustomerId == customerId))
                    .Select(x => new CustomerGrainCycle()
                    {
                        Id = x.Id,
                        GrainCycleId = x.GrainCycleId,
                        CustomerId = x.CustomerId,
                        IsDispatched = x.IsDispatched,
                        GrainCycle = new GrainCycle()
                        {
                            Id = x.GrainCycle.Id,
                            Name = x.GrainCycle.Name,
                            Grain = new Grain()
                            {
                                Id = x.GrainCycle.Grain.Id,
                                Name = x.GrainCycle.Grain.Name
                            }
                        },
                        Customer = new Customer()
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name
                        }
                    }).ToListAsync();
            return customerGrainCycles;
        }

        public async Task UpdateCustomerGrainCycleDispatched(int id)
        {
            var data = await _appDbContext.CustomerGrainCycles.FirstAsync(x=>x.Id == id);
            data.IsDispatched = true;
            await _appDbContext.SaveChangesAsync();

        }

        public async Task UpdateCustomerGrainCycleStatus(CustomerGrainCycleStatus customerGrainCycleStatus, int grainCycleId)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                IEnumerable<CustomerGrainCycleStatus> customerGrainCycleStatuses = await _appDbContext.CustomerGrainCycleStatuses
             .Where(x => x.CustomerGrainCycleId == customerGrainCycleStatus.CustomerGrainCycleId)
             .Select(x => new CustomerGrainCycleStatus()
             {
                 Id = x.Id,
                 CustomerGrainCycleId = x.CustomerGrainCycleId,
                 StatusId = x.StatusId,
                 Status = x.Status
             })
             .ToListAsync();
                Status status = await _appDbContext.Statuses.SingleAsync(x => x.Id == customerGrainCycleStatus.StatusId);
                if (customerGrainCycleStatuses.Any())
                {
                    //TODO: Code Review
                    if(customerGrainCycleStatuses.OrderByDescending(x => x.Id).First().StatusId < (int)StatusEnum.Sales && customerGrainCycleStatus.StatusId == (int)StatusEnum.PaymentReceived)
                    {
                        throw new DataValidationException("Payment cannot be received before sales");
                    }
                    foreach (var item in customerGrainCycleStatuses)
                    {
                        if (item.Status.OrderId >= status.OrderId)
                        {
                            throw new DataValidationException("Invalid Status");
                        }
                    }
                }
                customerGrainCycleStatus.Status = null;
                await _appDbContext.CustomerGrainCycleStatuses.AddAsync(customerGrainCycleStatus);
                await _appDbContext.SaveChangesAsync();

                if(customerGrainCycleStatus.StatusId == (int)StatusEnum.Sales)
                {  
                  
                    if(await _appDbContext.Stocks.AnyAsync(x => x.GrainCycleId == grainCycleId))
                    {
                        Stock stock = await _appDbContext.Stocks.FirstAsync(x => x.GrainCycleId == grainCycleId);
                        if (stock.RemainingQuantity < customerGrainCycleStatus.Quantity)
                        {
                            throw new DataValidationException("Insufficient quantity in stock");
                        }
                        else
                        {
                            Dispatch dispatch = new()
                            {
                                CustomerGrainCycleId = customerGrainCycleStatus.CustomerGrainCycleId,
                                UnitPrice = customerGrainCycleStatus.Price ?? 0,
                                Quantity = customerGrainCycleStatus.Quantity ?? 0,
                                Total = customerGrainCycleStatus.Price ?? 0 * customerGrainCycleStatus.Quantity ?? 0,
                                CreatedDate = _generalUtility.GetCurrentNepalTime(),
                                CreatedBy = _generalUtility.GetLoggedInUsername()
                            };
                            await _appDbContext.Dispatches.AddAsync(dispatch);
                            await _appDbContext.SaveChangesAsync();

                            stock.RemainingQuantity = stock.RemainingQuantity - customerGrainCycleStatus.Quantity ?? 0;
                            await _appDbContext.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        throw new DataValidationException("No grain in stock");
                    }
                    
                }

                await transaction.CommitAsync();
            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                throw new GeneralException(e.Message);
            }
        }

        public async Task<IEnumerable<CustomerGrainCycle>> GetBuyerReportData()
        {
            IEnumerable<CustomerGrainCycle> customerGrainCycles = await _appDbContext.CustomerGrainCycles
                .Select(x => new CustomerGrainCycle
                {
                    Id = x.Id,
                    Customer = new Customer
                    {
                        Id = x.Customer.Id,
                        Name = x.Customer.Name,
                        PhoneNumber = x.Customer.PhoneNumber,
                        Email = x.Customer.Email,
                    },
                    GrainCycle = new GrainCycle
                    {
                        Id = x.GrainCycle.Id,
                        Name = x.GrainCycle.Name,
                        Grain = new Grain()
                        {
                            Id = x.GrainCycle.Grain.Id,
                            Name = x.GrainCycle.Grain.Name
                        }
                    },
                    CustomerGrainCycleStatuses = x.CustomerGrainCycleStatuses.Select(grainCycleStatus => new CustomerGrainCycleStatus()
                    {
                        Id = grainCycleStatus.Id,
                        StatusId = grainCycleStatus.StatusId,
                        Remarks = grainCycleStatus.Remarks,
                        CreatedDate = grainCycleStatus.CreatedDate,
                        Quantity = grainCycleStatus.Quantity,
                        Price = grainCycleStatus.Price,
                        CustomerGrainCycleId = grainCycleStatus.CustomerGrainCycleId,
                        Status = new Status()
                        {
                            Id = grainCycleStatus.Status.Id,
                            Name = grainCycleStatus.Status.Name
                        }
                    }).ToList()

                })
                .ToListAsync();

            return customerGrainCycles;
        }
        public async Task<CustomerGrainCycleStatus> GetCustomerGrainCycleById(int id, int statusId)
        {
            CustomerGrainCycleStatus? customerGrainCycle = await _appDbContext.CustomerGrainCycleStatuses
                .Where(x => x.CustomerGrainCycleId == id && x.StatusId == statusId)
                .Select(x => new CustomerGrainCycleStatus()
                {
                    Id = x.Id,
                    CustomerGrainCycle = new CustomerGrainCycle()
                    {
                        Customer = new Customer()
                        {
                            Name = x.CustomerGrainCycle.Customer.Name,
                            Email = x.CustomerGrainCycle.Customer.Email,
                            PhoneNumber = x.CustomerGrainCycle.Customer.PhoneNumber,
                        },
                        GrainCycle = new GrainCycle()
                        {
                            Name = x.CustomerGrainCycle.GrainCycle.Name,
                            Grain = new Grain()
                            {
                                Name = x.CustomerGrainCycle.GrainCycle.Grain.Name
                            }
                        }
                    },
                    Status = new Status()
                    {
                        Name = x.Status.Name,
                    },
                    Remarks = x.Remarks,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    CreatedDate = x.CreatedDate                
                })
                .FirstOrDefaultAsync();
            return customerGrainCycle ?? new CustomerGrainCycleStatus();
        }

        public async Task<IEnumerable<CustomerGrainCycleStatus>> GetCustomerGrainCyclesByCustomerGrainCycleId(int id)
        {
            IEnumerable<CustomerGrainCycleStatus> customerGrainCycleStatuses = await _appDbContext.CustomerGrainCycleStatuses
                .Where(x => x.CustomerGrainCycleId == id)
                .Select(x => new CustomerGrainCycleStatus()
                {
                    Id = x.Id,
                    StatusId = x.StatusId,
                    Remarks = x.Remarks,
                    CreatedDate = x.CreatedDate,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    CustomerGrainCycleId = x.CustomerGrainCycleId,
                    Status = new Status()
                    {
                        Id = x.Status.Id,
                        Name = x.Status.Name,
                        Code = x.Status.Code
                    },
                    CustomerGrainCycle = new CustomerGrainCycle()
                    {
                        GrainCycle = new GrainCycle()
                        {
                            Name = x.CustomerGrainCycle.GrainCycle.Name,
                            Grain = new Grain()
                            {
                                Name = x.CustomerGrainCycle.GrainCycle.Grain.Name,
                            }
                        }
                    }
                }).ToListAsync();
            return customerGrainCycleStatuses;
        }

        public async Task DeleteCustomerGrainCycle(int id)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                IEnumerable<CustomerGrainCycleStatus> customerGrainCycleStatuses = await _appDbContext.CustomerGrainCycleStatuses.Where(x => x.CustomerGrainCycleId == id).ToListAsync();
                if (customerGrainCycleStatuses.Any())
                {
                    _appDbContext.RemoveRange(customerGrainCycleStatuses);
                    await _appDbContext.SaveChangesAsync();
                }

                CustomerGrainCycle customerGrainCycle = await _appDbContext.CustomerGrainCycles.FirstAsync(x => x.Id == id);
                if (customerGrainCycle is not null)
                {
                    _appDbContext.Remove(customerGrainCycle);
                    await _appDbContext.SaveChangesAsync();
                }
                await transaction.CommitAsync();
            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                throw new GeneralException(e.Message);
            }
        }
    } 
    #endregion
}
