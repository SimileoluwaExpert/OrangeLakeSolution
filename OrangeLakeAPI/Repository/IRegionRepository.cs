using OrangeLakeAPI.Models.Domains;

namespace OrangeLakeAPI.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAysnc();
    }
}
