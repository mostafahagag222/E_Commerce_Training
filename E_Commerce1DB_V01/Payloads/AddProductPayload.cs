using Microsoft.AspNetCore.Http;

namespace E_Commerce1DB_V01.Payloads;

public class AddProductPayload
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile Image { get; set; }
    public int TypeId { get; set; }
    public int BrandId { get; set; }
    public int UnitsInStock {  get; set; }
}