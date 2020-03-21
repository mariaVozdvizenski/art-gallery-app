using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvoiceStatusCodeRepository : BaseRepository<InvoiceStatusCode>, IInvoiceStatusCodeRepository
    {
        public InvoiceStatusCodeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}