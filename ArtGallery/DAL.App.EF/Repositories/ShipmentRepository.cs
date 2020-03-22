using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ShipmentRepository : EFBaseRepository<Shipment, AppDbContext>, IShipmentRepository
    {
        public ShipmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}