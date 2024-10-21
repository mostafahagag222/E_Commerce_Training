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
            services.AddValidatorsFromAssemblyContaining<GetProductsPayload>();
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
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<Lazy<ICartItemRepository>>(provider => new Lazy<ICartItemRepository>(() => provider.GetRequiredService<ICartItemRepository>()));
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<Lazy<ICartRepository>>(provider => new Lazy<ICartRepository>(() => provider.GetRequiredService<ICartRepository>()));
            services.AddScoped<IShippingMethodRepository, ShippingMethodRepository>();
            services.AddScoped<Lazy<IShippingMethodRepository>>(provider => new Lazy<IShippingMethodRepository>(() => provider.GetRequiredService<IShippingMethodRepository>()));

            //unit of work DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services DI
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IOrderService, OrderService>();

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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A7A123A7tin2223A7a3jsadfbfsdhbjidsakjdsankasdjbkjafbfakjsbjkasdbjklasdfbkajskd"))
                    };
                });

            return services;
        }
    }
}
