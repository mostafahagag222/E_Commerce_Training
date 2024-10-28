using E_Commerce1DB_V01.DTOs;

namespace E_Commerce2Business_V01.Services
{
    public class GetPaymentAmountDTO
    {
        public decimal ShippingPrice { get; set; }
        public List<GetItemPriceDetailsDTO> CartItemsWithProductPrices { get; set; }
    }
}