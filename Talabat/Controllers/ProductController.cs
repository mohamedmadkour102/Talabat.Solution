using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
<<<<<<< Updated upstream
=======
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecification;
using Talabat.DTO_s;
using Talabat.Errors;
using Talabat.Helpers;
>>>>>>> Stashed changes

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductController(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpGet]
<<<<<<< Updated upstream
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var product  = await _productRepo.GetAllAsync();
            return Ok(product);
=======
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecParams Params )
        {
            var spec = new ProductWithBrandAndCategorySpecifications(Params);
            var product  = await _productRepo.GetAllWithSpecAsync(spec);

            var MappedProduct = _mapper.Map<IReadOnlyList<Product> , IReadOnlyList<ProductDto>>(product);
            var countspec = new ProductWithFiltertionForCountAsync(Params);
            var count = await _productRepo.GetCountWithSpecAsync(countspec);

            //var ReturnObj = new Pagination<ProductDto>
            //{
            //    PageIndex = Params.PageIndex,

            //    PageSize = Params.PageSize,
            //    Data = MappedProduct
            //};
            //return Ok(ReturnObj);  easier way but need to make constructor with this variables 
            return Ok(new Pagination<ProductDto>(Params.PageIndex, Params.PageSize, MappedProduct , count));


>>>>>>> Stashed changes
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int id)
        {
            var product = await _productRepo.GetAsync(id);
            if (product == null)
            {
                return NotFound(new {Message = "Not Found" , StatusCode = 404 });
            }
<<<<<<< Updated upstream
            return Ok(product);
=======
            return Ok(_mapper.Map<Product , ProductDto>(product)); 
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
        {
            var brand = await _brandRepo.GetAllAsync();
            return Ok(brand);
        }
        [HttpGet("Category")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategory()
        {
            var brand = await _categoryRepo.GetAllAsync();
            return Ok(brand);
>>>>>>> Stashed changes
        }
    }
}
