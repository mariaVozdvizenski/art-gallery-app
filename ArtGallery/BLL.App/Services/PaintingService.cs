using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using PublicApi.DTO.v1;
using Painting = DAL.App.DTO.Painting;

namespace BLL.App.Services
{
    public class PaintingService : 
        BaseEntityService<IAppUnitOfWork, IPaintingRepository, IPaintingServiceMapper, Painting, DTO.Painting>, IPaintingService
    {
        public PaintingService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, unitOfWork.Paintings, new PaintingServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLPaintingView>> GetAllForViewAsync()
        {
            var query = await Repository.GetAllForViewAsync();
            var result = query.Select(e => Mapper.MapPaintingView(e));
            return result;
        }

        public async Task<BLLPaintingView> GetFirstOrDefaultForViewAsync(Guid id, Guid? userId = null)
        {
            var dalPaintingView = await Repository.GetFirstOrDefaultForViewAsync(id, userId);
            return Mapper.MapPaintingView(dalPaintingView);
        }

        public async Task<IEnumerable<BLLPaintingView>> FilterByConditionViewAsync(string condition, IEnumerable<BLLPaintingView> query)
        {
            if (condition.Equals("descending"))
            {
                return query.OrderByDescending(e => e.Price);
            }
            return query.OrderBy(e => e.Price);
        }

        public async Task<IEnumerable<BLLPaintingView>> FilterByInStockViewAsync(IEnumerable<BLLPaintingView> query)
        {
            return query.Where(e => e.Quantity > 0);
        }

        public async Task<DTO.Painting> DeletePaintingCategories(DTO.Painting painting)
        {
            if (painting.PaintingCategories != null && painting.PaintingCategories.Count > 0)
            {
                 painting.PaintingCategories!.Clear();
            }
            return  painting;
        }

        public async Task<IEnumerable<BLLPaintingView>> FilterByCategory(IEnumerable<BLLPaintingView> query, string categoryNames)
        {
            var list = categoryNames.Split('_');
            return query.Where(p => p.PaintingCategories.Any(pc => list.Contains(pc.Category!.CategoryName)));
        }

        public async Task<IEnumerable<BLLPaintingView>> ApplyFilters(IEnumerable<BLLPaintingView> query, string? condition, string? categoryNames, 
            bool? inStock)
        {
            if (categoryNames != null)
            {
                query = await FilterByCategory(query, categoryNames);
            }
            if (condition != null)
            {
                query = await FilterByConditionViewAsync(condition, query);
            }
            if (inStock == true)
            {
                query = await FilterByInStockViewAsync(query);
            }
            return query;
        }
    }
}