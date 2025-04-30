using AutoMapper.Internal.Mappers;
using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.Services
{
    public interface IProvinceService
    {
        Task<IEnumerable<ProvinceTableDto>> GetAll();
        Task UpdateActiveStatusOfProvince(int provinceId);

    }
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IGeneralUtility _generalUtility;
        public ProvinceService(IProvinceRepository provinceRepository, IGeneralUtility generalUtility)
        {
            _provinceRepository = provinceRepository;
            _generalUtility = generalUtility;
        }

        public async Task<IEnumerable<ProvinceTableDto>> GetAll()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<ProvinceTableDto>>(await _provinceRepository.GetAllAsync());
        }

        public async Task UpdateActiveStatusOfProvince(int provinceId)
        {
            await _provinceRepository.UpdateProvinceActiveStatus(provinceId);

        }
    }
}
