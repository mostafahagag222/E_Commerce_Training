using E_Commerce1DB_V01;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories;
using E_Commerce1DB_V01.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace E_Commerce1DB_V01
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECPContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IBrandRepository> _brandRepository;
        private readonly Lazy<ITypeRepository> _typeRepository;
        private readonly Lazy<IBasketRepository> _basketRepository;
        private readonly Lazy<IBasketItemRepository> _basketItemRepository;
        private readonly Lazy<IShippingMethodRepository> _shippingMethodRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly Lazy<IOrderItemRepository> _orderItemRepository;
        private readonly Lazy<IPaymentRepository> _paymentRepository;
        private readonly Lazy<IPaymentLogRepository> _paymentLogRepository;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(ECPContext context,
                          Lazy<IUserRepository> userRepository,
                          Lazy<IProductRepository> productRepository,
                          Lazy<IBrandRepository> brandRepository,
                          Lazy<ITypeRepository> typeRepository,
                          Lazy<IBasketRepository> basketRepository,
                          Lazy<IBasketItemRepository> basketItemRepository,
                          Lazy<IShippingMethodRepository> shippingMethodRepository,
                          Lazy<IOrderItemRepository> orderItemRepository,
                          Lazy<IOrderRepository> orderRepository,
                          Lazy<IPaymentRepository> paymentRepository,
                          Lazy<IPaymentLogRepository> paymentLogRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _shippingMethodRepository = shippingMethodRepository;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _paymentLogRepository = paymentLogRepository;
        }
        public IUserRepository UserRepository => _userRepository.Value;
        public IProductRepository ProductRepository => _productRepository.Value;
        public IBrandRepository BrandRepository => _brandRepository.Value;
        public ITypeRepository TypeRepository => _typeRepository.Value;
        public IBasketRepository BasketRepository => _basketRepository.Value;
        public IBasketItemRepository BasketItemRepository => _basketItemRepository.Value;
        public IOrderRepository OrderRepository => _orderRepository.Value;
        public IOrderItemRepository OrderItemRepository => _orderItemRepository.Value;
        public IShippingMethodRepository ShippingMethodRepository => _shippingMethodRepository.Value;
        public IPaymentRepository PaymentRepository => _paymentRepository.Value;
        public IPaymentLogRepository PaymentLogRepository => _paymentLogRepository.Value;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync() //create a transaction and begin it
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }
        //save all changes made and if any error occurred during saving updates in any transaction it will automatically roll back all changes and then dispose the transaction
        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
        //roll back all changes made to data base during transaction lifetime then dispose transaction
        public async Task RollbackTransactionAsync()
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.RollbackAsync();
                }
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }
        public void Dispose() //dispose connection
        {
            _context.Dispose();
        }
    }
}
