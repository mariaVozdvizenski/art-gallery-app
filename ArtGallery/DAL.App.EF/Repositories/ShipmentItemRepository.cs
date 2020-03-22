using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ShipmentItemRepository : EFBaseRepository<ShipmentItem, AppDbContext>, IShipmentItemRepository
    {
        public ShipmentItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}