using System.Linq;
using Contracts.DAL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;

namespace PublicApi.DTO.v1.Mappers
{
    public class ArtistMapper : ApiBaseMapper<BLLAppDTO.Artist, Artist>
    {
        public ArtistView MapForViewAsync(BLLAppDTO.Artist inObject)
        {
            return new ArtistView()
            {
                Bio = inObject.Bio,
                Country = inObject.Country,
                DateOfBirth = inObject.DateOfBirth,
                FirstName = inObject.FirstName,
                Id = inObject.Id,
                LastName = inObject.LastName,
                PlaceOfBirth = inObject.PlaceOfBirth,
                Paintings = inObject.Paintings.Select(e => new Painting()
                {
                    Id = e.Id,
                    Title = e.Title,
                    ArtistId = e.ArtistId,
                    Description = e.Description,
                    Price = e.Price,
                    Size = e.Size,
                    Quantity = e.Quantity,
                    ImageName = e.ImageName
                }).ToList()
            };
        }
    }
}