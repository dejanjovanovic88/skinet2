using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        public ProductRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
        {
            return await context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Products.Include(x => x.ProductBrand)
            .Include(x => x.ProductType)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await context.Products
            .Include(x => x.ProductBrand)
            .Include(x => x.ProductType)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypes()
        {
            return await context.ProductTypes.ToListAsync();

        }
    }
}