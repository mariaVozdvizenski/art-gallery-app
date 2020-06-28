using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using Category = Domain.App.Category;

namespace DAL.App.EF.Repositories
{
    public class CategoryRepository : EFBaseRepository<AppDbContext, AppUser, Category, DAL.App.DTO.Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext, new CategoryRepositoryMapper())
        {
        }
    }
}