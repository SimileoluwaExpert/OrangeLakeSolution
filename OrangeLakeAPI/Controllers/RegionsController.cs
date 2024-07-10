using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangeLakeAPI.Data;

namespace OrangeLakeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly OrangeLakeDbContext dbContext;

        public RegionsController(OrangeLakeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();

            return Ok(regions);

        }


        //GET REGION BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetAllById([FromRoute] Guid id)
        {
            var regions = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regions == null)
            {
                return NotFound();
            }
            return Ok(regions);
        }
    }
}
