using E_Commerce2Business_V01.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce3APIs_V01.Controllers
{
    public class ProductsEndPoints : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet("Brands", GetBrands)
                .WithOpenApi();
        }
        public async Task<IResult> GetBrands(IBrandService _brandService) => Results.Ok(await _brandService.GetBrandsDTOAsync());
    }
}
