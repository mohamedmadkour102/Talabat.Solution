using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecification;
using Talabat.DTO_s;
using Talabat.Errors;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo , IGenericRepository<ProductBrand> brandRepo ,

             IGenericRepository<ProductCategory> categoryRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();
            var product  = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Product> , IEnumerable<ProductDto>>(product));
        }
        // swagger edition fe 7alet reg3 el response tmam aw 7sal error 
        // hna 2oltlo en el return hykon mn no3 ProductDto talama el status code b 200
        [ProducesResponseType(typeof(ProductDto) , StatusCodes.Status200OK)]
        // fe 7alet error 7sal 
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepo.GetAsync(id);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product , ProductDto>(product)); 
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrand()
        {
            var brand = await _brandRepo.GetAllAsync();
            return Ok(brand);
        }
        [HttpGet("Category")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategory()
        {
            var brand = await _categoryRepo.GetAllAsync();
            return Ok(brand);
        }
    }
}
