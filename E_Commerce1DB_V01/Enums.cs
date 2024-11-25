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
        PendingDelivery,
        Delivered
    }
    public enum PaymentStatus
    {
        Pending = 0,
        SuccessfulPayment,
        FailedPayment
    }

    public enum TokenType
    {
        EmailVerification,
        
    }
}
