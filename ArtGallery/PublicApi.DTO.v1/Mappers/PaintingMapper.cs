using System.Linq;
using BLL.App.DTO;
using Contracts.DAL.Base.Mappers;

namespace PublicApi.DTO.v1.Mappers
{
    public class PaintingMapper : ApiBaseMapper<BLL.App.DTO.Painting, Painting>
    {
        public PaintingView MapPaintingView(BLLPaintingView inObject)
        {
            return Mapper.Map<BLLPaintingView, PaintingView>(inObject);
        }
    }
}