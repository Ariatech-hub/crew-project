using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services
{
    public interface IRelationService
    {
        Task<IEnumerable<RelationDto>> GetAllActiveRelations();
    }
    public class RelationService : IRelationService 
    {
        private readonly IRelationRepository _relationRepository;


        public RelationService(IRelationRepository relationRepository)
        {
            _relationRepository = relationRepository;
          
        }
        public async Task<IEnumerable<RelationDto>> GetAllActiveRelations()
        {
            return  ObjectMapper.Mapper.Map<IEnumerable<RelationDto>>(await _relationRepository.GetAsync(x => x.IsActive));
        }
    }
}
