namespace FarmToFork.Business.Services;

public interface IReceiptService
{
    Task<IEnumerable<ReceiptDto>> Getall();
}

public class ReceiptSercice : IReceiptService
{
    private readonly IReceiptRepository _receiptRepository;

    public ReceiptSercice(IReceiptRepository receiptRepository)
    {
        _receiptRepository = receiptRepository;
    }

    public async Task<IEnumerable<ReceiptDto>> Getall()
    {
        IEnumerable<ReceiptDetail> receipts = await _receiptRepository.GetAllReceiptDetail();
        IEnumerable<ReceiptDto> mappedReceipts = ObjectMapper.Mapper.Map<IEnumerable<ReceiptDto>>(receipts);
        return mappedReceipts;
    }
}