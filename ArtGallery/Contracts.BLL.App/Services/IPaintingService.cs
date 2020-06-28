using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.mavozd.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IPaintingService : IBaseEntityService<Painting>, IPaintingRepositoryCustom<BLLPaintingView>
    {
        IEnumerable<BLLPaintingView> FilterByConditionView(string condition, IEnumerable<BLLPaintingView> query);
        IEnumerable<BLLPaintingView> FilterByInStockView(IEnumerable<BLLPaintingView> query);
        Painting DeletePaintingCategories(Painting painting);
        IEnumerable<BLLPaintingView> FilterByCategory(IEnumerable<BLLPaintingView> query,
            string categoryNames);
        IEnumerable<BLLPaintingView> ApplyFilters(IEnumerable<BLLPaintingView> query, string? condition,
            string? categoryNames,
            bool? inStock);
    }
}