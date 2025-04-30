using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.Services
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAllStatus();
    }
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<IEnumerable<StatusDto>> GetAllStatus()
        {
            IEnumerable<Status> statuses = await _statusRepository.GetAllAsync();   
            IEnumerable<StatusDto> mappedStatuses = ObjectMapper.Mapper.Map<IEnumerable<StatusDto>>(statuses);
            return mappedStatuses;
        }
    }
}
