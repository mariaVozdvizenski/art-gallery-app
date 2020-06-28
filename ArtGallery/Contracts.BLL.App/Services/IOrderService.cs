using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IBaseEntityService<Order>
    {
        Task<IEnumerable<Order>> ApplyAllFiltersAsync(IEnumerable<Order> query, string? condition,
            string? statusCodes);

        Task PaintingQuantityOnCodeChange(Order oldOrder, Guid newOrderStatusCodeId, Order newOrder);
    }
}