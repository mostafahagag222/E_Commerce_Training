using E_Commerce1DB_V01;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories;
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
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly Lazy<ICartItemRepository> _cartItemRepository;
        private readonly Lazy<IShippingMethodRepository> _shippingMethodRepository;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(ECPContext context,
                          Lazy<IUserRepository> userRepository,
                          Lazy<IProductRepository> productRepository,
                          Lazy<IBrandRepository> brandRepository,
                          Lazy<ITypeRepository> typeRepository,
                          Lazy<ICartRepository> cartRepository,
                          Lazy<ICartItemRepository> cartItemRepository,
                          Lazy<IShippingMethodRepository> shippingMethodRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _shippingMethodRepository = shippingMethodRepository;
        }
        public IUserRepository UserRepository => _userRepository.Value;
        public IProductRepository ProductRepository => _productRepository.Value;
        public IBrandRepository BrandRepository => _brandRepository.Value;
        public ITypeRepository TypeRepository => _typeRepository.Value;
        public ICartRepository CartRepository => _cartRepository.Value;
        public ICartItemRepository CartItemRepository => _cartItemRepository.Value;
        public IShippingMethodRepository ShippingMethodRepository => _shippingMethodRepository.Value;

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
