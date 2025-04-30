using FarmToFork.Core.Entities;

namespace FarmToFork.Application.Controllers
{
    [Route("api")]
    [ApiController, Authorize]
    public class CustomerController: ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost, Route("customer/insert"), CheckAccessForMenu(MenuCode = 29)]
        public async Task<IActionResult> InsertCustomer(CustomerInsertDto customerInsertDto)
        {
            try
            {
             
                CustomerDto customer = await _customerService.Insert(customerInsertDto, 1);
                return Ok(customer);

            }
            catch(Exception e)
            {
                _logger.LogError($"Exception Occurred while Inserting Customer , {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("customer-contact-detail/insert"), CheckAccessForMenu(MenuCode = 29)]
        public async Task<ActionResult> InsertCustomerContactDetails(IEnumerable<CustomerContactDetailInsertDto> customerContactDetailInsertDto)
        {
            try
            {
                await _customerService.InsertCustomerContactDetails(customerContactDetailInsertDto);
                return Ok("Customer Contact Details has been inserted");
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception Occurred while Inserting Customer Contact Details , {e.Message}");
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost, Route("customer-grain-cycle/insert"), CheckAccessForMenu(MenuCode = 24)]
        public async Task<IActionResult> InsertCustomerGrainCycle(CustomerGrainCycleInsertDto customerGrainCycleInsert)
        {
            try
            {
                CustomerGrainCycleStatusDto customerGrainCycleStatus = new() { 
                    Quantity = customerGrainCycleInsert.EstimatedQuantity,
                    Price = customerGrainCycleInsert.EstimatedCost,
                    Remarks = customerGrainCycleInsert.Remarks,
                };

                await _customerService.InsertCustomerGrainCycle(customerGrainCycleInsert, customerGrainCycleStatus);
                return Ok("Customer Grain Cycle has been inserted");
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception Occurred while Inserting Customer Grain Cycle , {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                IEnumerable<CustomerDto> customers = await _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception occurred while getting customers {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("customer/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                CustomerDto customer = await _customerService.GetCustomerById(id);
                return Ok(customer);
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception occurred while getting customer by id {e.Message}");
                return BadRequest(e.Message);
            }
        }
        [HttpGet, Route("customer/{id}/customer-grain-cycle"), CheckAccessForMenu(MenuCode = 24)]
        public async Task<IActionResult> GetCustomerGrainCycleByCustomerId(int id)
        {
            try {
                IEnumerable<CustomerGrainCycleDto> customerGrainCycle = await _customerService.GetCurrentGrainCycleByCustomerId(id);
                return Ok(customerGrainCycle);
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception occured while getting customer grain cycle by customer id {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpGet , Route("customer/{customerId}/grain-cycle"), AllowAnonymous]
        public async Task<IActionResult> GetGrainCycle(int customerId)
        {
            try
            {
                IEnumerable<CustomerGrainCycleCustomerViewDto> customerGrainCycleCustomer = await _customerService.GetById(customerId);
                return Ok(customerGrainCycleCustomer);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception Occurred while Inserting Customer status , {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("customer/customer-grain-cycle-status-update")]
        public async Task<IActionResult> UpdateCustomerGrainCycleStatus(CustomerGrainCycleStatusUpdateDto customerGrainCycleStatusDto)
        {
            try
            {
                await _customerService.UpdateCustomerGrainCycleStatus(customerGrainCycleStatusDto);
                return Ok("The customer grain cycle status has been updated");
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception occurred while updating the status of customer grain cycle, ${e.Message}");
                return BadRequest(e.Message);
            }
        }
        
        

        [HttpGet, Route("customer/grain-cycle/{customerGrainCycleId}/status")]
        public async Task<IActionResult> GetCustomerGrainCycleStatusesByCustomerGrainCycleId(int customerGrainCycleId)
        {
            try
            {
                IEnumerable<CustomerGrainCycleStatusCustomerViewDto> customerGrainCycleStatusCustomers = await _customerService.GetCustomerGrainCyclesByCustomerGrainCycleId(customerGrainCycleId);
                return Ok(customerGrainCycleStatusCustomers);
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception occurred while getting customer grain cycle statuses by customer grain cycle id {e.Message}");
                return BadRequest(e.Message);
            }
        }

        // TODO : Api Route ??
        //TODO : Route should be customer/grain-cycle/delete
        //TODO: Code Review
        [HttpPost, Route("customer/delete-grain-cycle")]
        public async Task<IActionResult> DeleteCustomerGrainCycle(CustomerGrainCycleDeleteDto customerGrainCycle)
        {
            try
            {
                await _customerService.DeleteCustomerGrainCycle(customerGrainCycle.Id);
                return Ok("The Customer Grain Cycle is deleted");
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception occurred while deleting the customer grain cycle {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }

}
