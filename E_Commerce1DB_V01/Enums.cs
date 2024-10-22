using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01
{
    public enum Role
    {
        None = 0,
        User,
        Admin
    }
    public enum SortOptions
    {
        None,
        name,
        price,
        priceAsc,
        priceDesc
    }
    public enum Currency
    {
        USD,
        EGP,
        SAR
    }
    public enum OrderStatus
    {
        None = 0,
        PendingPayment,
        PendingDelivery,
        Delivered
    }
}
