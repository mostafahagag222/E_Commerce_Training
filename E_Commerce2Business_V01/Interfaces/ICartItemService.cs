using E_Commerce2Business_V01.Payloads;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IBasketItemService
    {
        Task SyncBasketItemsAsync(string basketId, List<BasketItemPayload> productPayloads);
        void TestAsync();
    }
}
