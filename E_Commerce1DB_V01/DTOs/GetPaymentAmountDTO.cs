namespace E_Commerce1DB_V01.DTOs
{
    public class GetPaymentAmountDTO
    {
        public decimal ShippingPrice { get; set; }
        public List<GetItemPriceDetailsDTO> BasketItemsWithProductPrices { get; set; }
    }
}