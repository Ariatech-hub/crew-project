using FarmToFork.Business.DTOs;
using FarmToFork.Core.Exception;
using FarmToFork.Core.Repositories;
using Newtonsoft.Json;
using QRCoder;

namespace FarmToFork.Application.Controllers
{
    public class ViewController : Controller
    {
        private readonly IFarmerService _farmerService;
        private readonly ICardImageService _cardImageService;
        private readonly ICustomerService _customerService;
        public ViewController(IFarmerService farmerSevice, ICardImageService cardImageService, ICustomerService customerService)
        {
            _farmerService = farmerSevice;
            _cardImageService = cardImageService;
            _customerService = customerService;
        }

        [HttpPost, Route("api/generate-card")]
        public async Task<IActionResult> GenerateCard(FarmerCardDto farmerCard)
        {
            try
            {
                FarmerCardView farmerCardView = new()
                {
                    IsCardFlipped = farmerCard.IsCardFlipped
                };

                List<FarmerReportRelated> farmers = new List<FarmerReportRelated>();
                int[] selectedFarmers = JsonConvert.DeserializeObject<int[]>(farmerCard.FarmerIds);

                foreach (var farmer in selectedFarmers)
                {
                    var selectedFarmer = await _farmerService.GetFarmerDetailsById(farmer);
                    farmers.Add(new FarmerReportRelated()
                    {
                        FullName = selectedFarmer.FullName,
                        GenderNepaliName = selectedFarmer.GenderNepaliName,
                        ProvinceNepaliName = selectedFarmer.ProvinceNepaliName,
                        DistrictNepaliName = selectedFarmer.DistrictNepaliName,
                        PalikaNepaliName = selectedFarmer.PalikaNepaliName,
                        CitizenshipNumber = selectedFarmer.CitizenshipNumber,
                        MobileNumber = selectedFarmer.MobileNumber,
                        DateOfBirth = selectedFarmer.DateOfBirth,
                        CommunityNepaliName = selectedFarmer.CommunityNepaliName,
                        Ward = selectedFarmer.Ward,
                        PhotoName = !string.IsNullOrEmpty(selectedFarmer.PhotoName) ? $"{Request.Scheme}://{Request.Host}/Images/Farmer/{selectedFarmer.PhotoName}" : $"{Request.Scheme}://{Request.Host}/Images/Farmer/no-image.png",
                        FarmerCode = selectedFarmer.FarmerCode,
                    });

                    var image = await _cardImageService.GetCardImageById(farmerCard.Id);
                    farmerCardView.ImageName = image.Name;
                    farmerCardView.ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/{image.FileName}";
                }
                farmerCardView.Farmers = farmers;

                foreach (var item in farmerCardView.Farmers)
                {
                    if (!string.IsNullOrEmpty(item.FarmerCode))
                    {
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(item.FarmerCode, QRCodeGenerator.ECCLevel.Q);
                        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                        byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
                        item.QRCode = Convert.ToBase64String(qrCodeAsPngByteArr);
                    }
                }
                return View("GenerateCard", farmerCardView);

            }
            catch (Exception e)
            {
                throw new GeneralException(e.Message);
            }
        }
    
        [HttpGet, Route("api/generate-sales-receipt/pdf/{id}/status/{statusId}")]
        public IActionResult GenerateReceiptPdf(int id, int statusId)
        {
            try
            {
                var url = $"{Request.Scheme}://{Request.Host}/api/generate-sales-receipt/{id}/status/{statusId}";
                return new PdfAsView(url);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("api/generate-sales-receipt/{id}/status/{statusId}")]
        public async Task<IActionResult> GenerateReceipt(int id, int statusId)
        {
            try
            {
                BuyerReceiptDto buyerReceiptDto = await _customerService.GetCustomerGrainCycleById(id, statusId);
                return View(buyerReceiptDto);
            }
            catch(Exception e)
            {
                throw new GeneralException(e.Message);
            }
        }

    }
}
