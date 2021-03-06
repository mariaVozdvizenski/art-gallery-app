﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.mavozd.DAL.Base.EF.Repositories;
using ee.itcollege.mavozd.DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvoiceStatusCodeRepository : EFBaseRepository<AppDbContext, AppUser, InvoiceStatusCode, DAL.App.DTO.InvoiceStatusCode>, IInvoiceStatusCodeRepository
    {
        public InvoiceStatusCodeRepository(AppDbContext dbContext) : base(dbContext, new InvoiceStatusCodeRepositoryMapper())
        {
        }
    }
}