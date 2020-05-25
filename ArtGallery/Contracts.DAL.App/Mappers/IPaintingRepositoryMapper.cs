using System.Threading.Tasks;
using DAL.App.DTO;
using Painting = Domain.App.Painting;

namespace Contracts.DAL.App.Mappers
{
    public interface IPaintingRepositoryMapper : IPaintingRepositoryMapper<DALPaintingView>
    {
    }

    public interface IPaintingRepositoryMapper<TPaintingView>
    {
        Task<TPaintingView> MapForViewAsync(Painting inObject);
    }
}