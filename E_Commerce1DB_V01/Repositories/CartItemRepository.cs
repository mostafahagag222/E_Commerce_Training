using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Payloads;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        private readonly ECPContext context;
        public CartItemRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }

        public void DeleteRange(List<CartItem> cartItemsToRemove)
        {
            context.RemoveRange(cartItemsToRemove);
        }

        public async Task DeleteRangeByCartId(string id)
        {
            var cartItems = await context.CartItems.Where(ci => ci.CartID == id).ToListAsync();
            context.CartItems.RemoveRange(cartItems);
        }

        public async Task<CartItem> GetCartItemByCartIdProductID(string cartId, int productId)
        {
            return await (from ci in context.CartItems
                          where ci.CartID == cartId
                          && ci.ProductID == productId
                          select ci).FirstOrDefaultAsync();
        }

        public async Task<QuantityUnitsInStockDTO> GetCartItemQuantityAndUnitInStockAsync(string cartId, int cartItemId)
        {

            return await (from ci in context.CartItems
                          where ci.CartID == cartId
                          && ci.Id == cartItemId
                          join p in context.Products
                          on ci.ProductID equals p.Id
                          select new QuantityUnitsInStockDTO()
                          {
                              CartItemQuantity = ci.Quantity,
                              ProdutcStockQuantity = p.UnitsInStock
                          }).FirstOrDefaultAsync();
        }

        public async Task<List<CartItem>> GetCartItemsFroSpecificCart(string cartId)
        {
            return await (from ci in context.CartItems
                          where ci.CartID == cartId
                          select ci).ToListAsync();
        }

        public async Task<BasketDTO> GetBasketDTOAsync(string cartID)
        {
            //        var requiredBasket = await context.Carts
            //.Where(c => c.Id == cartID)
            //.Include(c => c.ShippingMethod)
            //.Include(c => c.CartItems)
            //.ThenInclude(ci => ci.Product)
            //.FirstOrDefaultAsync();

            //        if (requiredBasket == null)
            //        {
            //            throw new Exception("Cart not found.");
            //        }

            //        if (requiredBasket.ShippingMethod == null)
            //        {
            //            throw new Exception("Shipping method is null.");
            //        }

            //        if (requiredBasket.CartItems.Any(ci => ci.Product == null))
            //        {
            //            throw new Exception("One or more cart items have a null product.");
            //        }

            #region MyRegion
            //return await (from ci in context.CartItems
            //              where ci.CartID == cartID
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
            var RequiredBasket = context.Carts
                .Where(c => c.Id == cartID)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product);

            var RequiredBasket1 = await RequiredBasket
                .Select(c => new BasketDTO()
                {
                    Id = c.Id,
                    DeliveryMethodId = c.ShippingMethodID != null? int.Parse(c.ShippingMethodID) : 0,
                    ShippingPrice =c.ShippingMethod!=null ? c.ShippingMethod.Price :0 ,
                    Items = c.CartItems.Select(c => new ProductDTO()
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
        public async Task<List<CreateOrderItemDTO>> GetCartItemsDTOAsync(string basketId)
        {
            var items = await (from ci in context.CartItems
                               where ci.CartID == basketId
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
