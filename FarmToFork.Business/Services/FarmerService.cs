


using FarmToFork.Core.Entities;
using FarmToFork.Core.Exception;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;

namespace FarmToFork.Business.Services
{
    public interface IFarmerService
    {
        Task<int> FarmerSync(string data, IFormFile file);

        Task<IEnumerable<FarmerReportRelated>> GetFarmerForReport(int province, int districtId, int palikaId, int ward, int communityId);

        Task<int> FarmerProductionPlanSync(FarmerProductionSyncDto farmerProductionSyncDto);

        Task<IEnumerable<FarmerTableDto>> GetAllFarmerList();

        Task<(string name, string message)> FarmerActiveStatusChanged(int farmerId);

        Task<FarmerViewDto> GetFarmerForView(int farmerId);

        Task<IEnumerable<FarmerMemberShipIdDto>> GetFarmerHavingMembershipId();

        Task<FarmerReportRelated> GetFarmerDetailsById(int id);
        Task<(string photoName, string imageUploadPath)> ChangePicture(IFormFile file, int id);

        Task<IEnumerable<FarmerLastProductionPlanDto>> GetFarmerProductionPlanList();
        Task<FarmerTableDto> UpdateIsNonMember(int id);
    }

    public class FarmerService : IFarmerService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGeneralUtility _generalUtility;
        private readonly IFarmerRepository _farmerRepository;
        private readonly IReceiptRepository _receiptRepository;
        private readonly IProductionPlanRepository _productionPlanRepository;

        public FarmerService(IWebHostEnvironment webHostEnvironment, IGeneralUtility generalUtility, IFarmerRepository farmerRepository, IReceiptRepository receiptRepository, IProductionPlanRepository productionPlanRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _generalUtility = generalUtility;
            _farmerRepository = farmerRepository;
            _receiptRepository = receiptRepository;
            _productionPlanRepository = productionPlanRepository;
        }

        public async Task<IEnumerable<FarmerReportRelated>> GetFarmerForReport(int province, int districtId, int palikaId, int ward, int communityId)
        {
            return await _farmerRepository.GetFarmerForReport(province, districtId, palikaId, ward, communityId);
        }

        public async Task<int> FarmerProductionPlanSync(FarmerProductionSyncDto farmerProductionSyncDto)
        {

            if (farmerProductionSyncDto is null)
            {
                throw new DataNotFoundException("Invalid Production Plan Data");
            }

            ProductionPlan mappedProductionPlan = ObjectMapper.Mapper.Map<ProductionPlan>(farmerProductionSyncDto.ProductionPlanData);

            if (farmerProductionSyncDto.Farmer.SyncId is 0 or null)
            {
                throw new DataValidationException("Invalid Farmer");
            }
            mappedProductionPlan.FarmerId = farmerProductionSyncDto.Farmer.SyncId.Value;
            mappedProductionPlan.CreatedBy = farmerProductionSyncDto.Enumerator.Username;
            mappedProductionPlan.CreatedDate = _generalUtility.GetCurrentNepalTime();
            mappedProductionPlan.SyncDate = _generalUtility.GetCurrentNepalTime();
            int insertedFarmerPlanDataId = await _farmerRepository.ProductionPlanSync(mappedProductionPlan);
            return insertedFarmerPlanDataId;
        }

        public async Task<int> FarmerSync(string data, IFormFile file)
        {
            string imageName = string.Empty;
            string imageUploadPath = _webHostEnvironment.ContentRootPath + "\\Images\\Farmer";
            try
            {

                _generalUtility.CreateFolderIfNotExist(imageUploadPath);

                FarmerDtoVm farmerDto = JsonConvert.DeserializeObject<FarmerDtoVm>(data);

                if (farmerDto.Enumerator != null)
                {
                    farmerDto.Farmer.Latitude = !string.IsNullOrEmpty(farmerDto.Enumerator.Location)
                       ? farmerDto.Enumerator.Location.Split(",")[0]
                       : null;

                    farmerDto.Farmer.Longitude = !string.IsNullOrEmpty(farmerDto.Enumerator.Location)
                        ? farmerDto.Enumerator.Location.Split(",").Last()
                        : null;
                }

                farmerDto.Farmer.PhotoName = imageName = await _generalUtility.ReturnFileNameFromFile(file, imageUploadPath);

                if (farmerDto.Farmer is null)
                {
                    throw new DataValidationException("Invalid Farmer");
                }

                DataTable farmerMemberTable = new();
                farmerMemberTable.Columns.Add("Name");
                farmerMemberTable.Columns.Add("RelationId");
                farmerMemberTable.Columns.Add("OccupationId");
                farmerMemberTable.Columns.Add("EducationLevelId");
                farmerMemberTable.Columns.Add("GenderId");
                farmerMemberTable.Columns.Add("Age");
                farmerMemberTable.Columns.Add("OwnSmartPhone");
                farmerMemberTable.Columns.Add("Remarks");
                farmerMemberTable.Columns.Add("CreatedDate");
                farmerMemberTable.Columns.Add("Identifier");
                farmerMemberTable.Columns.Add("MobileNumber");
                farmerMemberTable.Columns.Add("SyncId");
                farmerMemberTable.Columns.Add("LocationId");

                if (farmerDto.FarmerMember.Any())
                {
                    foreach (var farmerMember in farmerDto.FarmerMember)
                    {
                        DataRow dr = farmerMemberTable.NewRow();
                        dr[0] = farmerMember.Name ?? throw new GeneralException("Member Name is required");
                        dr[1] = farmerMember.RelationId == 0 ? throw new GeneralException("Member Relation is required") : farmerMember.RelationId;
                        dr[2] = farmerMember.OccupationId == 0 ? throw new GeneralException("Member Occupation is required") : farmerMember.OccupationId;
                        dr[3] = farmerMember.EducationLevelId == 0 ? throw new GeneralException("Member Education Level is required") : farmerMember.EducationLevelId;
                        dr[4] = farmerMember.GenderId == 0 ? throw new GeneralException("Member Gender is required") : farmerMember.GenderId;
                        dr[5] = farmerMember.Age == 0 ? throw new GeneralException("Member Age is required") : farmerMember.Age;
                        dr[6] = farmerMember.OwnSmartPhone;
                        dr[7] = farmerMember.Remarks;
                        if (farmerMember.CreatedDate is null)
                        {
                            throw new DataValidationException("Farmer Member Created Date is required");
                        }
                        string datetime = _generalUtility.ConvertEpochDateTime(farmerMember.CreatedDate!.Value).ToString("yyyy-MM-dd HH:mm:ss");
                        dr[8] = datetime;
                        dr[9] = string.IsNullOrEmpty(farmerMember.Identifier) ? throw new DataValidationException("Identifier is required") : farmerMember.Identifier;
                        dr[10] = string.IsNullOrEmpty(farmerMember.MobileNumber)
                            ? farmerMember.MobileNumber
                            : farmerMember.MobileNumber.Trim();
                        dr[11] = farmerMember.Id;
                        dr[12] = farmerMember.LocationId;
                        farmerMemberTable.Rows.Add(dr);
                    }
                }

                DataTable farmerMemberPropertyTable = new();
                farmerMemberPropertyTable.Columns.Add("MemberIdentifier");
                farmerMemberPropertyTable.Columns.Add("ProvinceId");
                farmerMemberPropertyTable.Columns.Add("DistrictId");
                farmerMemberPropertyTable.Columns.Add("PalikaId");
                farmerMemberPropertyTable.Columns.Add("Ward");
                farmerMemberPropertyTable.Columns.Add("Tole");
                farmerMemberPropertyTable.Columns.Add("KittaNumber");
                farmerMemberPropertyTable.Columns.Add("LandArea");
                farmerMemberPropertyTable.Columns.Add("LandValuation");
                farmerMemberPropertyTable.Columns.Add("CreatedDate");
                farmerMemberPropertyTable.Columns.Add("SyncId");
                farmerMemberPropertyTable.Columns.Add("FarmerMemberId");

                if (farmerDto.FarmerMemberProperty.Any())
                {
                    foreach (var farmerProperty in farmerDto.FarmerMemberProperty)
                    {
                        DataRow dr = farmerMemberPropertyTable.NewRow();
                        dr["MemberIdentifier"] = farmerProperty.MemberIdentifier;
                        dr["ProvinceId"] = farmerProperty.ProvinceId;
                        dr["DistrictId"] = farmerProperty.DistrictId;
                        dr["PalikaId"] = farmerProperty.PalikaId;
                        dr["Ward"] = farmerProperty.Ward;
                        dr["Tole"] = farmerProperty.Tole;
                        dr["KittaNumber"] = farmerProperty.KittaNumber;
                        dr["LandArea"] = farmerProperty.LandArea;
                        dr["LandValuation"] = farmerProperty.LandValuation;
                        dr["SyncId"] = farmerProperty.Id;
                        dr["FarmerMemberId"] = farmerProperty.FarmerMemberId == 0 ? throw new DataValidationException("Farmer Member Id is required") : farmerProperty.FarmerMemberId;
                        if (farmerProperty.CreatedDate is null)
                        {
                            throw new DataValidationException("Farmer Member Property Created Date is required");
                        }
                        string datetime = _generalUtility.ConvertEpochDateTime(farmerProperty.CreatedDate!.Value).ToString("yyyy-MM-dd HH:mm:ss");
                        dr["CreatedDate"] = datetime;
                        farmerMemberPropertyTable.Rows.Add(dr);
                    }

                }

                DataTable farmerMemberTransactionTable = new();
                farmerMemberTransactionTable.Columns.Add("MemberIdentifier");
                farmerMemberTransactionTable.Columns.Add("IncomeSource");
                farmerMemberTransactionTable.Columns.Add("IncomeAmount");
                farmerMemberTransactionTable.Columns.Add("ExpenseSource");
                farmerMemberTransactionTable.Columns.Add("ExpenseAmount");
                farmerMemberTransactionTable.Columns.Add("CreatedDate");
                farmerMemberTransactionTable.Columns.Add("SyncId");
                farmerMemberTransactionTable.Columns.Add("FarmerMemberId");


                if (farmerDto.FarmerMemberTransaction.Any())
                {
                    foreach (var memberTransaction in farmerDto.FarmerMemberTransaction)
                    {
                        DataRow dr = farmerMemberTransactionTable.NewRow();
                        dr["MemberIdentifier"] = memberTransaction.MemberIdentifier;
                        dr["IncomeSource"] = memberTransaction.IncomeSource;
                        dr["IncomeAmount"] = memberTransaction.IncomeAmount;
                        dr["ExpenseSource"] = memberTransaction.ExpenseSource;
                        dr["ExpenseAmount"] = memberTransaction.ExpenseAmount;
                        dr["FarmerMemberId"] = memberTransaction.FarmerMemberId == 0 ? throw new DataValidationException("Farmer Member Id is required") : memberTransaction.FarmerMemberId;

                        if (memberTransaction.CreatedDate is null)
                        {
                            throw new DataValidationException("Farmer Member Property Created Date is required");
                        }
                        string datetime = _generalUtility.ConvertEpochDateTime(memberTransaction.CreatedDate!.Value).ToString("yyyy-MM-dd HH:mm:ss");

                        dr["CreatedDate"] = datetime;
                        dr["SyncId"] = memberTransaction.Id;
                        farmerMemberTransactionTable.Rows.Add(dr);
                    }
                }

                Farmer mappedFarmer = ObjectMapper.Mapper.Map<Farmer>(farmerDto.Farmer);
                if (farmerDto.Farmer.CreatedDate != null)
                    mappedFarmer.CreatedDate = _generalUtility.ConvertEpochDateTime(farmerDto.Farmer.CreatedDate.Value);

                int insertedId = await _farmerRepository.FarmerSync(mappedFarmer, farmerMemberTable, farmerMemberPropertyTable, farmerMemberTransactionTable);
                return insertedId;
            }
            catch (Exception e)
            {
                _generalUtility.DeleteFileFromServer(imageUploadPath, imageName);
                throw new GeneralException(e.Message);
            }

        }

        public async Task<IEnumerable<FarmerTableDto>> GetAllFarmerList()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<FarmerTableDto>>(await _farmerRepository.GetAllAsync(true));
        }

        public async Task<(string name, string message)> FarmerActiveStatusChanged(int farmerId)
        {
            Farmer farmer = await _farmerRepository.FarmerActiveStatusChanged(farmerId, _generalUtility.GetLoggedInUsername(), _generalUtility.GetCurrentNepalTime());
            string responseMessage = $"Farmer {farmer.FullName}'s status has been changed";
            return (farmer.FullName, responseMessage);

        }

        public async Task<FarmerViewDto> GetFarmerForView(int farmerId)
        {
            FarmerReportRelated farmer = await _farmerRepository.GetById(farmerId);

            FarmerViewDto farmerViewDto = ObjectMapper.Mapper.Map<FarmerViewDto>(farmer);

            return farmerViewDto;

        }



        public async Task<IEnumerable<FarmerMemberShipIdDto>> GetFarmerHavingMembershipId()
        {
            IEnumerable<Farmer> farmers = await _farmerRepository.GetFarmerHavingMemberShipId();
            IEnumerable<FarmerMemberShipIdDto> farmerDtos = ObjectMapper.Mapper.Map<IEnumerable<FarmerMemberShipIdDto>>(farmers);
            return farmerDtos;
        }


        public async Task<FarmerReportRelated> GetFarmerDetailsById(int id)
        {
            return await _farmerRepository.GetFarmerReportRelatedById(id);
        }
        public async Task<(string photoName, string imageUploadPath)> ChangePicture(IFormFile file, int id)
        {
            string imageUploadPath = _webHostEnvironment.ContentRootPath + "\\Images\\Farmer";
            if (file == null)
            {
                throw new DataValidationException("File is required");

            }

            var fileName = await _generalUtility.ReturnFileNameFromFile(file, imageUploadPath);
            string oldFileName = await _farmerRepository.ChangePicture(fileName, id);
            return (oldFileName, imageUploadPath);

        }

        public async Task<IEnumerable<FarmerLastProductionPlanDto>> GetFarmerProductionPlanList()
        {
            IEnumerable<Farmer> farmers = await _farmerRepository.GetFarmerThisSeasonProduction();
            IEnumerable<FarmerLastProductionPlanDto> mappedFarmers = ObjectMapper.Mapper.Map<IEnumerable<FarmerLastProductionPlanDto>>(farmers);

            return mappedFarmers;

        }

        public async Task<FarmerTableDto> UpdateIsNonMember(int id)
        {
            Farmer farmerToBeUpdated = new Farmer()
            {
                Id = id,
                ModifiedBy = _generalUtility.GetLoggedInUsername(),
                ModifiedDate = _generalUtility.GetCurrentNepalTime()
            };
            Farmer farmer = await _farmerRepository.UpdateIsNonMember(farmerToBeUpdated);
            FarmerTableDto updatedFarmer = ObjectMapper.Mapper.Map<FarmerTableDto>(farmer);
            return updatedFarmer;
        }
    }
}

