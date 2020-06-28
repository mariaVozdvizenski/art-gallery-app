using Contracts.BLL.App.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ArtistServiceMapper : AppServiceBaseMapper<Artist, DTO.Artist>, IArtistServiceMapper
    {
        public ArtistServiceMapper()
        {
        }
    }
}