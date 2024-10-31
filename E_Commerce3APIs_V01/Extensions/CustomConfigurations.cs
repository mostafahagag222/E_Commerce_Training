using E_Commerce1DB_V01;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;
using FluentValidation;
using E_Commerce1DB_V01.Payloads;
using E_Commerce1DB_V01.Repositories;
using E_Commerce2Business_V01.Interfaces;
using E_Commerce2Business_V01.Services;
using System;
using E_Commerce1DB_V01.Entities;
using E_Commerce2Business_V01.Payloads;
using E_Commerce1DB_V01.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce3APIs_V01
{
    public static class CustomConfigurations
    {
        public static IServiceCollection AddCustomConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ECPContext>(options => options.UseSqlServer(cs));

            //add fluent validation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<RegistrationPayload>();
            services.AddValidatorsFromAssemblyContaining<GetProductsPagePayload>();
            services.AddControllers(o => { o.Filters.Add<ValidationFilter>(); });

            //repositories DI
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<Lazy<IUserRepository>>(provider => new Lazy<IUserRepository>(() => provider.GetRequiredService<IUserRepository>()));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<Lazy<IProductRepository>>(provider => new Lazy<IProductRepository>(() => provider.GetRequiredService<IProductRepository>()));
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<Lazy<IBrandRepository>>(provider => new Lazy<IBrandRepository>(() => provider.GetRequiredService<IBrandRepository>()));
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<Lazy<ITypeRepository>>(provider => new Lazy<ITypeRepository>(() => provider.GetRequiredService<ITypeRepository>()));
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<Lazy<IBasketItemRepository>>(provider => new Lazy<IBasketItemRepository>(() => provider.GetRequiredService<IBasketItemRepository>()));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<Lazy<IBasketRepository>>(provider => new Lazy<IBasketRepository>(() => provider.GetRequiredService<IBasketRepository>()));
            services.AddScoped<IShippingMethodRepository, ShippingMethodRepository>();
            services.AddScoped<Lazy<IShippingMethodRepository>>(provider => new Lazy<IShippingMethodRepository>(() => provider.GetRequiredService<IShippingMethodRepository>()));
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<Lazy<IOrderItemRepository>>(provider => new Lazy<IOrderItemRepository>(() => provider.GetRequiredService<IOrderItemRepository>())); 
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<Lazy<IOrderRepository>>(provider => new Lazy<IOrderRepository>(() => provider.GetRequiredService<IOrderRepository>()));
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<Lazy<IPaymentRepository>>(provider => new Lazy<IPaymentRepository>(() => provider.GetRequiredService<IPaymentRepository>()));
            services.AddScoped<IPaymentLogRepository, PaymentLogRepository>();
            services.AddScoped<Lazy<IPaymentLogRepository>>(provider => new Lazy<IPaymentLogRepository>(() => provider.GetRequiredService<IPaymentLogRepository>()));

            //unit of work DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services DI
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketItemService, BasketItemService>();
            services.AddScoped<IShippingMethodService, ShippingMethodService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IBrandService, BrandService>();

            //payment request configurations

            //cors policy
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            //add Authorization JWT
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                    };
                });




            return services;
        }
    }
}
