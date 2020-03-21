using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ShipmentItemRepository : BaseRepository<ShipmentItem>, IShipmentItemRepository
    {
        public ShipmentItemRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}