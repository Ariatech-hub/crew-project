namespace FarmToFork.Core.Repositories;

public interface IReceiptRepository
{
    Task<int> GetLatestReceipt();
    Task Add(Receipt receipt);

    Task<IEnumerable<ReceiptDetail>> GetAllReceiptDetail();



}