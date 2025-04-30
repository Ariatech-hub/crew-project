using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Core.Repositories;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace FarmToFork.Business.Services
{

    public interface IGenderService
    {
        Task<IEnumerable<GenderDto>> GetAllActiveGenders();
        
    }
    public class GenderServices : IGenderService
    {
        private readonly IGenderRepository _genderRepository;

        public GenderServices(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        public async Task<IEnumerable<GenderDto>> GetAllActiveGenders()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<GenderDto>>(await _genderRepository.GetAsync(x => x.IsActive));
        }
    }
}
