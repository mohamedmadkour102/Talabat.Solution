using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Talabat.Errors;
using Talabat.Repository.Data;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext )
        {
            _dbContext = dbContext;
        }
       
        [HttpGet("NotFound")] //Get : api/BuggyController/NotFound
        public ActionResult GetNotFoundRequest() 
        {
            var notexistedproduct = _dbContext.products.Find(100);
            if (notexistedproduct == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(notexistedproduct);

        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError() 
        {
            var notexistedproduct = _dbContext.products.Find(100);
            var productdto = notexistedproduct.ToString(); // it will through null ref exce
            return Ok(productdto);

        }

        [HttpGet("UnAuthorized")]
        public ActionResult GetUnAuthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }


        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest() { return BadRequest(new ApiResponse (400)); }
        
        [HttpGet("BadRequest/{id}")]

        public ActionResult GetBadRequest(int id) { return Ok(); }



    }
}
