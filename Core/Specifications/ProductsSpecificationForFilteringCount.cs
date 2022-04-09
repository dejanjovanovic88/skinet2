using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsSpecificationForFilteringCount : BaseSpecification<Product>
    {
        public ProductsSpecificationForFilteringCount(ProductParams productParams) : base(x =>
        ((!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
        && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        && (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))))
        {

        }
    }
}