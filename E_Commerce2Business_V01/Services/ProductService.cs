using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Payloads;
using E_Commerce2Business_V01.Exceptions;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace E_Commerce2Business_V01.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RedisContext _redisContext;
        private readonly IConfiguration _configuration;

        public ProductService(IUnitOfWork unitOfWork, RedisContext redisContext, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _redisContext = redisContext;
            _configuration = configuration;
        }

        public async Task<PaginationDTO<GetProductsDTO>> GetProductsPageAsync(GetProductsPagePayload payload)
        {
            if (payload.BrandID != null && !await _unitOfWork.BrandRepository.CheckExistenceByIDAsync(payload.BrandID))
                throw new NotFoundException("invalid brand id");
            if (payload.TypeID != null && !await _unitOfWork.TypeRepository.CheckExistenceByIDAsync(payload.TypeID))
                throw new NotFoundException("invalid type id");
            var redisKey = JsonConvert.SerializeObject(payload,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            if (await _redisContext.Database.KeyExistsAsync(redisKey))
            {
                var redisResult = await _redisContext.Database.StringGetAsync(redisKey);
                return JsonConvert.DeserializeObject<PaginationDTO<GetProductsDTO>>(redisResult);
            }

            var pageResult = await _unitOfWork.ProductRepository.GetProductsPageAsync(payload);
            var redisValue = JsonConvert.SerializeObject(pageResult);
            await _redisContext.Database.StringSetAsync(redisKey, redisValue);
            return pageResult;
        }

        public async Task AddNewProductAsync(AddProductPayload payload)
        {
            var product = new Product()
            {
                Name = payload.Name,
                Price = payload.Price,
                TypeId = payload.TypeId,
                UnitsInStock = payload.UnitsInStock,
                Description = payload.Description,
                ImageUrl = await UploadImageAsync(payload.Image),
                BrandId = payload.BrandId,
            };
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            await _redisContext.Database.ExecuteAsync("flushdb");
        }

        private async Task<string> UploadImageAsync(IFormFile image)
        {
            var cloudinaryUrl = _configuration["CLOUDINARY_URL"];
            if (string.IsNullOrEmpty(cloudinaryUrl))
            {
                throw new InvalidOperationException("CLOUDINARY_URL environment variable is not set.");
            }

            var cloudinary = new Cloudinary(cloudinaryUrl)
            {
                Api = { Secure = true }
            };

            try
            {
                await using var stream = image.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, stream)
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                return uploadResult.Url.ToString(); // Image URL
            }
            catch (Exception ex)
            {
                // Handle exception (log it, throw it, etc.)
                throw new ApplicationException("An error occurred while uploading the image.", ex);
            }
        }
    }
}