using E_Commerce2Business_V01.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface ICartItemService
    {
        Task SyncCartItemsAsync(string cartId, List<ProductPayload> productPayloads);
    }
}
