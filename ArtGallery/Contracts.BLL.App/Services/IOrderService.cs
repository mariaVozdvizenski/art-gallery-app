using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.mavozd.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IBaseEntityService<Order>
    {
        IEnumerable<Order> ApplyAllFilters(IEnumerable<Order> query, string? condition,
            string? statusCodes);

        Task PaintingQuantityOnCodeChange(Order oldOrder, Guid newOrderStatusCodeId, Order newOrder);
    }
}