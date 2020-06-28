using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IPaintingService : IBaseEntityService<Painting>, IPaintingRepositoryCustom<BLLPaintingView>
    {
        Task<IEnumerable<BLLPaintingView>> FilterByConditionViewAsync(string condition, IEnumerable<BLLPaintingView> query);
        Task<IEnumerable<BLLPaintingView>> FilterByInStockViewAsync(IEnumerable<BLLPaintingView> query);
        Task<Painting> DeletePaintingCategories(Painting painting);
        Task<IEnumerable<BLLPaintingView>> FilterByCategory(IEnumerable<BLLPaintingView> query,
            string categoryNames);
        Task<IEnumerable<BLLPaintingView>> ApplyFilters(IEnumerable<BLLPaintingView> query, string? condition,
            string? categoryNames,
            bool? inStock);
    }
}