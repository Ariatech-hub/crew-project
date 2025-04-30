using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services
{

    public interface ICommunityService
    {
        Task<CommunityDto> Insert(CommunityInsertDto community);
        Task Update(CommunityUpdateDto community);
        Task<CommunityDto> Delete(CommunityDeleteDto communityDto);
        Task<IEnumerable<CommunityDto>> GetAll();

        Task<CommunityDto> GetById(int id);

        Task<IEnumerable<CommunityDto>> GetActiveCommunitites();
    }

    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IGeneralUtility _generalUtility;

        public CommunityService(ICommunityRepository communityRepository , IGeneralUtility generalUtility)
        {
            _communityRepository = communityRepository;
            _generalUtility = generalUtility;
        }
        public async Task<CommunityDto> Insert(CommunityInsertDto community)
        {
            if (community is null)
            {
                throw new Exception("Invalid Community");
            }

            community.Name = community.Name.Trim();
            community.NepaliName = community.NepaliName.Trim();
            community.Code = community.Code?.Trim();

            var communities = await _communityRepository.GetAllAsync();
            foreach (var item in communities)
            {
                if (item.Code == community.Code)
                {
                    throw new Exception("Code Already Exists");
                }
            }

            var mappedCommunity = ObjectMapper.Mapper.Map<Community>(community);
            mappedCommunity.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedCommunity.IsActive = true;
            mappedCommunity.CreatedDate = _generalUtility.GetCurrentNepalTime();
            var returnedCommunity = await _communityRepository.AddAsync(mappedCommunity);
            return ObjectMapper.Mapper.Map<CommunityDto>(returnedCommunity);

        }

        public async Task Update(CommunityUpdateDto community)
        {
            if (community is null)
            {
                throw new Exception("Invalid Community");
            }



            community.Name = community.Name.Trim();
            community.NepaliName = community.NepaliName.Trim();
            community.Code = community.Code?.Trim();
            var communities = await _communityRepository.GetAllAsync();
            foreach (var item in communities)
            {
                if (item.Code == community.Code)
                {
                    throw new Exception("Code Already Exists");
                }
            }

            var mappedCommunity = ObjectMapper.Mapper.Map<Community>(community);
            mappedCommunity.ModifiedBy = _generalUtility.GetLoggedInUsername();
            mappedCommunity.ModifiedDate = _generalUtility.GetCurrentNepalTime();

            var communityToUpdate = await _communityRepository.GetByIdAsync(community.Id);
            if (communityToUpdate is null)
            {
                throw new Exception("Invalid Community");
            }

            await _communityRepository.UpdateAsync(mappedCommunity);
            
        }

        public async Task<CommunityDto> Delete(CommunityDeleteDto communityDto)
        {
            var communityToDelete = await _communityRepository.GetByIdAsync(communityDto.Id);
            if (communityToDelete is null)
            {
                throw new Exception("Invalid Community");
            }

            var returnedCommunity = await _communityRepository.DeleteAsync(communityToDelete);

            return ObjectMapper.Mapper.Map<CommunityDto>(returnedCommunity);
        }

        public async Task<IEnumerable<CommunityDto>> GetAll()
        {
            var data = await _communityRepository.GetAllAsync();
            ValidationUtility.IsModelExist(data);
            return ObjectMapper.Mapper.Map<IEnumerable<CommunityDto>>(await _communityRepository.GetAllAsync(false));
        }

        public async Task<CommunityDto> GetById(int id)
        {
            return ObjectMapper.Mapper.Map<CommunityDto>(await _communityRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<CommunityDto>> GetActiveCommunitites()
        {
            var community = await _communityRepository.GetAsync(x => x.IsActive);
            return ObjectMapper.Mapper.Map<IEnumerable<CommunityDto>>(community);
        }
    }
  
}
