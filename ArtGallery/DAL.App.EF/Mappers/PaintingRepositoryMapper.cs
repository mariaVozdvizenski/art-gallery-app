using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Mappers;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;
using Painting = Domain.App.Painting;

namespace DAL.App.EF.Mappers
{
    public class PaintingRepositoryMapper : AppDALBaseMapper<Painting, DTO.Painting>
    {
        public PaintingRepositoryMapper()
        {
        }
    }
}