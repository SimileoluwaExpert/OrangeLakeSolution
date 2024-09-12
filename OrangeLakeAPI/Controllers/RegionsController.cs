using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrangeLakeAPI.Data;
using OrangeLakeAPI.Models.Domains;
using OrangeLakeAPI.Models.DTO;
using OrangeLakeAPI.Repository;

namespace OrangeLakeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly OrangeLakeDbContext dbContext;
        private readonly  IRegionRepository regionRepository;

        public RegionsController(OrangeLakeDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain Models
            var regionDomainModel = await regionRepository.GetAllAysnc();

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
        public async Task<IActionResult> GetAllById([FromRoute] Guid id)
        {
            //Get Data From Database - Domain Models
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
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

        //POST to create New Region
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                code = addRegionRequestDto.code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };


            // Use Domian Model to create Region
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            //Map Domain Model back yo DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                code = regionDomainModel.code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetAllById), new {id = regionDomainModel.Id}, regionDto);
        }

        //Update 
        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            //Check if region exists
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null) {
                return NotFound();
                    }
            //Map DTO to Domain Model
            regionDomainModel.code = updateRegionDto.code;
            regionDomainModel.Name = updateRegionDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                code = regionDomainModel.code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(updateRegionDto);
        }

        //Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
            return NotFound(); 
            };

            //Delete region
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            //Return Deleted Region
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                code = regionDomainModel.code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

    }
}
