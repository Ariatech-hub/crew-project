

using FarmToFork.Business.DTOs;
using FarmToFork.Core.Entities;
using NuGet.Protocol;

namespace FarmToFork.Application.Controllers;

[Route("api/report")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly IFarmerService _farmerService;
    private readonly IGeneralUtility _generalUtility;
    private readonly IDispatchService _dispatchService;
    private readonly IStockService _stockService;
    private readonly IReceiptService _receiptService;
    private readonly ICustomerService _customerService;

    public ReportController(
        ILogger<ReportController> logger,
        IFarmerService farmerService, 
        IGeneralUtility generalUtility, 
        IDispatchService dispatchService,
        IStockService stockService, IReceiptService receiptService, ICustomerService customerService)
    {
        _logger = logger;
        _farmerService = farmerService;
        _generalUtility = generalUtility;
        _dispatchService = dispatchService;
        _stockService = stockService;
        _receiptService = receiptService;
        _customerService = customerService;
    }

    //[HttpGet, Route("farmer"), CheckAccessForMenu(MenuCode = 16)]
    [HttpGet, Route("farmer"), AllowAnonymous]
    public async Task<IActionResult> DownloadFarmerReport(int provinceId, int districtId, int palikaId, int ward, int communityId)
    {
        try
        {
            var report = await _farmerService.GetFarmerForReport(provinceId, districtId, palikaId, ward, communityId);
            byte[] reportBytes;
            using (var pck = new ExcelPackage())
            {
                #region  ---- Farmer Sheet Heading --------

                ExcelWorksheet farmerSheet = pck.Workbook.Worksheets.Add("Farmers");
                string[] farmerStaticHeaders = new string[]
                {
                    "S.N" , "Name" , "Code" , "Membership Number" , "Image" ,  "Province" , "District" , "Palika" , "Ward" , "Community" , "Gender" , "Ethnicity" , "Marital Status" ,
                    "Date Of Birth (B.S)" , "Age (Years)" , "Mobile Number" , "Latitude" , "Longitude" ,  "Occupation" , "Education Level (Grade)" , "Husband Or Father Name" , 
                    "Husband Or Wife Name" ,  "Grandfather Or Father In Law Name " ,
                    "Own Smart Phone" , "Citizenship Number" , "Nominee Name" , "Nominee Relationship" , "Cattle Valuation Amount (NPR)" , "House Valuation Amount (NPR)" , "Business Valuation Amount (NPR)" ,
                    "Jewelry Valuation Amount (NPR)" , "Bank Balance Valuation Amount (NPR)" , "Other Valuation Amount (NPR)" , "Is Active" , "Created By" , "Created Date (AD)"
                };
                //ToDo: Code Review
                var firstRow = farmerSheet.Cells[1, 2];
                firstRow.Value = "Farm To Fork";
                firstRow.Style.Font.Bold = true;
                firstRow.Style.Font.Size = 18;
                firstRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var secondRow = farmerSheet.Cells[2,  2];
                secondRow.Value = "Farmer Report";
                secondRow.Style.Font.Bold = true;
                secondRow.Style.Font.Size = 16;
                secondRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var thirdRow = farmerSheet.Cells[3, 2];
                thirdRow.Value = $"Generated on: {DateTime.UtcNow.AddMinutes(345):MMM dd, yyyy | hh:mm tt}";
                thirdRow.Style.Font.Size = 14;
                thirdRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                const int headingPrintStart = 5;
                for (var i = 0; i < farmerStaticHeaders.Length; i++)
                {
                    var currentValue = (i + 1);
                    farmerSheet.Cells[headingPrintStart, currentValue].Value = farmerStaticHeaders[i];
                    farmerSheet.Cells[headingPrintStart, currentValue].Style.Font.Bold = true;
                    farmerSheet.Cells[headingPrintStart, currentValue].Style.HorizontalAlignment = i is 0 ? ExcelHorizontalAlignment.Center : ExcelHorizontalAlignment.Left;
                }
                #endregion

                #region --------- Farmer Member sheet heading ------------

                ExcelWorksheet farmerMemberSheet = pck.Workbook.Worksheets.Add("Farmer Member");

                string[] farmerMemberStaticHeader = new string[]
                {
                    "S.N" , "Code" , "Farmer Name" , "Member Name" , "Relation" , "Occupation" , "Education Level (Grade)" , "Gender" , "Age (Years)" , "Own Smart Phone" , "Mobile Number" ,  
                    "Income Source" , "Income Amount (NPR)" ,
                    "Expense Amount (NPR)", "Saving Amount (NPR)" , "Location Id" , "Remarks" , "Province" , "District" , "Palika" , "Ward" , "Kitta Number" , "Land Area (Aana)" , "Land Valuation (NPR)" ,
                    "Tole" , "Created By" , "Created Date (AD)"
                };


                var firstRowMemberSheet = farmerMemberSheet.Cells[1, 1, 1, farmerMemberStaticHeader.Length];
                firstRowMemberSheet.Merge = true;
                firstRowMemberSheet.Value = "Farm To Fork";
                firstRowMemberSheet.Style.Font.Bold = true;
                firstRowMemberSheet.Style.Font.Size = 20;
                firstRowMemberSheet.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var secondRowMemberSheet = farmerMemberSheet.Cells[2, 1, 2, farmerMemberStaticHeader.Length];
                secondRowMemberSheet.Merge = true;
                secondRowMemberSheet.Value = "Farmer Member Report";
                secondRowMemberSheet.Style.Font.Bold = true;
                secondRowMemberSheet.Style.Font.Size = 20;
                secondRowMemberSheet.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var thirdRowMemberSheet = farmerMemberSheet.Cells[3, 1, 3, farmerMemberStaticHeader.Length];
                thirdRowMemberSheet.Merge = true;
                thirdRowMemberSheet.Value = $"Generated on: {DateTime.UtcNow.AddMinutes(345):MMM dd, yyyy | hh:mm tt}";
                thirdRowMemberSheet.Style.Font.Size = 14;
                thirdRowMemberSheet.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                for (var i = 0; i < farmerMemberStaticHeader.Length; i++)
                {
                    var currentValue = (i + 1);
                    farmerMemberSheet.Cells[headingPrintStart, currentValue].Value = farmerMemberStaticHeader[i];
                    farmerMemberSheet.Cells[headingPrintStart, currentValue].Style.Font.Bold = true;
                    farmerMemberSheet.Cells[headingPrintStart, currentValue].Style.HorizontalAlignment = i is 0 ? ExcelHorizontalAlignment.Center : ExcelHorizontalAlignment.Left;
                }
                #endregion

                #region -------- Last Season Sheet heading ------- 

                ExcelWorksheet lastSeasonProductionSheet = pck.Workbook.Worksheets.Add("Last Season Production");

                string[] lastSeasonProductionStaticHeaders = new string[]
                {
                    "S.N" , "Code" , "Farmer Name" , "Grain" , "Production Land Area (Aana)" , "Total Production (Kg)" , "Total Sale (Kg)" , "Tole Sale Amount (NPR)" , "Created By" , "Created Date"
                };

                var firstRowLastSeasonProductionSheet = lastSeasonProductionSheet.Cells[1, 1, 1, lastSeasonProductionStaticHeaders.Length];
                firstRowLastSeasonProductionSheet.Merge = true;
                firstRowLastSeasonProductionSheet.Value = "Farm To Fork";
                firstRowLastSeasonProductionSheet.Style.Font.Bold = true;
                firstRowLastSeasonProductionSheet.Style.Font.Size = 20;
                firstRowLastSeasonProductionSheet.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var secondRowLastSeasonProductionSheet = lastSeasonProductionSheet.Cells[2, 1, 2, lastSeasonProductionStaticHeaders.Length];
                secondRowLastSeasonProductionSheet.Merge = true;
                secondRowLastSeasonProductionSheet.Value = "Last Season Production";
                secondRowLastSeasonProductionSheet.Style.Font.Bold = true;
                secondRowLastSeasonProductionSheet.Style.Font.Size = 20;
                secondRowLastSeasonProductionSheet.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var thirdRowLastSeasonProductionSheet = lastSeasonProductionSheet.Cells[3, 1, 3, lastSeasonProductionStaticHeaders.Length];
                thirdRowLastSeasonProductionSheet.Merge = true;
                thirdRowLastSeasonProductionSheet.Value = $"Generated on: {DateTime.UtcNow.AddMinutes(345):MMM dd, yyyy | hh:mm tt}"; 
                thirdRowLastSeasonProductionSheet.Style.Font.Bold = true;
                thirdRowLastSeasonProductionSheet.Style.Font.Size = 18;
                thirdRowLastSeasonProductionSheet.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                for (var i = 0; i < lastSeasonProductionStaticHeaders.Length; i++)
                {
                    var currentValue = (i + 1);
                    lastSeasonProductionSheet.Cells[headingPrintStart, currentValue].Value = lastSeasonProductionStaticHeaders[i];
                    lastSeasonProductionSheet.Cells[headingPrintStart, currentValue].Style.Font.Bold = true;
                    lastSeasonProductionSheet.Cells[headingPrintStart, currentValue].Style.HorizontalAlignment = i is 0 ? ExcelHorizontalAlignment.Center : ExcelHorizontalAlignment.Left;
                }




                #endregion

                #region  --------- Production Plan Header Print ----------

                ExcelWorksheet productionPlanSheet = pck.Workbook.Worksheets.Add("Production Plan");

                string[] productionPlanStaticHeaders = new string[]
                {
                    "S.N" , "Code" , "Farmer Name" , "Grain Cycle" , "Grain" , "Land Area (Aana)" , "Total Production (Kg)" , "Home Use (Kg)" , "Total Sales(Kg)" , "Total Sale Through Cooperative (Kg)" , "Seed Only (Kg)" ,
                    "Created By" , "Created Date"
                };

                var firstRowProductionPlanSheet = productionPlanSheet.Cells[1, 1, 1, productionPlanStaticHeaders.Length];
                firstRowProductionPlanSheet.Merge = true;
                firstRowProductionPlanSheet.Value = "Farm To Fork";
                firstRowProductionPlanSheet.Style.Font.Bold = true;
                firstRowProductionPlanSheet.Style.Font.Size = 20;
                firstRowProductionPlanSheet.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var secondRowProductionPlanSheet = productionPlanSheet.Cells[2, 1, 2, productionPlanStaticHeaders.Length];
                secondRowProductionPlanSheet.Merge = true;
                secondRowProductionPlanSheet.Value = "This Season Production Plan";
                secondRowProductionPlanSheet.Style.Font.Bold = true;
                secondRowProductionPlanSheet.Style.Font.Size = 18;
                secondRowProductionPlanSheet.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var thirdRowProductionPlanSheet = productionPlanSheet.Cells[3, 1, 3, productionPlanStaticHeaders.Length];
                thirdRowProductionPlanSheet.Merge = true;
                thirdRowProductionPlanSheet.Value = $"Generated on: {DateTime.UtcNow.AddMinutes(345):MMM dd, yyyy | hh:mm tt}";
                thirdRowProductionPlanSheet.Style.Font.Bold = true;
                thirdRowProductionPlanSheet.Style.Font.Size = 16;
                thirdRowProductionPlanSheet.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                for (var i = 0; i < productionPlanStaticHeaders.Length; i++)
                {
                    var currentValue = (i + 1);
                    productionPlanSheet.Cells[headingPrintStart, currentValue].Value = productionPlanStaticHeaders[i];
                    productionPlanSheet.Cells[headingPrintStart, currentValue].Style.Font.Bold = true;
                    productionPlanSheet.Cells[headingPrintStart, currentValue].Style.HorizontalAlignment = i is 0 ? ExcelHorizontalAlignment.Center : ExcelHorizontalAlignment.Left;
                }

                #endregion
                
                #region --------- Research Sheet ----------

                ExcelWorksheet researchSheet = pck.Workbook.Worksheets.Add("Research");

                string[] researchSheetStaticHeader = new string[]
                {
                    "S.N" , "Code" , "Farmer Name" , "Economically Active People" , "Has Internet Connection" , "Internet Type" , "Internet Use " , "Making Land Ready" , "PlantSeed" ,
                    "Taking Care" , "HarvestingAndStoring", "Selling And Marketing" , "Land Ownership" ,    "Production" , "Sales" , 
                    "Grain" , "Month" , "Week" ,  "Sale Rate (NPR)" , "Is From Home"
                };


                var firstResearchSheetStaticHeader = researchSheet.Cells[1, 1, 1, researchSheetStaticHeader.Length];
                firstResearchSheetStaticHeader.Merge = true;
                firstResearchSheetStaticHeader.Value = "Farm To Fork";
                firstResearchSheetStaticHeader.Style.Font.Bold = true;
                firstResearchSheetStaticHeader.Style.Font.Size = 20;
                firstResearchSheetStaticHeader.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                var secondResearchSheetStaticHeader = researchSheet.Cells[2, 1, 2, researchSheetStaticHeader.Length];
                secondResearchSheetStaticHeader.Merge = true;
                secondResearchSheetStaticHeader.Value = "Research";
                secondResearchSheetStaticHeader.Style.Font.Bold = true;
                secondResearchSheetStaticHeader.Style.Font.Size = 18;
                secondResearchSheetStaticHeader.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var thirdResearchSheetStaticHeader = researchSheet.Cells[3, 1, 3, researchSheetStaticHeader.Length];
                thirdResearchSheetStaticHeader.Merge = true;
                thirdResearchSheetStaticHeader.Value = $"Generated on: {DateTime.UtcNow.AddMinutes(345):MMM dd, yyyy | hh:mm tt}"; 
                thirdResearchSheetStaticHeader.Style.Font.Bold = true;
                thirdResearchSheetStaticHeader.Style.Font.Size = 16;
                thirdResearchSheetStaticHeader.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                researchSheet.Cells["P6:T6"].Merge = true;
                researchSheet.Cells["P6:T6"].Style.Font.Bold = true;
                researchSheet.Cells["P6:T6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                researchSheet.Cells["P6:T6"].Value = "Market Status";

                researchSheet.Cells["N6:O6"].Merge = true;
                researchSheet.Cells["N6:O6"].Style.Font.Bold = true;
                researchSheet.Cells["N6:O6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                researchSheet.Cells["N6:O6"].Value = "Obstacles";

                researchSheet.Cells["H6:M6"].Merge = true;
                researchSheet.Cells["H6:M6"].Style.Font.Bold = true;
                researchSheet.Cells["H6:M6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                researchSheet.Cells["H6:M6"].Value = "Labour Division For Bean Production";



                for (var i = 0; i < researchSheetStaticHeader.Length; i++)
                {
                    var currentValue = (i + 1);
                    researchSheet.Cells[7, currentValue].Value = researchSheetStaticHeader[i];
                    researchSheet.Cells[7, currentValue].Style.Font.Bold = true;
                    researchSheet.Cells[7, currentValue].Style.HorizontalAlignment = i is 0 ? ExcelHorizontalAlignment.Center : ExcelHorizontalAlignment.Left;
                }
                
                #endregion



                int farmerMemberDataStart;
                int lastSeasonProductionDataStart;
                int productionPlanDataStart;
                int dataStart = farmerMemberDataStart = lastSeasonProductionDataStart = productionPlanDataStart =  headingPrintStart + 1;
                int researchDataStart = 8;
                foreach (var farmer in report.Select((x, i) => new { x, i }))
                {
                    #region ---------- Farmer Data Print  -----------

                    int startingColumn = 0;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.i + 1;
                    farmerSheet.Cells[dataStart, startingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.FullName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.FarmerCode;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.FarmerMembershipNumber;
                    farmerSheet.Cells[dataStart, startingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.PhotoName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.ProvinceNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.DistrictNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.PalikaNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.Ward;
                    farmerSheet.Cells[dataStart, startingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.CommunityNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.GenderNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.EthnicityNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.MartialStatusNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.DateOfBirth;
                    farmerSheet.Cells[dataStart, startingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.Age;
                    farmerSheet.Cells[dataStart, startingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.MobileNumber;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.Latitude;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.Longitude;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.OccupationNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.EducationLevelNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.HusbandOrFatherName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.HusbandOrWifeName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.GrandFatherOrFatherInLawName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.OwnSmartPhone ? "✓" : "×";
                    farmerSheet.Cells[dataStart, startingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.CitizenshipNumber;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.NomineeName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.NomineeRelationNepaliName;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value =  _generalUtility.ToLocalFormat(farmer.x.CattleValuationAmount ?? 0);
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = _generalUtility.ToLocalFormat(farmer.x.HouseValuationAmount ?? 0); 
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = _generalUtility.ToLocalFormat(farmer.x.BusinessValuationAmount ?? 0); 
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = _generalUtility.ToLocalFormat(farmer.x.JewelryValuationAmount ?? 0);
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = _generalUtility.ToLocalFormat(farmer.x.BankBalanceValuationAmount ?? 0);
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = _generalUtility.ToLocalFormat(farmer.x.OtherValuationAmount ?? 0);
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.IsActive ? "✓" : "×";
                    farmerSheet.Cells[dataStart, startingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.CreatedBy;

                    if (!string.IsNullOrEmpty(farmer.x.PhotoName))
                    {
                        var image = $"{Request.Scheme}://{Request.Host}/Images/Farmer/{farmer.x.PhotoName}";
                        farmerSheet.Cells[dataStart, 5].Hyperlink = new Uri(image);
                        farmerSheet.Cells[dataStart, 5].Style.Font.Color.SetColor(color: Color.DarkBlue);
                        farmerSheet.Cells[dataStart, 5].Style.Font.UnderLine = true;

                    }



                    if (farmer.x.SyncDate is not null)
                    {
                        //TODO: Code Review
                        farmerSheet.Cells[dataStart, ++startingColumn].Value = farmer.x.SyncDate!.Value.ToString("yyyy-MM-dd | hh:mm");
                    }
                    dataStart++;
                    #endregion

                    #region --------- Farmer member data Print --------

                    int farmerMemberStartingColumn = 0;
                    if (farmer.x.FarmerMembers.Any())
                    {
                        farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = farmer.i + 1;
                        farmerMemberSheet.Cells[farmerMemberDataStart, farmerMemberStartingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = farmer.x.FarmerCode;
                        farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = farmer.x.FullName;
                        farmerMemberSheet.Cells[farmerMemberDataStart, farmerMemberStartingColumn].Style.Font.Bold = false;

                        farmerMemberSheet.Cells[farmerMemberDataStart, 1, farmerMemberDataStart, farmerMemberStaticHeader.Length].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        farmerMemberSheet.Cells[farmerMemberDataStart, 1, farmerMemberDataStart, farmerMemberStaticHeader.Length].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#F8CBAD"));
                        foreach (var member in farmer.x.FarmerMembers)
                        {
                            farmerMemberDataStart++;
                            farmerMemberStartingColumn = 3;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.Name;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.RelationNepaliName;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.OccupationNepaliName;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.EducationLevelNepaliName;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.GenderNepaliName;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.Age;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.OwnSmartPhone ? "✓" : "×";
                            farmerMemberSheet.Cells[farmerMemberDataStart, farmerMemberStartingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.MobileNumber;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.IncomeSourceNepaliName;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = _generalUtility.ToLocalFormat(member.IncomeAmount ?? 0);
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = _generalUtility.ToLocalFormat(member.ExpenseAmount ?? 0); 
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = _generalUtility.ToLocalFormat(member.SavingAmount ?? 0);
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.LocationNepaliName;
                            farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberStartingColumn].Value = member.Remarks;
                            if (member.SyncDate is not null)
                            {
                                //TODO: Code Review
                                farmerMemberSheet.Cells[farmerMemberDataStart, 27].Value = member.SyncDate.Value.ToString("yyyy-MM-dd | hh:mm");
                            }
                            farmerMemberSheet.Cells[farmerMemberDataStart, 26].Value = farmer.x.CreatedBy;

                            #region -------- Farmer Member Property ---------

                            if (member.FarmerMemberProperties.Any())
                            {

                                foreach (var memberProperty in member.FarmerMemberProperties)
                                {
                                    int farmerMemberDataPropertyStart = 17;
                                    farmerMemberDataStart++;
                                    farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberDataPropertyStart].Value = memberProperty.ProvinceNepaliName;
                                    farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberDataPropertyStart].Value = memberProperty.DistrictNepaliName;
                                    farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberDataPropertyStart].Value = memberProperty.PalikaNepaliName;
                                    farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberDataPropertyStart].Value = memberProperty.Ward;
                                    farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberDataPropertyStart].Value = memberProperty.KittaNumber;
                                    farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberDataPropertyStart].Value = memberProperty.LandArea;
                                    farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberDataPropertyStart].Value = _generalUtility.ToLocalFormat(memberProperty.LandValuation ?? 0); 
                                    farmerMemberSheet.Cells[farmerMemberDataStart, ++farmerMemberDataPropertyStart].Value = memberProperty.Tole;
                                }

                            }

                            #endregion

                            farmerMemberDataStart++;
                        }
                       
                    }
                    #endregion

                    #region  ----------- Last Season Production Data Print ------------

                    if (farmer.x.LastSeasonProductions.Any())
                    {
                        int lastSeasonStartingColumn = 0;

                        lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value = farmer.i + 1;
                        lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, lastSeasonStartingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value = farmer.x.FarmerCode;
                        lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value = farmer.x.FullName;
                        lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, lastSeasonStartingColumn].Style.Font.Bold = false;
                        lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, 1, lastSeasonProductionDataStart, lastSeasonProductionStaticHeaders.Length].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, 1, lastSeasonProductionDataStart, lastSeasonProductionStaticHeaders.Length].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#F8CBAD"));

                        foreach (var lastSeasonProduction in farmer.x.LastSeasonProductions)
                        {
                            lastSeasonProductionDataStart++;
                            lastSeasonStartingColumn = 3;
                            lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value = lastSeasonProduction.GrainNepaliName;
                            lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value = lastSeasonProduction.ProductionLandArea;
                            lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value = lastSeasonProduction.TotalProduction;
                            lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value = lastSeasonProduction.TotalSales;
                            lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value =_generalUtility.ToLocalFormat(lastSeasonProduction.TotalSalesAmount ?? 0);
                            lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value = farmer.x.CreatedBy;
                            if (farmer.x.SyncDate is not null)
                            {
                                lastSeasonProductionSheet.Cells[lastSeasonProductionDataStart, ++lastSeasonStartingColumn].Value = farmer.x.SyncDate.Value.ToString("yyyy-mm-dd | hh:mm");

                            }
                        }
                        lastSeasonProductionDataStart++;
                    }
                    #endregion

                    #region ---------- Production Plan -----------

                    int thisSeasonProductionPlanStartingColumn = 0;

                    if (farmer.x.ProductionPlans.Any())
                    {
                        productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = farmer.i + 1;
                        productionPlanSheet.Cells[productionPlanDataStart, thisSeasonProductionPlanStartingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = farmer.x.FarmerCode;
                        productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = farmer.x.FullName;
                        productionPlanSheet.Cells[productionPlanDataStart, thisSeasonProductionPlanStartingColumn].Style.Font.Bold = false;
                        productionPlanSheet.Cells[productionPlanDataStart, 1, productionPlanDataStart, productionPlanStaticHeaders.Length].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        productionPlanSheet.Cells[productionPlanDataStart, 1, productionPlanDataStart, productionPlanStaticHeaders.Length].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#F8CBAD"));

                        foreach (var productionPlan in farmer.x.ProductionPlans)
                        {
                            productionPlanDataStart++;
                            thisSeasonProductionPlanStartingColumn = 3;
                            productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = productionPlan.GrainCycleNepaliName;
                            productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = productionPlan.GrainNepaliName;
                            productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = productionPlan.LandArea;
                            productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = productionPlan.TotalProduction ?? 0;
                            productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = productionPlan.HomeUse ?? 0;
                            productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = productionPlan.TotalSales ?? 0;
                            productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = productionPlan.TotalSalesThroughCooperative ?? 0;
                            productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = productionPlan.SeedOnly ?? 0;
                            productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = farmer.x.CreatedBy;
                            if (farmer.x.SyncDate is not null)
                            {
                                productionPlanSheet.Cells[productionPlanDataStart, ++thisSeasonProductionPlanStartingColumn].Value = farmer.x.SyncDate.Value.ToString("yyyy-mm-dd | hh:mm");

                            }

                        }
                        productionPlanDataStart++;

                    }
                    #endregion

                    #region ----------- Research ---------

                    if (farmer.x.Researches.Any())
                    {
                        int researchStartingColumn = 0;

                        researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = farmer.i + 1;
                        researchSheet.Cells[researchDataStart, researchStartingColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = farmer.x.FarmerCode;
                        researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = farmer.x.FullName;
                        researchSheet.Cells[researchDataStart, researchStartingColumn].Style.Font.Bold = false;
                        researchSheet.Cells[researchDataStart, 1, researchDataStart, researchSheetStaticHeader.Length].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        researchSheet.Cells[researchDataStart, 1, researchDataStart, researchSheetStaticHeader.Length].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#F8CBAD"));

                        foreach (var research in farmer.x.Researches)
                        {
                            researchStartingColumn = 3;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.EconomicallyActivePeople ?? 0;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.HasInternetConnection ? "✓" : "×";
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.InternetTypeNepaliName;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.InternetUses;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.MakingLandReadyForBeanProductionNepali;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.PlantSeedForBeanProductionNepali;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.TakingCareForBeanProductionNepali;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.HarvestingAndStoringAfterBeanProductionNepali;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.SellingAndMarketingAfterBeanProductionNepali;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.LandOwnershipForBeanProductionNepali;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.ProductionObstacles;
                            researchSheet.Cells[researchDataStart, ++researchStartingColumn].Value = research.SalesObstacles;

                            foreach (var marketStatus in research.MarketStatuses)
                            {
                                int marketStatusStartingColumn = 15;
                                researchDataStart++;
                                researchSheet.Cells[researchDataStart, ++marketStatusStartingColumn].Value = marketStatus.GrainNepaliName;
                                researchSheet.Cells[researchDataStart, ++marketStatusStartingColumn].Value = marketStatus.MonthNepaliName;
                                researchSheet.Cells[researchDataStart, ++marketStatusStartingColumn].Value = marketStatus.Week;
                                researchSheet.Cells[researchDataStart, ++marketStatusStartingColumn].Value = _generalUtility.ToLocalFormat(marketStatus.SaleRate ?? 0);
                                researchSheet.Cells[researchDataStart, ++marketStatusStartingColumn].Value = marketStatus.IsFromHome ? "✓" : "×";
                            }


                        }


                        researchDataStart++;
                    }

                    #endregion
                    //ToOD:Code Review
                    farmerSheet.View.FreezePanes(6, 3);

                }

                ExcelRange dataArea01 = farmerSheet.Cells[5, 1, report.Count() + 5, farmerStaticHeaders.Length];
                dataArea01.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea01.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea01.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea01.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ExcelRange dataArea02 = farmerMemberSheet.Cells[5, 1, farmerMemberDataStart - 2 , farmerMemberStaticHeader.Length];
                dataArea02.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea02.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea02.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea02.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ExcelRange dataArea03 = lastSeasonProductionSheet.Cells[5, 1, lastSeasonProductionDataStart - 1, lastSeasonProductionStaticHeaders.Length];
                dataArea03.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea03.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea03.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea03.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ExcelRange dataArea04 = productionPlanSheet.Cells[5, 1, productionPlanDataStart - 1, productionPlanStaticHeaders.Length];
                dataArea04.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea04.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea04.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea04.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ExcelRange dataArea05 = researchSheet.Cells[7, 1, researchDataStart - 1, researchSheetStaticHeader.Length];
                dataArea05.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea05.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea05.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea05.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ExcelRange dataArea06 = researchSheet.Cells["P6:T6"];
                dataArea06.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea06.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea06.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea06.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ExcelRange dataArea07 = researchSheet.Cells["N6:O6"];
                dataArea07.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea07.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea07.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea07.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

               

                ExcelRange dataArea08 = researchSheet.Cells["H6:M6"];
                dataArea08.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea08.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea08.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea08.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;



                farmerSheet.Cells.AutoFitColumns();
                farmerMemberSheet.Cells.AutoFitColumns();
                lastSeasonProductionSheet.Cells.AutoFitColumns();
                researchSheet.Cells.AutoFitColumns();
                productionPlanSheet.Cells.AutoFitColumns();
                reportBytes = pck.GetAsByteArray();


            }
            string fileName = @"Farmer_Report.xlsx";
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(reportBytes, fileType, fileName);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting farmer Report", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("dispatch/excel"), AllowAnonymous]
    public async Task<IActionResult> GetDispatchExcelReport()
    {
        try
        {
            IEnumerable<DispatchOverviewDto> dispatchOverviewDtos = await _dispatchService.GetAllDispatch();
            byte[] reportBytes;
            
            using (var pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Dispatch_report");
                string[] staticHeaders = new string[] { "S.No", "Customer", "Contact Number", "Email", "Grain Cycle", "Grain", "Unit Price (NPR)", "Quantity (kg)", "Total Price (NPR)" };

                //ToDO: Code Review
                var firstRow = ws.Cells[1,2];
                firstRow.Value = "Farm To Fork";
                firstRow.Style.Font.Bold = true;
                firstRow.Style.Font.Size = 20;
                firstRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var secondRow = ws.Cells[2,2];
                secondRow.Value = "Sale Report";
                secondRow.Style.Font.Bold = true;
                secondRow.Style.Font.Size = 14;
                secondRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var thirdRow = ws.Cells[3,2];
                thirdRow.Value = $"Generated on: {DateTime.UtcNow.AddMinutes(345):MMM dd, yyyy | hh:mm tt}";
                thirdRow.Style.Font.Size = 12;
                thirdRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                const int headingPrintStart = 5;
                for (var i = 0; i < staticHeaders.Length; i++)
                {
                    var currentValue = (i + 1);
                    ws.Cells[headingPrintStart, currentValue].Value = staticHeaders[i];
                    ws.Cells[headingPrintStart, currentValue].Style.Font.Bold = true;

                    ws.Cells[headingPrintStart, currentValue].Style.HorizontalAlignment = currentValue is 2 or 4 or 5 or 6 ? ExcelHorizontalAlignment.Left : ExcelHorizontalAlignment.Center;
                }

                int dataRow = headingPrintStart + 1;
                foreach (var item in dispatchOverviewDtos.Select((x, i) => new { x, i }))
                {
                    ws.Cells[dataRow, 1].Value = item.i + 1;
                    ws.Cells[dataRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells[dataRow, 2].Value = item.x.CustomerGrainCycleCustomerName;
                    ws.Cells[dataRow, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    ws.Cells[dataRow, 3].Value = item.x.CustomerGrainCycleCustomerPhoneNumber;
                    ws.Cells[dataRow, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells[dataRow, 4].Value = item.x.CustomerGrainCycleCustomerEmail;
                    ws.Cells[dataRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    ws.Cells[dataRow, 5].Value = item.x.CustomerGrainCycleGrainCycleName;
                    ws.Cells[dataRow, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    ws.Cells[dataRow, 6].Value = item.x.CustomerGrainCycleGrainCycleGrainName;
                    ws.Cells[dataRow, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    ws.Cells[dataRow, 7].Value = item.x.UnitPrice;
                    ws.Cells[dataRow, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells[dataRow, 8].Value = item.x.Quantity;
                    ws.Cells[dataRow, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells[dataRow, 9].Value = item.x.Total;
                    ws.Cells[dataRow, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    dataRow++;
                }

                // For Total
                int totalRow = headingPrintStart + dispatchOverviewDtos.Count() + 1;

                ws.Cells[totalRow, 1, totalRow, 8].Merge = true;
                ws.Cells[totalRow, 1, totalRow,8].Style.Font.Bold = true;
                ws.Cells[totalRow, 1, totalRow,8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[totalRow, 1, totalRow,8].Value = "Total";

                ws.Cells[totalRow, 9, totalRow, 9].Style.Font.Bold = true;
                ws.Cells[totalRow, 9, totalRow, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[totalRow, 9, totalRow, 9].Value = dispatchOverviewDtos.Sum(x => x.Total);

                ExcelRange dataArea = ws.Cells[headingPrintStart, 1, headingPrintStart + dispatchOverviewDtos.Count() + 1, staticHeaders.Length];
                dataArea.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells.AutoFitColumns();
                ws.View.FreezePanes(6, 3);
                reportBytes = pck.GetAsByteArray();
            }

            string fileName = @"Sale_Report.xlsx";
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(reportBytes, fileType, fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception Occurred while getting dispatch excel report", ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet, Route("stock/excel"), AllowAnonymous]
    public async Task<IActionResult> GetStockExcelReport(int? grainCycleId)
    {
        try
        {
            IEnumerable<StockDto> stockDtos = await _stockService.GetAllStock(grainCycleId ?? 0);
            byte[] reportBytes;

            using (var pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Stock_report");
                string[] staticHeaders = new string[] { "S.No", "Grain", "Grain Cycle", "Quantity (kg)", "Remaining Quantity (kg)" };

                //TODO: Code Review
                var firstRow = ws.Cells[1, 2];
                firstRow.Value = "Farm To Fork";
                firstRow.Style.Font.Bold = true;
                firstRow.Style.Font.Size = 20;
                firstRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var secondRow = ws.Cells[2, 2];
                secondRow.Value = "Stock Report";
                secondRow.Style.Font.Bold = true;
                secondRow.Style.Font.Size = 14;
                secondRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var thirdRow = ws.Cells[3, 2];
                thirdRow.Merge = true;
                thirdRow.Value = $"Generated on: {DateTime.UtcNow.AddMinutes(345):MMM dd, yyyy | hh:mm tt}";
                thirdRow.Style.Font.Size = 12;
                thirdRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                const int headingPrintStart = 5;
                for (var i = 0; i < staticHeaders.Length; i++)
                {
                    var currentValue = (i + 1);
                    ws.Cells[headingPrintStart, currentValue].Value = staticHeaders[i];
                    ws.Cells[headingPrintStart, currentValue].Style.Font.Bold = true;

                    ws.Cells[headingPrintStart, currentValue].Style.HorizontalAlignment = currentValue is 2 or 3 ? ExcelHorizontalAlignment.Left : ExcelHorizontalAlignment.Center;
                }

                int dataRow = headingPrintStart + 1;
                foreach (var item in stockDtos.Select((x, i) => new { x, i }))
                {
                    ws.Cells[dataRow, 1].Value = item.i + 1;
                    ws.Cells[dataRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells[dataRow, 2].Value = item.x.GrainCycleGrainName;
                    ws.Cells[dataRow, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    ws.Cells[dataRow, 3].Value = item.x.GrainCycleName;
                    ws.Cells[dataRow, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    ws.Cells[dataRow, 4].Value = item.x.Quantity;
                    ws.Cells[dataRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells[dataRow, 5].Value = item.x.RemainingQuantity;
                    ws.Cells[dataRow, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    dataRow++;
                }

                ExcelRange dataArea = ws.Cells[headingPrintStart, 1, dataRow - 1, staticHeaders.Length];
                dataArea.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells.AutoFitColumns();
                ws.View.FreezePanes(6, 3);
                reportBytes = pck.GetAsByteArray();
            }

            string fileName = @"Stock_Report.xlsx";
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(reportBytes, fileType, fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception Occurred while getting stock excel report", ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet, Route("receipt-details"), AllowAnonymous]
    public async Task<IActionResult> GetAllReceipt()
    {
        try
        {
            IEnumerable<ReceiptDto> receiptDtos = await _receiptService.Getall();
            return Ok(receiptDtos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("buyer-report"), AllowAnonymous]
    public async Task<IActionResult> GetBuyerReport()
    {
        try
        {
            IEnumerable<CustomerGrainCycleReportDto> customerGrainCycles = await _customerService.GetBuyerReportData();
            byte[] reportBytes;

            using (var pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Buyer_report");
                string[] staticHeaders = new string[] { "S.No", "Customer Name", "Phone Number", "Email", "Grain Cycle", "Grain", "Status", "Estimated Quantity", "Estimated Price", "Created Date" };

                var firstRow = ws.Cells[1, 1, 1, staticHeaders.Length];
                firstRow.Merge = true;
                firstRow.Value = "Farm To Fork";
                firstRow.Style.Font.Bold = true;
                firstRow.Style.Font.Size = 20;
                firstRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var secondRow = ws.Cells[2, 1, 2, staticHeaders.Length];
                secondRow.Merge = true;
                secondRow.Value = "Buyer Report";
                secondRow.Style.Font.Bold = true;
                secondRow.Style.Font.Size = 14;
                secondRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var thirdRow = ws.Cells[3, 1, 3, staticHeaders.Length];
                thirdRow.Merge = true;
                thirdRow.Value = $"Generated on: {DateTime.UtcNow.AddMinutes(345):MMM dd, yyyy | hh:mm tt}";
                thirdRow.Style.Font.Size = 12;
                thirdRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                const int headingPrintStart = 5;
                for (var i = 0; i < staticHeaders.Length; i++)
                {
                    var currentValue = (i + 1);
                    ws.Cells[headingPrintStart, currentValue].Value = staticHeaders[i];
                    ws.Cells[headingPrintStart, currentValue].Style.Font.Bold = true;

                    ws.Cells[headingPrintStart, currentValue].Style.HorizontalAlignment = currentValue is 1 or 3 or 10 ? ExcelHorizontalAlignment.Center : ExcelHorizontalAlignment.Left;
                }

                int dataRow = headingPrintStart + 1;
                foreach (var item in customerGrainCycles.Select((x, i) => new { x, i }))
                {
                    ws.Cells[dataRow, 1].Value = item.i + 1;
                    ws.Cells[dataRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[dataRow, 2].Value = item.x.Customer.Name;
                    ws.Cells[dataRow, 3].Value = item.x.Customer.PhoneNumber;
                    ws.Cells[dataRow, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[dataRow, 4].Value = item.x.Customer.Email;
                    ws.Cells[dataRow, 5].Value = item.x.GrainCycle.Name;
                    ws.Cells[dataRow, 6].Value = item.x.GrainCycle.Grain.Name;
                    ws.Cells[dataRow, 1, dataRow, staticHeaders.Length].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[dataRow, 1, dataRow, staticHeaders.Length].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#F8CBAD"));
                    foreach (var status in item.x.CustomerGrainCycleStatuses)
                    {
                        ws.Cells[++dataRow, 7].Value = status.Status.Name;
                        ws.Cells[dataRow, 8].Value = status.Quantity;
                        ws.Cells[dataRow, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left; 
                        ws.Cells[dataRow, 9].Value = status.Price;
                        ws.Cells[dataRow, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        ws.Cells[dataRow, 10].Value = status.CreatedDate.ToString("yyyy-MM-dd");
                        ws.Cells[dataRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    }
                    dataRow ++;
                }

                ExcelRange dataArea = ws.Cells[headingPrintStart, 1, dataRow - 1, staticHeaders.Length];
                dataArea.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataArea.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataArea.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataArea.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells.AutoFitColumns();
                reportBytes = pck.GetAsByteArray();
            }

            string fileName = @"Buyer_Report.xlsx";
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(reportBytes, fileType, fileName);

        }
        catch(Exception e)
        {
            _logger.LogError("Exception {e}  occurred while getting buyer report " , e);
            return BadRequest(e.Message);
        }
    }
}

