using AutoMapper;
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
