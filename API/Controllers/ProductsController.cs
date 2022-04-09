using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly StoreContext _context;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
            _productBrandRepository = productBrandRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery] ProductParams productParams)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(productParams);
            var countSpec = new ProductsSpecificationForFilteringCount(productParams);
            var total = await _productRepository.CountAsync(countSpec);
            var res = await _productRepository.ListAsync(spec);

            var productsDto = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(res);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, total, productsDto));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductBrands()
        {
            var res = await _productBrandRepository.GetAllAsync();
            return Ok(res);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductTypes()
        {
            var res = await _productTypeRepository.GetAllAsync();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id);

            var res = await _productRepository.GetEntityWithSpecAsync(spec);
            var dto = _mapper.Map<ProductToReturnDto>(res);
            return Ok(dto);
        }

    }
}