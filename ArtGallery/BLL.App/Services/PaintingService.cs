using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.mavozd.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using ee.itcollege.mavozd.Contracts.BLL.Base.Mappers;
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

        public IEnumerable<BLLPaintingView> FilterByConditionView(string condition, IEnumerable<BLLPaintingView> query)
        {
            if (condition.Equals("descending"))
            {
                return query.OrderByDescending(e => e.Price);
            }
            return query.OrderBy(e => e.Price);
        }

        public IEnumerable<BLLPaintingView> FilterByInStockView(IEnumerable<BLLPaintingView> query)
        {
            return query.Where(e => e.Quantity > 0);
        }

        public DTO.Painting DeletePaintingCategories(DTO.Painting painting)
        {
            if (painting.PaintingCategories != null && painting.PaintingCategories.Count > 0)
            {
                 painting.PaintingCategories!.Clear();
            }
            return  painting;
        }

        public IEnumerable<BLLPaintingView> FilterByCategory(IEnumerable<BLLPaintingView> query, string categoryNames)
        {
            var list = categoryNames.Split('_');
            return query.Where(p => p.PaintingCategories.Any(pc => list.Contains(pc.Category!.CategoryName)));
        }

        public IEnumerable<BLLPaintingView> ApplyFilters(IEnumerable<BLLPaintingView> query, string? condition, string? categoryNames, 
            bool? inStock)
        {
            if (categoryNames != null)
            {
                query =  FilterByCategory(query, categoryNames);
            }
            if (condition != null)
            {
                query = FilterByConditionView(condition, query);
            }
            if (inStock == true)
            {
                query =  FilterByInStockView(query);
            }
            return query;
        }
    }
}