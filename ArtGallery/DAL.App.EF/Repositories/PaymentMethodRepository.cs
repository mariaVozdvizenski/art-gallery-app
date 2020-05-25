using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaymentMethodRepository : EFBaseRepository<AppDbContext, AppUser, PaymentMethod, DTO.PaymentMethod>, 
        IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<PaymentMethod, DTO.PaymentMethod>())
        {
        }
        
    }
}