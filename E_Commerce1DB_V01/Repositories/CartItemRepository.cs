using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories.Interfaces;

namespace E_Commerce1DB_V01.Repositories
{
    public class BasketItemRepository : GenericRepository<BasketItem>, IBasketItemRepository
    {
        private readonly ECPContext context;
        public BasketItemRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }

        public void DeleteRange(List<BasketItem> basketItemsToRemove)
        {
            context.RemoveRange(basketItemsToRemove);
        }

        public async Task DeleteRangeByBasketId(string id)
        {
            var basketItems = await context.BasketItems.Where(ci => ci.BasketID == id).ToListAsync();
            context.BasketItems.RemoveRange(basketItems);
        }

        public async Task<BasketItem> GetBasketItemByBasketIdProductID(string basketId, int productId)
        {
            return await (from ci in context.BasketItems
                          where ci.BasketID == basketId
                          && ci.ProductID == productId
                          select ci).FirstOrDefaultAsync();
        }

        public async Task<QuantityUnitsInStockDTO> GetBasketItemQuantityAndUnitInStockAsync(string basketId, int basketItemId)
        {

            return await (from ci in context.BasketItems
                          where ci.BasketID == basketId
                          && ci.Id == basketItemId
                          join p in context.Products
                          on ci.ProductID equals p.Id
                          select new QuantityUnitsInStockDTO()
                          {
                              BasketItemQuantity = ci.Quantity,
                              ProdutcStockQuantity = p.UnitsInStock
                          }).FirstOrDefaultAsync();
        }

        public async Task<List<BasketItem>> GetBasketItemsFroSpecificBasket(string basketId)
        {
            return await (from ci in context.BasketItems
                          where ci.BasketID == basketId
                          select ci).ToListAsync();
        }

        public async Task<BasketDTO> GetBasketDTOAsync(string basketID)
        {
            //        var requiredBasket = await context.Baskets
            //.Where(c => c.Id == basketID)
            //.Include(c => c.ShippingMethod)
            //.Include(c => c.BasketItems)
            //.ThenInclude(ci => ci.Product)
            //.FirstOrDefaultAsync();

            //        if (requiredBasket == null)
            //        {
            //            throw new Exception("Basket not found.");
            //        }

            //        if (requiredBasket.ShippingMethod == null)
            //        {
            //            throw new Exception("Shipping method is null.");
            //        }

            //        if (requiredBasket.BasketItems.Any(ci => ci.Product == null))
            //        {
            //            throw new Exception("One or more Basket items have a null product.");
            //        }

            #region MyRegion
            //return await (from ci in context.BasketItems
            //              where ci.BasketID == basketID
            //              join p in context.Products
            //              on ci.ProductID equals p.Id
            //              select new ProductDTO()
            //              {
            //                  BrandId = p.BrandId,
            //                  Id = p.Id,
            //                  Description = p.Description,
            //                  Name = p.Name,
            //                  PictureUrl = p.ImageUrl,
            //                  Price = p.Price,
            //                  TypeId = p.TypeId,
            //                  UnitsInStock = p.UnitsInStock,
            //                  Quantity = ci.Quantity
            //              }).ToListAsync(); 
            #endregion
            var RequiredBasket = context.Baskets
                .Where(c => c.Id == basketID)
                .Include(c => c.BasketItems)
                .ThenInclude(ci => ci.Product);

            var RequiredBasket1 = await RequiredBasket
                .Select(c => new BasketDTO()
                {
                    Id = c.Id,
                    DeliveryMethodId = c.ShippingMethodID != null? int.Parse(c.ShippingMethodID) : 0,
                    ShippingPrice =c.ShippingMethod!=null ? c.ShippingMethod.Price :0 ,
                    Items = c.BasketItems.Select(c => new ProductDTO()
                    {
                        Id = c.Product.Id,
                        Price = c.Product.Price,
                        BrandId = c.Product.BrandId,
                        Description = c.Product.Description,
                        Name = c.Product.Name,
                        PictureUrl = c.Product.ImageUrl,
                        Quantity = c.Quantity,
                        TypeId = c.Product.TypeId,
                        UnitsInStock = c.Product.UnitsInStock
                    }).ToList()
                }).FirstOrDefaultAsync();
            return RequiredBasket1;
        }
        public async Task<List<CreateOrderItemDTO>> GetBasketItemsDTOAsync(string basketId)
        {
            var items = await (from ci in context.BasketItems
                               where ci.BasketID == basketId
                               select new CreateOrderItemDTO()
                               {
                                   Product = ci.Product,
                                   Quantity = ci.Quantity,
                                   TotalPrice = ci.Quantity * ci.Product.Price,
                                   Price = ci.Price
                               }).ToListAsync();
            return items;
        }
    }
}
