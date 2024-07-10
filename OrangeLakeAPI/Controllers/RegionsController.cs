using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangeLakeAPI.Data;


using OrangeLakeAPI.Models.DTO;

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
            //Get Data From Database - Domain Models
            var regionDomainModel = dbContext.Regions.ToList();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionDomainModel)
            {
                regionsDto.Add(new RegionDto() 
                { 
                  Id = regionDomain.Id, 
                  Name = regionDomain.Name, 
                  code = regionDomain.code,
                  RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            //Return DTOs to Client
            return Ok(regionsDto);

        }


        //GET REGION BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetAllById([FromRoute] Guid id)
        {
            //Get Data From Database - Domain Models
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Models to DTOs
            var regionsDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                code = regionDomainModel.code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionsDto);
        }
    }
}
