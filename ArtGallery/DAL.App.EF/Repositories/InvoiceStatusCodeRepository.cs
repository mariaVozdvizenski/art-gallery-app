using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvoiceStatusCodeRepository : EFBaseRepository<InvoiceStatusCode, AppDbContext>, IInvoiceStatusCodeRepository
    {
        public InvoiceStatusCodeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}